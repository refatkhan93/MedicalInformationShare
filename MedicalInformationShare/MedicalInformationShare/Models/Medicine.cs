using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalInformationShare.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Drug Name")]
        [Required(ErrorMessage = "Please Give the Drug Name")]
        public string Name { get; set; }

        [DisplayName("Drug Measurement")]
        [Required(ErrorMessage = "Please Give the Drug Measurement")]
        public string Measurement { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "Please Give the Company Name")]
        public string Company { get; set; }
        public int HospitalId { get; set; }

    }
}