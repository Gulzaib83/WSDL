using TP.DMS.WebServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TP.DMS.TestAPI.BusinessObjects;

namespace TP.DMS.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductService" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        Task<IEnumerable<object>> PS_BatchPerProduct();

        [OperationContract]
        Task<IEnumerable<object>> PSCIPDurationTimeperDateProcess();

        [OperationContract]
        Task<IEnumerable<object>> PSCIPDurationTimePerDateProcessMessage();

        [OperationContract]
        Task<IEnumerable<object>> PSPackagesProducedPerDate();

        [OperationContract]
        Task<IEnumerable<object>> PSProductProducedPerDate();

        [OperationContract]
        Task<IEnumerable<object>> PSProductProducedPerEquipment();

        [OperationContract]
        Task<IEnumerable<PackagedProduced>> GetPackagedProduceds();

        [OperationContract]
        Task<IEnumerable<Line>> GetLine();

        [OperationContract]
        Task<IEnumerable<Equipment>> GetEquipments();

        [OperationContract]
        Task<IEnumerable<Equipment>> GetEquipmentById(int Id);

        [OperationContract]
        Task<Equipment> GetEquipment(int id);

        [OperationContract]
        Task<IEnumerable<CIPDurationEquipment>> GetCIPDurationEquipment();

        [OperationContract]
        Task<IEnumerable<BatchesReportDTO>> GetBatchReprots();



    }
}
