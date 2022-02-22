using MHRSLiteBusinessLayer.Contracts;
using MHRSLiteBusinessLayer.EmailService;
using MHRSLiteEntityLayer.IdentityModels;
using MHRSLiteEntityLayer.Model;
using MHRSLiteUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHRSLiteUI.Controllers
{
    public class PatientController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        // Dependency Injection
        public PatientController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IEmailSender emailSender,
            IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        public IActionResult Appointment()
        {
            try
            {
                ViewBag.Cities = _unitOfWork.CityRepository.GetAll(orderBy: x => x.OrderBy(a => a.CityName));
                ViewBag.Clinics = _unitOfWork.ClinicRepository.GetAll(orderBy: x => x.OrderBy(a => a.ClinicName));
                return View();
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        [Authorize]
        public IActionResult FindAppointment(int cityid, int? distid, int cid, int? hid, int? dr)
        {
            try
            {
                // Dışarıdan gelen clinicid'nin olduğu HospitalClinic kayıtlarını al.
                var data = _unitOfWork.HospitalClinicRepository
                    .GetAll(x => x.ClinicId == cid
                    && x.HospitalId == hid.Value)
                    .Select(a => a.AppointmentHours).ToList();

                var list = new List<PatientAppointmentViewModel>();
                foreach (var item in data)
                {
                    foreach (var subitem in item)
                    {
                        var hospitalClinicData = _unitOfWork.HospitalClinicRepository.GetFirstOrDefault(x => x.Id == subitem.HospitalClinicId);

                        var hours = subitem.Hours.Split(',');

                        var appointments = _unitOfWork.AppointmentRepository.GetAll(
                            x =>
                            x.HospitalClinicId == x.HospitalClinicId
                            &&
                            (x.AppointmentDate > DateTime.Now.AddDays(-1)
                            &&
                            x.AppointmentDate < DateTime.Now.AddDays(2)
                            )).ToList();
                        foreach (var houritem in hours)
                        {
                            if (appointments.Count(x => x.AppointmentDate == (Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString())) && x.AppointmentHour == houritem) == 0)
                            {
                                list.Add(new PatientAppointmentViewModel()
                                {
                                    AppointmentDate = Convert.ToDateTime(DateTime.Now.AddDays(1)),
                                    HospitalClinicId=subitem.HospitalClinicId,
                                    DoctorId = hospitalClinicData.DoctorId,
                                    AvailableHour = houritem,
                                    Doctor = _unitOfWork.DoctorRepository.GetFirstOrDefault(x => x.TCNumber == hospitalClinicData.DoctorId, includeProperties: "AppUser")
                                });
                            }
                        }
                    }
                }
                list = list.Distinct().OrderBy(x => x.AppointmentDate).ToList();
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult SaveAppointment(int hid, string date, string hour)
        {
            try
            {
                // Randevu kayıy edilecek.
                // Aynı tarih ve saate başka randevusu var mı?
                DateTime appointmentDate = Convert.ToDateTime(date);
                if (_unitOfWork.AppointmentRepository.GetFirstOrDefault(x => x.AppointmentDate == appointmentDate && x.AppointmentHour == hour) != null)
                {
                    TempData["SaveAppointmentStatus"] = $"{date} - {hour} tarihinde zaten bir randevunuz mevcut.";
                    return RedirectToAction("Index", "Patient");
                }

                // Randevu kayıt edilecek.
                Appointment patientAppointment = new Appointment()
                {
                    CreatedDate = DateTime.Now,
                    PatientId = HttpContext.User.Identity.Name,
                    HospitalClinicId = hid,
                    AppointmentDate = appointmentDate,
                    AppointmentHour = hour
                };
                var result = _unitOfWork.AppointmentRepository.Add(patientAppointment);
                TempData["SaveAppointmentStatus"] = result ? "Randevunuz başarıyla kaydedilmiştir." : "HATA! - Beklenmedik bir hata oluştu.";
                return RedirectToAction("Index", "Patient");
            }
            catch (Exception ex)
            {
                TempData["SaveAppointmentStatus"] = "HATA! - " + ex.Message;
                return RedirectToAction("Index", "Patient");
            }
        }
    }
}
