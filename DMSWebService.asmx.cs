using TP.DMS.WebServices.BusinessObjects;
using TP.DMS.WebServices.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using TP.DMS.TestAPI.BusinessObjects;
using System.Xml;
using System.Web.Services.Description;
using System.IO;
using System.Web.Script.Serialization;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace TP.DMS.WebServices
{
    /// <summary>
    /// Summary description for DMSWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DMSWebService : System.Web.Services.WebService
    {
        private readonly Entities _context = new Entities();

        [WebMethod]

        public List<BatchesReportDTO> GetBatchReprots()
        {
            if (_context.BatchesReports == null)
            {
                //handle not found case.
            }
            return _context.BatchesReports.Select(x => new BatchesReportDTO
            {
                BatchId = x.BatchID,
                ProductId = x.ProductID,
                ProductName = x.ProductName,
                ActualStartTime = x.ActualStartTime,
                ActualEndTime = x.ActualEndTime,
                Description = x.Description,
                RecipeRid = x.Recipe_RID,
            }).ToList();
        }

        [WebMethod]
        public List<CIPDurationEquipment> GetCIPDurationEquipment()
        {
            return _context.CIPDurationEquipments.ToList();
        }

        [WebMethod]
        public Equipment GetEquipment(int id)
        {
            return _context.Equipments.Find(id);
        }

        [WebMethod]
        public List<Equipment> GetEquipmentById(int Id)
        {
            return _context.Equipments.Where(c => c.EquipmentId == Id).ToList();
        }

        [WebMethod]
        public List<Equipment> GetEquipments()
        {
            return _context.Equipments.ToList();
        }

        [WebMethod]
        public List<Line> GetLine()
        {
            return _context.Lines.ToList();
        }


        [WebMethod]
        public List<PackagedProduced> GetPackagedProduceds()
        {
            return _context.PackagedProduceds.ToList();
        }

        [WebMethod]
        public List<PSCIDTO> PSCIPDurationTimeperDateProcess(DateTime? StartTime = null, DateTime? EndTime = null, string AsProcessClass = null)
        {
            try
            {
                var returnObject = new List<PSCIDTO>();
                var cmd = _context.Database.Connection.CreateCommand();

                using (cmd)
                {
                    cmd.CommandText = "PS_CIP_Duration_Time_per_Date_Process";
                    cmd.CommandType = CommandType.StoredProcedure;
                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@StartTime",
                        SqlDbType.DateTime)
                    { Value = StartTime });

                    cmd.Parameters.Add(new SqlParameter("@EndTime",
                        SqlDbType.DateTime)
                    { Value = EndTime });

                    cmd.Parameters.Add(new SqlParameter("@AsProcessClass",
                        SqlDbType.NVarChar)
                    { Value = AsProcessClass });


#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                    }
                    else
                    {
                        cmd.Connection.Open();
                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.


                    using (var dataReader = cmd.ExecuteReader())
                    {
                        var retObject = new PSCIDTO();
                        while (dataReader.Read())
                        {
                            retObject.EquipmentId = Convert.ToInt16(dataReader["EquipmentId"]);
                            retObject.EquipmentName = Convert.ToString(dataReader["EquipmentName"]);
                            retObject.TotalDurationMinutes = Convert.ToInt16(dataReader["EquipmentId"]);
                            retObject.LastTimeCIP = Convert.ToDateTime(dataReader["LastTimeCIP"]);
                            returnObject.Add(retObject);
                        }
                    }
                    return returnObject;
                }
            }
            catch (Exception ex) {
                SoapException exc = new SoapException(ex.Message, SoapException.ClientFaultCode, "", ex.InnerException);
                throw exc;
            }
        }


        [WebMethod]
        public List<PSCIDTO> PSCIPDurationTimePerDateProcessMessage(DateTime? FromTime = null)
        {
            try
            {
                var returnObject = new List<PSCIDTO>();

                using (var cmd = _context.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = "PS_CIP_Duration_Time_per_Date_Process_message";
                    cmd.CommandType = CommandType.StoredProcedure;
                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@FromTime",
                        SqlDbType.DateTime)
                    { Value = FromTime });

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
#pragma warning restore CS8602 // Dereference of a possibly null reference.


                    using (var dataReader = cmd.ExecuteReader())
                    {
                        var retObject = new PSCIDTO();
                        while (dataReader.Read())
                        {
                            retObject.EquipmentId = Convert.ToInt16(dataReader["EquipmentId"]);
                            retObject.EquipmentName = Convert.ToString(dataReader["EquipmentName"]);
                            retObject.TotalDurationMinutes = Convert.ToInt16(dataReader["EquipmentId"]);
                            retObject.LastTimeCIP = Convert.ToDateTime(dataReader["LastTimeCIP"]);
                            returnObject.Add(retObject);
                        }
                    }
                    return returnObject;
                }

            }
            catch (Exception ex)
            {
                SoapException exc = new SoapException(ex.Message, SoapException.ClientFaultCode, "", ex.InnerException);
                throw exc;
            }
        }


        [WebMethod]
        public List<PackagedProduced> PSPackagesProducedPerDate(int AsLineId, DateTime? StartTime = null, DateTime? EndTime = null)
        {
            try {
                var returnObject = new List<PackagedProduced>();

                using (var cmd = _context.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = "PS_Packages_Produced_per_Date";
                    cmd.CommandType = CommandType.StoredProcedure;
                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@AsLineId",
                        SqlDbType.Int)
                    { Value = AsLineId });

                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@StartTime",
                        SqlDbType.DateTime)
                    { Value = StartTime });

                    cmd.Parameters.Add(new SqlParameter("@EndTime",
                        SqlDbType.DateTime)
                    { Value = EndTime });

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
#pragma warning restore CS8602 // Dereference of a possibly null reference.


                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var retObject = new PackagedProduced();
                            retObject.LineId = Convert.ToInt16(dataReader["LineId"]);
                            retObject.LineName = Convert.ToString(dataReader["LineName"]);
                            retObject.DateProduction = Convert.ToDateTime(dataReader["DateProduction"]);
                            retObject.ProductionTime = (TimeSpan)(dataReader["ProductionTime"]);
                            retObject.StopTime = (TimeSpan)(dataReader["StopTime"]);
                            retObject.OtherStopTime = (TimeSpan)(dataReader["OtherStopTime"]);
                            retObject.OutsideProductionTime = (TimeSpan)(dataReader["OutsideProductionTime"]);
                            retObject.PackagesOut = Convert.ToInt32(dataReader["PackagesOut"]);

                            returnObject.Add(retObject);

                        }
                    }
                    return returnObject;
                }
            }
            catch (Exception ex)
            {
                SoapException exc = new SoapException(ex.Message, SoapException.ClientFaultCode, "", ex.InnerException);
                throw exc;
            }
            
        }


        [WebMethod]
        public List<PSPPPDTO> PSProductProducedPerDate(int AsEquipmentId, DateTime? StartTime = null, DateTime? EndTime = null)
        {
            try {
                var returnObject = new List<PSPPPDTO>();

                using (var cmd = _context.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = "PS_Product_Produced_per_Date";
                    cmd.CommandType = CommandType.StoredProcedure;
                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@AsEquipmentId",
                        SqlDbType.Int)
                    { Value = AsEquipmentId });

                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@StartTime",
                        SqlDbType.DateTime)
                    { Value = StartTime });

                    cmd.Parameters.Add(new SqlParameter("@EndTime",
                        SqlDbType.DateTime)
                    { Value = EndTime });
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
#pragma warning restore CS8602 // Dereference of a possibly null reference.


                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var retObject = new PSPPPDTO();
                            retObject.ArticleName = Convert.ToString(dataReader["ArticleName"]);
                            retObject.TotalAmountLiters = Convert.ToInt32(dataReader["TotalAmountLiters"]);
                            retObject.TotalDurationProduceMinutes = Convert.ToInt32(dataReader["TotalDurationProduceMinutes"]);
                            retObject.LastTimeCIP = Convert.ToDateTime(dataReader["LastTimeCIP"]);

                            returnObject.Add(retObject);
                        }
                    }
                    return returnObject;
                }
            }
            catch (Exception ex)
            {
                SoapException exc = new SoapException(ex.Message, SoapException.ClientFaultCode, "", ex.InnerException);
                throw exc;
            }
            
        }


        [WebMethod]
        public List<PSPPPEDTO> PSProductProducedPerEquipment(int AsEquipmentId, DateTime? StartTime = null, DateTime? EndTime = null)
        {
            try {
                var returnObject = new List<PSPPPEDTO>();

                using (var cmd = _context.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = "PS_Product_Produced_per_Equipment";
                    cmd.CommandType = CommandType.StoredProcedure;
                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@AsEquipmentId",
                        SqlDbType.Int)
                    { Value = AsEquipmentId });

                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@StartTime",
                        SqlDbType.DateTime)
                    { Value = StartTime });

                    cmd.Parameters.Add(new SqlParameter("@EndTime",
                        SqlDbType.DateTime)
                    { Value = EndTime });
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
#pragma warning restore CS8602 // Dereference of a possibly null reference.


                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var retObject = new PSPPPEDTO();
                            retObject.EquipmentId = Convert.ToInt32(dataReader["EquipmentId"]);
                            retObject.EquipmentName = Convert.ToString(dataReader[1]); //TODO: SP need to be updated to fetch using column name.
                            retObject.TotalAmountLiters = Convert.ToInt32(dataReader["TotalAmountLiters"]);
                            retObject.TotalDurationProduceMinutes = Convert.ToInt32(dataReader["TotalDurationProduceMinutes"]);
                            retObject.LastTimeCIP = Convert.ToDateTime(dataReader["LastTimeCIP"]);

                            returnObject.Add(retObject);
                        }
                    }
                    return returnObject;
                }
            }

            catch (Exception ex)
            {
                SoapException exc = new SoapException(ex.Message, SoapException.ClientFaultCode, "", ex.InnerException);
                throw exc;
            }
           
        }

        [WebMethod]
        public List<PSBPPDTO> PS_BatchPerProduct(int AsProductId, DateTime? StartTime = null, DateTime? EndTime = null)
        {
            try {
                var returnObject = new List<PSBPPDTO>();

                using (var cmd = _context.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = "PS_BatchesPerProduct";
                    cmd.CommandType = CommandType.StoredProcedure;
                    // set some parameters of the stored procedure
                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@AsProductId",
                        SqlDbType.Int)
                    { Value = AsProductId });

                    // set some parameters of the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@StartTime",
                        SqlDbType.DateTime)
                    { Value = StartTime });

                    cmd.Parameters.Add(new SqlParameter("@EndTime",
                        SqlDbType.DateTime)
                    { Value = EndTime });

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
#pragma warning restore CS8602 // Dereference of a possibly null reference.


                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var retObject = new PSBPPDTO();
                            retObject.ProductID = Convert.ToInt32(dataReader["ProductID"]);
                            retObject.ProductName = Convert.ToString(dataReader[1]); //TODO: SP need to be updated to fetch using column name.
                            retObject.NumberofBatches = Convert.ToInt32(dataReader["NumberofBatches"]);
                            returnObject.Add(retObject);
                        }
                    }
                    return returnObject;
                }
            }
            catch (Exception ex)
            {
                SoapException exc = new SoapException(ex.Message, SoapException.ClientFaultCode, "", ex.InnerException);
                throw exc;
            }
            
        }

        [WebMethod]
        public Boolean InsertDataKPI()
        {
            DataSet ds_DataKPI = ReadGetKPI();

            if(ds_DataKPI != null && ds_DataKPI.Tables["Kpi_List"] != null)
            {
                using (var cmd = _context.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = "DATA_KPI_INSERT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@p_Date", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@p_KPI", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_Description", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_Value", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_DateInvariant", SqlDbType.Date));
                    
                    foreach (DataRow dr in ds_DataKPI.Tables["Kpi_List"].Rows)
                    {
                        cmd.Parameters["@p_Date"].Value = DateTime.Parse( dr["Date"].ToString() ).Date;
                        cmd.Parameters["@p_KPI"].Value = dr["KPI"].ToString();
                        cmd.Parameters["@p_Description"].Value = dr["Description"].ToString();
                        cmd.Parameters["@p_Value"].Value = dr["Value"].ToString();
                        cmd.Parameters["@p_DateInvariant"].Value = DateTime.Parse(dr["DateInvariant"].ToString()).Date;

                        if (cmd.Connection.State != ConnectionState.Open)
                            cmd.Connection.Open();
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                
            }
            
                return true;
        }

        [WebMethod]
        public Boolean InsertDataStatusLine()
        {
            ReadGetStatus();
            return true;
        }

        private DataSet ReadGetKPI()
        {
            DataSet ds = new DataSet();
            
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GetKPI.xml");
            var xmlFile = XmlReader.Create(path, new XmlReaderSettings());
            
            ds.ReadXml(xmlFile);

            return ds;
        }

        private bool ReadGetStatus()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GetStatus.json");
            string jsonstring = File.ReadAllText(path);
            var serializer = new JavaScriptSerializer();
            List<StatusLine> allItemsObj = serializer.Deserialize<List<StatusLine>>(jsonstring);

            if(allItemsObj != null && allItemsObj.Count > 0)
            {
                using (var cmd = _context.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = "DATA_STATUS_LINE_INSERT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add(new SqlParameter("@p_LineID", SqlDbType.BigInt));
                    cmd.Parameters.Add(new SqlParameter("@p_LineFillerSN", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_LineNickName", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_LineIsDualSide", SqlDbType.Bit));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentID", SqlDbType.BigInt));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentSN", SqlDbType.VarChar));

                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentNickName", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentIsFiller", SqlDbType.Bit));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentStatus", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentStep", SqlDbType.BigInt));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentStop", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentStopTime", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentLabel", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentStatus2", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentStep2", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentStop2", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentStopTime2", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentLabel2", SqlDbType.VarChar));

                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentXCoord", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentYCoord", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentSide", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentType", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@p_EquipmentTypeClass", SqlDbType.VarChar));
                    

                    foreach (var line in allItemsObj)
                    {
                        cmd.Parameters["@p_LineID"].Value = line.LineID ;
                        cmd.Parameters["@p_LineFillerSN"].Value = line.LineFillerSN;
                        cmd.Parameters["@p_LineNickName"].Value = line.LineNickName;
                        cmd.Parameters["@p_LineIsDualSide"].Value = line.LineIsDualSide;
                        cmd.Parameters["@p_EquipmentID"].Value = line.EquipmentID;

                        cmd.Parameters["@p_EquipmentSN"].Value = line.EquipmentSN;
                        cmd.Parameters["@p_EquipmentNickName"].Value = line.EquipmentNickName;
                        cmd.Parameters["@p_EquipmentIsFiller"].Value = line.EquipmentIsFiller;
                        cmd.Parameters["@p_EquipmentStatus"].Value = line.EquipmentStatus;
                        cmd.Parameters["@p_EquipmentStep"].Value = line.EquipmentStep;

                        cmd.Parameters["@p_EquipmentStop"].Value = line.EquipmentStop;
                        cmd.Parameters["@p_EquipmentStopTime"].Value = line.EquipmentStopTime;
                        cmd.Parameters["@p_EquipmentLabel"].Value = line.EquipmentLabel;
                        cmd.Parameters["@p_EquipmentStatus2"].Value = line.EquipmentStatus2;
                        cmd.Parameters["@p_EquipmentStep2"].Value = line.EquipmentStep2;

                        cmd.Parameters["@p_EquipmentStop2"].Value = line.EquipmentStop2;
                        cmd.Parameters["@p_EquipmentStopTime2"].Value = line.EquipmentStopTime2;
                        cmd.Parameters["@p_EquipmentLabel2"].Value = line.EquipmentLabel2;
                        cmd.Parameters["@p_EquipmentXCoord"].Value = line.EquipmentXCoord;
                        cmd.Parameters["@p_EquipmentYCoord"].Value = line.EquipmentYCoord;

                        cmd.Parameters["@p_EquipmentSide"].Value = line.EquipmentSide;
                        cmd.Parameters["@p_EquipmentType"].Value = line.EquipmentType;
                        cmd.Parameters["@p_EquipmentTypeClass"].Value = line.EquipmentTypeClass;
                        
                            
                            
                        if (cmd.Connection.State != ConnectionState.Open)
                            cmd.Connection.Open();
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            return true;
        }

    }
}
