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
    public class EFProductRepository : IRepository<Product>
    {
        DataContext context;

        public EFProductRepository(string name)
        {
            context = new DataContext(name);
        }

        public EFProductRepository(DataContext context)
        {
            this.context = context;
        }

        public void Create(Product t)
        {
            context.Product.Add(t);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = context.Product.Find(id);
            if (item != null)
            {
                context.Product.Remove(item);
            }

        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return context.Product.Where(predicate).ToList();
        }

        public Product Get(int id)
        {
            return context.Product.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Product.Include(x => x.Category);
        }

        public Task<Product> GetAsync(int id)
        {
            return context.Product.FindAsync(id);
        }

        public void Update(Product t)
        {
            context.Entry<Product>(t).State = EntityState.Modified;

        }
    }
}
