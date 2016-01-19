namespace SumaqHotelsApi.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SumaqHotelsApi.Infrastructure;
    using SumaqHotelsApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SumaqHotelsApi.Models.SumaqHotels_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SumaqHotelsApi.Models.SumaqHotels_Context context)
        {
            //fpaz:Semillas para el llenado inicial de la bd
            #region Carga de ApplicationUser
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SumaqHotels_Context()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SumaqHotels_Context()));

            var user = new ApplicationUser()
            {
                UserName = "Administrador",
                Email = "overcode_dev@outlook.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3),
                Hotel = new Hotel
                {
                    Nombre = "Hotel de Prueba 1",
                    Descripcion = "Hotel de Prueba Descripcion",
                    CantPisos = 3,
                    Categoria = new Categoria { CantEstrellas = 1, Descripcion = "Hoteles 1 Estrella" },
                    TipoHotel = new TipoHotel { Nombre = "Urbano", Descripcion = "descripcion temporal" }
                },
            };

            manager.Create(user, "qwerty123");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("Administrador");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });
            #endregion

            #region Semilla de Categorias de Hoteles
            var listCategorias = new List<Categoria>
                        {
                            new Categoria { CantEstrellas = 1, Descripcion="Hoteles 1 Estrella"},                     
                            new Categoria { CantEstrellas = 2, Descripcion="Hoteles 2 Estrellas"},
                            new Categoria { CantEstrellas = 3, Descripcion="Hoteles 3 Estrellas"},
                            new Categoria { CantEstrellas = 4, Descripcion="Hoteles 4 Estrellas"},
                            new Categoria { CantEstrellas = 5, Descripcion="Hoteles 5 Estrellas"}
                        };
            foreach (var item in listCategorias)
            {
                context.Categorias.Add(item);
            }
            #endregion

            #region Semilla de los Tipos de Hoteles
            var tiposHoteles = new List<TipoHotel>
                        {
                            new TipoHotel { Nombre="Posada", Descripcion="descripcion temporal"},
                            new TipoHotel { Nombre="Hotel Boutique", Descripcion="descripcion temporal"},
                            new TipoHotel { Nombre="Apart Hotel", Descripcion="descripcion temporal"},                            
                            new TipoHotel { Nombre="Hostel", Descripcion="descripcion temporal"},
                            new TipoHotel { Nombre="Hotel", Descripcion="descripcion temporal"}
                        };
            foreach (var item in tiposHoteles)
            {
                context.TipoHoteles.Add(item);
            }
            #endregion

            #region Semilla de los Servicios de Habitacion por defecto
            var listServicios = new List<ServicioDeHabitacion>
                        {
                            new ServicioDeHabitacion { Nombre="Baño Privado",Descripcion="descripcion temporal"},                    
                            new ServicioDeHabitacion { Nombre="Aire Acondicionado",Descripcion="descripcion temporal"},                    
                            new ServicioDeHabitacion { Nombre="Tv por Cable",Descripcion="descripcion temporal"},                    
                            new ServicioDeHabitacion { Nombre="Wifi",Descripcion="descripcion temporal"},                    
                            new ServicioDeHabitacion { Nombre="Frigobar",Descripcion="descripcion temporal"},                    
                        };
            foreach (var item in listServicios)
            {
                context.ServiciosDeHabitacion.Add(item);
            }
            #endregion

            #region Semilla de los Tipos de Camas
            var tiposCamas = new List<TipoCama>
                        {
                            new TipoCama { Nombre="1 Plaza", Descripcion="Cama de 1 Plaza"},
                            new TipoCama { Nombre="2 Plazas", Descripcion="Cama de 2 Plazas"},
                            new TipoCama { Nombre="2 Plazas Queen Size", Descripcion="Cama de 2 Plazas Queen Size"},
                            new TipoCama { Nombre="2 Plazas King Size", Descripcion="Cama de 2 Plazas King Size"}
                        };
            foreach (var item in tiposCamas)
            {
                context.TiposCamas.Add(item);
            }
            #endregion            

            #region Semilla de Tipos de Imagenes
            var tiposImagenes = new List<TipoImagen>
                        {                            
                            new TipoImagen {Descripcion="Logo del Hotel"},
                            new TipoImagen {Descripcion="Imagenes del Hotel"},
                            new TipoImagen {Descripcion="Imagenes de Habitaciones"},
                        };
            foreach (var item in tiposImagenes)
            {
                context.TiposImagenes.Add(item);
            }
            #endregion

            base.Seed(context);
        }
    }
}
