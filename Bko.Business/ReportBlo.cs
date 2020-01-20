using Bko.DataAccess;
using Bko.DataAccess.Model;
using Business;
using Framework.Logger;
using Framework.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.EntityClient;
using System.Globalization;

namespace Bko.Business
{
    public class ReportBlo
    {
        BkoLog logger = new BkoLog();
        //private readonly BaseBlo<AC_JournalDetail> _blo;
        private MSrvTxnEntities _txnuow;
        private MSrvChargeWarehouseEntities _txsim;
        private MsrvMainEntities _mainEntity;
        private aspnetdbEntities2 _mem;
        //private IRepository<AC_JournalDetail> _repository;
        public ReportBlo()
        {
            _txnuow = new MSrvTxnEntities();
            _txsim = new MSrvChargeWarehouseEntities();
            _mainEntity = new MsrvMainEntities();
            _mem = new aspnetdbEntities2();

        }

        public sp_GetUsername_Bid_Balance_Result GetName_Bid_Balance(string username)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            var dic = new Dictionary<string, string>();
            parameters[0] = new SqlParameter() { ParameterName = "username", Value = username ?? (object)DBNull.Value, SqlDbType = SqlDbType.NVarChar };
            
            var command = String.Empty;
            command += "exec sp_GetUsername_Bid_Balance";//+ functionName; 
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += " @" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ", ";
            }
                        
            var data =
                _mainEntity.Database.SqlQuery<sp_GetUsername_Bid_Balance_Result>(
                    command, sqlParams.ToArray());
            try
            {
                var result = data.ToList().FirstOrDefault();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public long JournalInsert(string title, string details)
        {
            SqlParameter[] parameters = new SqlParameter[8];
            var dic = new Dictionary<string, string>();
            parameters[0] = new SqlParameter() { ParameterName = "JournalReferenceID", Value = 0, SqlDbType = SqlDbType.BigInt };
            parameters[1] = new SqlParameter() { ParameterName = "JournalIdentifier", Value = 0, SqlDbType = SqlDbType.BigInt };
            parameters[2] = new SqlParameter() { ParameterName = "JournalTitle", Value = title, SqlDbType = SqlDbType.NVarChar };
            parameters[3] = new SqlParameter() { ParameterName = "JournalStatusCode", Value = 1, SqlDbType = SqlDbType.Int };
            parameters[4] = new SqlParameter() { ParameterName = "JournalTypeCode", Value = 4, SqlDbType = SqlDbType.Int };
            parameters[5] = new SqlParameter() { ParameterName = "JournalDate", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime };
            parameters[6] = new SqlParameter() { ParameterName = "JournalDetails", Value = details, SqlDbType = SqlDbType.NText };
            parameters[7] = new SqlParameter() { ParameterName = "TxnId", Value = 0, SqlDbType = SqlDbType.BigInt };


            var command = String.Empty;
            command += "exec SpJournalInsert";//+ functionName; 
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += " @" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ", ";
            }
            int totalRow = 0;

            var data =  _txnuow.Database.SqlQuery<Int64>(
                    command, sqlParams.ToArray());

            var res = data.ToList();
            return res.FirstOrDefault();
        }

        public string GetValet(string bid)
        {
            var result = _mainEntity.AC_Account.ToList();
            if (bid == null) return null;
            var result2 = result.Where(x => x.Id == long.Parse(bid)).FirstOrDefault().BalanceAmount;
            return result2.ToString();
        }
        public IEnumerable<SpSalesModel> GetSales(int? level, long? accountID, int? operatorId, string fromDate, string toDate)
        {

            SqlParameter[] parameters = new SqlParameter[5];
            var dic = new Dictionary<string, string>();
            parameters[0] = new SqlParameter() { ParameterName = "Level", Value = level ?? (object)DBNull.Value, SqlDbType = SqlDbType.NVarChar };
            parameters[1] = new SqlParameter() { ParameterName = "AccountID", Value = accountID ?? (object)DBNull.Value, SqlDbType = SqlDbType.NVarChar };
            parameters[2] = new SqlParameter() { ParameterName = "OperatorId", Value = operatorId ?? (object)DBNull.Value, SqlDbType = SqlDbType.NVarChar };
            parameters[3] = new SqlParameter() { ParameterName = "FromDate", Value = fromDate, SqlDbType = SqlDbType.NVarChar };
            parameters[4] = new SqlParameter() { ParameterName = "ToDate", Value = toDate, SqlDbType = SqlDbType.NVarChar };

            var command = String.Empty;
            command += "exec Sp_Sales";//+ functionName; 
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += " @" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ", ";
            }

