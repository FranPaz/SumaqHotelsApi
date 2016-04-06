using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using SumaqHotelsApi.Infrastructure;

namespace SumaqHotelsApi.Models
{
    public class SumaqHotels_Context : IdentityDbContext<ApplicationUser>
    {
    
        public SumaqHotels_Context() : base("name=SumaqHotels_Context", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false; //desactivo el lazy loading para llamar explicitamente los objetos asociados a propiedades de navegacion
            this.Configuration.ProxyCreationEnabled = false;     
        }

        #region definicion de tablas dbset
        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.Hotel> Hoteles { get; set; }

        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.GrupoHotelero> GruposHoteleros { get; set; }

        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.HotelDireccion> HotelesDireccion { get; set; }

        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.TipoHotel> TipoHoteles { get; set; }

        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.Habitacion> Habitaciones { get; set; }

        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.TipoHabitacion> TiposHabitaciones { get; set; }
        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.ServicioDeHabitacion> ServiciosDeHabitacion { get; set; }
        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.CamaAdicional> CamasAdicionales { get; set; }
        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.TipoCama> TiposCamas { get; set; }

        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.ImagenHotel> ImagenesHotel { get; set; }
        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.TipoImagen> TiposImagenes { get; set; }
        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.ImagenTipoHabitacion> ImagenesTipoHabitacion { get; set; }
        public System.Data.Entity.DbSet<SumaqHotelsApi.Models.Pasajero> Pasajeroes { get; set; }
        
        #endregion
        

        public static SumaqHotels_Context Create() {
            return new SumaqHotels_Context();
        }

     
    }
}
