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
    public class EFSettingRepository
    {
        DataContext context;

        public EFSettingRepository(string name)
        {
            context = new DataContext(name);
        }

        public EFSettingRepository(DataContext context)
        {
            this.context = context;
        }

        public Settings Get(int id)
        {
            return context.Settings.Find(id);
        }

        public IEnumerable<Settings> GetAll()
        {
            return context.Settings;
        }

        public Task<Settings> GetAsync(int id)
        {
            return context.Settings.FindAsync(id);
        }

        public void Update(Settings t)
        {
            context.Entry<Settings>(t).State = EntityState.Modified;
        }
    }
}
