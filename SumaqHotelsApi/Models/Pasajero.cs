using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumaqHotelsApi.Models
{
    public class Pasajero
    {
        public int Id { get; set; }
        public string TipoDoc { get; set; }
        public int NumDoc { get; set; }
        public string NomApe { get; set; }
        public string Sexo { get; set; }
        public string ECivil { get; set; }
        public string Dir { get; set; }
        public string CodPostal { get; set; }
        public string Tel { get; set; }
        public string Cel { get; set; }
        public string EMail { get; set; }

    }
}