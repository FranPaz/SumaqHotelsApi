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
    public class ServiciosDeHabitacionesController : ApiController
    {
        private SumaqHotels_Context db = new SumaqHotels_Context();

        // GET: api/ServiciosDeHabitaciones
        public IHttpActionResult GetServiciosDeHabitacion()
        {
            try
            {
                var listServicios = db.ServiciosDeHabitacion.ToList();
                if (listServicios == null)
                {
                    return BadRequest("No Existen Servicios Cargados");
                }
                return Ok(listServicios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            

            
        }

        // GET: api/ServiciosDeHabitaciones/5
        [ResponseType(typeof(ServicioDeHabitacion))]
        public IHttpActionResult GetServicioDeHabitacion(int id)
        {
            ServicioDeHabitacion servicioDeHabitacion = db.ServiciosDeHabitacion.Find(id);
            if (servicioDeHabitacion == null)
            {
                return NotFound();
            }

            return Ok(servicioDeHabitacion);
        }

        // PUT: api/ServiciosDeHabitaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServicioDeHabitacion(int id, ServicioDeHabitacion servicioDeHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != servicioDeHabitacion.Id)
            {
                return BadRequest();
            }

            db.Entry(servicioDeHabitacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioDeHabitacionExists(id))
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

        // POST: api/ServiciosDeHabitaciones
        [ResponseType(typeof(ServicioDeHabitacion))]
        public IHttpActionResult PostServicioDeHabitacion(ServicioDeHabitacion servicioDeHabitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiciosDeHabitacion.Add(servicioDeHabitacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = servicioDeHabitacion.Id }, servicioDeHabitacion);
        }

        // DELETE: api/ServiciosDeHabitaciones/5
        [ResponseType(typeof(ServicioDeHabitacion))]
        public IHttpActionResult DeleteServicioDeHabitacion(int id)
        {
            ServicioDeHabitacion servicioDeHabitacion = db.ServiciosDeHabitacion.Find(id);
            if (servicioDeHabitacion == null)
            {
                return NotFound();
            }

            db.ServiciosDeHabitacion.Remove(servicioDeHabitacion);
            db.SaveChanges();

            return Ok(servicioDeHabitacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServicioDeHabitacionExists(int id)
        {
            return db.ServiciosDeHabitacion.Count(e => e.Id == id) > 0;
        }
    }
}