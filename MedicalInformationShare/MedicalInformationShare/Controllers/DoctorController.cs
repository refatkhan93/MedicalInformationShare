using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalInformationShare.App_Start;
using MedicalInformationShare.Models;
using MedicalInformationShare.Context;

namespace MedicalInformationShare.Controllers
{
    [AuthorizationFilter]
    public class DoctorController : Controller
    {
        private MedicalContext db = new MedicalContext();

        private int HospitalId;
        public DoctorController()
        {
            if (System.Web.HttpContext.Current.Session["HospitalId"] != null)
            {
                HospitalId = (int) System.Web.HttpContext.Current.Session["HospitalId"];
            }
            
        }
        // GET: /Doctor/
        public ActionResult Doctor()
        {
            return View(db.Doctors.Where(r => r.HospitalId == HospitalId).ToList());
        }

        

        // GET: /Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Designation")] Doctor doctor, HttpPostedFileBase Image)
        {
            doctor.HospitalId = HospitalId;
            if (Image != null && Image.ContentLength > 0)
            {
                try
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Image.FileName);
                    string uploadUrl = Server.MapPath("~/Photos/Doctor");
                    Image.SaveAs(Path.Combine(uploadUrl, fileName));
                    doctor.Image = "Photos/Doctor/" + fileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "ERROR:" + ex.Message.ToString();
                }
            }
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Doctor");
            }

            return View(doctor);
        }

        // GET: /Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: /Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Designation")] Doctor doctor, HttpPostedFileBase Image,string pastImage)
        {
            doctor.HospitalId = HospitalId;
            doctor.Image = pastImage;
            if (Image != null && Image.ContentLength > 0)
            {
                string fullPath = Request.MapPath("~/" + pastImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                try
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Image.FileName);
                    string uploadUrl = Server.MapPath("~/Photos/Doctor");
                    Image.SaveAs(Path.Combine(uploadUrl, fileName));
                    doctor.Image = "Photos/Doctor/" + fileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "ERROR:" + ex.Message.ToString();
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Doctor");
            }
            return View(doctor);
        }

        

        // POST: /Doctor/Delete/5
        
        public ActionResult Delete(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            string fullPath = Request.MapPath("~/" + doctor.Image);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Doctor");
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
