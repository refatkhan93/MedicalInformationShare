using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalInformationShare.App_Start;
using MedicalInformationShare.Context;

namespace MedicalInformationShare.Controllers
{
    [AuthorizationFilter]
   
    public class PrimaryController : Controller
    {
        
        //
        // GET: /Primary/
        MedicalContext db=new MedicalContext();
        private int HospitalId;

        public PrimaryController()
        {
            if (System.Web.HttpContext.Current.Session["HospitalId"] != null)
            {
                HospitalId = (int)System.Web.HttpContext.Current.Session["HospitalId"];
            }
        }
        
        public ActionResult Index()
        {
            ViewBag.Blood = db.Bloods.Where(r => r.HospitalId == HospitalId).ToList();
            ViewBag.Medicine = db.Medicines.Where(r => r.HospitalId == HospitalId).ToList();
            return View();
        }

        public ActionResult SearchInventory(int id)
        {

            return RedirectToAction("Index");
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