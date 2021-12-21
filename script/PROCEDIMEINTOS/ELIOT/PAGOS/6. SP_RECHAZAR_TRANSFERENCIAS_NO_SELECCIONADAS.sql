USE BDMultinivel;
GO
CREATE proc [dbo].[SP_RECHAZAR_TRANSFERENCIAS_NO_SELECCIONADAS]
    @CicloId    int,
    @EmpresaId  int,
    @UsuarioId  int,
    @ComisionDetalleEmpresaId  int
AS
BEGIN
    BEGIN TRY
        DECLARE @IMPBODY        VARCHAR (500);
        DECLARE @IMPSUBJECT     VARCHAR (500);
        DECLARE @EstadoPendiente        int,
                @EstadoRechazado       int,
                @TipoPagoTransferencia  int;                
        SET @EstadoPendiente    = 1;
        SET @EstadoRechazado   = 3;
        SET @TipoPagoTransferencia = 2;
        BEGIN TRANSACTION
            update COMISION_DETALLE_EMPRESA set estado = @EstadoRechazado, fecha_actualizacion = GETDATE(), id_usuario = @UsuarioId
            where id_comision_detalle_empresa = @ComisionDetalleEmpresaId
        COMMIT TRANSACTION
        RETURN 0;
    END TRY
    BEGIN CATCH   
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