            var data =
                _txnuow.Database.SqlQuery<SpSalesModel>(
                    command, sqlParams.ToArray());

            var result = data.ToList();

            return result;

        }

        public IEnumerable<Bko.DataAccess.SpPinSalesClass> GetPinSales(string AccountID, char? OperatorId, string OriginalAmount, string fromDate, string ToDate, int? Pagestart, int? PageEND, bool Excel)
        {

            SqlParameter[] parameters = new SqlParameter[8];
            if (AccountID == "انتخاب کنید") AccountID = null;
            //DateTime dt = DateTime.ParseExact(fromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            //DateTime dt2 = DateTime.ParseExact(ToDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            parameters[0] = new SqlParameter() { ParameterName = "AccountID", Value = string.IsNullOrEmpty(AccountID) ? (object)DBNull.Value : AccountID.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[1] = new SqlParameter() { ParameterName = "OperatorId", Value = OperatorId.HasValue ? OperatorId : (object)DBNull.Value, SqlDbType = SqlDbType.Char };
            parameters[2] = new SqlParameter() { ParameterName = "OriginalAmount", Value = string.IsNullOrEmpty(OriginalAmount) ? (object)DBNull.Value : OriginalAmount.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[3] = new SqlParameter() { ParameterName = "FromDate", Value = fromDate, SqlDbType = SqlDbType.VarChar };
            parameters[4] = new SqlParameter() { ParameterName = "Todate", Value = fromDate, SqlDbType = SqlDbType.VarChar };
            parameters[5] = new SqlParameter() { ParameterName = "Start", Value = Pagestart, SqlDbType = SqlDbType.Int };
            parameters[6] = new SqlParameter() { ParameterName = "End", Value = PageEND, SqlDbType = SqlDbType.Int };
            parameters[7] = new SqlParameter() { ParameterName = "Excel", Value = Excel, SqlDbType = SqlDbType.Bit };
            var command = string.Empty;
            command += " exec dbo.SP_Sales_Pin ";
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += "@" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ",";
            }
            var data =
               _txnuow.Database.SqlQuery<SpPinSalesClass>(
                   command, sqlParams.ToArray());
            var result = data.ToList();
            return result;
        }

        public IEnumerable<SimBalance_Result2> GetSimBalancepreCode(string date)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter() { ParameterName = "FromDate", Value = date, SqlDbType = SqlDbType.VarChar };

            var command = string.Empty;
            command += " exec SimBalance_Precode ";
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += "@" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ",";
            }
            _txnuow.Database.CommandTimeout = 200;
            var data =
               _txsim.Database.SqlQuery<SimBalance_Result2>(
                   command, sqlParams.ToArray());
            var result = data.ToList();
            return result;
        }

        public IEnumerable<ChargeReport> GetChargeReport(string fromdate, string todate, string accountID)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter() { ParameterName = "FromDate", Value = fromdate, SqlDbType = SqlDbType.VarChar };
            parameters[1] = new SqlParameter() { ParameterName = "ToDate", Value = todate, SqlDbType = SqlDbType.VarChar };
            parameters[2] = new SqlParameter() { ParameterName = "AccountId", Value = accountID, SqlDbType = SqlDbType.VarChar };
            var command = string.Empty;
            command += " exec chargeReport ";
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += "@" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ",";
            }
            _txnuow.Database.CommandTimeout = 200;
            var data =
               _txnuow.Database.SqlQuery<ChargeReport>(
                   command, sqlParams.ToArray());
            var result = data.ToList();
            return result;
        }


        public IEnumerable<SpSalesDetail2Model> GetDailySales(int? type, string AccountID, char? OperatorId, string OriginalAmount, string fromDate, string ToDate, int? Pagestart, int? PageEND, bool Excel)
        {
            SqlParameter[] parameters = new SqlParameter[9];
            var dic = new Dictionary<string, string>();
            if (AccountID == "انتخاب کنید")
                AccountID = null;
            parameters[0] = new SqlParameter() { ParameterName = "type", Value = type ?? (object)DBNull.Value, SqlDbType = SqlDbType.TinyInt };
            parameters[1] = new SqlParameter() { ParameterName = "AccountID", Value = string.IsNullOrEmpty(AccountID) ? (object)DBNull.Value : AccountID.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[2] = new SqlParameter() { ParameterName = "OperatorId", Value = OperatorId ?? (object)DBNull.Value, SqlDbType = SqlDbType.Char };
            parameters[3] = new SqlParameter() { ParameterName = "OriginalAmount", Value = OriginalAmount ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar };
            parameters[4] = new SqlParameter() { ParameterName = "FromDate", Value = fromDate, SqlDbType = SqlDbType.VarChar };
            parameters[5] = new SqlParameter() { ParameterName = "ToDate", Value = ToDate, SqlDbType = SqlDbType.VarChar };
            parameters[6] = new SqlParameter() { ParameterName = "Start", Value = Pagestart, SqlDbType = SqlDbType.Int };
            parameters[7] = new SqlParameter() { ParameterName = "End", Value = PageEND, SqlDbType = SqlDbType.Int };
            parameters[8] = new SqlParameter() { ParameterName = "Excel", Value = Excel, SqlDbType = SqlDbType.Bit };
            var command = string.Empty;
            command += "exec SP_Sales_Detail2 ";
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += " @" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ", ";
            }
            _txnuow.Database.CommandTimeout = 200;
            try
            {
                var data =
                _txnuow.Database.SqlQuery<SpSalesDetail2Model>(
                    command, sqlParams.ToArray());
                var result = data.ToList();

                return result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public IEnumerable<Txn_SystemInfoD> GetChart(string date, long? bId)
        {
            if (string.IsNullOrEmpty(date))
            {
                var year = (DateTime.Today.Year).ToString();
                var m = (DateTime.Today.Month).ToString();
                var day = (DateTime.Today.Day).ToString();
                date = year + '/' + m + '/' + day;
            }
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter() { ParameterName = "AccountID", Value = date ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar };
            parameters[1] = new SqlParameter() { ParameterName = "OperatorId", Value = bId ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar };
            var command = String.Empty;
            command += "exec Dashboard_Chart";//+ functionName;
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += " @" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType,
                    Size = parameters[i].Size
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ", ";
            }
            var data =
                    _txnuow.Database.SqlQuery<Txn_SystemInfoD>(
                        command, sqlParams.ToArray());

            var result = data.ToList();
            return result;
        }

        public IEnumerable<SpSalesDetailModel> GetDailySalessDetail(string accountID, short? operatorId, decimal? originalAmount, string fromDate, string toDate, string sortOrder, int pageIndex, int pageSize)
        {
            //   var response = _txnuow.SP_Sales_Detail(accountID, operatorId, originalAmount, fromDate, toDate);

            // return response;

            SqlParameter[] parameters = new SqlParameter[8];
            //  var dic = new Dictionary<string, string>();

            parameters[2] = new SqlParameter() { ParameterName = "OriginalAmount", Value = originalAmount ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar, Size = 7 };
            parameters[0] = new SqlParameter() { ParameterName = "AccountID", Value = accountID ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar, Size = 3 };
            parameters[1] = new SqlParameter() { ParameterName = "OperatorId", Value = operatorId ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar, Size = 1 };
            parameters[3] = new SqlParameter() { ParameterName = "FromDate", Value = fromDate, SqlDbType = SqlDbType.VarChar };
            parameters[4] = new SqlParameter() { ParameterName = "ToDate", Value = toDate, SqlDbType = SqlDbType.VarChar };
            parameters[6] = new SqlParameter() { ParameterName = "RowspPage", Value = pageSize, SqlDbType = SqlDbType.Int };
            parameters[5] = new SqlParameter() { ParameterName = "PageNumber", Value = pageIndex, SqlDbType = SqlDbType.Int };
            parameters[7] = new SqlParameter() { ParameterName = "OrderBy", Value = "AccountID", SqlDbType = SqlDbType.VarChar };



            var command = String.Empty;
            command += "exec SP_Sales_Detail";//+ functionName;
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += " @" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType,
                    Size = parameters[i].Size
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ", ";
            }

            var data =
                     _txnuow.Database.SqlQuery<SpSalesDetailModel>(
                         command, sqlParams.ToArray());

            var result = data.ToList<SpSalesDetailModel>();

            return result;

        }

        public void setLastDate(string username, string LastLoginate)
        {
            //SqlParameter[] parameters = new SqlParameter[1];
            //parameters[0] = new SqlParameter() { ParameterName = "username", Value = username, SqlDbType = SqlDbType.VarChar };
            //parameters[1] = new SqlParameter() { ParameterName = "LastLoginate", Value = LastLoginate, SqlDbType = SqlDbType.VarChar };
            var connectstring = "AspMembership";
            using (SqlConnection con = new SqlConnection("Data Source=192.168.5.17;Initial Catalog=aspnetdb;user id=bkouser;password=123456;")){
                using (SqlCommand cmd = new SqlCommand("SetLastActiveDate", con)){
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@LastLoginDate", SqlDbType.VarChar).Value = LastLoginate;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    

        public string GetLastDate(string username)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter() { ParameterName = username, Value = username, SqlDbType = SqlDbType.VarChar };
            var command = String.Empty;
            command += "exec GetLastLoginDate";
            IList<SqlParameter> sqlparams = new List<SqlParameter>();
            command += " @" + parameters[0].ParameterName;
            sqlparams.Add(new SqlParameter()
            {
                ParameterName = parameters[0].ParameterName,
                Value = parameters[0].Value,
                SqlDbType = parameters[0].SqlDbType,
                Size = parameters[0].Size
            });
           
            var data =
                _mem.Database.SqlQuery<userlastDate>(
                    command, sqlparams.FirstOrDefault());

            var result = data.FirstOrDefault<userlastDate>();
            //  var rr = _txnuow.SP_Sales_Detail("10", "1", "10000", fromDate, toDate, "AccountID", 1, 10).ToList < SpSalesDetailModel>();

            //var courseList = _txnuow.Database.SqlQuery<SpSalesDetailModel>(
            //command, parameters).ToList<SpSalesDetailModel>();
            return result.LastLoginate;
        }

        public IEnumerable<SpSalesDetailModel> GetSalesDetail(string accountID, short? operatorId, decimal? originalAmount, string fromDate, string toDate, string sortOrder, int pageIndex, int pageSize)
        {
            
            SqlParameter[] parameters = new SqlParameter[8];
           
            parameters[2] = new SqlParameter() { ParameterName = "OriginalAmount", Value = originalAmount ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar,Size=7 };
            parameters[0] = new SqlParameter() { ParameterName = "AccountID", Value = accountID ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar, Size = 3 };
            parameters[1] = new SqlParameter() { ParameterName = "OperatorId", Value = operatorId ?? (object)DBNull.Value, SqlDbType = SqlDbType.VarChar, Size = 1 };
            parameters[3] = new SqlParameter() { ParameterName = "FromDate", Value = fromDate, SqlDbType = SqlDbType.VarChar };
            parameters[4] = new SqlParameter() { ParameterName = "ToDate", Value = toDate, SqlDbType = SqlDbType.VarChar };
            parameters[6] = new SqlParameter() { ParameterName = "RowspPage", Value = pageSize, SqlDbType = SqlDbType.Int };
            parameters[5] = new SqlParameter() { ParameterName = "PageNumber", Value = pageIndex, SqlDbType = SqlDbType.Int };
            parameters[7] = new SqlParameter() { ParameterName = "OrderBy", Value = "AccountID", SqlDbType = SqlDbType.VarChar};
            


            var command = String.Empty;
            command += "exec SP_Sales_Detail";//+ functionName;
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += " @" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType,
                    Size = parameters[i].Size
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ", ";
            }
   
        var data =
                 _txnuow.Database.SqlQuery<SpSalesDetailModel>(
                     command, sqlParams.ToArray());

            var result = data.ToList<SpSalesDetailModel>();
     
           return result;

        }

        public IEnumerable<RepCheckModel> Sp_Rep_Check(short? tbl, string cellNum, string responseCode, string PrCode, string id, string refNum, string localSerial, string resNum, string beEntityList, string date, char? txnStatus,Int64? FWRef,string OperatorId, int pageIndex, int pageSize)
        {
            string Where = null;
            if (txnStatus == '2')
            {
                txnStatus = null;
                Where = "ti.TxStatus in (2,3,4,5)";
                if (PrCode == "0")
                {
                    PrCode = null;
                    Where = Where + " and ti.prcode in (0,1,2,6,80,81,82,83,84)";
                }
            }
            if ((PrCode == "0") && (txnStatus != 2))
            {
                PrCode = null;
                Where = "ti.prcode in (0,1,2,6,80,81,82,83,84)";
            }


            if (!txnStatus.HasValue) txnStatus = null; 
            string OrderBy = null;
            SqlParameter[] parameters = new SqlParameter[17];
            var dic = new Dictionary<string, string>();
            parameters[0] = new SqlParameter() { ParameterName = "tbl", Value = tbl ?? (object)DBNull.Value, SqlDbType = SqlDbType.TinyInt };
            parameters[1] = new SqlParameter() { ParameterName = "CellNUmber", Value = string.IsNullOrEmpty(cellNum) ? (object)DBNull.Value : cellNum.ToString(), SqlDbType = SqlDbType.NVarChar };
            parameters[2] = new SqlParameter() { ParameterName = "ResponseCode", Value = string.IsNullOrEmpty(responseCode) ? (object)DBNull.Value : responseCode.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[3] = new SqlParameter() { ParameterName = "PrCode", Value = string.IsNullOrEmpty(PrCode) ? (object)DBNull.Value : PrCode.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[4] = new SqlParameter() { ParameterName = "id", Value = string.IsNullOrEmpty(id) ? (object)DBNull.Value : id.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[5] = new SqlParameter() { ParameterName = "RefrenceNumber", Value = string.IsNullOrEmpty(refNum) ? (object)DBNull.Value : refNum.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[6] = new SqlParameter() { ParameterName = "LocalSerial", Value = string.IsNullOrEmpty(localSerial) ? (object)DBNull.Value : localSerial.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[7] = new SqlParameter() { ParameterName = "ReserveNumber", Value = string.IsNullOrEmpty(resNum) ? (object)DBNull.Value : resNum.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[8] = new SqlParameter() { ParameterName = "bid", Value = string.IsNullOrEmpty(beEntityList) ? (object)DBNull.Value : beEntityList.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[9] = new SqlParameter() { ParameterName = "Date", Value = string.IsNullOrEmpty(date) ? (object)DBNull.Value : date.ToString(), SqlDbType = SqlDbType.VarChar };
            parameters[10] = new SqlParameter() { ParameterName = "TxStatus", Value = txnStatus ?? (object)DBNull.Value, SqlDbType = SqlDbType.Char };
            parameters[11] = new SqlParameter() { ParameterName = "where", Value = !string.IsNullOrEmpty(Where) ?Where:(object)DBNull.Value, SqlDbType = SqlDbType.NVarChar };
            parameters[12] = new SqlParameter() { ParameterName = "OrderBy", Value = string.IsNullOrEmpty(OrderBy) ? (object)DBNull.Value : OrderBy.ToString(), SqlDbType = SqlDbType.NVarChar };
            parameters[13] = new SqlParameter() { ParameterName = "FWRef", Value = string.IsNullOrEmpty(FWRef.ToString()) ? (object)DBNull.Value:(Int64)(FWRef), SqlDbType = SqlDbType.NVarChar };
            parameters[14] = new SqlParameter() { ParameterName = "PageNumber", Value = pageIndex, SqlDbType = SqlDbType.Int };
            parameters[15] = new SqlParameter() { ParameterName = "RowspPage", Value = pageSize, SqlDbType = SqlDbType.Int };
            parameters[16] = new SqlParameter() { ParameterName = "Operatorid", Value = string.IsNullOrEmpty(OperatorId) ? (object)DBNull.Value : OperatorId.ToString(), SqlDbType = SqlDbType.VarChar };

            var command = String.Empty;
            command += "exec dbo.Sp_rep_check";//+ functionName; 
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += " @" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ", ";
            }
            _txnuow.Database.CommandTimeout = 60;
           
            try
            { var data =
                _txnuow.Database.SqlQuery<RepCheckModel>(
                    command, sqlParams.ToArray());
                var result = data.ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw  ex;
            }
            

        }
        public IEnumerable<Bko.DataAccess.SpSimSale> Getsimsales(int? Type, string fromdate, string todate)
        {

            SqlParameter[] parameters = new SqlParameter[3];

            DateTime dt = DateTime.ParseExact(fromdate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(todate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            parameters[0] = new SqlParameter() { ParameterName = "SimType", Value = Type ?? (object)DBNull.Value, SqlDbType = SqlDbType.Int };
            parameters[1] = new SqlParameter() { ParameterName = "FromDate", Value =dt, SqlDbType = SqlDbType.Date };
            parameters[2] = new SqlParameter() { ParameterName = "Todate", Value = dt2, SqlDbType = SqlDbType.Date };
            var command = string.Empty;
            command += " exec dbo.SimSale2 ";
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += "@" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value=parameters[i].Value,
                    SqlDbType=parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ",";
            }
            var data =
               _txsim.Database.SqlQuery<SpSimSale>(
                   command, sqlParams.ToArray());
            var result = data.ToList();
            return result;
        }
        public IEnumerable<spsimcashClass> GetsimCash(string date)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            DateTime dt = DateTime.ParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            parameters[0] = new SqlParameter() { ParameterName = "Date", Value = dt, SqlDbType = SqlDbType.Date };
            var command = string.Empty;
            command += " exec dbo.SimBalance ";
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += "@" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ",";
            }
            var data =
               _txsim.Database.SqlQuery<spsimcashClass>(
                   command, sqlParams.ToArray());
            var result = data.ToList();
            
            return result;
        }
        public IEnumerable<sppincash> GetpinCash(string date)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            
            DateTime dt = DateTime.ParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            parameters[0] = new SqlParameter() { ParameterName = "Date", Value = dt, SqlDbType = SqlDbType.Date };
            var command = string.Empty;
            command += " exec dbo.PinBalance ";
            IList<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i <= parameters.Length - 1; i++)
            {
                command += "@" + parameters[i].ParameterName;
                sqlParams.Add(new SqlParameter()
                {
                    ParameterName = parameters[i].ParameterName,
                    Value = parameters[i].Value,
                    SqlDbType = parameters[i].SqlDbType
                });
                if (parameters[i].ParameterName != parameters[parameters.Length - 1].ParameterName)
                    command += ",";
            }
            
            var data =
               _txsim.Database.SqlQuery<sppincash>(
                   command, sqlParams.ToArray());
            var result = data.ToList();
            return result;
        }
    }

    public partial class userlastDate
    {
        public string UserName { get; set; }
        public string LastLoginate { get; set; }
    }
}
