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
    descriptionText: {
        color: "#212121"
    },
    businessSelect: {
        flex: 1
    },
    dialogConfirmButton: {
        color: theme.palette.primary.main,
    },
    downloadButton: {
        display: "flex",
        verticalAlign: 'center'
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
}) => {
    const style = useStyles();
    const dispatch = useDispatch();
    const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
    const [empresaId, setEmpresaId] = useState(-1);
    const [authorizationDisabled, setAuthorizationDisabled] = useState(true);


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
          console.log('handleDownloadFileEmpresas response ', response); 
          if(response && response.code == 0) { 
            downloadExcel(response.data.file, response.data.fileName);
            // setStatusBusqueda(true);    
          } else {
            dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
          }
        }
      }

    const handleEmpresasSelectChange = (event) => {
        setEmpresaId(event.target.value);
        handleAuthorizationDisabled(event.target.value);
    };

    const handleAuthorizationDisabled = (empresaId) => {
        if(empresaId == -1) {
            setAuthorizationDisabled(true);
        } else {
            setAuthorizationDisabled(false);
        }
    } 

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
            <Typography variant="body1" component="h2" className={style.descriptionText}>
                Selecciona la empresa a realizar la transferencia para generar el archivo.  
            </Typography>          
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
          <Grid container 
            alignItems="center"
            justifyContent="center"
            alignContent="center"
            alignSelf= 'center'
            spacing={1}
            direction="row">
              <Grid item xs={8} sm={8}>
              <TextField
                className={style.businessSelect}
                id="outlined-select-currency"
                select
                label="Seleccione una empresa"
                value={empresaId?empresaId:-1}
                onChange={handleEmpresasSelectChange}
                helperText="Por favor seleccione una empresa."
                variant="outlined"
                fullWidth
              >
                <MenuItem value={-1}>Seleccione una empresa</MenuItem>
                {empresas.length > 0 && empresas.map((x, i) => {
                  return <MenuItem value={x.idEmpresa}>{x.empresa}</MenuItem>
                })}
              </TextField>
            </Grid>
            <Grid item xs={4} sm={4}>
              <Button disabled={authorizationDisabled} className={style.downloadButton} variant="outlined" color="primary" onClick={() => handleDownloadFileEmpresas(userName, empresaId)}>
                Descargar
              </Button>
            </Grid>
          </Grid>
          </div>
            {/* </Container> */}
        </DialogContent>
        <DialogActions>
            <Button disabled={authorizationDisabled} className={style.dialogConfirmButton} onClick={()=>closeTransferenciasDialog()}>Confimar todos</Button>
            <Button disabled={authorizationDisabled} className={style.dialogConfirmButton} onClick={()=>closeTransferenciasDialog()}>Confirmar seleccion</Button>
            <Button className={style.dialogConfirmButton} onClick={()=>closeTransferenciasDialog()}>Cerrar</Button>
        </DialogActions>
      </Dialog>
    );
}

export default TransferenciasDialog;