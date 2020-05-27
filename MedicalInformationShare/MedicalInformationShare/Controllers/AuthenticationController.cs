using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalInformationShare.App_Start;
using MedicalInformationShare.Context;
using MedicalInformationShare.Models;

namespace MedicalInformationShare.Controllers
{
    
    public class AuthenticationController : Controller
    {
        private MedicalContext db = new MedicalContext();
        private int HospitalId;
        
        //
        // GET: /Authentication/
        public ActionResult Login(string id)
        {
            if (Session["HospitalId"] != null)
            {
                return RedirectToAction("Index", "Primary");
            }
            ViewBag.Error = id;
            return View();
        }

        public ActionResult LoginUser(string username,string password)
        {
            Hospital hs = db.Hospitals.FirstOrDefault(r => r.Email == username && r.Password == password);
            if (hs != null)
            {
                Session["HospitalId"] = hs.Id;
                Session["HospitalName"] = hs.Name;
                return RedirectToAction("Index", "Primary");
            }
            else
                return RedirectToAction("Login", "Authentication", new {id="Error"});
            
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}