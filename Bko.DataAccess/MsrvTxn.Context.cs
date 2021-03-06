﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bko.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MSrvTxnEntities : DbContext
    {
        public MSrvTxnEntities()
            : base("name=MSrvTxnEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AC_Journal> AC_Journal { get; set; }
        public virtual DbSet<AC_JournalDetail> AC_JournalDetail { get; set; }
        public virtual DbSet<Txn_BusinessDate> Txn_BusinessDate { get; set; }
        public virtual DbSet<Txn_Error> Txn_Error { get; set; }
        public virtual DbSet<Txn_Info> Txn_Info { get; set; }
        public virtual DbSet<Txn_Queue> Txn_Queue { get; set; }
        public virtual DbSet<Txn_SystemInfo> Txn_SystemInfo { get; set; }
    
        public virtual ObjectResult<Nullable<long>> SpJournalInsert(Nullable<long> journalReferenceID, Nullable<int> journalIdentifier, string journalTitle, Nullable<int> journalStatusCode, Nullable<int> journalTypeCode, Nullable<System.DateTime> journalDate, string journalDetails, Nullable<long> txnId)
        {
            var journalReferenceIDParameter = journalReferenceID.HasValue ?
                new ObjectParameter("JournalReferenceID", journalReferenceID) :
                new ObjectParameter("JournalReferenceID", typeof(long));
    
            var journalIdentifierParameter = journalIdentifier.HasValue ?
                new ObjectParameter("JournalIdentifier", journalIdentifier) :
                new ObjectParameter("JournalIdentifier", typeof(int));
    
            var journalTitleParameter = journalTitle != null ?
                new ObjectParameter("JournalTitle", journalTitle) :
                new ObjectParameter("JournalTitle", typeof(string));
    
            var journalStatusCodeParameter = journalStatusCode.HasValue ?
                new ObjectParameter("JournalStatusCode", journalStatusCode) :
                new ObjectParameter("JournalStatusCode", typeof(int));
    
            var journalTypeCodeParameter = journalTypeCode.HasValue ?
                new ObjectParameter("JournalTypeCode", journalTypeCode) :
                new ObjectParameter("JournalTypeCode", typeof(int));
    
            var journalDateParameter = journalDate.HasValue ?
                new ObjectParameter("JournalDate", journalDate) :
                new ObjectParameter("JournalDate", typeof(System.DateTime));
    
            var journalDetailsParameter = journalDetails != null ?
                new ObjectParameter("JournalDetails", journalDetails) :
                new ObjectParameter("JournalDetails", typeof(string));
    
            var txnIdParameter = txnId.HasValue ?
                new ObjectParameter("TxnId", txnId) :
                new ObjectParameter("TxnId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("SpJournalInsert", journalReferenceIDParameter, journalIdentifierParameter, journalTitleParameter, journalStatusCodeParameter, journalTypeCodeParameter, journalDateParameter, journalDetailsParameter, txnIdParameter);
        }
    
        public virtual ObjectResult<Nullable<long>> SpJournalInsert1(Nullable<long> journalReferenceID, Nullable<int> journalIdentifier, string journalTitle, Nullable<int> journalStatusCode, Nullable<int> journalTypeCode, Nullable<System.DateTime> journalDate, string journalDetails, Nullable<long> txnId)
        {
            var journalReferenceIDParameter = journalReferenceID.HasValue ?
                new ObjectParameter("JournalReferenceID", journalReferenceID) :
                new ObjectParameter("JournalReferenceID", typeof(long));
    
            var journalIdentifierParameter = journalIdentifier.HasValue ?
                new ObjectParameter("JournalIdentifier", journalIdentifier) :
                new ObjectParameter("JournalIdentifier", typeof(int));
    
            var journalTitleParameter = journalTitle != null ?
                new ObjectParameter("JournalTitle", journalTitle) :
                new ObjectParameter("JournalTitle", typeof(string));
    
            var journalStatusCodeParameter = journalStatusCode.HasValue ?
                new ObjectParameter("JournalStatusCode", journalStatusCode) :
                new ObjectParameter("JournalStatusCode", typeof(int));
    
            var journalTypeCodeParameter = journalTypeCode.HasValue ?
                new ObjectParameter("JournalTypeCode", journalTypeCode) :
                new ObjectParameter("JournalTypeCode", typeof(int));
    
            var journalDateParameter = journalDate.HasValue ?
                new ObjectParameter("JournalDate", journalDate) :
                new ObjectParameter("JournalDate", typeof(System.DateTime));
    
            var journalDetailsParameter = journalDetails != null ?
                new ObjectParameter("JournalDetails", journalDetails) :
                new ObjectParameter("JournalDetails", typeof(string));
    
            var txnIdParameter = txnId.HasValue ?
                new ObjectParameter("TxnId", txnId) :
                new ObjectParameter("TxnId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("SpJournalInsert1", journalReferenceIDParameter, journalIdentifierParameter, journalTitleParameter, journalStatusCodeParameter, journalTypeCodeParameter, journalDateParameter, journalDetailsParameter, txnIdParameter);
        }
    
        public virtual int SP_Rep_Check(Nullable<byte> tbl, string cellNUmber, string responseCode, string id, string refrenceNumber, string reserveNumber, string bid, string date, string txStatus, string where)
        {
            var tblParameter = tbl.HasValue ?
                new ObjectParameter("tbl", tbl) :
                new ObjectParameter("tbl", typeof(byte));
    
            var cellNUmberParameter = cellNUmber != null ?
                new ObjectParameter("CellNUmber", cellNUmber) :
                new ObjectParameter("CellNUmber", typeof(string));
    
            var responseCodeParameter = responseCode != null ?
                new ObjectParameter("ResponseCode", responseCode) :
                new ObjectParameter("ResponseCode", typeof(string));
    
            var idParameter = id != null ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(string));
    
            var refrenceNumberParameter = refrenceNumber != null ?
                new ObjectParameter("RefrenceNumber", refrenceNumber) :
                new ObjectParameter("RefrenceNumber", typeof(string));
    
            var reserveNumberParameter = reserveNumber != null ?
                new ObjectParameter("ReserveNumber", reserveNumber) :
                new ObjectParameter("ReserveNumber", typeof(string));
    
            var bidParameter = bid != null ?
                new ObjectParameter("bid", bid) :
                new ObjectParameter("bid", typeof(string));
    
            var dateParameter = date != null ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(string));
    
            var txStatusParameter = txStatus != null ?
                new ObjectParameter("TxStatus", txStatus) :
                new ObjectParameter("TxStatus", typeof(string));
    
            var whereParameter = where != null ?
                new ObjectParameter("where", where) :
                new ObjectParameter("where", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Rep_Check", tblParameter, cellNUmberParameter, responseCodeParameter, idParameter, refrenceNumberParameter, reserveNumberParameter, bidParameter, dateParameter, txStatusParameter, whereParameter);
        }
    
        public virtual ObjectResult<SpSalesModel> Sp_Sales(string level, string accountID, string operatorId, string fromDate, string toDate)
        {
            var levelParameter = level != null ?
                new ObjectParameter("Level", level) :
                new ObjectParameter("Level", typeof(string));
    
            var accountIDParameter = accountID != null ?
                new ObjectParameter("AccountID", accountID) :
                new ObjectParameter("AccountID", typeof(string));
    
            var operatorIdParameter = operatorId != null ?
                new ObjectParameter("OperatorId", operatorId) :
                new ObjectParameter("OperatorId", typeof(string));
    
            var fromDateParameter = fromDate != null ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(string));
    
            var toDateParameter = toDate != null ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpSalesModel>("Sp_Sales", levelParameter, accountIDParameter, operatorIdParameter, fromDateParameter, toDateParameter);
        }
    
        public virtual ObjectResult<SpSalesDetailModel> SP_Sales_Detail(string accountID, string operatorId, string originalAmount, string fromDate, string toDate, string orderBy, Nullable<int> pageNumber, Nullable<int> rowspPage)
        {
            var accountIDParameter = accountID != null ?
                new ObjectParameter("AccountID", accountID) :
                new ObjectParameter("AccountID", typeof(string));
    
            var operatorIdParameter = operatorId != null ?
                new ObjectParameter("OperatorId", operatorId) :
                new ObjectParameter("OperatorId", typeof(string));
    
            var originalAmountParameter = originalAmount != null ?
                new ObjectParameter("OriginalAmount", originalAmount) :
                new ObjectParameter("OriginalAmount", typeof(string));
    
            var fromDateParameter = fromDate != null ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(string));
    
            var toDateParameter = toDate != null ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(string));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(string));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var rowspPageParameter = rowspPage.HasValue ?
                new ObjectParameter("RowspPage", rowspPage) :
                new ObjectParameter("RowspPage", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpSalesDetailModel>("SP_Sales_Detail", accountIDParameter, operatorIdParameter, originalAmountParameter, fromDateParameter, toDateParameter, orderByParameter, pageNumberParameter, rowspPageParameter);
        }
    
        public virtual int SP_Rep_Check1(Nullable<byte> tbl, string cellNUmber, string responseCode, string prCode, string id, string refrenceNumber, string localSerial, string reserveNumber, string bid, string date, string txStatus, string where, string orderBy, string fWRef, Nullable<int> pageNumber, Nullable<int> rowspPage, string operatorid)
        {
            var tblParameter = tbl.HasValue ?
                new ObjectParameter("tbl", tbl) :
                new ObjectParameter("tbl", typeof(byte));
    
            var cellNUmberParameter = cellNUmber != null ?
                new ObjectParameter("CellNUmber", cellNUmber) :
                new ObjectParameter("CellNUmber", typeof(string));
    
            var responseCodeParameter = responseCode != null ?
                new ObjectParameter("ResponseCode", responseCode) :
                new ObjectParameter("ResponseCode", typeof(string));
    
            var prCodeParameter = prCode != null ?
                new ObjectParameter("PrCode", prCode) :
                new ObjectParameter("PrCode", typeof(string));
    
            var idParameter = id != null ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(string));
    
            var refrenceNumberParameter = refrenceNumber != null ?
                new ObjectParameter("RefrenceNumber", refrenceNumber) :
                new ObjectParameter("RefrenceNumber", typeof(string));
    
            var localSerialParameter = localSerial != null ?
                new ObjectParameter("LocalSerial", localSerial) :
                new ObjectParameter("LocalSerial", typeof(string));
    
            var reserveNumberParameter = reserveNumber != null ?
                new ObjectParameter("ReserveNumber", reserveNumber) :
                new ObjectParameter("ReserveNumber", typeof(string));
    
            var bidParameter = bid != null ?
                new ObjectParameter("bid", bid) :
                new ObjectParameter("bid", typeof(string));
    
            var dateParameter = date != null ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(string));
    
            var txStatusParameter = txStatus != null ?
                new ObjectParameter("TxStatus", txStatus) :
                new ObjectParameter("TxStatus", typeof(string));
    
            var whereParameter = where != null ?
                new ObjectParameter("where", where) :
                new ObjectParameter("where", typeof(string));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(string));
    
            var fWRefParameter = fWRef != null ?
                new ObjectParameter("FWRef", fWRef) :
                new ObjectParameter("FWRef", typeof(string));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var rowspPageParameter = rowspPage.HasValue ?
                new ObjectParameter("RowspPage", rowspPage) :
                new ObjectParameter("RowspPage", typeof(int));
    
            var operatoridParameter = operatorid != null ?
                new ObjectParameter("Operatorid", operatorid) :
                new ObjectParameter("Operatorid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Rep_Check1", tblParameter, cellNUmberParameter, responseCodeParameter, prCodeParameter, idParameter, refrenceNumberParameter, localSerialParameter, reserveNumberParameter, bidParameter, dateParameter, txStatusParameter, whereParameter, orderByParameter, fWRefParameter, pageNumberParameter, rowspPageParameter, operatoridParameter);
        }
    }
}
