using MHRSLiteBusinessLayer.Contracts;
using MHRSLiteDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLiteBusinessLayer.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _myContext;

        public UnitOfWork(MyContext myContext)
        {
            _myContext = myContext;
            CityRepository = new CityRepository(_myContext);
        }

        public ICityRepository CityRepository { get; set; }
        public void Dispose()
        {
            _myContext.Dispose();
        }
    }
}
