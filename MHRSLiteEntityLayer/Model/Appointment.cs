﻿using MHRSLiteEntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteEntityLayer.Model
{
    [Table("Appointments")]
    public class Appointment : Base<int>
    {
        [Required]
        public string PatientId { get; set; }
        public int HospitalClinicId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Randevu saati SS:DD formatında olmalıdır.")]
        public string AppointmentHour { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("HospitalClinicId")]
        public virtual HospitalClinic HospitalClinic { get; set; }

    }
}
