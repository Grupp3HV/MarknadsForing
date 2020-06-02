using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess;

namespace MarknadsForing.Controllers
{
    public class KampanjController : ApiController
    {
        private EkonomiMarknadsforingDBEntities db = new EkonomiMarknadsforingDBEntities();

        // GET: api/Kampanj
        public IQueryable<Kampanj> GetKampanj()
        {
            return db.Kampanj;
        }

        // GET: api/Kampanj/5
        [ResponseType(typeof(Kampanj))]
        public IHttpActionResult GetKampanj(int id)
        {
            Kampanj kampanj = db.Kampanj.Find(id);
            if (kampanj == null)
            {
                return NotFound();
            }

            return Ok(kampanj);
        }

        // PUT: api/Kampanj/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKampanj(int id, Kampanj kampanj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kampanj.Id)
            {
                return BadRequest();
            }

            db.Entry(kampanj).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KampanjExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kampanj
        [ResponseType(typeof(Kampanj))]
        public IHttpActionResult PostKampanj(Kampanj kampanj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kampanj.Add(kampanj);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kampanj.Id }, kampanj);
        }

        // DELETE: api/Kampanj/5
        [ResponseType(typeof(Kampanj))]
        public IHttpActionResult DeleteKampanj(int id)
        {
            Kampanj kampanj = db.Kampanj.Find(id);
            if (kampanj == null)
            {
                return NotFound();
            }

            db.Kampanj.Remove(kampanj);
            db.SaveChanges();

            return Ok(kampanj);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KampanjExists(int id)
        {
            return db.Kampanj.Count(e => e.Id == id) > 0;
        }
    }
}