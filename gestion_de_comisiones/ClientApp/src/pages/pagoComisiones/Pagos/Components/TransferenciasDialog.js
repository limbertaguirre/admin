import {Container,Tooltip ,Zoom, Chip, InputAdornment,Card, Button,
    Grid, TextField, Typography,FormControl,
     InputLabel, MenuItem, Breadcrumbs } from "@material-ui/core";
  import React, { useState, useEffect } from "react";
  import { useDispatch, useSelector } from "react-redux";
  import {  makeStyles } from '@material-ui/core/styles';

import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import * as Actions from '../../../../redux/actions/PagosGestorAction';
import * as ActionMensaje from '../../../../redux/actions/messageAction';
  
  const useStyles = makeStyles((theme) => ({
    appBar: {
      position: "relative",
    },
    title: {
      textAlign: "center",
      flex: 1,
    },
    textoProducto: {
      fontSize: "12px",
    },
    dialgoTitle: {
      backgroundColor: theme.palette.primary.main,
      color: "white",
      "& .MuiTypography-root": {
        color: "white",
      },
    },
  
    dialogContainer: {
      "& .MuiPaper-root": {
        [theme.breakpoints.down("lg")]: {
          minWidth: "740px",
        },
        [theme.breakpoints.down("md")]: {
          minWidth: "500px",
        },
        [theme.breakpoints.down("xs")]: {
          minWidth: "350px",
        },
      },
    },
  }));

const TransferenciasDialog = ({
    cicloId,
    openDialog,
    closeTransferenciasDialog,
    empresas,
    //handleDownloadFileEmpresas
}) => {
    const style = useStyles();
    const dispatch = useDispatch();
    const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
    const [empresaId, setEmpresaId] = useState(-1);
    // const[empresasTransferencias, setEmpresasTransferencias]= useState([]);
    
    // useEffect(()=>{
    //     try { 
    //         handleTransferenciasEmpresas(userName);
    //         //verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
    //     } catch (err) {
    //         //verificarAcceso(perfiles, 'none', history);
    //         console.error(err)
    //     }
    //  },[]);

    // const handleTransferenciasEmpresas = async (user) => {
    //     // Verificar si hay conexion a internet.
    //     if(cicloId && cicloId !== 0) {  
    //     let response = await Actions.handleTransferenciasEmpresas(user, cicloId, dispatch);
    //       console.log('handleTransferenciasEmpresas response ',response);
    //       if(response && response.code == 0) { 
    //         setEmpresasTransferencias(response.data);
    //         // setStatusBusqueda(true);    
    //       } else {
    //         dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
    //       }
    //     }
    //   }

      const downloadExcel = (base64, fileName) => {
        // const contentType = "application/vnd.ms-excel";
        const contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        const str = base64;
        const byteCharacters = atob(str);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
          byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], {
          type: contentType,
        });
        const objectURL = window.URL.createObjectURL(blob);
        const anchor = document.createElement("a");
      
        anchor.href = objectURL;
        anchor.target = "_blank";
        anchor.download = fileName;
        anchor.click();
      
        URL.revokeObjectURL(objectURL);
      }
    
      const handleDownloadFileEmpresas = async (user, empresaId) => {
        if(cicloId && cicloId !== 0 && empresaId && empresaId != -1) {  
          let response = await Actions.handleDownloadFileEmpresas(user, cicloId, empresaId, dispatch);      
          if(response && response.code == 0) { 
            downloadExcel(response.data.file, response.data.fileName);
            // setStatusBusqueda(true);    
          } else {
            dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
          }
        }
      }

    const handleEmpresasSelectChange = (event) => {
        console.log('handleEmpresasSelectChange empresaId: ', event.target.value)
        // console.log('handleEmpresasSelectChange empresaId: ', event.currentTarget.value)
        // setEmpresaId(event.currentTarget.value);
        setEmpresaId(event.target.value);
    };

    console.log('TransferenciasDialog openDialog ', openDialog);

    return(
        <Dialog
        open={openDialog}
        fullWidth={false}
        maxWidth='md' 
        className={style.dialogContainer}       
        onClose={() => closeTransferenciasDialog()}
      >
        <DialogTitle className={style.dialgoTitle}>Transferencia</DialogTitle>
        <DialogContent>
          <DialogContentText>
          Hola María, selecciona la empresa a realizar la transferencia para generar el archivo.
          </DialogContentText>
          {/* <div className="d-flex flex-row align-items-center"> */}
          {/* <Container
            noValidate
            component="form"
            sx={{
              display: 'flex',
              flexDirection: 'row',
              m: 'auto',
              width: 'fit-content',
            }}
          > */}
          <div style={{flexGrow: 1}}>
          <Grid container spacing={1}>
              <Grid item xs={8} sm={6}>
              <TextField
                id="outlined-select-currency"
                select
                label="Seleccione una empresa"
                value={empresaId}
                onChange={handleEmpresasSelectChange}
                helperText="Por favor seleccione una empresa."
                variant="outlined"
              >
                {/* <MenuItem value=""><em>Seleccione una empresa</em></MenuItem> */}
                {empresas.map((x, i) => {
                  return <MenuItem value={x.idEmpresa}>{x.empresa}</MenuItem>
                })
              }
              </TextField>
            </Grid>
            <Grid item xs={4} sm={6}>
              <Button variant="outlined" color="primary" onClick={() => handleDownloadFileEmpresas(userName, empresaId)}>
                Descargar
              </Button>
              </Grid>
          </Grid>
          </div>
            {/* </Container> */}
        </DialogContent>
        <DialogActions>
          <Button onClick={()=>closeTransferenciasDialog()}>Close</Button>
        </DialogActions>
      </Dialog>
    );
}

export default TransferenciasDialog;