USE BDMultinivel;
GO
CREATE PROC [dbo].[SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS_REZAGADOS] @ComisionId INT, @EstadoComisionId INT, @CicloId INT, @EmpresaId INT, @UsuarioId INT, @FechaPago VARCHAR(50)
    AS
    BEGIN
        DECLARE @IdTipoPagoTransferencia                 int,
                @IdEstadoComisionDetalleEmpresaPendienteRezagado int,
                @IMPBODY VARCHAR(500),
                @IMPSUBJECT VARCHAR(500);
        set @IdTipoPagoTransferencia = 2;
        set @IdEstadoComisionDetalleEmpresaPendienteRezagado = 1;
        BEGIN TRY
            BEGIN TRANSACTION
                UPDATE COMISION_DETALLE_EMPRESA SET fecha_pago = CAST(@FechaPago AS datetime), id_usuario = @UsuarioId
                    where estado = @IdEstadoComisionDetalleEmpresaPendienteRezagado and
                        id_comision_detalle_empresa in (select i.id_comision_detalle_empresa from dbo.vwObtenerRezagadosPagos i
                                                            where i.id_comision = @ComisionId and i.id_ciclo = @CicloId and i.id_empresa = @EmpresaId and
                                                            i.id_tipo_pago = @IdTipoPagoTransferencia and i.id_estado_comision = @EstadoComisionId and
                                                            i.id_estado_comision_detalle_empresa = @IdEstadoComisionDetalleEmpresaPendienteRezagado)
                                                            select * from BDMultinivel.dbo.vwObtenerRezagadosPagos
                COMMIT TRANSACTION
                return 0;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0
                BEGIN
                    SET @IMPBODY =
                            concat ('SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS ',
                                    ' ');
                    SET @IMPSUBJECT = 'ALERTA PRODUCCION : no se pudo actualizar la fecha de pago en las comisiones detalle empresa por transferencias.';
                    --EXECUTE msdb.dbo.sp_send_dbmail @profile_name   = 'NotificacionSQL',
                    --                                @recipients = 'desarrollo@gruposion.bo; UIT-SION@gruposion.bo',
                    --                                @body           = @IMPBODY,
                    --                                @subject        = @IMPSUBJECT;
                    ROLLBACK TRANSACTION;        
                    return 1
                END
        END CATCH
    END

GO
