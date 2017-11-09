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
    public class EFClientRepository : IRepository<Client>
    {
        DataContext context;

        public EFClientRepository(string name)
        {
            context = new DataContext(name);
        }

        public EFClientRepository(DataContext context)
        {
            this.context = context;
        }

        public void Create(Client t)
        {
            context.Client.Add(t);
            
        }

        public void Delete(int id)
        {
            var item = context.Client.Find(id);
            if (item != null)
            {
                context.Client.Remove(item);
            }
            
        }

        public IEnumerable<Client> Find(Func<Client, bool> predicate)
        {
            return context.Client.Where(predicate).ToList();
        }

        public Client Get(int id)
        {
            return context.Client.Find(id);
        }

        public IEnumerable<Client> GetAll()
        {
            return context.Client;
        }

        public Task<Client> GetAsync(int id)
        {
            return context.Client.FindAsync(id);
        }

        public void Update(Client t)
        {
            context.Entry<Client>(t).State = EntityState.Modified;
            
        }
    }
}
