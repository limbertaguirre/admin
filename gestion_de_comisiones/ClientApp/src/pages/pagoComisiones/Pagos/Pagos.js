import React, {useEffect, useState} from 'react';
import SnackbarSion from "../../../components/message/SnackbarSion";
import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';
import MessageConfirm from './Components/ConfirmarPago';

import GridPagos from './Components/GridPagos'
import {Container,Tooltip ,Zoom, Chip, InputAdornment,Card, Button,
  Grid, TextField, Typography,FormControl,
   InputLabel, Select, Menu, MenuItem, Breadcrumbs, IconButton } from "@material-ui/core";
import HomeIcon from '@material-ui/icons/Home';
import {emphasize, withStyles} from '@material-ui/core';
import {  makeStyles } from '@material-ui/core/styles';
import { useDispatch, useSelector} from "react-redux";

import { useHistory } from 'react-router-dom';

import SaveIcon from '@material-ui/icons/Save';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import SearchIcon from '@material-ui/icons/Search';
import * as Actions from '../../../redux/actions/PagosGestorAction';
import * as ActionMensaje from '../../../redux/actions/messageAction';
import TransferenciasDialog from './Components/TransferenciasDialog'
import MoreVertIcon from '@material-ui/icons/MoreVert';

const StyledBreadcrumb = withStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.grey[100],
    height: theme.spacing(3),
    color: theme.palette.grey[800],
    fontWeight: theme.typography.fontWeightRegular,
    '&:hover, &:focus': {
      backgroundColor: theme.palette.grey[300],
    },
    '&:active': {
      boxShadow: theme.shadows[1],
      backgroundColor: emphasize(theme.palette.grey[300], 0.12),
    },
  },
}))(Chip); 

