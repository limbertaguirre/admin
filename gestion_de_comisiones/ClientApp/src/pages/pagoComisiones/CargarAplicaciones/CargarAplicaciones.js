import React, {useEffect, useState}  from 'react';

import {Container,Tooltip ,Zoom, Chip, InputAdornment,Card, Button,
   Grid, TextField, Typography,FormControl,
    InputLabel, Select,MenuItem, Breadcrumbs } from "@material-ui/core";
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import HomeIcon from "@material-ui/icons/Home";
import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import {requestGet, requestPost} from '../../../service/request';
import * as ActionMensaje from '../../../redux/actions/messageAction';
import GridAplicaciones from './Components/GridAplicaciones';
import SearchIcon from '@material-ui/icons/Search';
import SaveIcon from '@material-ui/icons/Save';
import DetalleDescuentoModal from './Components/DetalleDescuentoModal';
import MessageConfirm from "../../../components/mesageModal/MessageConfirm";
import SnackbarSion from "../../../components/message/SnackbarSion";


const StyledBreadcrumb = withStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.grey[100],
    height: theme.spacing(3),
    color: theme.palette.grey[800],
    fontWeight: theme.typography.fontWeightRegular,
    "&:hover, &:focus": {
      backgroundColor: theme.palette.grey[300],
    },
    "&:active": {
      boxShadow: theme.shadows[1],
      backgroundColor: emphasize(theme.palette.grey[300], 0.12),
    },
  },
}))(Chip);

