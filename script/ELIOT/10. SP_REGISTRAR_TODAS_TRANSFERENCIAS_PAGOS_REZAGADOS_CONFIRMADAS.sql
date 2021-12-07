USE BDMultinivel;
GO
CREATE PROC [dbo].[SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_REZAGADOS_CONFIRMADAS](@comisionId int, @cicloId int, @usuarioId int)
AS
BEGIN TRANSACTION
BEGIN TRY
-- VARIABLES PARA EMAIL
    DECLARE @IMPBODY   VARCHAR (500);
    DECLARE @IMPSUBJECT   VARCHAR (500);

    DECLARE @cantidadRezagados int = 0 
    DECLARE @estadoListadoFormaPagoExitoso int = 3
    DECLARE @tipoPagoTransferencia int = 2
    DECLARE @estadoComisionDetalleEmpresaRechazado int = 3
    DECLARE @estadoComisionDetalleEmpresaConfirmado int = 2
    DECLARE @tipoComisionPagoRezagados int = 2
    SET @cantidadRezagados = (select COUNT(i.id_lista_formas_pago) from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                            where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_estado_comision_detalle_empresa = @estadoComisionDetalleEmpresaRechazado 
                            and i.id_empresa in (select i.id_empresa from BDMultinivel.dbo.VwObtenerEmpresasComisionesDetalleEmpresa i
                                                where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_tipo_comision = @tipoComisionPagoRezagados and i.monto_transferir <> 0))

                            select * from BDMultinivel.dbo.GP_COMISION c 
                            inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I cec on cec.id_comision = c.id_comision
                            where cec.id_estado_comision = 11 and c.id_tipo_comision = 2 and cec.habilitado = 1

                            select COUNT(i.id_lista_formas_pago) from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                            where i.id_comision = 120 and i.id_ciclo = 83 and i.id_tipo_pago = 2 and i.id_estado_comision_detalle_empresa = 3
                            and i.id_empresa in (select i.id_empresa from BDMultinivel.dbo.VwObtenerEmpresasComisionesDetalleEmpresa i
                                                where i.id_ciclo = 83 and i.id_tipo_pago = 2 and i.id_tipo_comision = 2 and i.monto_transferir <> 0 and i.id_comision = 120)
    IF (@cantidadRezagados <= 0)        
    BEGIN                            
        -- No hay comisiones_detalles en estado de rechazado en todas las empresas.
        -- Verificando si esta o no en detalle listado forma pago
        DECLARE @cantidad int = (select COUNT(i.id_lista_formas_pago) from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                            where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_estado_comision_detalle_empresa = @estadoComisionDetalleEmpresaConfirmado
                            and i.id_empresa in (select i.id_empresa from BDMultinivel.dbo.VwObtenerEmpresasComisionesDetalleEmpresa i
                                                where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_tipo_comision = @tipoComisionPagoRezagados and i.monto_transferir <> 0)
                            and i.id_lista_formas_pago in (select i.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL i where i.habilitado = 1 and i.id_estado_listado_forma_pago = @estadoListadoFormaPagoExitoso))
                                                       
        IF(@cantidad <= 0)
        BEGIN
            -- No esta en detalle listado forma pago por tanto registramos en detalle
            INSERT INTO GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL (habilitado, id_lista_formas_pago, id_estado_listado_forma_pago, id_usuario) select 1, i.id_lista_formas_pago, @estadoListadoFormaPagoExitoso, @usuarioId from BDMultinivel.dbo.vwObtenerInfoExcelFormatoBanco i
                            where i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_estado_comision_detalle_empresa = @estadoComisionDetalleEmpresaConfirmado
                            and i.id_empresa in (select i.id_empresa from BDMultinivel.dbo.VwObtenerEmpresasComisionesDetalleEmpresa i where i.id_tipo_pago = @tipoPagoTransferencia and i.id_tipo_comision = @tipoComisionPagoRezagados and i.monto_transferir <> 0)
                            group by i.id_lista_formas_pago

            COMMIT TRANSACTION
            RETURN 1                  
        END
        ELSE
        BEGIN
            --  Esta en detalle listado forma pago por tanto no registramos en detalle
            COMMIT TRANSACTION
            RETURN 3
        END    
    END
    ELSE
    BEGIN
        -- Aun hay comisiones_detalles en estado de rechazado en algunas empresas.
        COMMIT TRANSACTION
        RETURN 2
    END

END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER () AS ErrorNumber,
          ERROR_SEVERITY () AS ErrorSeverity,
          ERROR_STATE () AS ErrorState,
          ERROR_PROCEDURE () AS ErrorProcedure,
          ERROR_LINE () AS ErrorLine,
          ERROR_MESSAGE () AS ErrorMessage;
    declare @error int, @message varchar(4000)
    select @error = ERROR_NUMBER(), @message = ERROR_MESSAGE()
   IF @@TRANCOUNT > 0
      BEGIN
         SET @IMPBODY =
                concat ('SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_CONFIRMADAS ',
                        ' ');
         SET @IMPSUBJECT = 'ALERTA PRODUCCION : no se pudo cargar las comisiones ';
         --EXECUTE msdb.dbo.sp_send_dbmail @profile_name   = 'NotificacionSQL',
         --                                @recipients = 'desarrollo@gruposion.bo; UIT-SION@gruposion.bo',
         --                                @body           = @IMPBODY,
         --                                @subject        = @IMPSUBJECT;
         ROLLBACK TRANSACTION
         raiserror ('SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_CONFIRMADAS: %d: %s', 16, 1, @error, @message) ;       
		 return -1
      END
END CATCH;

GO
