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
    public class PasajeroController : ApiController
    {
        private SumaqHotels_Context db = new SumaqHotels_Context();

        // GET: api/Pasajero
        public IQueryable<Pasajero> GetPasajeroes()
        {
            return db.Pasajeroes;
        }

        // GET: api/Pasajero/5
        [ResponseType(typeof(Pasajero))]
        public IHttpActionResult GetPasajero(int id)
        {
            Pasajero pasajero = db.Pasajeroes.Find(id);
            if (pasajero == null)
            {
                return NotFound();
            }

            return Ok(pasajero);
        }

        // PUT: api/Pasajero/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPasajero(int id, Pasajero pasajero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pasajero.Id)
            {
                return BadRequest();
            }

            db.Entry(pasajero).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasajeroExists(id))
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

        // POST: api/Pasajero
        [ResponseType(typeof(Pasajero))]
        public IHttpActionResult PostPasajero(Pasajero pasajero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Pasajeroes.Add(pasajero);
                db.SaveChanges();

                return Ok("Alta de Pasajero Exitosa");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message.ToString());
            }


           
        }

        // DELETE: api/Pasajero/5
        [ResponseType(typeof(Pasajero))]
        public IHttpActionResult DeletePasajero(int id)
        {
            Pasajero pasajero = db.Pasajeroes.Find(id);
            if (pasajero == null)
            {
                return NotFound();
            }

            db.Pasajeroes.Remove(pasajero);
            db.SaveChanges();

            return Ok(pasajero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PasajeroExists(int id)
        {
            return db.Pasajeroes.Count(e => e.Id == id) > 0;
        }
    }
}