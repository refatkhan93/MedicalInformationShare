using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MedicalInformationShare.Models;

namespace MedicalInformationShare.Context
{
    public class MedicalContext:DbContext
    {
        public DbSet<Blood> Bloods { get; set; }
        public DbSet<GasInventory> GasInventorys { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<OTInstrument> OtInstruments { get; set; }
        public DbSet<WardInfo> WardInfos { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }


    }
}