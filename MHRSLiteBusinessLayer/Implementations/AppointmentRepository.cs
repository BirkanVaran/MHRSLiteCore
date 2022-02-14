using MHRSLiteBusinessLayer.Contracts;
using MHRSLiteDataLayer;
using MHRSLiteEntityLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteBusinessLayer.Implementations
{
   public class AppointmentRepository:Repository<Appointment>,IAppointmentRepository
    {
        private readonly MyContext _myContext;
        public AppointmentRepository(MyContext myContext) :base(myContext)
        {
            _myContext = myContext;
        }
    }
}
