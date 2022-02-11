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


        // İlçe tablosuyla ilişki kuruluyor.
        [ForeignKey("DistrictId")]
        public virtual District HospitalDistrict { get; set; }

        // HospitalClinics ile ilişki kuruluyor.

        public virtual List<HospitalClinic> HospitalClinics { get; set; }
    }
}
