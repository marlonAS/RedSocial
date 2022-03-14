using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedSocial.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public byte[]Foto { get; set; }
        public string Telefono { get; set; }
    }
}