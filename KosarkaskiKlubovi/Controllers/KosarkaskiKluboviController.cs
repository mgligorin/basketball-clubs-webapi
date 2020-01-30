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
    public class KosarkaskiKluboviController : ApiController
    {
        IKosarkaskiKlubRepository _repository { get; set; }

        public KosarkaskiKluboviController(IKosarkaskiKlubRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<KosarkaskiKlub> Get()
        {
            return _repository.GetAll();
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var kosarkaskiKlub = _repository.GetById(id);
            if (kosarkaskiKlub == null)
            {
                return NotFound();
            }
            return Ok(kosarkaskiKlub);
        }

        [Authorize]
        public IHttpActionResult Post(KosarkaskiKlub kosarkaskiKlub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(kosarkaskiKlub);
            return CreatedAtRoute("DefaultApi", new { id = kosarkaskiKlub.Id }, kosarkaskiKlub);
        }

        [Authorize]
        public IHttpActionResult Put(int id, KosarkaskiKlub kosarkaskiKlub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kosarkaskiKlub.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(kosarkaskiKlub);
            }
            catch
            {
                throw;
            }

            return Ok(kosarkaskiKlub);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var kosarkaskiKlub = _repository.GetById(id);
            if (kosarkaskiKlub == null)
            {
                return NotFound();
            }

            _repository.Delete(kosarkaskiKlub);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("api/ekstremi")]
        public IEnumerable<KosarkaskiKlub> GetExtreme()
        {
            var kosarkaskiKlubovi = new List<KosarkaskiKlub>();
            kosarkaskiKlubovi.Add(_repository.GetAll().OrderBy(x => x.BrojOsvojenihTrofeja).FirstOrDefault());
            kosarkaskiKlubovi.Add(_repository.GetAll().OrderBy(x => x.BrojOsvojenihTrofeja).LastOrDefault());
            return kosarkaskiKlubovi;
        }
    }
}