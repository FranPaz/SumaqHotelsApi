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
    public class TiposHabitacionesController : ApiController
    {
        private SumaqHotels_Context db = new SumaqHotels_Context();

 // GET: api/TiposHabitaciones
        public IHttpActionResult GetTipoHabitacions(int prmIdHotel)
        {
            try
            {
                var listaTiposHabitaciones = (from th in db.TiposHabitaciones
                                                 where th.HotelId == prmIdHotel
                                                 select th)
                .Include(t => t.CamasAdicionales)
                .Include(t => t.ServiciosDeHabitacion)
                .Include(h => h.Habitaciones)
                .ToList();

                if (listaTiposHabitaciones == null)
                {
                    return BadRequest("No Existen Tipos de Habitaciones");
                }
                return Ok(listaTiposHabitaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }            
        }

        // GET: api/TiposHabitaciones/5
        [ResponseType(typeof(TipoHabitacion))]
        public IHttpActionResult GetTipoHabitacion(int prmIdHotel, int prmIdHabitacion)
        {
            TipoHabitacion tipoHabitacion =(from th in db.TiposHabitaciones
                                                where th.HotelId == prmIdHotel
                                                && th.Id == prmIdHabitacion
                                                select th)
                                                .FirstOrDefault();
            if (tipoHabitacion == null)
            {
                return NotFound();
            }

            return Ok(tipoHabitacion);
        }

        // PUT: api/TiposHabitaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoHabitacion(int id, TipoHabitacion tipoHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoHabitacion.Id)
            {
                return BadRequest();
            }

            db.Entry(tipoHabitacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoHabitacionExists(id))
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

       // POST: api/TiposHabitaciones
        [ResponseType(typeof(TipoHabitacion))]
        public IHttpActionResult PostTipoHabitacion(TipoHabitacion tipoHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.TiposHabitaciones.Add(tipoHabitacion);
                db.SaveChanges();


                return Ok("Tipo de Habitacion Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            
        }

        // DELETE: api/TiposHabitaciones/5
        [ResponseType(typeof(TipoHabitacion))]
        public IHttpActionResult DeleteTipoHabitacion(int id)
        {
            TipoHabitacion tipoHabitacion = db.TiposHabitaciones.Find(id);
            if (tipoHabitacion == null)
            {
                return NotFound();
            }

            db.TiposHabitaciones.Remove(tipoHabitacion);
            db.SaveChanges();

            return Ok(tipoHabitacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoHabitacionExists(int id)
        {
            return db.TiposHabitaciones.Count(e => e.Id == id) > 0;
        }
    }
}