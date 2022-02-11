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
        private readonly MyContext _myContext;
        public CityRepository(MyContext mycontext) : base(mycontext)
        {
            _myContext = mycontext;
            
        }

        

        public void Dispose()
        {

        }
    }
}
