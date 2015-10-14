using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumaqHotelsApi.Models
{
    public class TipoHabitacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioBase { get; set; }
        public int PlazasBase { get; set; }

        //relacion muchos a muchos con ServicioDeHabitacion
        public virtual ICollection<ServicioDeHabitacion> ServiciosDeHabitacion { get; set; }

        //relacion 1 a M con CamaAdicional (muchos)
        public virtual ICollection<CamaAdicional> CamasAdicionales { get; set; } 

        //relacion 1 a M con Habitacion (muchos)
        public virtual ICollection<Habitacion> Habitaciones { get; set; }
        
        // 1 a M con Hotel (uno)
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }       
        
    }

    public class ServicioDeHabitacion // servicios de tipos de habitaciones
    {
        public ServicioDeHabitacion()
        {
            this.TiposHabitaciones = new HashSet<TipoHabitacion>();
        }         
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        //relacion m a m con TipoHabitacion (muchos)
        public virtual ICollection<TipoHabitacion> TiposHabitaciones { get; set; }
    }
}