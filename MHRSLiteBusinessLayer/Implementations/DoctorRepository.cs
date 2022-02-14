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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        
        public DoctorRepository(MyContext mycontext) :base(mycontext)
        {
           
        }
    }

}
