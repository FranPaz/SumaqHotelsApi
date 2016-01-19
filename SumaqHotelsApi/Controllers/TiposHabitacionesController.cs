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
                .Include(t => t.CamasAdicionales.Select(s => s.TipoCama))
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

            //db.Entry(tipoHabitacion).State = EntityState.Modified;

            try
            {
                var tipoHabOrig = (from th in db.TiposHabitaciones //obtengo los datos originales del tipo de habitacion que voy a modificar
                                   where th.Id == id
                                   select th)
                                      .Include(c => c.CamasAdicionales)
                                      .Include(s => s.ServiciosDeHabitacion)
                                      .FirstOrDefault();
              
                if (tipoHabOrig != null)
                {
                    #region update de camas adicionales
                    var camasAdicionalesOriginales = tipoHabOrig.CamasAdicionales;

                    // parte para carga de nuevas camas adicionales
                    List<CamaAdicional> camasAdicAgregadas = new List<CamaAdicional>();
                    foreach (var ca in tipoHabitacion.CamasAdicionales) // eliminacion de camas adicionales que ya no estan en el array
                    {
                        var camaAd = (from co in camasAdicionalesOriginales // verifico si la camaAdicional esta en el obj modificado
                                      where co.Id == ca.Id
                                      select co).FirstOrDefault();

                        if (camaAd == null) // si no encontro la cama adicional la agrego al array para su carga
                        {
                            var camaAdicionalAgregada = new CamaAdicional()
                            {
                                Cantidad = ca.Cantidad,
                                PrecioAdicional = ca.PrecioAdicional,
                                TipoCamaId = ca.TipoCama.Id
                            };

                            camasAdicAgregadas.Add(camaAdicionalAgregada);
                        }
                    }

                    
                    //parte para eliminacion de camas adicionales
                    List<CamaAdicional> camasAdicEliminadas = new List<CamaAdicional>();
                    foreach (var co in camasAdicionalesOriginales) // eliminacion de camas adicionales que ya no estan en el array
                    {
                        var camaAdOrig = (from ca in tipoHabitacion.CamasAdicionales // verifico si la camaAdicional esta en el obj modificado
                                      where ca.Id == co.Id
                                      select ca).FirstOrDefault();

                        if (camaAdOrig == null) // si no encontro la cama adicional la elimino del array
                        {
                            camasAdicEliminadas.Add(co);
                        }                        
                    }

                    //parte para actualizacion de datos basicos
                    foreach (var co in camasAdicionalesOriginales) // eliminacion de camas adicionales que ya no estan en el array
                    {
                        var camaAdMod = (from ca in tipoHabitacion.CamasAdicionales // verifico si la camaAdicional esta en el obj modificado
                                          where ca.Id == co.Id
                                          select ca).FirstOrDefault();

                        if (camaAdMod != null) // si no encontro la cama adicional modifico los datos
                        {
                            co.PrecioAdicional = camaAdMod.PrecioAdicional;
                            co.Cantidad = camaAdMod.Cantidad;
                        }
                    }



                    foreach (var item in camasAdicAgregadas)
                    {
                        tipoHabOrig.CamasAdicionales.Add(item);
                    }

                    foreach (var item in camasAdicEliminadas)
                    {
                        db.CamasAdicionales.Remove(item);
                    }

                    #endregion

                    #region update de servicios de habitacion
                    var serviciosOriginales = tipoHabOrig.ServiciosDeHabitacion;

                    // parte para carga de nuevos servicios de habitacion
                    List<ServicioDeHabitacion> serviciosAgregados = new List<ServicioDeHabitacion>();
                    foreach (var sa in tipoHabitacion.ServiciosDeHabitacion) // eliminacion de servicios que ya no estan en el array
                    {
                        var s = (from so in serviciosOriginales // verifico si el servicio esta en el obj modificado
                                      where so.Id == sa.Id
                                      select so).FirstOrDefault();

                        if (s == null) // si no encontro el servicio agrego al array para su carga
                        {
                            var serv = db.ServiciosDeHabitacion.Find(sa.Id);
                            if (serv != null)
                            {
                                serviciosAgregados.Add(serv);                                
                            }
                        }
                    }


                    //parte para eliminacion de servicios
                    List<ServicioDeHabitacion> serviciosEliminados = new List<ServicioDeHabitacion>();
                    foreach (var so in serviciosOriginales) // eliminacion de camas adicionales que ya no estan en el array
                    {
                        var servicioOrig = (from sa in tipoHabitacion.ServiciosDeHabitacion // verifico si la camaAdicional esta en el obj modificado
                                          where sa.Id == so.Id
                                          select sa).FirstOrDefault();

                        if (servicioOrig == null) // si no encontro la cama adicional la elimino del array
                        {
                            serviciosEliminados.Add(so);                            
                        }
                    }

                    foreach (var item in serviciosAgregados)
                    {
                        tipoHabOrig.ServiciosDeHabitacion.Add(item);
                    }

                    foreach (var item in serviciosEliminados)
                    {
                        tipoHabOrig.ServiciosDeHabitacion.Remove(item);
                    }
                    #endregion




                }                      

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                #region fpaz: armado de servicios de habitacion agregados en el alta de tipos de habitaciones
                List<ServicioDeHabitacion> servicios = new List<ServicioDeHabitacion>();
                foreach (var item in tipoHabitacion.ServiciosDeHabitacion)
                {
                    var serv = db.ServiciosDeHabitacion.Find(item.Id);
                    if (serv != null)
                    {
                        servicios.Add(serv);
                    }
                }

                tipoHabitacion.ServiciosDeHabitacion = servicios;
                #endregion

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