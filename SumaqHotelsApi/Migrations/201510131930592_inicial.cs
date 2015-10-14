namespace SumaqHotelsApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CamaAdicionals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        PrecioAdicional = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoCamaId = c.Int(nullable: false),
                        TipoHabitacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoCamas", t => t.TipoCamaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoHabitacions", t => t.TipoHabitacionId, cascadeDelete: true)
                .Index(t => t.TipoCamaId)
                .Index(t => t.TipoHabitacionId);
            
            CreateTable(
                "dbo.TipoCamas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoHabitacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        PrecioBase = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlazasBase = c.Int(nullable: false),
                        HotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.Habitacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NroHab = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Plazas = c.Int(nullable: false),
                        TipoHabitacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoHabitacions", t => t.TipoHabitacionId, cascadeDelete: true)
                .Index(t => t.TipoHabitacionId);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        CantPisos = c.Int(nullable: false),
                        Telefono = c.String(),
                        CodPostal = c.String(),
                        UrlWeb = c.String(),
                        DireccionComun = c.String(),
                        CategoriaId = c.Int(nullable: false),
                        TipoHotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoHotels", t => t.TipoHotelId, cascadeDelete: true)
                .Index(t => t.CategoriaId)
                .Index(t => t.TipoHotelId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        HotelId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CantEstrellas = c.Int(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HotelDireccions",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.HotelId)
                .ForeignKey("dbo.Hotels", t => t.HotelId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.TipoHotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServicioDeHabitacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GrupoHoteleroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ServicioDeHabitacionTipoHabitacions",
                c => new
                    {
                        ServicioDeHabitacion_Id = c.Int(nullable: false),
                        TipoHabitacion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServicioDeHabitacion_Id, t.TipoHabitacion_Id })
                .ForeignKey("dbo.ServicioDeHabitacions", t => t.ServicioDeHabitacion_Id, cascadeDelete: true)
                .ForeignKey("dbo.TipoHabitacions", t => t.TipoHabitacion_Id, cascadeDelete: true)
                .Index(t => t.ServicioDeHabitacion_Id)
                .Index(t => t.TipoHabitacion_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ServicioDeHabitacionTipoHabitacions", "TipoHabitacion_Id", "dbo.TipoHabitacions");
            DropForeignKey("dbo.ServicioDeHabitacionTipoHabitacions", "ServicioDeHabitacion_Id", "dbo.ServicioDeHabitacions");
            DropForeignKey("dbo.TipoHabitacions", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Hotels", "TipoHotelId", "dbo.TipoHotels");
            DropForeignKey("dbo.HotelDireccions", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Hotels", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Habitacions", "TipoHabitacionId", "dbo.TipoHabitacions");
            DropForeignKey("dbo.CamaAdicionals", "TipoHabitacionId", "dbo.TipoHabitacions");
            DropForeignKey("dbo.CamaAdicionals", "TipoCamaId", "dbo.TipoCamas");
            DropIndex("dbo.ServicioDeHabitacionTipoHabitacions", new[] { "TipoHabitacion_Id" });
            DropIndex("dbo.ServicioDeHabitacionTipoHabitacions", new[] { "ServicioDeHabitacion_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.HotelDireccions", new[] { "HotelId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "HotelId" });
            DropIndex("dbo.Hotels", new[] { "TipoHotelId" });
            DropIndex("dbo.Hotels", new[] { "CategoriaId" });
            DropIndex("dbo.Habitacions", new[] { "TipoHabitacionId" });
            DropIndex("dbo.TipoHabitacions", new[] { "HotelId" });
            DropIndex("dbo.CamaAdicionals", new[] { "TipoHabitacionId" });
            DropIndex("dbo.CamaAdicionals", new[] { "TipoCamaId" });
            DropTable("dbo.ServicioDeHabitacionTipoHabitacions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.GrupoHoteleroes");
            DropTable("dbo.ServicioDeHabitacions");
            DropTable("dbo.TipoHotels");
            DropTable("dbo.HotelDireccions");
            DropTable("dbo.Categorias");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Hotels");
            DropTable("dbo.Habitacions");
            DropTable("dbo.TipoHabitacions");
            DropTable("dbo.TipoCamas");
            DropTable("dbo.CamaAdicionals");
        }
    }
}
