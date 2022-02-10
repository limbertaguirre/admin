using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class LogPagoComisionTransferenciaPorEmpresa
    {
        public int Id { get; set; }
        public int CicloId { get; set; }
        public int ComisionId { get; set; }
        public int ComisionDetalleId { get; set; }
        public int ComisionDetalleEmpresaId { get; set; }
        public int EmpresaId { get; set; }
        public int EmpresaIdCnx { get; set; }
        public int FichaId { get; set; }
        public string Ci { get; set; }
        public string NombreCompleto { get; set; }
        public string NroCuentaBanco { get; set; }
        public string Banco { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public string NombreSp { get; set; }
        public int? CodigoRespSp { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
