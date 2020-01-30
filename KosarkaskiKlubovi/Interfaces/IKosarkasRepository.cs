using KosarkaskiKlubovi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KosarkaskiKlubovi.Interfaces
{
    public interface IKosarkasRepository
    {
        void Add(Kosarkas kosarkas);
        void Delete(Kosarkas kosarkas);
        IEnumerable<Kosarkas> GetAll();
        Kosarkas GetById(int id);
        void Update(Kosarkas kosarkas);
    }
}