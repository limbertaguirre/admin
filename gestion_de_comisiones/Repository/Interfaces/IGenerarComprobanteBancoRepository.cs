using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Modelos.GestionPagos;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IGenerarComprobanteBancoRepository
    {
        public Task<List<GenerarComprobanteEvent>> GenerarTodos(GenerarComprobanteInput body);
        public Task<List<GenerarComprobanteEvent>> GenerarParcial(GenerarComprobanteInput body);
        public Task<List<GenerarComprobanteEvent>> GenerarTodosRezagados(GenerarComprobanteInput body);
        public Task<List<GenerarComprobanteEvent>> GenerarParcialRezagados(GenerarComprobanteInput body, List<int> confirmados);
    }
}
