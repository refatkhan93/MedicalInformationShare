using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalInformationShare.App_Start;
using MedicalInformationShare.Models;
using MedicalInformationShare.Context;
using EntityState = System.Data.EntityState;


namespace MedicalInformationShare.Controllers
{
     [AuthorizationFilter]
    public class MedicineController : Controller
    {
        private MedicalContext db = new MedicalContext();
        private int HospitalId;
        public MedicineController()
        {
            if (System.Web.HttpContext.Current.Session["HospitalId"] != null)
            {
                HospitalId = (int) System.Web.HttpContext.Current.Session["HospitalId"];
            }
            
        }
        // GET: /Medicine/
        public ActionResult MedicineIndex()
        {
            return View(db.Medicines.Where(r=>r.HospitalId==HospitalId).ToList());
        }

       

        // GET: /Medicine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Medicine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Measurement,Company")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                medicine.HospitalId = HospitalId;
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return RedirectToAction("MedicineIndex");
            }

            return View(medicine);
        }

        // GET: /Medicine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: /Medicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Measurement,Company")] Medicine medicine)
        {
            
            if (ModelState.IsValid)
            {
                medicine.HospitalId = HospitalId;
                db.Entry(medicine).State = (System.Data.Entity.EntityState) EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MedicineIndex");
            }
            return View(medicine);
        }

        

        // POST: /Medicine/Delete/5
        
        public ActionResult Delete(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            db.Medicines.Remove(medicine);
            db.SaveChanges();
            return RedirectToAction("MedicineIndex");
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
