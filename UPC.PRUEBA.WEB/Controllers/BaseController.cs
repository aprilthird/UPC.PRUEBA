using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPC.PRUEBA.DATA.Context;

namespace UPC.PRUEBA.WEB.Controllers
{
    public class BaseController : Controller
    {
        protected AppDbContext _context;

        public BaseController()
        {
            _context = new AppDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}