using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Bko.DataAccess;
using Business;
using Framework.Helpers;
using Framework.Logger;
using Framework.Repository;

namespace Bko.Business
{
   
    public class AccountHelper
    {
        BkoLog logger = new BkoLog();
        private readonly BaseBlo<AC_JournalDetail> _blo;
        private MSrvTxnEntities _txnuow;
        private IRepository<AC_JournalDetail> _repository;
        // protected ILog Log = LogHelper.GetLogger("Accounting");
        private static HashSet<long> LockAccounts = new HashSet<long>();

        public AccountHelper()
        {
            _txnuow = new MSrvTxnEntities();
            _repository = new Repository<AC_JournalDetail>(_txnuow);
            _blo = new BaseBlo<AC_JournalDetail>(_repository, _txnuow);

        }
        public long? AddJournal(List<AC_JournalDetail> items, int JournalTypeCode, int JournalStatusCode, String Title, long? RefJournalID, long? TxnId, bool CheckLock, DateTime JournalDate, int JIdentifier)
        {
            var res = "";
            var Template = "<JournalDetail AccountID=\"{0}\" TransactionAmount=\"{1}\"></JournalDetail>";
            foreach (var dt in items)
            {
                if (LockAccounts.Contains(dt.AccountId) && CheckLock) return null;
                res += String.Format(Template, dt.AccountId, dt.TxnAmount);
            }

           
            SqlParameter[] parameters = new SqlParameter[9];
            var dic = new Dictionary<string, string>();
            parameters[0] = new SqlParameter() { ParameterName = "JournalReferenceID", Value = RefJournalID ?? (object)DBNull.Value, SqlDbType = SqlDbType.BigInt };
            parameters[1] = new SqlParameter() { ParameterName = "JournalIdentifier", Value = JIdentifier , SqlDbType = SqlDbType.BigInt };
            parameters[2] = new SqlParameter() { ParameterName = "JournalTitle", Value = Title ?? (object)DBNull.Value, SqlDbType = SqlDbType.NVarChar };
            parameters[3] = new SqlParameter() { ParameterName = "JournalStatusCode", Value=JournalStatusCode, SqlDbType = SqlDbType.Int };
            parameters[4] = new SqlParameter() { ParameterName = "JournalTypeCode", Value = JournalTypeCode, SqlDbType = SqlDbType.Int };
            parameters[5] = new SqlParameter() { ParameterName = "JournalDate", Value = JournalDate , SqlDbType = SqlDbType.DateTime };
            parameters[6] = new SqlParameter() { ParameterName = "journaldetails", Value = "<ROOT>" + res + "</ROOT>" , SqlDbType = SqlDbType.NText };
            parameters[7] = new SqlParameter() { ParameterName = "TxnId", Value = TxnId, SqlDbType = SqlDbType.BigInt };
            int totalRow = 0;
           var outParam = new SqlParameter();
            outParam.ParameterName = "InsertedJID";
            outParam.SqlDbType = SqlDbType.BigInt;
            outParam.Direction = ParameterDirection.Output; 
            
            var t = _txnuow.ExecuteDatabaseFunction2(parameters, "SpJournalInsert", null, out totalRow);
          
          
            try
            {
                if (t == null || t.Count < 1)
                    return null;
                return 1;
                //if (mm[0].JId <= 0) return mm[0].JId ?? null;
                //var jid = mm[0].JId; //= (long)total.Value;
                //return jid;
                // return null;
            }
            catch (Exception ee)
            {
                // log.Fatal(ee);
                return null;
            }
        }

        public static long? AddToAcount(int mti, long TxnId, long srcAccount, long destAccount, long wageAccount,
            decimal wageAmount, decimal Amount, string Description, long refId)
        {
            decimal SrcAmount, DestAmount;
            var Desc = string.Empty;
            if (mti == 420)
            {
                SrcAmount = Amount;
                DestAmount = -Amount;
                Desc = "سند اصلاحی از سند شماره " + Description;
            }
            else
            {
                SrcAmount = -Amount;
                DestAmount = Amount;
                Desc = "افزایش اعتبار از حساب " + Description;
            }
            try
            {
                var jList = new List<AC_JournalDetail>();
                jList.Add(new AC_JournalDetail() { AccountId = srcAccount, TxnAmount = SrcAmount });
                jList.Add(new AC_JournalDetail() { AccountId = destAccount, TxnAmount = DestAmount });
                var jId = new AccountHelper().AddJournal(jList,4,// mti == 420 ? 2 : 1,
                    1,
                    Desc + " مورخ: " + DateTime.Now.ToShamsi().Value,
                    refId, TxnId, true, DateTime.Now, mti == 420 ? 1 : 0);
                return jId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
