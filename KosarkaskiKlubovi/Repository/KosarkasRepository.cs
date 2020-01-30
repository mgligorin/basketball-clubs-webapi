using KosarkaskiKlubovi.Interfaces;
using KosarkaskiKlubovi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace KosarkaskiKlubovi.Repository
{
    public class KosarkasRepository : IDisposable, IKosarkasRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Kosarkas kosarkas)
        {
            db.Kosarkas.Add(kosarkas);
            db.SaveChanges();
        }

        public void Delete(Kosarkas kosarkas)
        {
            db.Kosarkas.Remove(kosarkas);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Kosarkas> GetAll()
        {
            return db.Kosarkas.Include(k => k.KosarkaskiKlub);
        }

        public Kosarkas GetById(int id)
        {
            return db.Kosarkas.Include(k => k.KosarkaskiKlub).FirstOrDefault(k => k.Id == id);
        }

        public void Update(Kosarkas kosarkas)
        {
            db.Entry(kosarkas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}