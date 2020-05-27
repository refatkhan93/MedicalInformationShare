using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalInformationShare.Models
{
    public class GasInventory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public int HospitalId { get; set; }
    }
}