using MHRSLiteEntityLayer;
using MHRSLiteEntityLayer.Enums;
using MHRSLiteEntityLayer.IdentityModels;
using MHRSLiteUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MHRSLiteBusinessLayer.EmailService;
using Microsoft.AspNetCore.Authorization;

namespace MHRSLiteUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly RoleManager<AppRole> _roleManager;

        private readonly IEmailSender _emailSender;

        // Dependency Injection
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IEmailSender emailSender)


        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            CheckRoles();
        }

        private void CheckRoles()
        {
            var allRoles = Enum.GetNames(typeof(RoleNames));
            foreach (var item in allRoles)
            {
                if (!_roleManager.RoleExistsAsync(item).Result)
                {
                    var result = _roleManager.CreateAsync(new AppRole()
                    {
                        Name = item,
                        Description = item
                    }).Result;
                }
            }
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var checkUserName = await _userManager.FindByIdAsync(model.UserName);
                if (checkUserName != null)
                {
                    ModelState.AddModelError(nameof(model.UserName), "Kullanıcı adı zaten mevcut.");
                    return View(model);
                }
                var checkUserForEmail = await _userManager.FindByEmailAsync(model.Email);
                if (checkUserForEmail != null)
                {
                    ModelState.AddModelError(nameof(model.Email), "Bu e-mail zaten sistemde mevcut.");
                    return View(model);
                }
                // Yeni kullanıcı oluşturulabilir.
                AppUser newUser = new AppUser()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    UserName = model.UserName,
                    Gender = model.Gender
                    //TODO: BirthDate?
                    //TODO: PhoneNumber?
                };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var roleResult = _userManager.AddToRoleAsync(newUser, RoleNames.Patient.ToString());
                    // Email gönderilmelidir.
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callBackUrl = Url.Action("ConfirmEmail", "Account", new { userId = newUser.Id, code = code }, protocol: Request.Scheme);

                    var emailMessage = new EmailMessage()
                    {
                        Contacts = new string[] { newUser.Email },
                        Subject = "MHRSLITE - Email Aktivasyonu",
                        Body = $"Merhaba {newUser.Name} {newUser.Surname}.<br/>Hesabınızı aktifleşitmek için <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>buraya</a> tıklayınız."
                    };
                    await _emailSender.SendAsync(emailMessage);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Beklenmedik bir hata oluştu!");
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }
        #endregion

        #region ConfirmEmail
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                if (userId == null || code == null)
                {
                    return NotFound("Sayfa bulunamadı.");
                }
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    NotFound("Kullanıcı bulunamadı.");
                }
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

                // Email Confirmed=1 ya da True
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    if (_userManager.IsInRoleAsync(user, RoleNames.Passive.ToString()).Result)
                    {
                        await _userManager.RemoveFromRoleAsync(user, RoleNames.Passive.ToString());
                        await _userManager.AddToRoleAsync(user, RoleNames.Patient.ToString());
                    }
                    TempData["EmailConfirmedMessage"] = "Hesabınız aktifleştirilmiştir...";
                    return RedirectToAction("Login", "Account");
                }

                // Login sayfasında bu TempData View ekranında kontrol edilecektir.
                ViewBag.EmailConfirmedMessage = "Hesabınız aktifleştirilemedi...";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.EmailConfirmedMessage = "Beklenmedik bir hata oluştu! Tekrar deneyiniz.";
                return View();
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Geçersiz veri girişi yapıldı. Verilerinizi uygun şekilde giririniz.");
                    return View(model);

                }

                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");          
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalıdır.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu.");
                return View(model);

            }
        }
        #endregion

        #region Logout
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        } 
        #endregion
    }
}
