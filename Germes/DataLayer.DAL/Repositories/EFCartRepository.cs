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
    public class EFCartRepository : IRepository<CartItem>
    {
        DataContext context;

        public EFCartRepository(string name)
        {
            context = new DataContext(name);
        }
        public EFCartRepository(DataContext context)
        {
            this.context = context;
        }

        public void Create(CartItem t)
        {
            context.Cart.Add(t);
        }

        public void Delete(int id)
        {
            var item = context.Cart.Find(id);
            if (item != null)
            {
                context.Cart.Remove(item);
            }
            
        }

        public IEnumerable<CartItem> Find(Func<CartItem, bool> predicate)
        {
            return context.Cart.Where(predicate).ToList();
        }

        public CartItem Get(int id)
        {
            return context.Cart.Find(id);
        }

        public IEnumerable<CartItem> GetAll()
        {
            return context.Cart;
        }

        public Task<CartItem> GetAsync(int id)
        {
            return context.Cart.FindAsync(id);
        }

        public void Update(CartItem t)
        {
            context.Entry<CartItem>(t).State = EntityState.Modified;           
        }

    }
}
