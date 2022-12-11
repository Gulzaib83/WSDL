using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using TP.DMS.WebServices.Model;
using TP.DMS.TestAPI.BusinessObjects;

namespace TP.DMS.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductService.svc or ProductService.svc.cs at the Solution Explorer and start debugging.
    public class ProductService : IProductService
    {
        private readonly Entities _context = new Entities();
        public void DoWork()
        {
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetBatchReprots")]
        public async Task<IEnumerable<BatchesReportDTO>> GetBatchReprots()
        {
            if (_context.BatchesReports == null)
            {
                //handle not found case.
            }
            return await _context.BatchesReports.Select(x => new BatchesReportDTO
            {
                BatchId = x.BatchID,
                ProductId = x.ProductID,
                ProductName = x.ProductName,
                ActualStartTime = x.ActualStartTime,
                ActualEndTime = x.ActualEndTime,
                Description = x.Description,
                RecipeRid = x.Recipe_RID,
            }).ToListAsync();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetCIPDurationEquipment")]
        public Task<IEnumerable<CIPDurationEquipment>> GetCIPDurationEquipment()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetEquipment?id={id}")]
        public Task<Equipment> GetEquipment(int id)
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetEquipmentById?Id={Id}")]
        public Task<IEnumerable<Equipment>> GetEquipmentById(int Id)
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetEquipments")]
        public Task<IEnumerable<Equipment>> GetEquipments()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetLine")]
        public Task<IEnumerable<Line>> GetLine()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetPackagedProduceds")]
        public Task<IEnumerable<PackagedProduced>> GetPackagedProduceds()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "PSCIPDurationTimeperDateProcess")]
        public Task<IEnumerable<object>> PSCIPDurationTimeperDateProcess()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "PSCIPDurationTimePerDateProcessMessage")]
        public Task<IEnumerable<object>> PSCIPDurationTimePerDateProcessMessage()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "PSPackagesProducedPerDate")]
        public Task<IEnumerable<object>> PSPackagesProducedPerDate()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "PSProductProducedPerDate")]
        public Task<IEnumerable<object>> PSProductProducedPerDate()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "PSProductProducedPerEquipment")]
        public Task<IEnumerable<object>> PSProductProducedPerEquipment()
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "GET", UriTemplate = "PS_BatchPerProduct")]
        public Task<IEnumerable<object>> PS_BatchPerProduct()
        {
            throw new NotImplementedException();
        }    
    }
}
