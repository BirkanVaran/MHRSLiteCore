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
  public  class PatientRepository:Repository<Patient>,IPatientRepository
    {
        private readonly MyContext _mycontext;
        public PatientRepository(MyContext myContext) :base(myContext)
        {

        }
    }
}
