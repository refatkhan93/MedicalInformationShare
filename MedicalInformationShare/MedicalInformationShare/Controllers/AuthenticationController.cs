using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Register()
        {
            if (Session["HospitalId"] != null)
            {
                return RedirectToAction("Index", "Primary");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(Hospital hospital, HttpPostedFileBase Image)
        {
            if (Image != null && Image.ContentLength > 0)
            {

                try
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Image.FileName);
                    string uploadUrl = Server.MapPath("~/Photos");
                    Image.SaveAs(Path.Combine(uploadUrl, fileName));
                    hospital.Image = "Photos/" + fileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "ERROR:" + ex.Message.ToString();
                }
            }
            int k=db.Hospitals.Count(r => r.Email == hospital.Email);
            if (k == 0)
            {
                db.Hospitals.Add(hospital);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Another Hospital is Registered with this Email";
                return View();
            }
            
            return RedirectToAction("Login");
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