const CargarAplicaciones = (props) => {

  let history = useHistory  ();
  let style= useStyles();

  const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
  const [namePage, setNamePage] = useState(""); 
  useEffect(()=>{  try{   
      setNamePage(props.location.state.namePagina);
      verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
      }catch (err) {  verificarAcceso(perfiles, 'none', history); }
  },[]);
  const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
  const dispatch = useDispatch();
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = useState("");
  const [tipoSnackbar, settipTSnackbar] = useState(true);
  const[ txtBusqueda, setTxtBusqueda]= useState('');

  const[ciclos, setCiclos]= useState([]);
  const[idCiclo, setIdCiclo]= useState(0);
  const[idCicloSelected, setIdCicloSelected]= useState(0);
  const[nameComboSeleccionado, setNameComboSeleccionado] = useState("");
  const[listaComisionesCerrados, setListaComisionesCerrados]= useState([]);
  const[statusBusqueda, setStatusBusqueda]= useState(false);
  useEffect(()=>{ 
    handleOnGetCiclos();
  },[])

   const handleOnGetCiclos=()=>{    
        const headers={usuarioLogin:userName};
        requestGet('Aplicaciones/GetCiclos',headers,dispatch).then((res)=>{             
            if(res.code === 0){                 
                 setCiclos(res.data);                            
            }else{
                dispatch(ActionMensaje.showMessage({ message: res.message, variant: "info" }));
            }    
        })   
   };

   const handleOnGetAplicaciones=()=>{    
    if(idCiclo && idCiclo !== 0){  
      setIdCicloSelected(idCiclo);     
      const data={
        usuarioLogin:userName,
        idCiclo: idCiclo
       };
      requestPost('Aplicaciones/ObtenerAplicaciones',data,dispatch).then((res)=>{           
          if(res.code === 0){  
              setListaComisionesCerrados(res.data);  
              setStatusBusqueda(true);                        
          }else{
              dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
          }    
      })   


    }else{
      setOpenSnackbar(true);
      setMensajeSnackbar('¡Debe Seleccionar un permiso!');
      settipTSnackbar('warning');
    }
    
  }
  const seleccionarNombreCombo = (nombre)=>{
    setNameComboSeleccionado(nombre);
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

  //detalle
  const[openDetalle, setOpenDetalle] = useState(false);
  const[ficha, setFicha]= useState({idFicla:0, nombreFicha:'', rango:'', ciclo:'',idCiclo:0,avatar:null  });
   const[listaDetalleAplicaciones, setListaDetalleAplicaciones]= useState([]);
   const[idComisionDetalleSelected, setIidComisionDetalleSelected]= useState(0);
  const CerrarDetalleModal =()=> {
    setOpenDetalle(false);
  }

  //grid
  const selecionarDetalleFrelances = (comisionDetalleId)=>{
      CargarDetalleFrelancers(userName, comisionDetalleId )
  }
  const CargarDetalleFrelancers =(nombreUsuario,  idDetalleComision)=>{
    if(idCiclo && idCiclo !== 0){       
      const data={
        usuarioLogin:nombreUsuario,
        idComisionDetalle: parseInt(idDetalleComision)

       };
      requestPost('Aplicaciones/ListarDetalleAplicacionesXFreelancer',data,dispatch).then((res)=>{                 
          if(res.code === 0){  
             setFicha({idFicla:res.data.idFicla, nombreFicha:res.data.nombreFicha, rango:res.data.rango, ciclo: res.data.ciclo,idCiclo:res.data.idCiclo,avatar:res.data.avatar });
             setListaDetalleAplicaciones(res.data.listAplicaciones)
             setOpenDetalle(true)
             setIidComisionDetalleSelected(idDetalleComision);                        
          }else{
              setIidComisionDetalleSelected(0); 
              dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
          }    
      })   
    }else{
      setOpenSnackbar(true);
      setMensajeSnackbar('¡Debe Seleccionar un ciclo!');
      settipTSnackbar('warning');
    }
    
  }

  const buscarFreelanzer=()=>{
    console.log('enter', txtBusqueda);
    if(txtBusqueda.length > 4){
          if(idCiclo && idCiclo !== 0){
                  const data={
                    usuarioLogin:userName,
                    idCiclo: idCiclo,
                    nombreCriterio:txtBusqueda

                  };
                  requestPost('Aplicaciones/BuscarComisionCerradosXCarnet',data,dispatch).then((res)=>{                 
                      if(res.code === 0){  
                          setListaComisionesCerrados(res.data);            
                      }else{                        
                          dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
                      }    
                  }) 
            }else{

            }  
      }
      
  }
    const[openConfirm, setOpenConfirm]= useState(false);

    const  CerrarAplicacion =()=>{
      if(idCicloSelected !== 0 && idCiclo !== 0){ 
        setOpenConfirm(true);
       }else{
        setOpenSnackbar(true);
        setMensajeSnackbar('¡Debe tener Seleccionado el ciclo para su cierre!');
        settipTSnackbar('warning');

       }


    }
    const CancelarConfirm=()=>{
       setOpenConfirm(false);
    } 
    const AceptarConfirm=()=>{
      if(idCiclo && idCiclo !== 0){ 
        const data={
          usuarioLogin:userName,
          usuarioId: idUsuario,
          idCiclo:idCiclo
        };
        requestPost('Aplicaciones/CerrarAplicacion',data,dispatch).then((res)=>{                 
            if(res.code === 0){  
                setOpenConfirm(false);  
                setListaComisionesCerrados([]);
                setCiclos([]);
                setStatusBusqueda(false); 
                handleOnGetCiclos();
            }else{              
                dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
            }    
        })   
      }else{
        setOpenSnackbar(true);
        setMensajeSnackbar('¡Debe tener Seleccionado el ciclo para el cierre de aplicación!');
        settipTSnackbar('warning');
      }

    }


  return (
    <>
         <Container maxWidth="xl" >
      <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
              <Breadcrumbs aria-label="breadcrumb">
                        <StyledBreadcrumb key={1} component="a" label="Gestión de pagos"icon={<HomeIcon fontSize="small" />}  />
                        <StyledBreadcrumb key={2} component="a" label="Pago de comisiones"  />
                        <StyledBreadcrumb key={3} label="Cargar Aplicaciones"  onClick={handleClick}/>
              </Breadcrumbs>
      </div>
      <br/>
      <Typography variant="h4" gutterBottom  >
             {'Cargar Aplicaciones'}
           </Typography>
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
                          onClick = {()=> CerrarAplicacion()}                                         
                          >
                            <SaveIcon style={{marginRight:'5px'}} /> CERRAR APLICACIÓN
                          </Button> 
                          :
                            <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin Acceso'}>
                              <Button variant="contained"  > <SaveIcon style={{marginRight:'5px'}} /> CERRAR APLICACIÓN </Button> 
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
       <br />
       <SnackbarSion open={openSnackbar} closeSnackbar={closeSnackbar} tipo={tipoSnackbar} duracion={2000} mensaje={mensajeSnackbar}  /> 
      <GridAplicaciones aplicacionesList={listaComisionesCerrados} selecionarDetalleFrelances={selecionarDetalleFrelances} />
      <DetalleDescuentoModal  namePage={namePage} open={openDetalle} handleCloseCancel={CerrarDetalleModal} ficha={ficha} listaAplicaciones={listaDetalleAplicaciones} idComisionDetalleSelected={idComisionDetalleSelected} CargarDetalleFrelancers={CargarDetalleFrelancers} handleOnGetAplicaciones={handleOnGetAplicaciones} />
      <MessageConfirm open={openConfirm} titulo={'CERRAR APLICACIÓN'} subTituloModal={'¿Estás seguro de cerrar la Aplicacion del CICLO ' + nameComboSeleccionado.toUpperCase()+  '?'} tipoModal={'success'} mensaje={'Una vez cerrado el ciclo de facturación no podrá editar.'} handleCloseConfirm={AceptarConfirm} handleCloseCancel={CancelarConfirm}  />

      </Container>  
    </>
  );
};
export default CargarAplicaciones;

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

