using System;
using DataLayer.DAL.Repositories;
using DataLayer.DAL.Context;
using DataLayer.DAL.Interfaces;
using System.Diagnostics;

namespace DataLayer.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DataContext context = new DataContext("Connection");
        private EFOrderRepository orderRepository;
        private EFClientRepository clientRepository;
        private EFStatusRepository statusRepository;
        private EFPriceRepository priceRepository;
        private EFProductRepository productRepository;
        private EFCategoryRepository categoryRepository;
        private EFSupplierRepository supplierRepository;
        private EFCartRepository cartRepository;
        private EFSettingRepository settingsRepository;

        public EFSettingRepository Settings
        {
            get
            {
                if (settingsRepository == null)
                    settingsRepository = new EFSettingRepository(context);
                return settingsRepository;
            }
        }

        public EFCartRepository Cart
        {
            get
            {
                if (cartRepository == null)
                    cartRepository = new EFCartRepository(context);
                return cartRepository;
            }
        }
        public EFOrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new EFOrderRepository(context);
                return orderRepository;
            }
        }
        public EFClientRepository Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new EFClientRepository(context);
                return clientRepository;
            }
        }
        public EFStatusRepository Statuses
        {
            get
            {
                if (statusRepository == null)
                    statusRepository = new EFStatusRepository(context);
                return statusRepository;
            }
        }
        public EFPriceRepository Prices
        {
            get
            {
                if (priceRepository == null)
                    priceRepository = new EFPriceRepository(context);
                return priceRepository;
            }
        }
        public EFProductRepository Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new EFProductRepository(context);
                return productRepository;
            }
        }
        public EFCategoryRepository Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new EFCategoryRepository(context);
                return categoryRepository;
            }
        }

        public EFSupplierRepository Suppliers
        {
            get
            {
                if (supplierRepository == null)
                    supplierRepository = new EFSupplierRepository(context);
                return supplierRepository;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error save context: " + ex.Message.ToString());
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
    }
}
