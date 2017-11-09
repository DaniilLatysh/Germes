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
    public class EFStatusRepository : IRepository<Status>
    {
        DataContext context;

        public EFStatusRepository(string name)
        {
            context = new DataContext(name);
        }

        public EFStatusRepository(DataContext context)
        {
            this.context = context;
        }


        public void Create(Status t)
        {
            context.Status.Add(t);
        }

        public void Delete(int id)
        {
            var item = context.Status.Find(id);
            if (item != null)
            {
                context.Status.Remove(item);
            }
        }

        public IEnumerable<Status> Find(Func<Status, bool> predicate)
        {
            return context.Status.Where(predicate).ToList();
        }

        public Status Get(int id)
        {
            return context.Status.Find(id);
        }

        public IEnumerable<Status> GetAll()
        {
            return context.Status;
        }

        public Task<Status> GetAsync(int id)
        {
            return context.Status.FindAsync(id);
        }

        public void Update(Status t)
        {
            context.Entry<Status>(t).State = EntityState.Modified;
        }
    }
}
