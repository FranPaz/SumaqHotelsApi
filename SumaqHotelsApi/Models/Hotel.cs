using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SumaqHotelsApi.Infrastructure;

namespace SumaqHotelsApi.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CantPisos { get; set; }        
        public string Telefono { get; set; }        
        public string CodPostal { get; set; }
        public string UrlWeb { get; set; }
        public string DireccionComun { get; set; }
        public virtual HotelDireccion HotelDireccion { get; set; } // 1 a 1 con HotelDireccion

        // 1 a m con Categoria (uno)
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        //1 a M con TipoHotel (uno)
        public int TipoHotelId { get; set; }
        public virtual TipoHotel TipoHotel { get; set; }       

        // 1 a M con TipoHabitacion (muchos)
        public virtual ICollection<TipoHabitacion> TiposHabitaciones { get; set; }

        // 1 a M con Application User
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        // 1 a M con GrupoHotelero (uno) -- PARA FUTURAS MEJORAS
        //public int GrupoHoteleroId { get; set; }
        //public virtual GrupoHotelero GrupoHotelero { get; set; }
		
    }

    public class HotelDireccion
    {
        [Key, ForeignKey("Hotel")] // 1 a 1 con Hotel
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual Hotel Hotel { get; set; } // 1 a 1 con Hotel
    }

    public class Categoria
    {
        public int Id { get; set; }
        public int CantEstrellas { get; set; }
        public string Descripcion { get; set; }

        // 1 a m con Hotel (muchos)
        public virtual ICollection<Hotel> Hoteles { get; set; }
    }

    public class TipoHotel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        // 1 a m con Hotel (muchos)
        public virtual ICollection<Hotel> Hoteles { get; set; }
    }

    public class GrupoHotelero
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // 1 a m con Hotel (muchos) -- PARA FUTURAS MEJORAS
        //public virtual ICollection<Hotel> Hoteles { get; set; }
    }
}