const useStyles = makeStyles((theme) => ({
  etiqueta: {
    marginBottom: theme.spacing(1),
    marginTop: theme.spacing(1),
    marginRight:theme.spacing(1),
    paddingRight:theme.spacing(1),
    paddingLeft:theme.spacing(1),
   },
   submitCargar: {                 
    background: "#1872b8", 
    boxShadow: '2px 4px 5px #1872b8',
    color:'white',
   },
   submitSAVE: {                 
  //  background: "#1872b8", 
    background: "#f44336", 
    boxShadow: '2px 4px 5px #1872b8',
    color:'white',
    marginLeft:theme.spacing(1),

   },
   gridContainer:{
    paddingLeft:theme.spacing(1),
    paddingRight:theme.spacing(1),
    paddingTop:theme.spacing(1),
    paddingBottom:theme.spacing(1),
  },
   containerCiclo:{
    paddingLeft:theme.spacing(1),
    paddingRight:theme.spacing(1),
    paddingTop:theme.spacing(1),
    paddingBottom:theme.spacing(1),

    display:'flex',
    alignItems:'center',
    justifyContent:'center', 

  },
  containerSave:{
    paddingLeft:theme.spacing(1),
    paddingRight:theme.spacing(1),
    paddingTop:theme.spacing(1),
    paddingBottom:theme.spacing(1),

    display:'flex',
    alignItems:'center',
    justifyContent:'center', 
  },
  containerCargar:{
    paddingLeft:theme.spacing(1),
    paddingRight:theme.spacing(1),
    paddingTop:theme.spacing(1),
    paddingBottom:theme.spacing(1),

    display:'flex',
    flexDirection:'column',
    alignContent:'center',
    alignItems:'center',
    justifyContent:'center',
  },
  containerPaymentMethods:{
    display:'flex',
    alignItems:'center',
    justifyContent:'center', 
  },

}));


 const Pagos =(props)=> {
  let history = useHistory();
  const dispatch=useDispatch();
  const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
  const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
  useEffect(()=>{  try{  
     verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
     }catch (err) {  verificarAcceso(perfiles, 'none', history); }
  },[])
  const style= useStyles();

  const[statusBusqueda, setStatusBusqueda]= useState(false);
  const[ txtBusqueda, setTxtBusqueda]= useState('');

  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = useState("");
  const [tipoSnackbar, settipTSnackbar] = useState(true);
 

  const[idCiclo, setIdCiclo]= useState(0);
  const[nameComboSeleccionado, setNameComboSeleccionado] = useState("");
  const[idCicloSelected, setIdCicloSelected]= useState(0);
  const[listCiclo, setListCiclo]= useState([]);

  const[listaComisionesAPagar, setListaComisionesAPagar]= useState([]);
  const[listaComisionPaginacionNueva, setListaComisionPaginacionNueva]= useState(false);
  const[empresasTransferencias, setEmpresasTransferencias]= useState([]);
  const [openTransferenciasDialog, setOpenTransferenciasDialog] = useState(false);

  async function cargarCiclo(userNa){      
      let respuesta = await Actions.ObtenerCiclosPagos(userNa, dispatch);
      
      if(respuesta && respuesta.code == 0){ 
        setListCiclo(respuesta.data);
      }else{
        dispatch(ActionMensaje.showMessage({ message: respuesta.message , variant: "error" }));
      }
   }
   const seleccionarNombreCombo = (nombre)=>{
    setNameComboSeleccionado(nombre);
   }
   const onChangeSelectCiclo= (e) => {
      const texfiel = e.target.name;
      const value = e.target.value;
      if (texfiel === "idCiclo") {
          setIdCiclo(value);        
      }
        if (texfiel === "txtBusqueda") {
          setTxtBusqueda(value);
       }
   };

   const handleOnGetPagos=()=>{         
        if(idCiclo && idCiclo !== 0){  
            setIdCicloSelected(idCiclo);     
            cargarComisionesPagos(userName, idCiclo)

        }else{
          generarSnackBar('¡Debe Seleccionar un ciclo para cargar las comisiones!','warning') 
        }      
    }

    async function cargarComisionesPagos(userNa, cicloId){      
      let respuesta = await Actions.ObtenerComisionesPagos(userNa, cicloId, dispatch);
      
      if(respuesta && respuesta.code == 0){ 
        setListaComisionesAPagar(respuesta.data);
        setListaComisionPaginacionNueva(true);
        setStatusBusqueda(true);    
      }else{
        dispatch(ActionMensaje.showMessage({ message: respuesta.message , variant: "error" }));
      }
    }
    //--navbar
      const closeSnackbar= (event, reason) => {
        if (reason === 'clickaway') {
          return;
        }
        setOpenSnackbar(false);
      };
      const generarSnackBar =(mensaje, tipo)=>{
        setOpenSnackbar(true);
        setMensajeSnackbar(mensaje);
        settipTSnackbar(tipo);
      }
    
     const selecionarDetalleFrelances =() =>{

     } 
    
    const seleccionarTipoFiltroBusqueda=(idTipoFormaPago)=>{
      filtrarComisionPorFormaPago(idTipoFormaPago)
    }
    async function filtrarComisionPorFormaPago(idTipoFormaPago){
      if(idCiclo && idCiclo !== 0){  
          let response= await Actions.ListarFiltrada(userName, idCiclo, idTipoFormaPago, dispatch)   
              
          if(response && response.code == 0){
              let data= response.data;        
              setListaComisionesAPagar(data.lista);  
              setListaComisionPaginacionNueva(true);
          }       
      }else{
             generarSnackBar('¡Debe Seleccionar un ciclo para cargar las comisiones!','warning');
      }    
    }
    const buscarFreelanzer=()=>{
          if(txtBusqueda != ""){ 
              buscarFrelancerPorCi();
          }else{
              generarSnackBar('¡Introduzca carnet de identidad!','info');
          }
      }
  
      async function buscarFrelancerPorCi(){   

             let response= await Actions.buscarPorCarnetFormaPago(userName, idCiclo, txtBusqueda, dispatch)               
             if(response && response.code == 0){
               
                 let data= response.data;                
                 setListaComisionesAPagar(data);  
                 setListaComisionPaginacionNueva(true);               
             }       
       }

   useEffect(()=>{
      cargarCiclo(userName);
   },[])

    const [openModalConfirm, setOpenModalConfirm] = useState(false);
    const [subtitulo, setSubtitulo] = useState('');
   const abrirModal = ()=> {
    verificarConfirmarSionPay(userName,idCiclo);      
      handleClose();
   }
   async function verificarConfirmarSionPay(userN, cicloId){   

    let response= await Actions.verificarPagoSionPayXCiclo(userN, cicloId, dispatch)               
      if(response && response.code == 0){   
          var body= response.data;        
          setOpenModalConfirm(true);    
          setSubtitulo('Se pagará a '+body.cantidad+' ACI con un monto total de '+ body.totalPagoSionPay.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, }) +' Puntos.')
      } else{
         dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
      }      
    }

   const CloseModalConfirmacion =()=>{
      setOpenModalConfirm(false);      
   }
   const confirmarModal =()=>{
    if(idCiclo && idCiclo !== 0){  
      prosesarPagoSionPay(userName,idUsuario, idCiclo);
    }else{
      generarSnackBar('¡Debe seleccionar un ciclo para el cierre','info');
    }
   }
   async function prosesarPagoSionPay(userN,usuarioId, cicloId){   

    let response= await Actions.pagarComisionSionPay(userN,usuarioId, cicloId, dispatch)               
      if(response && response.code == 0){           
           setOpenModalConfirm(false);      
           dispatch(ActionMensaje.showMessage({ message: response.message , variant: "success" }));   
           handleOnGetPagos();      
      } else{
         dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
      }      
    }

    const handleClickOpenTransferencias = () => {
      handleTransferenciasEmpresas(userName);
      handleClose();
    };
  
    const handleCloseTransferencias = () => {
      setOpenTransferenciasDialog(false);
    };
  
    const handleTransferenciasEmpresas = async (user) => {
      // Verificar si hay conexion a internet.
      if(idCiclo && idCiclo !== 0) {  
      let response = await Actions.handleTransferenciasEmpresas(user, idCiclo, dispatch);
        if(response && response.code == 0) { 
          setEmpresasTransferencias(response.data);
          setOpenTransferenciasDialog(true);  
        } else {
          dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
        }
      }
    }

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    const handlePages = (event) => {
      setAnchorEl(event.currentTarget);
    };
  
    const handleClose = () => {
      setAnchorEl(null);
    };
    useEffect(()=>{         
    },[  listaComisionesAPagar,listaComisionPaginacionNueva]);

    const confirmarCierrePagos =()=>{
      ApiVerificarConfirmarCierrePago(userName, idUsuario,idCiclo);
    }
    async function ApiVerificarConfirmarCierrePago(userNa, idUser,idCICLO){
        if(idCICLO && idCICLO !== 0){  
            let response= await Actions.ConfirmarCierrePago(userNa, idUser,idCICLO, dispatch)   
            console.log('respom : ',response);
            if(response && response.code == 0){
                 console.log('code ok  : ',response);
            }else{
                dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
            }
  
        }else{
            mensajeGenericoCiclo();
        }    
    }

    const mensajeGenericoCiclo =()=>{
      setOpenSnackbar(true);
      setMensajeSnackbar('¡Debe Seleccionar un ciclo!');
      settipTSnackbar('warning');
    }

  
    return (
      <>
        <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}>
          <Breadcrumbs aria-label="breadcrumb">
                <StyledBreadcrumb key={1} component="a" label="Pagos"icon={<HomeIcon fontSize="small" />}  />                
          </Breadcrumbs>
        </div>
        <br />
        <Typography variant="h4" gutterBottom  >
             {'Pagos'}
        </Typography>     

        {empresasTransferencias && (
          <TransferenciasDialog
            cicloId={idCiclo} 
            openDialog={openTransferenciasDialog}
            closeTransferenciasDialog={handleCloseTransferencias}
            empresas={empresasTransferencias}
            recargarCicloActual={handleOnGetPagos}
            // handleDownloadFileEmpresas = {handleDownloadFileEmpresas}          
          />
        )}

        <Card>
             <Grid container className={style.gridContainer} >
              {statusBusqueda ?                
                  <>
                    <Grid item xs={12} sm={3} md={3} className={style.containerSave} >
                      {statusBusqueda&&
                        <>
                          {validarPermiso(perfiles, props.location.state.namePagina + permiso.CREAR)?
                            <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            className={style.submitCargar}
                            onClick = {()=> confirmarCierrePagos()}                                         
                            >
                              <SaveIcon style={{marginRight:'5px'}} /> CERRAR FORMA PAGO
                            </Button> 
                            :
                              <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin Acceso'}>
                                <Button variant="contained"  
                                > <SaveIcon style={{marginRight:'5px'}} /> CERRAR FORMA PAGO</Button> 
                              </Tooltip> 
                          }
                        </> 
                    }    

                    </Grid>
                  </>
                  :
                  <>
                    <Grid item xs={12} sm={4} md={4} className={style.containerSave} ></Grid>
                  </>                  
              }
                 {/* <Grid item xs={12} md={4} className={style.containerSave} >
                    {statusBusqueda&&
                      <>
                        {validarPermiso(perfiles, props.location.state.namePagina + permiso.CREAR)?
                        <>
                          <Button
                          type="submit"
                          variant="contained"
                          color="secondary"
                          className={style.submitSAVE}                          
                          onClick = {()=> abrirModal()}                                         
                          >
                             PAGAR SION PAY
                          </Button> 
                          <Button
                          type="submit"
                          variant="contained"
                          color="secondary"
                          className={style.submitSAVE}                          
                         onClick = {()=> handleClickOpenTransferencias()}                                         
                          >
                            GENERAR PARA TRANSFERENCIA
                          </Button> 

                          </>
                          : <>
                              <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin Acceso'}>
                                <Button variant="contained"  
                                > <SaveIcon style={{marginRight:'5px'}} /> PAGAR SION PAY</Button> 
                              </Tooltip> 
                              <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin Acceso'}>
                                <Button variant="contained"  
                                > <SaveIcon style={{marginRight:'5px'}} /> GENERAR PARA TRANSFERENCIA</Button> 
                              </Tooltip> 
                            </>
                         }
                      </> 
                   }    
                  </Grid> */}
                  <Grid item xs={12} md={3} className={style.containerSave}>
                    {statusBusqueda&&
                        <TextField
                          label="Buscar freelancer"
                          type={'text'}
                          variant="outlined"
                          placeholder={'Buscar por carnet identidad'}
                          name="txtBusqueda"                    
                          value={txtBusqueda}
                         onChange={onChangeSelectCiclo}
                          fullWidth
                          onKeyPress={(ev) => {
                            if (ev.key === 'Enter') {
                              buscarFreelanzer();
                            }
                          }}                    
                          InputProps={{
                              startAdornment: (
                              <InputAdornment position="start">
                                  <SearchIcon />
                              </InputAdornment>
                              ),
                          }}                    
                        />       
                     }
                  </Grid>

                  <Grid item xs={12} md={3} className={style.containerCiclo}>
                              <FormControl  variant="outlined"  
                              fullWidth                       
                              className={style.TextFiel}  >
                                <InputLabel id="demo-simple-select-outlined-labelciclo">CICLO # </InputLabel>
                                <Select
                                    labelId="demo-simple-select-outlined-labelciclo"                              
                                    value={idCiclo}
                                    name="idCiclo"                              
                                    onChange={onChangeSelectCiclo}
                                    label="CICLO # "
                                    >
                                    <MenuItem value={0}>
                                        <em>Seleccione un ciclo</em>
                                    </MenuItem>
                                    {listCiclo.map((value,index)=> ( <MenuItem key={index} onClick={()=> seleccionarNombreCombo(`${value.nombre}`)} value={value.idCiclo}>{value.nombre}</MenuItem> ))}   
                                </Select>                               
                            </FormControl>
                    </Grid>
                    <Grid item  xs={12} md={2}  className={style.containerCargar} >
                          <Button
                          type="submit"
                          fullWidth
                          variant="contained"
                          color="primary"
                          className={style.submitCargar}
                          onClick = {()=> handleOnGetPagos()}                                         
                          > 
                            {'CARGAR '} <CloudUploadIcon style={{marginLeft:'12px'}} />
                          </Button>   
                    </Grid>
                    
                    {statusBusqueda &&
                      <>                      
                        {validarPermiso(perfiles, props.location.state.namePagina + permiso.CREAR) &&
                        <>
                        <Grid item xs={12} sm={1} md={1} lg={1} className={style.containerPaymentMethods}>
                          <IconButton aria-label="more"
                            aria-controls="long-menu"
                            aria-haspopup="true"
                            onClick={handlePages}>                        
                            <MoreVertIcon/>
                          </IconButton>
                          <Menu
                            id="long-menu"
                            anchorEl={anchorEl}
                            keepMounted
                            open={open}
                            onClose={handleClose}
                            PaperProps={{
                              style: {
                                maxHeight: 48 * 4.5,
                                width: '50ch',
                              },
                            }}
                          >
                              <MenuItem key="1" onClick={() => abrirModal()}>
                                PAGAR SION PAY
                              </MenuItem>
                              <MenuItem key="2" onClick={() => handleClickOpenTransferencias()}>
                                GENERAR ARCHIVO PARA TRANSFERENCIA
                              </MenuItem>                            
                          </Menu>
                        </Grid>
                      </>
                      }                      
                      </>
                    }
              </Grid>
            </Card>
            <SnackbarSion open={openSnackbar} closeSnackbar={closeSnackbar} tipo={tipoSnackbar} duracion={2000} mensaje={mensajeSnackbar}  /> 
            <GridPagos listaComisionesAPagar={listaComisionesAPagar} listaComisionPaginacionNueva={listaComisionPaginacionNueva} setListaComisionPaginacionNueva={setListaComisionPaginacionNueva}  selecionarDetalleFrelances={selecionarDetalleFrelances} seleccionarTipoFiltroBusqueda={seleccionarTipoFiltroBusqueda} idCiclo={idCiclo} permisoActualizar={validarPermiso(perfiles, props.location.state.namePagina + permiso.ACTUALIZAR)} permisoCrear={validarPermiso(perfiles, props.location.state.namePagina + permiso.CREAR)} />
               <MessageConfirm open={openModalConfirm} titulo={"Confirmación de pagos por SION PAY."} nombreCiclo={nameComboSeleccionado} subTituloModal={subtitulo} tipoModal={"warning"} mensaje={"¿Desea confirmar el pago a través de SION PAY? "} handleCloseConfirm={confirmarModal} handleCloseCancel={CloseModalConfirmacion}  />
      </>
    );

}

export default Pagos;
