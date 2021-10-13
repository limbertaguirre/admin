import React, {useState, useEffect}  from 'react';
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import {Container,Tooltip ,Zoom, Chip, InputAdornment,Card, Button,
  Grid, TextField, Typography,FormControl,
   InputLabel, Select,MenuItem, Breadcrumbs } from "@material-ui/core";
import HomeIcon from "@material-ui/icons/Home";
import SearchIcon from '@material-ui/icons/Search';
import SaveIcon from '@material-ui/icons/Save';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import {requestGet, requestPost} from '../../../service/request';
import * as ActionMensaje from '../../../redux/actions/messageAction';
import SnackbarSion from "../../../components/message/SnackbarSion";
import GridFormaPagos from './Components/GridFormaPagos';
import TipoPagosModal from './Components/TipoPagosModal';
import VistaListaAutorizados from './Components/VistaListaAutorizados';
import * as Actions from '../../../redux/actions/FormaPagosAction';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';
import HelpOutlineIcon from '@material-ui/icons/HelpOutline';
import  imageFac from "../../../../src/assets/img/pendiente.png";
import ConfirmarCierrePagoModal from "./Components/ConfirmarCierrePagoModal";

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
     submitPendiente: {                 
      background: "#E29020", 
      boxShadow: '2px 4px 5px #1872b8',
      color:'white',
     },
     submitAprobado: {                 
      background: "#197608", 
      boxShadow: '2px 4px 5px #1872b8',
      color:'white',
     },
     submitSAVE: {                 
      background: "#1872b8", 
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
  
  
  }));


 const FormaPago =(props)=> {
    let history = useHistory();
    const dispatch=useDispatch();
    const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
    const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
    useEffect(()=>{  try{  
       verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
       }catch (err) {  verificarAcceso(perfiles, 'none', history); }
    },[])
    const style= useStyles();

    const[ciclos, setCiclos]= useState([]);
    const[idCiclo, setIdCiclo]= useState(0);
    const[idCicloSelected, setIdCicloSelected]= useState(0);
    const[nameComboSeleccionado, setNameComboSeleccionado] = useState("");
    const[listaComisionesAPagar, setListaComisionesAPagar]= useState([]);
    const[statusBusqueda, setStatusBusqueda]= useState(false);

    const [openSnackbar, setOpenSnackbar] = useState(false);
    const [mensajeSnackbar, setMensajeSnackbar] = useState("");
    const [tipoSnackbar, settipTSnackbar] = useState(true);
    const[ txtBusqueda, setTxtBusqueda]= useState('');

    const [openTipoPago, setTipoPago]= useState(false);
    const [listTipoPagos, setListTipoPagos] = useState([]);
    const [idcomisionDetalleSelect, setIdcomisionDetalleSelect]= useState(0);
    const [idtipoPagoSelect, setIdtipoPagoSelect] = React.useState("0");

    const[openModalAutorizadores, setOpenModalAutorizadores] = useState(false);
    const [pendienteFormaPago, setPendienteFormaPago]= useState(false);


    const mensajeGenericoCiclo =()=>{
      setOpenSnackbar(true);
      setMensajeSnackbar('¡Debe Seleccionar un ciclo!');
      settipTSnackbar('warning');
    }

    useEffect(()=>{ 
      handleOnGetCiclos();
    },[])
  
     const handleOnGetCiclos=()=>{    
          const headers={usuarioLogin:userName};
          requestGet('Pagos/GetCiclos',headers,dispatch).then((res)=>{             
              if(res.code === 0){                 
                   setCiclos(res.data);                            
              }else{
                   dispatch(ActionMensaje.showMessage({ message: res.message, variant: "info" }));
              }    
          })   
     };
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

    const handleOnGetAplicaciones=()=>{         
      if(idCiclo && idCiclo !== 0){  
          setIdCicloSelected(idCiclo);     
          const data={
            usuarioLogin:userName,
            idCiclo: idCiclo
          };
          requestPost('Pagos/ObtenerFormasPagos',data,dispatch).then((res)=>{  
           // console.log('orrrrrrrrrrrrr', res.data.lista);         
              if(res.code === 0){  
                let data= res.data;
                  setPendienteFormaPago(data.pendienteFormaPago);
                  setListaComisionesAPagar(data.lista);  
                  setStatusBusqueda(true);    
                  ApiVerificarAutorizador(userName,idCiclo,idUsuario, dispatch);               
              }else{
                  dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
              }    
          })   
  
      }else{
        setOpenSnackbar(true);
        setMensajeSnackbar('¡Debe Seleccionar un ciclo!');
        settipTSnackbar('warning');
      }      
    }
    const closeSnackbar= (event, reason) => {
      if (reason === 'clickaway') {
        return;
      }
      setOpenSnackbar(false);
    };



    const selecionarDetalleFrelances = (comisionDetalleId, ciSeleccionado,idTipoPago)=>{     
      setIdtipoPagoSelect(String(idTipoPago));   
        listarTiposPagos(ciSeleccionado);
        setIdcomisionDetalleSelect(comisionDetalleId)
    }
    const cerrarModalTipoPagoModal =()=>{
      setTipoPago(false);
      setIdtipoPagoSelect("0");
    }

    async function listarTiposPagos(ciSeleccionado){      
      let respuesta = await Actions.listarFormaPagos(userName, ciSeleccionado, idCiclo , dispatch);
      if(respuesta && respuesta.code == 0){ 
        setListTipoPagos(respuesta.data);
        setTipoPago(true);
      }else{
         dispatch(ActionMensaje.showMessage({ message: respuesta.message , variant: "error" }));
      }
    }

    useEffect(()=>{

    },[listTipoPagos, idcomisionDetalleSelect, listaComisionesAPagar]);
    

    const handleChangeRadio = (event) => {     
        setIdtipoPagoSelect(event.target.value);
    };
    const confirmarTipoPago= ()=>{      
      funcionConfirmarTipoPago();
    }

    async function funcionConfirmarTipoPago(){
     if (idcomisionDetalleSelect != 0){     
          let response= await Actions.aplicarFormaPago(userName, idcomisionDetalleSelect, idtipoPagoSelect,idUsuario,idCiclo, dispatch)             
          if(response && response.code == 0){
              setTipoPago(false);
              setIdtipoPagoSelect("0");
              handleOnGetAplicaciones();
          }else{
            dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
          }
      }
    }

    const buscarFreelanzer=()=>{
    // console.log('texto q se :',txtBusqueda);
     buscarFrelancerPorCi();
    }

    async function buscarFrelancerPorCi(){         
           let response= await Actions.buscarPorCarnetFormaPago(userName, idCiclo, txtBusqueda, dispatch)               
           if(response && response.code == 0){
               let data= response.data;
               setPendienteFormaPago(data.pendienteFormaPago);
               setListaComisionesAPagar(data.lista);                 
           }       
     }

    const seleccionarTipoFiltroBusqueda=(idTipoFormaPago)=>{
      //console.log("listo tipo :", idTipoFormaPago)
      filtrarComisionPorFormaPago(idTipoFormaPago)
    }
    async function filtrarComisionPorFormaPago(idTipoFormaPago){
      if(idCiclo && idCiclo !== 0){  
          let response= await Actions.ListarComisionFormaPagoFiltrada(userName, idCiclo, idTipoFormaPago, dispatch)   
          //console.log('busqueda por filtro',response)          
          if(response && response.code == 0){
              let data= response.data;
              setPendienteFormaPago(data.pendienteFormaPago);
              setListaComisionesAPagar(data.lista);  
          }       
      }else{
          mensajeGenericoCiclo();
      }    
   }

    const [autorizadorObjeto, setAutorizadorObjeto ]=useState({autorizador:false,comisionAutorizada:false,idciclo:0, idComision:0,idAutorizacionComision:0,autorizadores:[] })
   async function ApiVerificarAutorizador(user,cicloId, idUser, dispatch){      
    let respuesta = await Actions.VerificarAutorizadorComision(user, cicloId,idUser, dispatch);      
    if(respuesta && respuesta.code == 0){ 
      setAutorizadorObjeto(respuesta.data);
      if(respuesta.data.autorizador == true){
        setOpenModalAutorizadores(true); //abrir modal visualizaciones
      }
    }else{
      setAutorizadorObjeto({autorizador:false,comisionAutorizada:false,idciclo:0, idComision:0, idAutorizacionComision:0, autorizadores:[] });
    }    
    
  }
  
  const cerrarModalListaAutorizadosConfirm = ()=>{
        setOpenModalAutorizadores(false);
  }
  const confirmarModalAutorizacion =(idComision, idAutorizacionComision)=>{
   // console.log('confirmar : ',idComision, idAutorizacionComision)      
       ApiConfirmarAutorizacion(userName, idUsuario,idCiclo, idComision, idAutorizacionComision)
  }
  async function ApiConfirmarAutorizacion(userNa, idUser,idCiclo, idComision, idAutorizacionComision){
    if(idCiclo && idCiclo !== 0){  
        let response= await Actions.ConfirmarAutorizacion(userNa, idUser,idCiclo, idComision,idAutorizacionComision, dispatch)   
       // console.log('response confirm',response);          
        if(response && response.code == 0){
              let data= response.data;
               setOpenModalAutorizadores(false);
               setPendienteFormaPago(data.pendienteFormaPago);
               dispatch(ActionMensaje.showMessage({ message: response.message , variant: "success" }));
               //api recarga el estado y lista de autorizaciones
              ApiVerificarAutorizador(userName,idCiclo,idUsuario, dispatch); 
              //cerrar el modal
            //  setOpenModalAutorizadores(false)
        }else{
              dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
        }  
    }else{
        mensajeGenericoCiclo();
    }    
 }

  useEffect(()=>{
    
  },[autorizadorObjeto])

   const[openCierrePagoModal, setOpenCierrePagoModal] = useState(false);
   const[habilitadoCierrePago, setHabilitadoCierrePago]= useState(false);
   const[listadoConfirm,setListadoConfirm ]= useState([]);

  const verificarConfirmarFomaPago =()=>{
    ApiVerificarConfirmarFormaPago(userName, idUsuario,idCiclo);
  }
  async function ApiVerificarConfirmarFormaPago(userNa, idUser,idCICLO){
      if(idCICLO && idCICLO !== 0){  
          let response= await Actions.VerificarCierreFormaPago(userNa, idUser,idCICLO, dispatch)   
          console.log('respo : ', response);
          if(response && response.code == 0){
            let data= response.data;
            setOpenCierrePagoModal(true);
            setHabilitadoCierrePago(data.habilitado);
            setListadoConfirm(data.listado);
          }else{
              dispatch(ActionMensaje.showMessage({ message: response.message , variant: "error" }));
          }

      }else{
          mensajeGenericoCiclo();
      }    
    }
    const cancelarModalConfirmarCierre =()=>{
       setOpenCierrePagoModal(false);
    }
    const confirmarCierrePagoModal =()=>{
       setOpenCierrePagoModal(false);
    }

    return (
      <>
          <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}>
              <Breadcrumbs aria-label="breadcrumb">
                        <StyledBreadcrumb key={1} component="a" label="Gestión de pagos"icon={<HomeIcon fontSize="small" />}  />
                        <StyledBreadcrumb key={2} component="a" label="Pago de comisiones"  />
                        <StyledBreadcrumb key={3} label="Forma de pagos" />
              </Breadcrumbs>
           </div>           
           <br/>
           <Typography variant="h4" gutterBottom  >
             {'Forma de pagos'}
           </Typography>    
           <Grid  container item xs={12}  justify="flex-end">
                  {pendienteFormaPago&&
                    <img src={imageFac} alt={'sion'} style={{width:'100px'}} />
                   }
           </Grid>
            {autorizadorObjeto.autorizador&&
           <Card>             
              <Grid container className={style.gridContainer} >         
                  <Grid item  xs={12} md={2}  className={style.containerCargar} >
                    {autorizadorObjeto.comisionAutorizada?
                          <Button
                          type="submit"
                          fullWidth
                          variant="contained"
                         // color="primary"
                          className={style.submitAprobado}
                         // onClick = {()=> handleOnGetAplicaciones()}                                         
                          > 
                            <>{'PAGO APROBADO '}<CheckCircleOutlineIcon /> </>
                          </Button> 
                    : 
                        <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        //color="primary"
                        className={style.submitPendiente}
                      // onClick = {()=> handleOnGetAplicaciones()}                                         
                        > 
                         <>{'PENDIENTE APROBACION '}<HelpOutlineIcon /> </>
                        </Button> 
                    } 
                    </Grid>
                </Grid>
            </Card>
            }

           <Card>
           <Grid container className={style.gridContainer} >
           <Grid item xs={12} md={3} className={style.containerSave} >
                    {statusBusqueda&&
                      <>
                        {validarPermiso(perfiles, props.location.state.namePagina + permiso.CREAR)?
                          <Button
                          type="submit"
                          variant="contained"
                          color="primary"
                          className={style.submitSAVE}
                          onClick = {()=> verificarConfirmarFomaPago()}                                         
                          >
                            <SaveIcon style={{marginRight:'5px'}} /> CERRAR FORMA PAGO
                          </Button> 
                          :
                            <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin Acceso'}>
                              <Button variant="contained"   onClick = {()=> verificarConfirmarFomaPago()} > <SaveIcon style={{marginRight:'5px'}} /> CERRAR FORMA PAGO</Button> 
                            </Tooltip> 
                         }
                      </> 
                   }    
                  </Grid>
                  <Grid item xs={12} md={4} className={style.containerSave}>
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
                                    {ciclos.map((value,index)=> ( <MenuItem key={index} onClick={()=> seleccionarNombreCombo(`${value.nombre}`)} value={value.idCiclo}>{value.nombre}</MenuItem> ))}  
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
                          onClick = {()=> handleOnGetAplicaciones()}                                         
                          > 
                            {'CARGAR '} <CloudUploadIcon style={{marginLeft:'12px'}} />
                          </Button>   
                    </Grid>
              </Grid>
            </Card>

            <SnackbarSion open={openSnackbar} closeSnackbar={closeSnackbar} tipo={tipoSnackbar} duracion={2000} mensaje={mensajeSnackbar}  /> 
            <GridFormaPagos listaComisionesAPagar={listaComisionesAPagar} selecionarDetalleFrelances={selecionarDetalleFrelances} seleccionarTipoFiltroBusqueda={seleccionarTipoFiltroBusqueda} idCiclo={idCiclo} pendienteFormaPago={pendienteFormaPago} permisoActualizar={validarPermiso(perfiles, props.location.state.namePagina + permiso.ACTUALIZAR)} permisoCrear={validarPermiso(perfiles, props.location.state.namePagina + permiso.CREAR)} />
            <TipoPagosModal open={openTipoPago} closeHandelModal={cerrarModalTipoPagoModal} confirmarTipoPago={confirmarTipoPago} listTipoPagos={listTipoPagos} idtipoPagoSelect={idtipoPagoSelect} handleChangeRadio={handleChangeRadio}  />  
            <VistaListaAutorizados open={openModalAutorizadores} objList={autorizadorObjeto} nameComboSeleccionado={nameComboSeleccionado} closeHandelModal={cerrarModalListaAutorizadosConfirm} confirmarModalAutorizacion={confirmarModalAutorizacion} />
            <ConfirmarCierrePagoModal open={openCierrePagoModal} closeHandelModal={cancelarModalConfirmarCierre} confirmarPago={confirmarCierrePagoModal} listado={listadoConfirm} habilidado={habilitadoCierrePago} />

      </>
    );

}

export default FormaPago;