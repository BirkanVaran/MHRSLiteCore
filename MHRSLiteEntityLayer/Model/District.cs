using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteEntityLayer.Model
{
    [Table("Districts")]
    public class District : Base<int>
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İlçe adı 2-50 karakter aralığında olmaldırı.")]
        public string DistrictName { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual List<Hospital> DistrictHospitals { get; set; }
    }
}
