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
    public class KosarkaskiKlubRepository : IDisposable, IKosarkaskiKlubRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Add(KosarkaskiKlub kosarkaskiKlub)
        {
            db.KosarkaskiKlub.Add(kosarkaskiKlub);
            db.SaveChanges();
        }

        public void Delete(KosarkaskiKlub kosarkaskiKlub)
        {
            db.KosarkaskiKlub.Remove(kosarkaskiKlub);
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

        public IEnumerable<KosarkaskiKlub> GetAll()
        {
            return db.KosarkaskiKlub;
        }

        public KosarkaskiKlub GetById(int id)
        {
            return db.KosarkaskiKlub.FirstOrDefault(d => d.Id == id);
        }

        public void Update(KosarkaskiKlub kosarkaskiKlub)
        {
            db.Entry(kosarkaskiKlub).State = EntityState.Modified;

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