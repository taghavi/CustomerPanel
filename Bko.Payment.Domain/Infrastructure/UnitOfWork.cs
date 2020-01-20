using Bko.Payment.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bko.Payment.Domain.Infrastructure
{
    public class UnitOfWork : IDisposable,IUnitOfWork
    {
        private PaymentContext context = new PaymentContext();
        private Repository<Invoice> invoiceRepository;
        private Repository<TerminalInfo> terminalRepository;
        private Repository<PaymentTransactions> transRepository;

        public Repository<Invoice> InvoiceRepository
        {
            get
            {

                if (this.invoiceRepository == null)
                {
                    this.invoiceRepository = new Repository<Invoice>(context);
                }
                return invoiceRepository;
            }
        }

        public Repository<TerminalInfo> TerminalRepository
        {
            get
            {

                if (this.terminalRepository == null)
                {
                    this.terminalRepository = new Repository<TerminalInfo>(context);
                }
                return terminalRepository;
            }
        }

        public Repository<PaymentTransactions> TransRepository
        {
            get
            {

                if (this.transRepository == null)
                {
                    this.transRepository = new Repository<PaymentTransactions>(context);
                }
                return transRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
