using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class LogComprobanteBanco
    {
        public int ErrorId { get; set; }
        public string UserName { get; set; }
        public int? ErrorNumber { get; set; }
        public int? ErrorState { get; set; }
        public int? ErrorSeverity { get; set; }
        public int? ErrorLine { get; set; }
        public string ErrorProcedure { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? ErrorDateTime { get; set; }
        public string DocId { get; set; }
        public string NameFreelance { get; set; }
        public string Glosa { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DateRegisterTransaction { get; set; }
        public string UserConexion { get; set; }
    }
}
