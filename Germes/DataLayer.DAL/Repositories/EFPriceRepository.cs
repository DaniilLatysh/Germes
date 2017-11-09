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
    public class EFPriceRepository : IRepository<Price>
    {
        DataContext context;

        public EFPriceRepository(string name)
        {
            context = new DataContext(name);
        }

        public EFPriceRepository(DataContext context)
        {
            this.context = context;
        }

        public void Create(Price t)
        {
            context.Price.Add(t);

        }

        public void Delete(int id)
        {
            var item = context.Price.Find(id);
            if (item != null)
            {
                context.Price.Remove(item);
            }

        }

        public IEnumerable<Price> Find(Func<Price, bool> predicate)
        {
            return context.Price.Where(predicate).ToList();
        }

        public Price Get(int id)
        {
            return context.Price.Find(id);
        }

        public IEnumerable<Price> GetAll()
        {
            return context.Price;
        }

        public Task<Price> GetAsync(int id)
        {
            return context.Price.FindAsync(id);
        }

        public void Update(Price t)
        {
            context.Entry<Price>(t).State = EntityState.Modified;

        }
    }
}