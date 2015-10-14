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
using SumaqHotelsApi.Models;

namespace SumaqHotelsApi.Controllers
{
    public class TipoCamasController : ApiController
    {
        private SumaqHotels_Context db = new SumaqHotels_Context();

        // GET: api/TipoCamas
        public IHttpActionResult GetTiposCamas()
        {
            try
            {
                var listTiposCamas = db.TiposCamas.ToList();
                if (listTiposCamas == null)
                {
                    return BadRequest("No existen tipos de camas cargadas");
                }
                else
                {
                    return Ok(listTiposCamas);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/TipoCamas/5
        [ResponseType(typeof(TipoCama))]
        public IHttpActionResult GetTipoCama(int id)
        {
            TipoCama tipoCama = db.TiposCamas.Find(id);
            if (tipoCama == null)
            {
                return NotFound();
            }

            return Ok(tipoCama);
        }

        // PUT: api/TipoCamas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoCama(int id, TipoCama tipoCama)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoCama.Id)
            {
                return BadRequest();
            }

            db.Entry(tipoCama).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCamaExists(id))
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

        // POST: api/TipoCamas
        [ResponseType(typeof(TipoCama))]
        public IHttpActionResult PostTipoCama(TipoCama tipoCama)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TiposCamas.Add(tipoCama);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoCama.Id }, tipoCama);
        }

        // DELETE: api/TipoCamas/5
        [ResponseType(typeof(TipoCama))]
        public IHttpActionResult DeleteTipoCama(int id)
        {
            TipoCama tipoCama = db.TiposCamas.Find(id);
            if (tipoCama == null)
            {
                return NotFound();
            }

            db.TiposCamas.Remove(tipoCama);
            db.SaveChanges();

            return Ok(tipoCama);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoCamaExists(int id)
        {
            return db.TiposCamas.Count(e => e.Id == id) > 0;
        }
    }
}