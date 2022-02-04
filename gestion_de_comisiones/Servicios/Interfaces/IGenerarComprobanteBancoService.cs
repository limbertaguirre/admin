using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Modelos.GestionPagos;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IGenerarComprobanteBancoService
    {
        public Task<List<GenerarComprobanteEvent>> GenerarTodos(GenerarComprobanteInput body);
        public Task<List<GenerarComprobanteEvent>> GenerarParcial(GenerarComprobanteInput i);
        public Task<List<GenerarComprobanteEvent>> GenerarTodosRezagados(GenerarComprobanteInput i);
        public Task<List<GenerarComprobanteEvent>> GenerarParcialRezagados(GenerarComprobanteInput i, List<int> confirmados);
    }
}
