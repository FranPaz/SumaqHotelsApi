using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SumaqHotelsApi.Models
{
    public class BlobUploadModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileSizeInBytes { get; set; }
        public long FileSizeInKb { get { return (long)Math.Ceiling((double)FileSizeInBytes / 1024); } }
    }

    public class BlobDownloadModel
    {
        public int Id { get; set; }
        public MemoryStream BlobStream { get; set; }
        public string BlobFileName { get; set; }
        public string BlobContentType { get; set; }
        public long BlobLength { get; set; }
    }

    public class ImagenHotel : BlobUploadModel
    {
        //fpaz: relacion 1 a m con hotel (uno)
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

        //fpaz: relacion 1 a m con Tipo de Imagen (uno)
        public int TipoImagenId { get; set; }
        public virtual TipoImagen TipoImagen { get; set; }

    }

    public class ImagenTipoHabitacion : BlobUploadModel
    {        
        //fpaz: relacion 1 a m con Tipo de Habitacion (uno)
        public int TipoHabitacionId { get; set; }
        public virtual TipoHabitacion TipoHabitacion { get; set; }

        //fpaz: relacion 1 a m con Tipo de Imagen (uno)
        public int TipoImagenId { get; set; }
        public virtual TipoImagen TipoImagen { get; set; }

    }

    public class TipoImagen
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        //fpaz: 1 a M con ImagenesHotel (muchos)
        public virtual ICollection<ImagenHotel> ImagenesHotel { get; set; }

        //fpaz: 1 a M con ImagenesTipoHabitacion (muchos)
        public virtual ICollection<ImagenTipoHabitacion> ImagenesTipoHabitacion { get; set; }
    }


}