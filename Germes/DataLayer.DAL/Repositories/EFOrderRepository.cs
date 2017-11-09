using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DAL.Interfaces;
using DataLayer.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using DataLayer.DAL.Context;

namespace DataLayer.DAL.Repositories
{
    public class EFOrderRepository : IRepository<Order>
    {
        DataContext context;

        public EFOrderRepository(string name)
        {
            context = new DataContext(name);
        }

        public EFOrderRepository(DataContext context)
        {
            this.context = context;
        }

        public void Create(Order t)
        {
            context.Order.Add(t);

        }

        public void Delete(int id)
        {
            var item = context.Order.Find(id);
            if (item != null)
            {
                context.Order.Remove(item);
            }
        }


        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return context.Order.Where(predicate).ToList();
        }

        public Order Get(int id)
        {
            return context.Order.Find(id);
        }


        public IEnumerable<Order> GetAll()
        {
            return context.Order;
        }

        public Task<Order> GetAsync(int id)
        {
            return context.Order.FindAsync(id);
        }

        public void Update(Order t)
        {
            context.Entry<Order>(t).State = EntityState.Modified;           
        }
    }
}
