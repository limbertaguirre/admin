import {
    Container,
    Tooltip,
    Zoom,
    Chip,
    InputAdornment,
    Card,
    Button,
    Grid,
    TextField,
    Typography,
    FormControl,
    InputLabel,
    MenuItem,
    Breadcrumbs
  } from "@material-ui/core";
  import {
    KeyboardDatePicker,
    MuiPickersUtilsProvider
  } from "@material-ui/pickers";
  import DateFnsUtils from "@date-io/date-fns";
  import esLocale from "date-fns/locale/es";
  import React, { useState, useEffect } from "react";
  import { useDispatch, useSelector } from "react-redux";
  import { makeStyles } from "@material-ui/core/styles";
  
  import Dialog from "@material-ui/core/Dialog";
  import DialogActions from "@material-ui/core/DialogActions";
  import DialogContent from "@material-ui/core/DialogContent";
  import DialogContentText from "@material-ui/core/DialogContentText";
  import DialogTitle from "@material-ui/core/DialogTitle";
  import {formatearNumero} from "../../../../lib/utility";
  
  const useStyles = makeStyles((theme) => ({
    appBar: {
      position: "relative"
    },
    title: {
      textAlign: "center",
      flex: 1
    },
    textoProducto: {
      fontSize: "12px"
    },
    dialgoTitle: {
    //   backgroundColor: theme.palette.primary.main,
      backgroundColor: "#1872b8",
      color: "white",
      "& .MuiTypography-root": {
        color: "white"
      }
    },
    descriptionText: {
      color: "#212121"
    },
    businessSelect: {
      flex: 1
    },
    dialogConfirmButton: {
      color: theme.palette.primary.main
    },
    downloadButton: {
      display: "flex",
      verticalAlign: "center"
    },
    dialogContainer: {
      "& .MuiPaper-root": {
        [theme.breakpoints.down("lg")]: {
          minWidth: "740px"
        },
        [theme.breakpoints.down("md")]: {
          minWidth: "500px"
        },
        [theme.breakpoints.down("xs")]: {
          minWidth: "550px"
        }
      }
    },
    btnContainerDescargar: {
      display: "flex",
      flex: 2,
      flexDirection: "column",
      alignContent: "center",
      alignItems: "flex-end",
      justifyContent: "center"
    },
    borderContainer: {
      border: "1px solid #c6c8cb",
      borderRadius: "8px",
      paddingTop: "18px",
      paddingBottom: "16px",
      paddingRight: "24px",
      paddingLeft: "24px"
    },
    bold: {
        fontWeight: 600
    }
  }));
  
  const PagosTransferenciaDetalleDialog = ({
    isConfirm,
    open,
    close,
    data,
    handleConfirm,
    empresaId
  }) => {
      const style = useStyles();
      const dispatch = useDispatch();
      const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});    
    console.log('PagosTransferenciaDetalleDialog data ', data);
    console.log('PagosTransferenciaDetalleDialog empresaId ', empresaId);
    console.log('PagosTransferenciaDetalleDialog userName ', userName);

    return (
      <Dialog
        open={open}
        fullWidth={false}
        maxWidth="sm"
        className={style.dialogContainer}
        onClose={() => close()}
      >
        <DialogTitle className={style.dialgoTitle}>Detalle pago por transferencias confirmadas</DialogTitle>
  
        <DialogContent>
          <DialogContentText>
            <Typography
              variant="body1"
              component="h2"
              className={style.descriptionText}
            >
              {data.message}
            </Typography>
          </DialogContentText>
          {/* <div className="d-flex flex-row align-items-center"> */}
          <div className={style.borderContainer}>
            <Grid
              container
              alignItems="center"
              justifyContent="center"
              alignContent="center"
              spacing={1}
              flex="1"
              direction="row"
            >
              {!isConfirm ? (
              <>
                <Grid item xs={6} sm={6}>
                    <Typography variant="body1" className={style.bold} gutterBottom>Total enviados a confirmar:</Typography>
                </Grid>            
                <Grid item xs={6} sm={6}>
                    <Typography variant="body1" gutterBottom>{data.totalEnviadosConfirmar}</Typography>
                </Grid>
                
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" className={style.bold} gutterBottom>Total confirmados:</Typography>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{data.totalConfirmados}</Typography>
                </Grid>
            
                <Grid item xs={6} sm={6}>
                    <Typography variant="body1" className={style.bold} gutterBottom>Total rechazados:</Typography>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{data.totalRechazados}</Typography>
                </Grid>
               
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" className={style.bold} gutterBottom>Monto total confirmados ($us.):</Typography>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{formatearNumero(data.montoTotalConfirmados)}</Typography>
                </Grid>
                {data.montoTotalRechazados > 0 && (
                <>
                    <Grid item xs={6} sm={6}>              
                        <Typography variant="body1" className={style.bold} gutterBottom>Monto total rechazados ($us.):</Typography>
                    </Grid>            
                    <Grid item xs={6} sm={6}>              
                        <Typography variant="body1" gutterBottom>{formatearNumero(data.montoTotalRechazados)}</Typography>
                    </Grid>
                </>)
                }
              </>):
                  (<>
                  <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" className={style.bold} gutterBottom>Cantidad total pendientes:</Typography>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{data.totalPendientes}</Typography>
                </Grid>
                  <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" className={style.bold} gutterBottom>Monto total pendientes ($us.):</Typography>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{(parseFloat(data.montoTotalPendientes).toFixed(2) > 0) ? formatearNumero(parseFloat(data.montoTotalPendientes).toFixed(2)):0}</Typography>
                </Grid>
                  </>
              )}
            </Grid>
            </div>
              {/* </Container> */}
          </DialogContent>
          <DialogActions>             
            {isConfirm && <Button className={style.dialogConfirmButton} onClick={()=>handleConfirm(userName, empresaId)}>Confirmar transferencia</Button>}
            <Button className={style.dialogConfirmButton} onClick={()=>close()}>Cerrar</Button>
          </DialogActions>                 
        </Dialog>
      );
  }
  
  export default PagosTransferenciaDetalleDialog;
  