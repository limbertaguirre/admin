USE BDMultinivel;
GO
  CREATE PROC [dbo].[SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_REZAGADOS_CONFIRMADAS](@comisionId int, @cicloId int, @estadoComisionRezagadoId INT, @usuarioId int)
    AS
    BEGIN TRANSACTION
    BEGIN TRY
    -- VARIABLES PARA EMAIL
        DECLARE @IMPBODY   VARCHAR (500);
        DECLARE @IMPSUBJECT   VARCHAR (500);

        DECLARE @cantidadConfirmados int = 0 
        DECLARE @estadoListadoFormaPagoExitoso int = 3
        DECLARE @estadoListadoFormaPagoRechazado int = 4
        DECLARE @tipoPagoTransferencia int = 2
        DECLARE @estadoComisionDetalleEmpresaPendiente int = 1
        DECLARE @estadoComisionDetalleEmpresaConfirmado int = 2
        DECLARE @tipoComisionRezagado int = 2
        DECLARE @idEstadoComisionRezagado int = @estadoComisionRezagadoId;
        DECLARE @habilitado int = 1;
        DECLARE @deshabilitado int = 0;

        SET @cantidadConfirmados = (select COUNT(i.id_lista_formas_pago) from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                                where i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_estado_comision_detalle_empresa = @estadoComisionDetalleEmpresaPendiente and i.id_comision = @comisionId
                                and i.id_empresa in (select i.id_empresa from BDMultinivel.dbo.VwObtenerEmpresasComisionesDetalleEmpresa i
                                                    where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_tipo_comision = @tipoComisionRezagado and i.id_estado_comision = @idEstadoComisionRezagado and i.monto_transferir <> 0))
        IF (@cantidadConfirmados <= 0)
        BEGIN                            
            -- No hay pendientes de confirmacion
            -- Verificando si esta o no en detalle listado forma pago        
            DECLARE @cantidad int = (select COUNT(i.id_lista_formas_pago) from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                                where i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_estado_comision_detalle_empresa = @estadoComisionDetalleEmpresaConfirmado and i.id_comision = @comisionId 
                                and i.id_empresa in (select i.id_empresa from BDMultinivel.dbo.VwObtenerEmpresasComisionesDetalleEmpresa i
                                                    where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_tipo_comision = @tipoComisionRezagado and i.id_estado_comision = @idEstadoComisionRezagado and i.monto_transferir <> 0)
                                and i.id_lista_formas_pago in (select i.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL i where i.habilitado = @habilitado and i.id_estado_listado_forma_pago = @estadoListadoFormaPagoExitoso))
                                                               
            IF(@cantidad > 0)
            BEGIN
                -- Esta en detalle listado forma pago con estado 3 de pago exitoso.
                COMMIT TRANSACTION
                RETURN 3               
            END
            ELSE
            BEGIN
                -- Deshabilitamos los estados rechazados activos.
                UPDATE BDMultinivel.DBO.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL SET habilitado = @deshabilitado WHERE id in (
                    select i.id_detalle_estado_listado_forma_pago from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                    where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_estado_comision_detalle_empresa = @estadoComisionDetalleEmpresaConfirmado
                    and i.id_empresa in (select i.id_empresa from BDMultinivel.dbo.VwObtenerEmpresasComisionesDetalleEmpresa i
                                        where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_tipo_comision = @tipoComisionRezagado and i.id_estado_comision = @idEstadoComisionRezagado and i.monto_transferir <> 0)
                    --and i.id_lista_formas_pago in (select i.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL i where i.habilitado = @habilitado and i.id_estado_listado_forma_pago = @estadoListadoFormaPagoRechazado)
                    and i.estado_listado_forma_pago_habilitado = @habilitado and i.id_estado_listado_forma_pago = @estadoListadoFormaPagoRechazado
                    group by i.id_detalle_estado_listado_forma_pago)
                /*
                    Se cambió la lógica para rechazados de transferencia en rezagados, al rechazar una transferencia, no se inserta un nuevo registro en la tabla GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL
                    Entonces debemos insertar a la tabla GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL con un estado de pago exitoso (3).
                */
                INSERT INTO GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL (habilitado, id_lista_formas_pago, id_estado_listado_forma_pago, id_usuario) select 1, i.id_lista_formas_pago, @estadoListadoFormaPagoExitoso, @usuarioId from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                                where i.id_comision = @comisionId and i.id_ciclo = @cicloId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_estado_comision_detalle_empresa = @estadoComisionDetalleEmpresaConfirmado
                                and i.id_empresa in (select i.id_empresa from BDMultinivel.dbo.VwObtenerEmpresasComisionesDetalleEmpresa i where i.id_comision = @comisionId and i.id_tipo_pago = @tipoPagoTransferencia and i.id_tipo_comision = @tipoComisionRezagado and i.id_estado_comision = @idEstadoComisionRezagado and i.monto_transferir <> 0)
                                group by i.id_lista_formas_pago
                COMMIT TRANSACTION
                RETURN 1
            END    
        END
        ELSE
        BEGIN
            -- Aun hay pendientes de confirmacion
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
