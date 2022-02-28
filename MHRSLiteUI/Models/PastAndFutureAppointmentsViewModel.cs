﻿using MHRSLiteEntityLayer.Model;
using MHRSLiteEntityLayer.PagingListModels;
using MHRSLiteEntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHRSLiteUI.Models
{
    public class PastAndFutureAppointmentsViewModel
    {
        //public List<Appointment> PastAppointments { get; set; }
        //public List<Appointment> UpcomingAppointments { get; set; }

        // Randevuları sayfalama yaparak görüntülemek istediğim için PaginatedList kullandık.
        public PaginatedList<AppointmentVM> PastAppointments { get; set; }
        public PaginatedList<AppointmentVM> UpcomingAppointments { get; set; }
    }
}
