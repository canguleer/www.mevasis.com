using MevaPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MevaPro.Controllers
{
    [RoutePrefix("mevasis")]
    public class KategoriListelemeController : Controller
    {
        mevasisEntities db = new mevasisEntities();
        // GET: KategoriListeleme
        [Route("kategori-listeleme/{id}")]
        public ActionResult Index(int id)
        {
            var haberler = db.Haberlers.Where(m => m.Kategori.KategoriId == id).ToList();
            return View(haberler);
                   
        }

       
    }
}