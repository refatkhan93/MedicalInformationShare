using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalInformationShare.App_Start;
using MedicalInformationShare.Context;
using MedicalInformationShare.Models;

namespace MedicalInformationShare.Controllers
{
    [AuthorizationFilter]
    public class SearchController : Controller
    {
        private MedicalContext db = new MedicalContext();
        //
        // GET: /Search/
        public ActionResult SearchBlood()
        {
            var data = from b in db.Bloods
                join h in db.Hospitals on b.HospitalId equals h.Id
                select new
                {
                    bId = b.Id,
                    bGroup=b.BloodGroup,
                    bBag = b.AvailableBag,
                    hName=h.Name,
                    hAddress=h.Address,
                    hPhone=h.Phone,
                    hEmail=h.Email
                };
            List<HospitalElementView> hospitalElementViews = new List<HospitalElementView>();
            foreach (var d in data)
            {
                HospitalElementView h = new HospitalElementView();
                h.Blood.Id = d.bId;
                h.Blood.BloodGroup = d.bGroup;
                h.Blood.AvailableBag = d.bBag;
                h.Hospital.Name = d.hName;
                h.Hospital.Address = d.hAddress;
                h.Hospital.Phone = d.hPhone;
                h.Hospital.Email = d.hEmail;
                hospitalElementViews.Add(h);
            }

            return View(hospitalElementViews);
            
        }
        public ActionResult SearchMedicine()
        {
            var data = from m in db.Medicines
                       join h in db.Hospitals on m.HospitalId equals h.Id
                       select new
                       {
                           mId = m.Id,
                           mName = m.Name,
                           mCompany = m.Company,
                           mUnit=m.Measurement,
                           hName = h.Name,
                           hAddress = h.Address,
                           hPhone = h.Phone,
                           hEmail = h.Email
                       };
            List<HospitalElementView> hospitalElementViews = new List<HospitalElementView>();
            foreach (var d in data)
            {
                HospitalElementView h = new HospitalElementView();
                h.Medicine.Id = d.mId;
                h.Medicine.Name = d.mName;
                h.Medicine.Company = d.mCompany;
                h.Medicine.Measurement = d.mUnit;
                h.Hospital.Name = d.hName;
                h.Hospital.Address = d.hAddress;
                h.Hospital.Phone = d.hPhone;
                h.Hospital.Email = d.hEmail;
                hospitalElementViews.Add(h);
            }

            return View(hospitalElementViews);

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