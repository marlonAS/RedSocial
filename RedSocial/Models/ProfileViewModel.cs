namespace RedSocial.Models
{
    public class ProfileViewModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public byte[] Foto { get; set; }
        public string Telefono { get; set; }

        public ProfileViewModel(string nombre, string apellido, byte[] foto, string telefono)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Foto = foto;
            this.Telefono = telefono;
        }
        public ProfileViewModel()
        {

        }
    }
  

}