using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumaqHotelsApi.Models
{
    public class CamaAdicional //clase que va a tener el tipo de cama adicional para cada tipo de habitacion y el precio adicional que se cobrara
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioAdicional { get; set; }
        
        //relacion 1 a M con TipoCama (uno)
        public int TipoCamaId { get; set; }
        public virtual TipoCama TipoCama { get; set; }

        //relacion 1 a M con TipoHabitacion (uno)
        public int TipoHabitacionId { get; set; }
        public virtual TipoHabitacion TipoHabitacion { get; set; }
    }

    public class TipoCama
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        //relacion 1 a m con CamaAdicional (muchos)
        public virtual ICollection<CamaAdicional> CamasAdicionales { get; set; }
    }
}