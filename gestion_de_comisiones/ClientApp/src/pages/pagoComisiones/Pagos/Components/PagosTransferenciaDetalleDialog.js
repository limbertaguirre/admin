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
    Breadcrumbs,
    List,
    ListItem,
    ListItemText,
    ListItemIcon,
    ListSubheader
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
  import HelpOutlineIcon from '@material-ui/icons/HelpOutline';
  
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
    },root: {
      width: '100%',
      
      backgroundColor: theme.palette.background.paper,
    },
    item: {
      paddingTop: 0,
      paddingBottom: 0
    },
    listText: {
      textAlign: 'left'
    },
    listItemText: {
      maxWidth: 270
    },
    listIcon: {
      alignSelf: 'left'
    },
    listItemDiv: {
      maxWidth: 270,
      display: 'flex',
      flexDirection: 'row'
    },
    tooltipText: {
      '& .MuiTooltip-popper': {
        fontSize: "14px !important"
      },
      '& .MuiTooltip-popperArrow': {
        fontSize: "14px !important"
      }
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
      const totalPendientesText = `
      Es la cantidad de ACI que están pendientes a confirmar que se le realizarán las transferencias de pago a sus cuentas bancarias respectivas.
      `;
      const montoTotalPendientesText = `
      Es la suma total de los ACI que están pendientes a confirmar que se le realizarán las transferencias de pago a sus cuentas bancarias respectivas.
      `;
      const cantidadEnviadosText = `
      Es la cantidad de ACI que fueron enviados a travéz del EXCEL al banco para que se le realicen las transferencias de pago a sus cuentas bancarias respectivamente.
      `;
      const cantidadConfirmadosText = `
      Es la cantidad de ACI que ya se les hizo la transferencia bancaria a sus cuentas respectivamente.
      `;
      const cantidadRechazadosText = `
      Es la cantidad de ACI que el banco rechazó y no les hizo la transferencia bancaria a sus cuentas respectivamente. Estos ACI están en REZAGADOS.
      `;
      const montoConfirmadosText = `
      Es la suma total de los montos ya transferidos a los ACI.
      `;
      const montoRechazadosText = `
      Es la suma total de los montos de los ACI rechazados.
      `;
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
        {isConfirm?
        <DialogTitle className={style.dialgoTitle}>Confirmar transferencia</DialogTitle>
        :
        <DialogTitle className={style.dialgoTitle}>Detalle de pago por transferencias</DialogTitle>}
  
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
                    <div className={style.listItemDiv}>
                      <Typography variant="body1" className={style.bold} gutterBottom>Cantidad de ACI enviados:</Typography>
                      <Tooltip title={cantidadEnviadosText}  PopperProps={{style:{fontSize:'14px'}}} arrow={true} placement='top' enterTouchDelay={10} className={style.tooltipText} style={{marginLeft: 4}}>
                        <HelpOutlineIcon color="disabled"></HelpOutlineIcon>
                      </Tooltip>
                    </div>
                </Grid>            
                <Grid item xs={6} sm={6}>
                    <Typography variant="body1" gutterBottom>{data.totalEnviadosConfirmar}</Typography>
                </Grid>
                
                <Grid item xs={6} sm={6}>       
                  <div className={style.listItemDiv}>       
                    <Typography variant="body1" className={style.bold} gutterBottom>Total confirmados (ACI):</Typography>
                    <Tooltip title={cantidadConfirmadosText}  PopperProps={{style:{fontSize:'14px'}}} arrow={true} placement='top' enterTouchDelay={10} className={style.tooltipText} style={{marginLeft: 4}}>
                      <HelpOutlineIcon color="disabled"></HelpOutlineIcon>
                    </Tooltip>
                  </div>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{data.totalConfirmados}</Typography>
                </Grid>
            
                <Grid item xs={6} sm={6}>
                  <div className={style.listItemDiv}>       
                    <Typography variant="body1" className={style.bold} gutterBottom>Total rechazados (ACI):</Typography>
                    <Tooltip title={cantidadRechazadosText}  PopperProps={{style:{fontSize:'14px'}}} arrow={true} placement='top' enterTouchDelay={10} className={style.tooltipText} style={{marginLeft: 4}}>
                      <HelpOutlineIcon color="disabled"></HelpOutlineIcon>
                    </Tooltip>
                  </div>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{data.totalRechazados}</Typography>
                </Grid>
               
                <Grid item xs={6} sm={6}>    
                <div className={style.listItemDiv}>         
                    <Typography variant="body1" className={style.bold} gutterBottom>Monto confirmados ($us.):</Typography>
                    <Tooltip title={montoConfirmadosText}  PopperProps={{style:{fontSize:'14px'}}} arrow={true} placement='top' enterTouchDelay={10} className={style.tooltipText} style={{marginLeft: 4}}>
                      <HelpOutlineIcon color="disabled"></HelpOutlineIcon>
                    </Tooltip>
                    </div>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{data.montoTotalConfirmados}</Typography>
                </Grid>
                {data.totalRechazados > 0 && (
                <>
                    <Grid item xs={6} sm={6}> 
                    <div className={style.listItemDiv}>             
                        <Typography variant="body1" className={style.bold} gutterBottom>Monto rechazados ($us.):</Typography>
                        <Tooltip title={montoRechazadosText}  PopperProps={{style:{fontSize:'14px'}}} arrow={true} placement='top' enterTouchDelay={10} className={style.tooltipText} style={{marginLeft: 4}}>
                      <HelpOutlineIcon color="disabled"></HelpOutlineIcon>
                    </Tooltip>
                    </div>
                    </Grid>            
                    <Grid item xs={6} sm={6}>              
                        <Typography variant="body1" gutterBottom>{data.montoTotalRechazados}</Typography>
                    </Grid>
                </>)
                }
              </>):
                  (<>
                  <List className={style.root}>
                    <ListItem className={style.item} alignItems='flex-start'>
                      <ListItemText disableTypography={true} className={style.listItemDiv}>
                        <Typography variant="body1" className={style.bold} gutterBottom>Total pendientes (ACI):</Typography>
                        <Tooltip title={totalPendientesText}  PopperProps={{style:{fontSize:'14px'}}} arrow={true} placement='top' enterTouchDelay={10} className={style.tooltipText} style={{marginLeft: 4}}>
                          <HelpOutlineIcon color="disabled"></HelpOutlineIcon>
                        </Tooltip>
                      </ListItemText>                      
                      <ListItemText className={style.listText}>{data.totalPendientes}</ListItemText>
                    </ListItem>  
                    <ListItem className={style.item}>
                      <ListItemText disableTypography={true} className={style.listItemDiv}>
                        <Typography variant="body1" className={style.bold} gutterBottom>Monto total pendientes ($us.):</Typography>
                        <Tooltip title={montoTotalPendientesText}  PopperProps={{style:{fontSize:'14px'}}} arrow={true} placement='top' enterTouchDelay={10} className={style.tooltipText} style={{marginLeft: 4}}>
                          <HelpOutlineIcon color="disabled"></HelpOutlineIcon>
                        </Tooltip>
                      </ListItemText>
                      <ListItemText className={style.listText}>{data.montoTotalPendientes}</ListItemText>
                    </ListItem> 
                  </List>      
                  {/* <Grid item xs={6} sm={6}>                  
                    <Typography variant="body1" className={style.bold} gutterBottom>Total pendientes:</Typography>
                    <HelpOutlineIcon color="disabled"></HelpOutlineIcon>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{data.totalPendientes}</Typography>
                </Grid>
                  <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" className={style.bold} gutterBottom>Monto total pendientes ($us.):</Typography>
                </Grid>            
                <Grid item xs={6} sm={6}>              
                    <Typography variant="body1" gutterBottom>{data.montoTotalPendientes}</Typography>
                </Grid> */}
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
  