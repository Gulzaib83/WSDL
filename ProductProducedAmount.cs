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
    
    public partial class ProductProducedAmount
    {
        public int EquipmentId { get; set; }
        public System.Guid EquipmentGuid { get; set; }
        public string EquipmentName { get; set; }
        public string ArticleName { get; set; }
        public int AmountLiters { get; set; }
        public int DurationProduceMinutes { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}