﻿using MHRSLiteBusinessLayer.Contracts;
using MHRSLiteDataLayer;
using MHRSLiteEntityLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteBusinessLayer.Implementations
{
    public class HospitalClinicRepository : Repository<HospitalClinic>, IHospitalClinicRepository
    {
        private readonly MyContext _mycontext;
        public HospitalClinicRepository(MyContext myContext) : base(myContext)
        {
            _mycontext = myContext;
        }
    }
}