USE BDMultinivel;
GO
CREATE proc [dbo].[SP_CONFIRMAR_TRANSFERENCIAS_REZAGADOS_TODOS]
    @ComisionId    int,
    @CicloId    int,
    @EmpresaId  int,
    @UsuarioId  int
AS
BEGIN
    DECLARE @Resp   int;
    BEGIN TRY
        DECLARE @IMPBODY        VARCHAR (500);
        DECLARE @IMPSUBJECT     VARCHAR (500);
        DECLARE @EstadoConfirmado       int,
                @TipoPagoTransferencia  int,
                @CantidadActualizados  int,
                @EstadoRechazado      int;
                
        SET @EstadoConfirmado   = 2;
        SET @EstadoRechazado   = 3;
        SET @TipoPagoTransferencia = 2;
        SET @Resp = 0; 
        BEGIN TRANSACTION
            update COMISION_DETALLE_EMPRESA set estado = @EstadoConfirmado, fecha_actualizacion = GETDATE(), id_usuario = @UsuarioId
            where   id_empresa = @EmpresaId and estado = @EstadoRechazado and
                    id_comision_detalle_empresa in (select i.id_comision_detalle_empresa from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                                            where i.id_empresa = @EmpresaId and i.id_ciclo = @CicloId and i.id_comision = @ComisionId and i.id_tipo_pago = @TipoPagoTransferencia)

                                            select i.* from BDMultinivel.dbo.vwObtenerRezagadosPagos i
                                            where i.id_empresa = 3 and i.id_ciclo = 80 and i.id_tipo_pago = 2
        COMMIT TRANSACTION                                     
        select @CantidadActualizados = COUNT(*) from  BDMultinivel.dbo.vwObtenerRezagadosPagos i where i.id_empresa = @EmpresaId and i.id_ciclo = @CicloId and i.id_comision = @ComisionId and i.id_tipo_pago = @TipoPagoTransferencia and i.id_estado_comision_detalle_empresa = @EstadoRechazado
        if(@CantidadActualizados > 0)
        BEGIN
            return 1;
        END
        return 0;                        
    END TRY
    BEGIN CATCH   
        SET @Resp = 1;          
        IF @@TRANCOUNT > 0
            BEGIN
                SET @IMPBODY = concat ('SP_CONFIRMAR_TRANSFERENCIAS_TODOS ', ' ');
                SET @IMPSUBJECT = 'ALERTA PRODUCCION : No se pudo actualizar los estados de las comisiones en sion pay';
                --EXECUTE msdb.dbo.sp_send_dbmail @profile_name   = 'NotificacionSQL',
                --                                @recipients = 'desarrollo@gruposion.bo; UIT-SION@gruposion.bo',
                --                                @body           = @IMPBODY,
                --                                @subject        = @IMPSUBJECT;
                ROLLBACK TRANSACTION;        
                RETURN 1
            END
    END CATCH;    
END
GO
