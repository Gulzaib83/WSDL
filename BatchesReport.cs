using System;
using System.Collections.Generic;

namespace TP.DMS.TestAPI.BusinessObjects
{
    public partial class BatchesReportDTO
    {
        public int BatchId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public DateTime ActualStartTime { get; set; }
        public DateTime ActualEndTime { get; set; }
        public string? RecipeRid { get; set; }
        public string? Description { get; set; }
    }
}
