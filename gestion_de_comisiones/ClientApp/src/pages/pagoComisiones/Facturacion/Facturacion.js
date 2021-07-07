import React, {useEffect, useState}  from 'react';
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';
import {Container, InputAdornment,Tooltip ,Zoom, Dialog,Card, DialogContent, Button, Grid, TextField, Typography, FormGroup, FormControlLabel,Checkbox,FormControl, InputLabel, Select, FormHelperText,MenuItem } from "@material-ui/core";
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import SnackbarSion from "../../../components/message/SnackbarSion";
import * as ActionMesaje from "../../../redux/actions/messageAction";

import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import { requestPost, requestGet } from "../../../service/request";
import SearchIcon from '@material-ui/icons/Search';
import SaveIcon from '@material-ui/icons/Save';

import GridComisiones from './Component/GridComisiones';
import DetalleAdjuntoModal from './Component/DetalleAdjuntoModal';
import MessageConfirm from "../../../components/mesageModal/MessageConfirm";

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


 const Facturacion =(props)=> {    
     
  let history = useHistory();
  let style= useStyles();
  const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
  const [namePage, setNamePage] = useState(""); 
  useEffect(()=>{  try{  
     setNamePage(props.location.state.namePagina);
     verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
     }catch (err) {  verificarAcceso(perfiles, 'none', history); }
  },[])
  const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
  const dispatch = useDispatch();
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = useState("");
  const [tipoSnackbar, settipTSnackbar] = useState(true);

  const[ciclos, setCiclos]= useState([]);
  const[idCiclo, setIdCiclo]= useState(0);
  const[idCicloSelected, setIdCicloSelected]= useState(0);
  const[listaComisionesPendientes, setListaComisionesPendientes]= useState([]);
  const [txtBusqueda, setTxtBusqueda] = useState("");
  const[idDetalleComisionSelect, setIdDetalleComisionSelect ]= useState(0);

   const [estadoComisionGlobalFacturado, setEstadoComisionGlobalFacturado]= useState(false);
   const[Ficha, setFicha]= useState({idFicla:0, nombreFicha:'', rango:'', ciclo:'',idCiclo:0,avatar:null  });
   const[listaDetalleEmpresa, setListaDetalleEmpresa]= useState([]);

   const [idComsionDetalleSelected, setiIdComsionDetalleSelected ]= useState(0);

   useEffect(()=>{
     //console.log('idcomision selected: ', idComsionDetalleSelected);
   },[idComsionDetalleSelected])
  const [open, setOpen] =useState(false);
  useEffect(()=>{  
    obtenerCiclos();
  },[]);

  const obtenerCiclos=()=>{
    const data={usuarioLogin:userName };
     requestGet('Factura/ObtenerCiclos',data,dispatch).then((res)=>{ 
     // console.log('ciclos : ', res);
          if(res.code === 0){                 
            setCiclos(res.data);
          }

        })    
   };
   const onChange= (e) => {
        const texfiel = e.target.name;
        const value = e.target.value;
        if (texfiel === "idCiclo") {
            setIdCiclo(value);
        }
        if (texfiel === "txtBusqueda") {
             setTxtBusqueda(value);
        }
  };

    const cargarComisiones=()=>{
      if(idCiclo != 0){
         setIdCicloSelected(idCiclo);
         obtenerComisiones(userName, idCiclo);
      }else{
        setOpenSnackbar(true);
        setMensajeSnackbar('¡Debe Seleccionar un permiso!');
        settipTSnackbar('warning');
      }
      
    }

    function handleClick(event) {
        event.preventDefault();
    }

    const closeSnackbar= (event, reason) => {
      if (reason === 'clickaway') {
        return;
      }
      setOpenSnackbar(false);
    };
    const obtenerComisiones=(user,IDciclo)=>{
      const data={
        usuarioLogin:user,
        idCiclo: IDciclo
       };
       requestPost('Factura/ListarComisionesPendientes',data,dispatch).then((res)=>{ 
        console.log('comisones : ', res);
            if(res.code === 0){                 
              setListaComisionesPendientes(res.data);
            }
          })    
     };
     const ApiBuscarPorNombre=(user,IDciclo,nombreCriterio )=>{
      const data={
        usuarioLogin:user,
        idCiclo: IDciclo,
        nombreCriterio:nombreCriterio
       };
       requestPost('Factura/BuscarComisionNombre',data,dispatch).then((res)=>{ 
      //  console.log('search  : ', res);
            if(res.code === 0){                 
              setListaComisionesPendientes(res.data);
            }
          })    
     };
     const ApiCargarComisionesDetalleEmpresa=(user,idcomisionDetalle )=>{
      const data={
        usuarioLogin:user,
        idComisionDetalleEmpresa:parseInt(idcomisionDetalle)
       };
      // console.log('parame detalle  : ', data);
       requestPost('Factura/ComisionesDetalleEmpresa',data,dispatch).then((res)=>{ 
       console.log('detalle  : ', res);
            if(res.code === 0){         
              setFicha({idFicla:res.data.idFicla, nombreFicha:res.data.nombreFicha, rango:res.data.rango, ciclo: res.data.ciclo,idCiclo:res.data.idCiclo,avatar:res.data.avatar });
              setListaDetalleEmpresa(res.data.listDetalle);
              setIdDetalleComisionSelect(idcomisionDetalle);
              setOpen(true);
            }
          })    
     };
     const guardarFactura=()=>{

     }
     const [openModalConfiCerrarFactura,setOpenModalConfiCerrarFactura]= useState(false);
     const CerrarFactura=()=>{
        if(idCicloSelected != 0){            
          setOpenModalConfiCerrarFactura(true); 
        }else{
            setOpenSnackbar(true);
            setMensajeSnackbar('¡Debe tener Seleccionado el ciclo para su cierre!');
            settipTSnackbar('warning');
        }      
     }
     const cerrarModalCierre =()=>{
      setOpenModalConfiCerrarFactura(false);
     }

     const condirmarCierreFacturar=()=>{
      setOpenModalConfiCerrarFactura(false);
      ApiCerrarFactura(userName,idUsuario, idCiclo)
       
     
     }


    const buscarClientepornombre=(ev)=>{
     // console.log('enter');
     // if(txtBusqueda === ""){
        ApiBuscarPorNombre(userName, idCiclo,txtBusqueda)
     // }
      
    }

  const selecionarDetalleFrelances=(idDetalleComision, estadoFacturado)=>{
      setEstadoComisionGlobalFacturado(estadoFacturado== 2? true: false);//1 => pendiente, 2 facturado=>, 0 no tiene estadoo no se actualizo
      setiIdComsionDetalleSelected(idDetalleComision);
      ApiCargarComisionesDetalleEmpresa(userName,idDetalleComision );
  }
   const handleCloseConfirm=()=>{
     console.log('fin select => iddetalleComision: ',idComsionDetalleSelected );
     ApiFacturarDetalleComision(userName,idComsionDetalleSelected,idUsuario );   
   }
   const handleCloseCancel=()=>{
      setOpen(false);
   }
   const ApiFacturarDetalleComision=(user,idcomisionDetalle,userId )=>{
    const data={
      usuarioLogin:user,
      idComisionDetalle:parseInt(idcomisionDetalle),
      usuarioId:userId
     };
    // console.log('parame detalle  : ', data);
     requestPost('Factura/FacturarComisionDetalle',data,dispatch).then((res)=>{ 
     console.log('ACTUALIZAR COMI DETALL  : ', res);
          if(res.code === 0){         
            setOpen(false);
            if(idCiclo != 0){
              obtenerComisiones(userName, idCiclo);
            }
            
          }else{
            dispatch(ActionMesaje.showMessage({ message: res.message, variant: "error" }));
          }

        })    
   };


   useEffect(()=>{
     //console.log('ficha', Ficha);
     //console.log('lisdetalle :', listaDetalleEmpresa);
   //  console.log('global estado comision :', estadoComisionGlobalFacturado)
   },[Ficha, listaDetalleEmpresa, estadoComisionGlobalFacturado]);

   const checkdComisionDetalleEmpresa =(idComisionDetalleEmpresa, isFacturo)=> {
     //console.log('cabezera  iddeta: ', idComsionDetalleSelected);
     //console.log('check id: ', idComisionDetalleEmpresa, ' facturo: ', isFacturo);

     const data={
      usuarioLogin:userName,
      idComisionDetalle:parseInt(idComsionDetalleSelected),
      idComisionDetalleEmpresa:parseInt(idComisionDetalleEmpresa),
      estadoDetalleEmpresa:(isFacturo === "false"),
      usuarioId:idUsuario
     };
     //console.log('parame estad detalle  : ', data);
     requestPost('Factura/ActualizarDetalleEmpresaEstado',data,dispatch).then((res)=>{ 
    // console.log('ACTUALIZAR estado : ', res);
          if(res.code === 0){     
            if(idCiclo != 0){
              
              setEstadoComisionGlobalFacturado(false);            
              obtenerComisiones(userName, idCiclo);
              ApiCargarComisionesDetalleEmpresa(userName,idComsionDetalleSelected ); //este lista el detalle empresa
           }    
           
            
          }else{
            dispatch(ActionMesaje.showMessage({ message: res.message, variant: "error" }));
          }

        })    


   }
   
   const desCheckdComisionDetalleEmpresa =(idComisionDetalleEmpresa, isFacturo)=> {
    //llamar api y listar empresas devuelta..
    //console.log('cabezera  iddeta: ', idComsionDetalleSelected);
   // console.log('se cancelara :',  idComisionDetalleEmpresa, ' facturo: ', isFacturo);
    ApiCambiarEstadoComisionDetalleEmpresa(userName, idComsionDetalleSelected, idComisionDetalleEmpresa, isFacturo, idUsuario );

   }
   const ApiCambiarEstadoComisionDetalleEmpresa=(user,pidcomisionDetalle, PidComisionDetalleEmpresa, pestadoDetalle,userId )=>{
    const data={
      usuarioLogin:user,
      idComisionDetalle:parseInt(pidcomisionDetalle),
      idComisionDetalleEmpresa:parseInt(PidComisionDetalleEmpresa),
      estadoDetalleEmpresa:pestadoDetalle,
      usuarioId:userId
     };
    // console.log('parame estad detalle  : ', data);
     requestPost('Factura/ActualizarDetalleEmpresaEstado',data,dispatch).then((res)=>{ 
     //console.log('ACTUALIZAR estado : ', res);
          if(res.code === 0){     
            if(idCiclo != 0){
              if(pestadoDetalle == false){
                setEstadoComisionGlobalFacturado(false);
              }              
              obtenerComisiones(userName, idCiclo);
              ApiCargarComisionesDetalleEmpresa(userName,idComsionDetalleSelected ); //este lista el detalle empresa
           }    
           
            
          }else{
            dispatch(ActionMesaje.showMessage({ message: res.message, variant: "error" }));
          }

        })    
   };
    
   const procesarPdf =(idComisionDetalleEmpresa, base64pdf )=>{
    ApiSubirPdf(userName,idComisionDetalleEmpresa,base64pdf, idUsuario );
   }

   const ApiSubirPdf=(user, PidComisionDetalleEmpresa,archivoPdf ,userId )=>{
    const data={
      usuarioLogin:user,
      idComisionDetalleEmpresa:parseInt(PidComisionDetalleEmpresa),
      archivoPdf:archivoPdf,
      usuarioId:userId
     };
     requestPost('Factura/SubirArchivoFacturaPdfEmpresa',data,dispatch).then((res)=>{ 
     console.log('ACTUALIZAR estado : ', res);
          if(res.code === 0){     
              if(idCiclo != 0){              
                ApiCargarComisionesDetalleEmpresa(userName,idComsionDetalleSelected ); 
              }               
          }else{
            dispatch(ActionMesaje.showMessage({ message: res.message, variant: "error" }));
          }

        })    
   };

   const cancelarTodo=()=>{
    console.log('cancelar todo: estado', !estadoComisionGlobalFacturado); //userName,idComsionDetalleSelected,idUsuario
    console.log('comision seleccionado: ', idComsionDetalleSelected);
    setEstadoComisionGlobalFacturado(!estadoComisionGlobalFacturado);
    ApiTAplicarTodoFactura(userName,idComsionDetalleSelected, idUsuario,!estadoComisionGlobalFacturado );

   }
   const AceptarTodo=()=>{
    console.log('aceptar todo: ');
    console.log('aceptar todo: estado', !estadoComisionGlobalFacturado); //userName,idComsionDetalleSelected,idUsuario
    console.log('comision seleccionado: ', idComsionDetalleSelected);
    setEstadoComisionGlobalFacturado(!estadoComisionGlobalFacturado);
   ApiTAplicarTodoFactura(userName,idComsionDetalleSelected, idUsuario,!estadoComisionGlobalFacturado );
   }

   const ApiTAplicarTodoFactura=(user,idcomisionDetalle,userId, estadoFacturado )=>{
    const data={
      usuarioLogin:user,
      idComisionDetalle:parseInt(idcomisionDetalle),
      estadoFacturado:estadoFacturado,
      usuarioId:userId
     };
    // console.log('parame detalle  : ', data);
     requestPost('Factura/AplicarFacturaTodoEstado',data,dispatch).then((res)=>{ 
     console.log('ACTUALIZAR COMI DETALL  : ', res);
          if(res.code === 0){                    
            if(idCiclo != 0){
              
              obtenerComisiones(userName, idCiclo);
              ApiCargarComisionesDetalleEmpresa(userName,idComsionDetalleSelected );
            }
            
          }else{
            dispatch(ActionMesaje.showMessage({ message: res.message, variant: "error" }));
          }

        })    
   };
   const ApiCerrarFactura=(user,userId, cicloId )=>{
    const data={
      usuarioLogin:user,
      idCiclo:cicloId,
      usuarioId:userId
     };
     requestPost('Factura/CerrarFactura',data,dispatch).then((res)=>{ 
     console.log('ACTUALIZAR COMI DETALL  : ', res);
          if(res.code === 0){                    
            dispatch(ActionMesaje.showMessage({ message: res.message, variant: "success" }));
            
          }else{
            dispatch(ActionMesaje.showMessage({ message: res.message, variant: "error" }));
          }

        })    
   };



    return (
      <>  
      <Container maxWidth="xl" >
           <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
              <Breadcrumbs aria-label="breadcrumb">
                        <StyledBreadcrumb key={1} component="a" label="Gestion de pagos"icon={<HomeIcon fontSize="small" />}  />
                        <StyledBreadcrumb key={2} component="a" label="Pago de comisiones"  />
                        <StyledBreadcrumb key={3} label="Facturacion"  onClick={handleClick}/>
              </Breadcrumbs>
           </div>
           <br/>
           <Typography variant="h4" gutterBottom className={style.etiqueta} >
             {'Facturacion'}
           </Typography>
           <Card> 
                <Grid container className={style.gridContainer}> 
                  <Grid item xs={12} md={3} className={style.containerSave} >
                     {listaComisionesPendientes.length>0&&
                       <>
                         {validarPermiso(perfiles, props.location.state.namePagina + permiso.ACTUALIZAR)?
                           <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            className={style.submitSAVE}
                            onClick = {()=> CerrarFactura()}                                         
                            >
                             <SaveIcon style={{marginRight:'5px'}} /> CERRAR FACTURA
                            </Button> 
                            :
                              <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin Acceso'}>
                                <Button variant="contained"  > <SaveIcon style={{marginRight:'5px'}} /> CERRAR FACTURA </Button> 
                              </Tooltip>
                            }
                        </>
                       }     
                  </Grid>
                  <Grid item xs={12} md={4} className={style.containerSave}>
                  {listaComisionesPendientes.length>0&&
                        <TextField
                          label="BUSCAR CLIENTE"
                          type={'text'}
                          variant="outlined"
                          placeholder={'Buscar cliente...'}
                          name="txtBusqueda"                    
                          value={txtBusqueda}
                          onChange={onChange}
                          fullWidth
                          onKeyPress={(ev) => {
                            if (ev.key === 'Enter') {
                              buscarClientepornombre();
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
                                className={style.TextFiel}
                                >
                                  <InputLabel id="demo-simple-select-outlined-labelciclo">CICLO # </InputLabel>
                                  <Select
                                      labelId="demo-simple-select-outlined-labelciclo"
                                      id="demo-simple-select-outlined"
                                      value={idCiclo}
                                      name="idCiclo"
                                      onChange={onChange}
                                      label="CICLO # "
                                      >
                                      <MenuItem value={0}>
                                          <em>Seleccione un ciclo</em>
                                      </MenuItem>
                                      {ciclos.map((value,index)=> ( <MenuItem key={index} value={value.idCiclo}>{value.nombre}</MenuItem> ))}  
                                  </Select>                               
                              </FormControl>
                    </Grid>
                    <Grid item  xs={12} md={2} className={style.containerCargar}  >
                            <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={style.submitCargar}
                            onClick = {()=> cargarComisiones()}                                         
                            >
                             {'CARGAR '} <CloudUploadIcon style={{marginLeft:'5px'}} />
                            </Button>   
                    </Grid>
                  </Grid>
                </Card>
            <br />           
            <GridComisiones listaComisionesPendientes={listaComisionesPendientes} selecionarDetalleFrelances={selecionarDetalleFrelances} />
            <SnackbarSion open={openSnackbar} closeSnackbar={closeSnackbar} tipo={tipoSnackbar} duracion={2000} mensaje={mensajeSnackbar} txtBusqueda={txtBusqueda} />   
            <DetalleAdjuntoModal namePage={namePage} open={open} handleCloseConfirm={handleCloseConfirm} handleCloseCancel={handleCloseCancel} Ficha={Ficha} listaDetalleEmpresa={listaDetalleEmpresa} estadoComisionGlobalFacturado={estadoComisionGlobalFacturado} checkdComisionDetalleEmpresa={checkdComisionDetalleEmpresa} desCheckdComisionDetalleEmpresa={desCheckdComisionDetalleEmpresa} procesarPdf={procesarPdf} cancelarTodo={cancelarTodo} AceptarTodo={AceptarTodo} />
            <MessageConfirm open={openModalConfiCerrarFactura} titulo={'CERRAR FACTURACION'} subTituloModal={'¿Estás seguro de cerrar la facturación del CICLO ENERO 2021?'} tipoModal={'success'} mensaje={'Una vez cerrado el ciclo de facturación no podrá editar.'} handleCloseConfirm={condirmarCierreFacturar} handleCloseCancel={cerrarModalCierre}  />
      </Container>    
      </>
    );

}
export default Facturacion;