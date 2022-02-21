using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteEntityLayer.Model
{
    [Table("Hospitals")]
    public class Hospital : Base<int>
    {
        [Required]
        [StringLength(400, MinimumLength = 2, ErrorMessage = "Hastane adı 2-50 karakter aralığında olmalıdır.")]
        public string HospitalName { get; set; }

        public int DistrictId { get; set; }

        [StringLength(500, ErrorMessage = "Adres bilgisi en fazla 500 karakter olmalıdır.")]
        public string Address { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telefon numarası başında 0 olmadan, 10 haneli olarak girilmelidir.")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }


        // İlçe tablosuyla ilişki kuruluyor.
        [ForeignKey("DistrictId")]
        public virtual District HospitalDistrict { get; set; }

        // HospitalClinics ile ilişki kuruluyor.

        public virtual List<HospitalClinic> HospitalClinics { get; set; }
    }
}
