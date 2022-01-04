USE BDMultinivel;
GO
CREATE PROC [dbo].[SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS](@estadoComision INT, @cicloId INT, @empresaId INT, @usuarioId INT, @tipoPago INT)
AS
BEGIN TRANSACTION
BEGIN TRY
    -- VARIABLES PARA EMAIL
    DECLARE @IMPBODY   VARCHAR (500);
    DECLARE @IMPSUBJECT   VARCHAR (500);
    
    /*DECLARE @empresaId int;
    DECLARE @cicloId int;
    DECLARE @usuarioId int;*/

    -- VARIABLES PROPIAS DEL SP
    DECLARE @nuevoIdComisionDetalleRezagado int,        
            @estadoComisionDetalleEmpresaRechazado int,
            @tipoComisionPago int,
            @tipoComisionRezagado int,            
            @GP_EstadoComisionCerradoFormaPago int,
            @GP_EstadoComisionRezagadoFormasPagos int, @habilitado int;            

    -- PARAMETROS DE ENTRADA
    /*set @cicloId = 88;
    set @empresaId = 1;
    set @usuarioId = 2;*/
    -- VARIABLES NO EXCLUYENTES
    set @estadoComisionDetalleEmpresaRechazado = 3;
    set @tipoComisionPago = 1;
    set @tipoComisionRezagado = 2;
    set @GP_EstadoComisionCerradoFormaPago = 10;
    set @GP_EstadoComisionRezagadoFormasPagos = @estadoComision; --9;
    set @habilitado = 1;
    --
    DECLARE @idComisionDetalle int,
            @idComisionDetalleEmpresa int,
            @montoNetoComisionDetalleEmpresa decimal(18, 2),
            @fichaId int,
            @existeCicloComisionRezagado int;
    --
    /* Cursor con los rechazados por parte del banco en pagos por transferencias.
    */

    DECLARE Detalle_Cursor1 CURSOR FOR select i.id_comisiones_detalle, i.id_comision_detalle_empresa, i.IMPORTE_POR_EMPRESA, i.id_ficha
                                        from BDMultinivel.dbo.vwObtenerInfoExcelFormatoBanco i 
                                        where i.id_ciclo = @cicloId and i.id_empresa = @empresaId and i.id_estado_comision_detalle_empresa = @estadoComisionDetalleEmpresaRechazado and i.id_tipo_pago = @tipoPago
    --
    DECLARE @ExisteComision as table(id_comision int, id_ciclo INT, monto_total_neto decimal(18,2), porcentaje_retencion decimal(18,2));

    INSERT INTO @ExisteComision select c.id_comision, c.id_ciclo, c.monto_total_neto, c.porcentaje_retencion
                                        from BDMultinivel.dbo.GP_COMISION c
                                        inner join BDMultinivel.dbo.GP_COMISION_DETALLE cd on cd.id_comision = c.id_comision
                                        inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I i on i.id_comision = c.id_comision
                                        where c.id_tipo_comision = @tipoComisionRezagado
                                        and c.id_ciclo = @cicloId
                                        and i.id_estado_comision = @GP_EstadoComisionRezagadoFormasPagos
                                        group by c.id_comision, c.id_ciclo, c.monto_total_neto, c.porcentaje_retencion

    SET @existeCicloComisionRezagado = (select COUNT(*) from @ExisteComision);
    SELECT e.*, ' select fila 60' as NroFila FROM @ExisteComision e
    select @existeCicloComisionRezagado as existeCicloComisionRezagado, ' select fila 61' 
    --
    -- obtener y asi verificar si hay comision rezagados de un ciclo en especifico
    -- si hay, no hacer nada
    -- si no hay, crear un nuevo registro para comision rezagados de un ciclo en especifico e insertar la suma de montos del comision detalle (el monto neto 
    -- de comision detalle es la suma de los montos_netos de comision_detalle_empresa de rechazados)
    
        DECLARE @porcentajeRetencion DECIMAL(18, 2) = 0.0;
        DECLARE @newIdComision int;
        DECLARE @sumaTotalMontoNetoComisionDetalle DECIMAL(18, 2) = 0.0;
        DECLARE @idEstadoDetalleRechazadoEnPagoTransferencia int = 4  
        DECLARE @ERROR_INSERT INT = -1;
        DECLARE @ERRORMESSAGE VARCHAR(500);
        IF(@existeCicloComisionRezagado <= 0)
        BEGIN
            BEGIN TRY
                -- Obtenenos el porcentaje de retencion 
                SELECT TOP 1 @porcentajeRetencion = c.porcentaje_retencion FROM BDMultinivel.DBO.GP_COMISION c
                                                                    INNER JOIN BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I cec on cec.id_comision = c.id_comision
                                                                    WHERE c.id_tipo_comision = @tipoComisionPago and c.id_ciclo = @cicloId and cec.id_estado_comision = @GP_EstadoComisionCerradoFormaPago

                SELECT @porcentajeRetencion AS porcentajeRetencion, ' select fila 82'
                -- Nuevo registro de comision para los nuevos rezagados.
                INSERT INTO BDMultinivel.DBO.GP_COMISION (monto_total_bruto, porcentaje_retencion, monto_total_retencion, monto_total_aplicacion, monto_total_neto, id_ciclo, id_tipo_comision, id_usuario) VALUES (
                    0, @porcentajeRetencion, 0, 0, 0, @cicloId, @tipoComisionRezagado, @usuarioId
                )
                SET @newIdComision = (select IDENT_CURRENT('GP_COMISION'));
                -- Nuevo registro de para el estado de la nueva comision de los nuevos rezagados, estado = 11 (Comision Rezagado Pendiente de Pago)
                INSERT INTO BDMultinivel.DBO.GP_COMISION_ESTADO_COMISION_I (habilitado, id_comision, id_estado_comision, id_usuario) VALUES(@habilitado, @newIdComision, @GP_EstadoComisionRezagadoFormasPagos, @usuarioId)

                SELECT c.*, ' select fila 91' FROM BDMultinivel.DBO.GP_COMISION c where c.id_ciclo = @cicloId and c.id_tipo_comision = @tipoComisionRezagado order by c.id_comision desc
                SELECT c.*, ' select fila 92' FROM BDMultinivel.DBO.GP_COMISION_ESTADO_COMISION_I c where c.id_comision = @newIdComision and c.id_estado_comision = @GP_EstadoComisionRezagadoFormasPagos order by c.id_comision_estado_comision_i desc
                --RAISERROR('ELIOT CRACK', 15, 1)
            END TRY
            BEGIN CATCH
                SELECT 'ENTRO AL CATCH LINEA 96'
                SET @ERROR_INSERT = 1
                SET @ERRORMESSAGE = 'ERROR AL INSERTAR EN LAS TABLAS GP_COMISION | GP_COMISION_ESTADO_COMISION_I'
                RAISERROR(@ERRORMESSAGE, 15, 1)
            END CATCH
        END
        ELSE
        BEGIN
            SELECT top 1 @newIdComision = e.id_comision FROM @ExisteComision e
            -- ACTUALIZAR TODO
            -- @newIdComision = 85
            select @newIdComision as newIdComision, ' select fila 107' as NroFila
        END
        SELECT @newIdComision AS newIdComision, ' select fila 109' as NroFila
        SELECT 'INICIO CURSOR----------------------------------------------------------------------------------------------------------------'
        -- Abrimos el cursor para crear nuevos registros comision detalle.    
            OPEN Detalle_Cursor1;
            FETCH NEXT FROM Detalle_Cursor1 INTO @idComisionDetalle, @idComisionDetalleEmpresa, @montoNetoComisionDetalleEmpresa, @fichaId;
            WHILE @@FETCH_STATUS = 0
            BEGIN
                SELECT 'Detalle_Cursor1'
                SELECT @idComisionDetalle AS idComisionDetalle, @idComisionDetalleEmpresa AS idComisionDetalleEmpresa, @montoNetoComisionDetalleEmpresa AS montoNetoComisionDetalleEmpresa, @fichaId AS fichaId, ' select fila 117' as NroFila
                SELECT '----------------------------------------------------------------------------------------------------------------'
                DECLARE @newIdComisionDetalle int;
                DECLARE @estadoComisionDetalleRezagado int = 5;                
                DECLARE @idComisionDetalleRezagado int = ISNULL((SELECT cd.id_comision_detalle from BDMultinivel.dbo.GP_COMISION c 
                                                    inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I cec on cec.id_comision = c.id_comision
                                                    inner join BDMultinivel.dbo.GP_COMISION_DETALLE cd on cd.id_comision = c.id_comision
                                                    where cec.id_estado_comision = @GP_EstadoComisionRezagadoFormasPagos and c.id_tipo_comision = @tipoComisionRezagado and cd.id_ficha = @fichaId and c.id_ciclo = @cicloId and c.id_comision = @newIdComision), 0)
                --- 
                SELECT @idComisionDetalleRezagado as cantComisionDetalleRezagado, ' select fila 126' as NroFila                
                IF(@idComisionDetalleRezagado <= 0)
                BEGIN
                    -- Nuevo registro para la comision detalle para los nuevos rezagados
                    INSERT INTO BDMultinivel.dbo.GP_COMISION_DETALLE (monto_bruto, porcentaje_retencion, monto_retencion, monto_aplicacion, monto_neto, id_comision, id_ficha, id_usuario) VALUES (
                        0, 0, 0, 0, @montoNetoComisionDetalleEmpresa, @newIdComision, @fichaId, @usuarioId
                    );
                    SELECT @montoNetoComisionDetalleEmpresa AS montoNetoComisionDetalleEmpresa, @newIdComision AS newIdComision, @fichaId AS fichaId, ' select fila 134' as NroFila
                    SET @newIdComisionDetalle = (select IDENT_CURRENT('GP_COMISION_DETALLE'))
                    SELECT cd.*, ' select fila 135' as NroFila from BDMultinivel.dbo.GP_COMISION_DETALLE cd where cd.id_comision = @newIdComision and cd.id_ficha = @fichaId order by cd.id_comision_detalle desc                                        
                    -- Nuevo registro para la tabla intermedia para enlazar el estado de la nueva comision detalle, en este caso enlazamos el estado 5 (Rezagado)                    
                    INSERT INTO BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I (id_comision_detalle, id_estado_comision_detalle, habilitado, id_usuario) VALUES (@newIdComisionDetalle, @estadoComisionDetalleRezagado, @habilitado, @usuarioId)
                    SELECT i.*, ' select fila 138' FROM BDMultinivel.DBO.GP_COMISION_DETALLE_ESTADO_I i order by i.id_comision_detalle_estado_i desc
                END
                ELSE
                BEGIN
                    -- Ya existe un registro de comision detalle para la comision de rezagados, se asigna el id_comision_detalle a @newIdComisionDetalle.
                    SET @newIdComisionDetalle = @idComisionDetalleRezagado
                    SELECT @newIdComisionDetalle as newIdComisionDetalle, ' select fila 144' as NroFila
                    -- Actualiza el monto neto de la comision detalle existente, sumando el monto neto de la comision detalle empresa.
                    UPDATE BDMultinivel.DBO.GP_COMISION_DETALLE SET monto_neto = monto_neto + @montoNetoComisionDetalleEmpresa where id_comision_detalle = @newIdComisionDetalle
                END
                SELECT '----------------------------------------------------------------------------------------------------------------'
                SELECT @newIdComisionDetalle as newIdComisionDetalle, ' select fila 149' as NroFila
                -- Referenciamos las comisiones detalle empresa con estado Rechazado al nuevo registro comision detalle.
                select top 1 * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA cde
                UPDATE BDMultinivel.dbo.COMISION_DETALLE_EMPRESA SET id_comision_detalle = @newIdComisionDetalle, estado = 1, fecha_pago = NULL WHERE id_comision_detalle_empresa = @idComisionDetalleEmpresa            
                SELECT cde.*, ' select fila 152' as NroFila from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA cde WHERE cde.id_comision_detalle_empresa = @idComisionDetalleEmpresa order by cde.id_comision_detalle_empresa                
                SELECT cde.*, ' select fila 153' as NroFila from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA cde WHERE cde.id_comision_detalle = @newIdComisionDetalle order by cde.id_comision_detalle_empresa
                SELECT '----------------------------------------------------------------------------------------------------------------'
                -- Actualizamos el monto total de la comision detalle restando el monto neto de los antiguos registros de la comision detalle con gp_comision_estado = 10 (Cerrado Forma de Pago)
                UPDATE BDMultinivel.dbo.GP_COMISION_DETALLE SET monto_neto = monto_neto - @montoNetoComisionDetalleEmpresa WHERE id_comision_detalle = @idComisionDetalle
                -- Se obtiene el monto neto de la comision detalle de tipo comision PAGO y verificamos.
                DECLARE @esMontoCero DECIMAL(18, 2) = (SELECT cd.monto_neto FROM BDMultinivel.dbo.GP_COMISION_DETALLE cd WHERE cd.id_comision_detalle = @idComisionDetalle)
                SELECT @esMontoCero AS esMontoCero, ' select fila 159'                
                IF(@esMontoCero = 0)
                BEGIN
                    -- Al ser monto neto igual a cero esa comision detalle rechazada, la agregamos a la tabla intermedia de lista forma pago con estado 4 (rechazado).
                    INSERT INTO BDMultinivel.DBO.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL (habilitado, id_lista_formas_pago, id_estado_listado_forma_pago, id_usuario) SELECT @habilitado, l.id_lista_formas_pago, @idEstadoDetalleRechazadoEnPagoTransferencia, @usuarioId
                                                                                                                                                        FROM BDMultinivel.DBO.LISTADO_FORMAS_PAGO l
                                                                                                                                                        WHERE l.id_comisiones_detalle = @idComisionDetalle
                    SELECT d.*, ' select fila 166' FROM BDMultinivel.DBO.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL d order by d.fecha_creacion desc                                                                                                                                                       
                END
                SELECT cd.*, ' select fila 168' as NroFila from BDMultinivel.dbo.GP_COMISION_DETALLE cd WHERE cd.id_comision_detalle = @idComisionDetalle order by cd.id_comision_detalle desc                
                SELECT '----------------------------------------------------------------------------------------------------------------'
                -- Acumulador de los monto neto de la comision detalle empresa
                SET @sumaTotalMontoNetoComisionDetalle = @sumaTotalMontoNetoComisionDetalle + @montoNetoComisionDetalleEmpresa;                
                --
                DECLARE @newIdListaFormasPago INT = ISNULL((SELECT top 1 l.id_lista_formas_pago FROM BDMultinivel.DBO.LISTADO_FORMAS_PAGO l WHERE l.id_comisiones_detalle = @newIdComisionDetalle), 0)
                SELECT @newIdListaFormasPago AS newIdListaFormasPago, ' select fila 174' as NroFila
                IF(@newIdListaFormasPago <= 0)
                BEGIN
                    -- Nuevo registro para listado formas pago con la informacion de la nueva comision detalle haciendo referencia al nuevo idComisionDetalle
                    INSERT INTO BDMultinivel.DBO.LISTADO_FORMAS_PAGO (monto_neto, id_tipo_pago, id_comisiones_detalle, id_usuario) VALUES (
                        @montoNetoComisionDetalleEmpresa, @tipoPago, @newIdComisionDetalle, @usuarioId
                    )
                    select l.*, ' select fila 181' from BDMultinivel.DBO.LISTADO_FORMAS_PAGO l WHERE l.id_comisiones_detalle = @newIdComisionDetalle order by l.id_lista_formas_pago desc
                    
                    SET @newIdListaFormasPago = (select IDENT_CURRENT('LISTADO_FORMAS_PAGO'))           
                    /*INSERT INTO BDMultinivel.DBO.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL (habilitado, id_lista_formas_pago, id_estado_listado_forma_pago, id_usuario) VALUES (
                        @habilitado, @newIdListaFormasPago, @idEstadoDetalleRechazadoEnPagoTransferencia, @usuarioId
                    )*/
                END
                ELSE
                BEGIN
                    UPDATE BDMultinivel.DBO.LISTADO_FORMAS_PAGO SET monto_neto = monto_neto + @montoNetoComisionDetalleEmpresa WHERE id_lista_formas_pago = @newIdListaFormasPago AND id_comisiones_detalle = @newIdComisionDetalle
                    SELECT l.*, ' select fila 191' FROM BDMultinivel.DBO.LISTADO_FORMAS_PAGO l order by l.id_lista_formas_pago desc
                END                
                SELECT l.*, ' select fila 193' from BDMultinivel.DBO.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL l WHERE l.id_lista_formas_pago = @newIdListaFormasPago order by l.id_lista_formas_pago desc
                SELECT '----------------------------------------------------------------------------------------------------------------'
                FETCH NEXT FROM Detalle_Cursor1 INTO @idComisionDetalle, @idComisionDetalleEmpresa, @montoNetoComisionDetalleEmpresa, @fichaId;
            END
            CLOSE Detalle_Cursor1;
            DEALLOCATE Detalle_Cursor1;
        -- FIN CURSOR
        SELECT 'FIN CURSOR----------------------------------------------------------------------------------------------------------------'
        -- Actualizamos el monto total neto de la comision con la suma del monto neto de los nuevos registros de la comision detalle 
        UPDATE BDMultinivel.DBO.GP_COMISION SET monto_total_neto = monto_total_neto + @sumaTotalMontoNetoComisionDetalle
            WHERE id_comision = @newIdComision

        SELECT c.*, ' select fila 205' FROM BDMultinivel.DBO.GP_COMISION C WHERE C.id_comision = @newIdComision ORDER by c.id_comision desc
        -- Actualizamos el monto total neto de la comision con la suma del monto neto de los antiguos registros de la comision detalle 
        DECLARE @idComisionAntigua int = (SELECT TOP 1 c.id_comision FROM BDMultinivel.dbo.GP_COMISION c
                                INNER JOIN BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I cec on cec.id_comision = c.id_comision
                                WHERE id_ciclo = @cicloId and id_tipo_comision = @tipoComisionPago and cec.id_estado_comision = @GP_EstadoComisionCerradoFormaPago)

        UPDATE BDMultinivel.DBO.GP_COMISION SET monto_total_neto = monto_total_neto - @sumaTotalMontoNetoComisionDetalle
            WHERE id_comision = @idComisionAntigua  

        SELECT c.*, ' select fila 214' FROM BDMultinivel.DBO.GP_COMISION C WHERE C.id_comision = @idComisionAntigua ORDER by c.id_comision desc                             
        SELECT 'FIN----------------------------------------------------------------------------------------------------------------'
        COMMIT TRANSACTION
        RETURN @newIdComision;
END TRY
BEGIN CATCH
    SELECT 'ENTRO AL CATCH LINEA 220'
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
                concat ('SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS ',
                        ' ');
        SET @IMPSUBJECT = 'ALERTA PRODUCCION : no se pudo cargar las comisiones ';
        --EXECUTE msdb.dbo.sp_send_dbmail @profile_name   = 'NotificacionSQL',
        --                                @recipients = 'desarrollo@gruposion.bo; UIT-SION@gruposion.bo',
        --                                @body           = @IMPBODY,
        --                                @subject        = @IMPSUBJECT;
        ROLLBACK TRANSACTION
        raiserror ('SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS: %d: %s', 16, 1, @error, @message) ;       
        return -1
    END
END CATCH;




GO
