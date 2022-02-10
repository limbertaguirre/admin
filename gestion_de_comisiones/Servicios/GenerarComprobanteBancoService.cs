using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Servicios
{
    public class GenerarComprobanteBancoService : IGenerarComprobanteBancoService
    {        
        private readonly ILogger<GenerarComprobanteBancoService> Logger;
        private readonly IGenerarComprobanteBancoRepository Repository;
        public GenerarComprobanteBancoService(ILogger<GenerarComprobanteBancoService> logger, IGenerarComprobanteBancoRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }

        public async Task<List<GenerarComprobanteEvent>> GenerarParcial(GenerarComprobanteInput i)
        {
            Logger.LogWarning($"Inicio GenerarComprobanteBancoService - GenerarParcial()");
            return await Repository.GenerarParcial(i);
        }

        public async Task<List<GenerarComprobanteEvent>> GenerarParcialRezagados(GenerarComprobanteInput i, List<int> confirmados)
        {
            Logger.LogWarning($"Inicio GenerarComprobanteBancoService - GenerarParcialRezagados()");
            return await Repository.GenerarParcialRezagados(i, confirmados);
        }

        public async Task<List<GenerarComprobanteEvent>> GenerarTodos(GenerarComprobanteInput body)
        {
            Logger.LogWarning($"Inicio GenerarComprobanteBancoService - GenerarTodos()");
            return await Repository.GenerarTodos(body);
        }

        public async Task<List<GenerarComprobanteEvent>> GenerarTodosRezagados(GenerarComprobanteInput i)
        {
            Logger.LogWarning($"Inicio GenerarComprobanteBancoService - GenerarTodosRezagados()");
            return await Repository.GenerarTodosRezagados(i);
        }
    }
}
