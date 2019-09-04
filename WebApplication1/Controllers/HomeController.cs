using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MevaPro.Models;

namespace MevaPro.Controllers
{
    [RoutePrefix("mevasis")]
    public class HomeController : Controller
    {
        mevasisEntities db = new mevasisEntities();       
        // GET: Home
        public ActionResult Index()
        {
            var hizmetlerimiz = db.Hizmetlerimizs.OrderByDescending(m => m.HizmetId).ToList();

            return View(hizmetlerimiz);
        }
        [Route("hakkimizda")]
        public ActionResult About()
        {
            return View();
        }
        [Route("is-ortaklarımız")]
        public ActionResult Partners()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
        public ActionResult Bright_Cluster()
        {
            return View();
        }
        public ActionResult Engineframe()
        {
            return View();
        }
        public ActionResult Slurm()
        {
            return View();
        }
        public ActionResult BeeGFS()
        {
            return View();
        }
        public ActionResult SlurmGUI()
        {
            return View();
        }
        public ActionResult MevaCloud()
        {
            return View();
        }
        public ActionResult Yüksek_basarimli_hesaplama()
        {
            return View();
        }
        public ActionResult Kurumsal_göc_hizmetleri()
        {
            return View();
        }
        public ActionResult Teknik_danismanlik()
        {
            return View();
        }
        public ActionResult Yazilim_gelistirme()
        {
            return View();
        }
        public ActionResult Bakim_destek()
        {
            return View();
        }
        public ActionResult Egitimler()
        {
            return View();
        }
        public ActionResult ERP_cözümler()
        {
            return View();
        }
        [Route("iletisim")]
        public ActionResult Iletisim()
        {
            return View();
        }
        [Route("projeler")]
        public ActionResult Projeler()
        {
            return View(db.Projelers.ToList());
        }

        [Route("proje-detay/{seourl}")]
        public ActionResult ProjeDetay(string seourl)
        {
            var projedetay = db.Projelers.Where(m =>m.seourl == seourl).SingleOrDefault();
            if (projedetay == null)
            {
                return HttpNotFound();
            }
            return View(projedetay);
        }
        [Route("bizden-haberler")]
        public ActionResult BizdenHaberler()
        {         
            return View(db.Kategoris.ToList());
        }
        
        public ActionResult _HaberIcerikler(int? id)
        {
            List<Haberler> Icerikler = new List<Haberler>();
            IQueryable<Haberler> data = db.Haberlers;
            if (id != null)
            {
                Icerikler = data.Where(r => r.KategoriId == id).ToList();
            }
            else
            {
                Icerikler = data.ToList();
            }           
            return PartialView("_HaberIcerik", Icerikler);
        }


        [Route("haber-detay/{seourl}")]
        public ActionResult HaberDetay(string seourl)
        {
            var haberdetay = db.Haberlers.Where(m => m.seourl == seourl).SingleOrDefault();
            if (haberdetay == null)
            {
                return HttpNotFound();
            }
            return View(haberdetay);
        }
        [Route("hizmetler")]
        public ActionResult Cozumler()
        {
          
            return View(db.Hizmetlerimizs.ToList());
        }
        [Route("urunler")]
        public ActionResult Urunler()
        {
            return View(db.Urunlers.ToList());
        }

        public ActionResult Besinci_Uluslararasi_Hesaplamali_Bilimler_ve_Teknolojileri_Konferansia_Katildik()
        {
            return View();
        }

        public ActionResult ProjelerPartial()
        {

            return View(db.Projelers.ToList());
        }

  
        public ActionResult UrunlerPartial()
        {

            return View(db.Urunlers.ToList());
        }

        [Route("urun-detay/{seourl}")]
        public ActionResult UrunDetay(string seourl)
        {

            var urundetay = db.Urunlers.Where(m => m.seourl == seourl).SingleOrDefault();
            if (urundetay == null)
            {
                return HttpNotFound();
            }
            return View(urundetay);
        }
        
        public ActionResult HaberlerPartial()
        {

            return View(db.Haberlers.OrderByDescending(r => r.HaberId).Take(4).ToList());
        }

        public ActionResult ReferanslarPartial()
        {

            return View(db.Referanslars.OrderByDescending(m=>m.ReferansId).ToList());
        }


        public ActionResult KategorilerPartial()
        {

            return View(db.Kategoris.ToList());
        }

        public ActionResult HizmetBaslikPartial()
        {
            return View(db.Hizmetlerimizs.Select(m=>m.Baslik).ToList());
        }


        public ActionResult UrunlerBaslikPartial()
        {
            return View(db.Urunlers.Select(m => m.Baslik).ToList());
        }
        [Route("hizmet-detay/{seourl}")]
        public ActionResult HizmetDetay(string seourl)
        {
            var hizmetdetay = db.Hizmetlerimizs.Where(m => m.seourl == seourl).SingleOrDefault();
            if (hizmetdetay == null)
            {
                return HttpNotFound();
            }
            return View(hizmetdetay);
        }



        public ActionResult ÜrünlerSideBarPartial()
        {
            return View(db.Urunlers.ToList());
        }
    


    }
}