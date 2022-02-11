using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteEntityLayer.Model
{
    [Table("Clinics")]
    public class Clinic : Base<int>
    {
        [Requierd]
        [StringLength(100,MinimumLength =2,ErrorMessage ="Klinik adı 2-5 karakter aralığında olmalıdır.")]
        public string ClinicName { get; set; }

        // HospitalClinics tablosunda Clinic ile ilişki kuruldu.
        public virtual List<HospitalClinics> HospitalClinics { get; set; }

    }
}
