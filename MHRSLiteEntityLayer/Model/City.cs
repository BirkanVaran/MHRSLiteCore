using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteEntityLayer.Model
{
    [Table("Cities")]
    public class City : Base<byte>
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Şehir adı 2-50 karakter aralığında olmaldır.")]
        public string CityName { get; set; }

        [Required]
        public byte PlateCode { get; set; }

        // İlçeler ile ilişkisi var.
        public virtual List<District> Districts { get; set; }
    }
}
