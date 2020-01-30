using KosarkaskiKlubovi.Interfaces;
using KosarkaskiKlubovi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KosarkaskiKlubovi.Controllers
{
    public class KosarkasiController : ApiController
    {
        IKosarkasRepository _repository { get; set; }

        public KosarkasiController(IKosarkasRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Kosarkas> Get()
        {
            return _repository.GetAll().OrderByDescending(x => x.ProsecanBrojPoena);
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var kosarkas = _repository.GetById(id);
            if (kosarkas == null)
            {
                return NotFound();
            }
            return Ok(kosarkas);
        }

        [Authorize]
        public IHttpActionResult Post(Kosarkas kosarkas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(kosarkas);
            return CreatedAtRoute("DefaultApi", new { id = kosarkas.Id }, kosarkas);
        }

        [Authorize]
        public IHttpActionResult Put(int id, Kosarkas kosarkas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kosarkas.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(kosarkas);
            }
            catch
            {
                throw;
            }

            return Ok(kosarkas);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var kosarkas = _repository.GetById(id);
            if (kosarkas == null)
            {
                return NotFound();
            }

            _repository.Delete(kosarkas);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Kosarkas> Search(int godRodjenja)
        {
            return _repository.GetAll().Where(x => x.GodinaRodjenja > godRodjenja).OrderBy(x => x.GodinaRodjenja);
        }

        [Authorize]
        [HttpPost]
        [Route("api/pretraga")]
        public IEnumerable<Kosarkas> SearchPost([FromBody] KosarkasPretraga kosarkasPretraga)
        {
            return _repository.GetAll().Where(x => x.BrojUtakmicaZaKlub > kosarkasPretraga.Min && x.BrojUtakmicaZaKlub < kosarkasPretraga.Max).OrderByDescending(x => x.ProsecanBrojPoena);
        }
    }
}