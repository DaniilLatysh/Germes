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
    public class EFCategoryRepository : IRepository<Category>
    {
        DataContext context;

        public EFCategoryRepository(string name)
        {
            context = new DataContext(name);
        }
        public EFCategoryRepository(DataContext context)
        {
            this.context = context;
        }

        public void Create(Category t)
        {
            context.Category.Add(t);
            
        }

        public void Delete(int id)
        {
            var item = context.Category.Find(id);
            if (item != null)
            {
                context.Category.Remove(item);
            }
            
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return context.Category.Where(predicate).ToList();
        }

        public Category Get(int id)
        {
            return context.Category.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Category;
        }

        public Task<Category> GetAsync(int id)
        {
            return context.Category.FindAsync(id);
        }

        public void Update(Category t)
        {
            context.Entry<Category>(t).State = EntityState.Modified;
            
        }

    }
}
