﻿using AutoMapper.Configuration;
using gestion_de_comisiones.Modelos.Incentivo;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class IncentivoSionPayService : IIncentivoSionPayService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<IncentivoSionPayService> Logger;

        public IncentivoSionPayService(ILogger<IncentivoSionPayService> logger, IIncentivoSionPayRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }
        public IIncentivoSionPayRepository Repository { get; set; }
        public object CargarDatosPlanillaExcel(PlanillaPagoIncentivo param)
        {
            try
            {
                List<DatosPlanillaExcel> lista = Repository.verificarIncentivosEmpresaCiNoRepetidos(param);
                if(lista != null && lista.Count > 0)
                {
                    Logger.LogInformation($"usuario : {param.UsuarioNombre} inicio el servicio IncentivoService => CargarDatosPlanillaExcel() verificando lista");
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "Lista Observada", lista);
                }
                else
                {
                    Logger.LogInformation($"usuario : {param.UsuarioNombre} inicio el servicio IncentivoService => CargarDatosPlanillaExcel()");
                    var planillaIncentivo = Repository.GuardarPlanillaIncentivoSionPay(param);
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "Se cargo la planilla correctamente", planillaIncentivo);
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.UsuarioNombre} error catch CargarDatosPlanillaExcel() al cargar planilla excel ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "Problemas al cargar la planilla", "problemas en el servidor, intente mas tarde");
            }
        }
        
        public object VerificarCuentaSionPay(string cuenta)
        {
            throw new NotImplementedException();
        }

        public object ObtenerCiclos(string usuario)
        {
            try
            {
                var ciclos = Repository.ObtenerCiclos(usuario);
                return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "Se obtuvo los ciclos", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch ObtenerCiclos(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "Problemas al obtener ciclos", "problemas en el servidor, intente mas tarde");
            }            
        }

        public object ObtenerTipoIncentivo(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} Inicio ObtenerTipoIncentivo()");
                var ciclos = Repository.ObtenerTipoIncentivo(usuario);
                return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "Se obtuvo los ciclos", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch ObtenerTipoIncentivo(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "Problemas al obtener tipo de incentivo", "problemas en el servidor, intente mas tarde");
            }
        }
    }
}