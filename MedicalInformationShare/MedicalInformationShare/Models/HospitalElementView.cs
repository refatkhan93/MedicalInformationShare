using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalInformationShare.Models
{
    public class HospitalElementView
    {
        public Blood Blood = new Blood();
        public Hospital Hospital = new Hospital();
        public Medicine Medicine =new Medicine();
    }
}