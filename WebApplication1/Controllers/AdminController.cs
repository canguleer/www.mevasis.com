using MevaPro.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MevaPro.Controllers
{
    public class AdminController : Controller
    {
        private mevasisEntities db = new mevasisEntities();
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.haberAdet = db.Haberlers.Count();
            ViewBag.hizmetAdet = db.Hizmetlerimizs.Count();
            ViewBag.projeler = db.Projelers.Count();
            ViewBag.kategoriler = db.Kategoris.Count();
            ViewBag.urunler = db.Urunlers.Count();
            ViewBag.referanslar = db.Referanslars.Count();
    

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GirisBasarili(Uye uye)
        {
            Result result = new Result();
            var kontrol = db.Uyes.Where(e => e.Email == uye.Email && e.Sifre == uye.Sifre).FirstOrDefault();
            if (kontrol != null) //Check the database
            {
                int userid = kontrol.UyeId;
                List<Claim> claims = new Autorizing().GetClaims(kontrol); //Get the claims from the headers or db or your user store
                if (null != claims)
                {
                    new Autorizing().SignIn(claims, false);
                    var identity = (ClaimsIdentity)User.Identity;
                    List<string> rol = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                    result.adres = "../Admin/Index";
                    result.sonuc = true;
                }
            }
            else
            {
                result.sonuc = false;
                result.mesaj = "Hatalı kullanıcı adı ve/vaya şifre";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}