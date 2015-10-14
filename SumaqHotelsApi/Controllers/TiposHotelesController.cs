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
    public class TiposHotelesController : ApiController
    {
        private SumaqHotels_Context db = new SumaqHotels_Context();

        // GET: api/TiposHoteles
        [Authorize]
        public IHttpActionResult GetTipoHoteles()
        {
            try
            {
                var listTiposHoteles = db.TipoHoteles.ToList();
                if (listTiposHoteles == null)
                {
                    return BadRequest("No existen Tipos de Hoteles");   
                }

                return Ok(listTiposHoteles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());                
            }
            
        }

        // GET: api/TiposHoteles/5
        [ResponseType(typeof(TipoHotel))]
        public IHttpActionResult GetTipoHotel(int id)
        {
            TipoHotel tipoHotel = db.TipoHoteles.Find(id);
            if (tipoHotel == null)
            {
                return NotFound();
            }

            return Ok(tipoHotel);
        }

        // PUT: api/TiposHoteles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoHotel(int id, TipoHotel tipoHotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoHotel.Id)
            {
                return BadRequest();
            }

            db.Entry(tipoHotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoHotelExists(id))
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

        // POST: api/TiposHoteles
        [ResponseType(typeof(TipoHotel))]
        public IHttpActionResult PostTipoHotel(TipoHotel tipoHotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.TipoHoteles.Add(tipoHotel);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }            
        }

        // DELETE: api/TiposHoteles/5
        [ResponseType(typeof(TipoHotel))]
        public IHttpActionResult DeleteTipoHotel(int id)
        {
            TipoHotel tipoHotel = db.TipoHoteles.Find(id);
            if (tipoHotel == null)
            {
                return NotFound();
            }

            db.TipoHoteles.Remove(tipoHotel);
            db.SaveChanges();

            return Ok(tipoHotel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoHotelExists(int id)
        {
            return db.TipoHoteles.Count(e => e.Id == id) > 0;
        }
    }
}