//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TP.DMS.WebServices.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackagedProduced
    {
        public int LineId { get; set; }
        public System.Guid LineGuid { get; set; }
        public string LineName { get; set; }
        public System.DateTime DateProduction { get; set; }
        public System.TimeSpan ProductionTime { get; set; }
        public System.TimeSpan StopTime { get; set; }
        public System.TimeSpan OtherStopTime { get; set; }
        public System.TimeSpan OutsideProductionTime { get; set; }
        public int PackagesOut { get; set; }
    }
}
