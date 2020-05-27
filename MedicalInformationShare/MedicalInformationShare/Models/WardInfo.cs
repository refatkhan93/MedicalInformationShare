using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalInformationShare.Models
{
    public class WardInfo
    {
        [Key]
        public int Id { get; set; }
        public int GeneralWard { get; set; }
        public int Icu { get; set; }
        public int Ccu { get; set; }
        public int HospitalId { get; set; }
    }
}