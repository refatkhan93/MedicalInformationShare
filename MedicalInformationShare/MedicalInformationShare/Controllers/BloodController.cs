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
    public class BloodController : Controller
    {
        private MedicalContext db = new MedicalContext();
        private int HospitalId;
        public BloodController()
        {
            if (System.Web.HttpContext.Current.Session["HospitalId"] != null)
            {
                HospitalId = (int) System.Web.HttpContext.Current.Session["HospitalId"];
            }
            
        }
        // GET: /Blood/
        public ActionResult Blood()
        {
            return View(db.Bloods.Where(r => r.HospitalId == HospitalId).ToList());
        }

       

        // GET: /Blood/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Blood/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BloodGroup,AvailableBag")] Blood blood)
        {
            blood.HospitalId = HospitalId;
            if (ModelState.IsValid)
            {
                db.Bloods.Add(blood);
                db.SaveChanges();
                return RedirectToAction("Blood");
            }

            return View(blood);
        }

        // GET: /Blood/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blood blood = db.Bloods.Find(id);
            if (blood == null)
            {
                return HttpNotFound();
            }
            return View(blood);
        }

        // POST: /Blood/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,BloodGroup,AvailableBag")] Blood blood)
        {
            
            blood.HospitalId = HospitalId;
            if (ModelState.IsValid)
            {
                db.Entry(blood).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Blood");
            }
            return View(blood);
        }

        

        // POST: /Blood/Delete/5
        
       
        public ActionResult Delete(int id)
        {
            Blood blood = db.Bloods.Find(id);
            db.Bloods.Remove(blood);
            db.SaveChanges();
            return RedirectToAction("Blood");
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
