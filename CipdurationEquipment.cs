using System;
using System.Collections.Generic;

namespace TP.DMS.TestAPI.BusinessObjects
{
    public partial class CipdurationEquipmentDTO
    {
        public int EquipmentId { get; set; }
        public Guid EquipmentGuid { get; set; }
        public string? EquipmentName { get; set; }
        public string ProcessClass { get; set; } = null!;
        public int DurationMinutes { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
