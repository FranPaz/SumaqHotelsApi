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
    public class HotelesController : ApiController
    {
        private SumaqHotels_Context db = new SumaqHotels_Context();

        // GET: api/Hoteles
        public IHttpActionResult GetHotels()
        {
            try
            {
                var listHoteles = db.Hoteles
                    .Include(a => a.ApplicationUsers);
                if (listHoteles == null)
                {
                    return BadRequest("No existen hoteles");
                }

                return Ok(listHoteles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/Hoteles/5
        //[ResponseType(typeof(Hotel))]
        public IHttpActionResult GetHotel(int id)
        {
            

           var hotel = (from h in db.Hoteles
                             where h.Id == id
                             select h)
                             .Include(c => c.Categoria)
                             .Include(th => th.TipoHotel)
                             .FirstOrDefault();
          
           hotel.TipoHotel.Hoteles = null;
           hotel.Categoria.Hoteles = null;

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // PUT: api/Hoteles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHotel(int id, Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.Id)
            {
                return BadRequest();
            }

            db.Entry(hotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hoteles
        [ResponseType(typeof(Hotel))]
        public IHttpActionResult PostHotel(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hoteles.Add(hotel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hoteles/5
        [ResponseType(typeof(Hotel))]
        public IHttpActionResult DeleteHotel(int id)
        {
            Hotel hotel = db.Hoteles.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            db.Hoteles.Remove(hotel);
            db.SaveChanges();

            return Ok(hotel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelExists(int id)
        {
            return db.Hoteles.Count(e => e.Id == id) > 0;
        }
    }
}