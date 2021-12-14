USE BDMultinivel;
GO
CREATE PROC [dbo].[SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS] @CicloId INT, @EmpresaId INT, @UsuarioId INT, @FechaPago VARCHAR(50)
AS
BEGIN
    DECLARE @IdTipoPagoTransferencia                 int,
            @IdEstadoComisionDetalleEmpresaPendiente int,
            @IMPBODY VARCHAR(500),
            @IMPSUBJECT VARCHAR(500);
    set @IdTipoPagoTransferencia = 2;
    set @IdEstadoComisionDetalleEmpresaPendiente = 1;
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE COMISION_DETALLE_EMPRESA SET fecha_pago = CAST(@FechaPago AS datetime), id_usuario = @UsuarioId
                where estado = @IdEstadoComisionDetalleEmpresaPendiente and
                    id_comision_detalle_empresa in (select i.id_comision_detalle_empresa from dbo.vwObtenerInfoExcelFormatoBanco i
                                                        where i.id_ciclo = @CicloId and i.id_empresa = @EmpresaId and
                                                        i.id_tipo_pago = @IdTipoPagoTransferencia and
                                                        i.id_estado_comision_detalle_empresa = @IdEstadoComisionDetalleEmpresaPendiente)
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
