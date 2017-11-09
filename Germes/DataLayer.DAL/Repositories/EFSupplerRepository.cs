using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DAL.Interfaces;
using DataLayer.DAL.Entities;
using System.Data.Entity;
using DataLayer.DAL.Context;

namespace DataLayer.DAL.Repositories
{
    public class EFSupplierRepository : IRepository<Supplier>
    {
        DataContext context;

        public EFSupplierRepository(string name)
        {
            context = new DataContext(name);
        }

        public EFSupplierRepository(DataContext context)
        {
            this.context = context;
        }


        public void Create(Supplier t)
        {
            context.Supplier.Add(t);
        }

        public void Delete(int id)
        {
            var item = context.Supplier.Find(id);
            if (item != null)
            {
                context.Supplier.Remove(item);
            }
        }

        public IEnumerable<Supplier> Find(Func<Supplier, bool> predicate)
        {
            return context.Supplier.Where(predicate).ToList();
        }

        public Supplier Get(int id)
        {
            return context.Supplier.Find(id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return context.Supplier;
        }

        public Task<Supplier> GetAsync(int id)
        {
            return context.Supplier.FindAsync(id);
        }

        public void Update(Supplier t)
        {
            context.Entry<Supplier>(t).State = EntityState.Modified;
        }
    }
}
