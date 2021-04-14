using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.Models
{
    public partial class Persona
    {
        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string City { get; set; }
        public long? Edad { get; set; }
    }
}
