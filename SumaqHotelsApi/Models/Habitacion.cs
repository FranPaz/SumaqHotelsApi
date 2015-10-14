using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumaqHotelsApi.Models
{
    public class Habitacion
    {
        public int Id { get; set; }
        public int NroHab { get; set; }
        public string Descripcion { get; set; }        
        public int Plazas { get; set; } //numero maximo de plazas para la habitacion

        //relacion 1 a m con TipoHab (uno)
        public int TipoHabitacionId { get; set; }
        public virtual TipoHabitacion TipoHabitacion { get; set; }       
        
    }

}