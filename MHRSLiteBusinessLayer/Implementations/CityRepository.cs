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
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(MyContext mycontext) : base(mycontext)
        {
            
        }

        

        public void Dispose()
        {

        }
    }
}
