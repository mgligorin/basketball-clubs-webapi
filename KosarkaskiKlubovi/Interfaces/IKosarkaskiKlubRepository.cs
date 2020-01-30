using KosarkaskiKlubovi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KosarkaskiKlubovi.Interfaces
{
    public interface IKosarkaskiKlubRepository
    {
        void Add(KosarkaskiKlub kosarkaskiKlub);
        void Delete(KosarkaskiKlub kosarkaskiKlub);
        IEnumerable<KosarkaskiKlub> GetAll();
        KosarkaskiKlub GetById(int id);
        void Update(KosarkaskiKlub kosarkaskiKlub);
    }
}