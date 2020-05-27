using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalInformationShare.Models
{
    public class Blood
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Blood Group")]
        [Required(ErrorMessage = "Please Give the Blood Group")]
        public string BloodGroup { get; set; }

        [DisplayName("Available Bag")]
        [Required(ErrorMessage = "Please Give the Blood Group")]
        
        public int AvailableBag { get; set; }
        public int HospitalId { get; set; }

    }
}