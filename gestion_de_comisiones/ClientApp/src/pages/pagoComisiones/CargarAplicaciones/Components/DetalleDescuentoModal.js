import React, { Fragment, useState, useEffect } from "react";
import { Button, Dialog, Typography,Grid, Container, Tooltip ,Zoom, Card } from "@material-ui/core";
import {useSelector,useDispatch} from 'react-redux';
import { requestPost, requestGet } from "../../../../service/request";
import * as permiso from '../../../../routes/permiso'; 
import * as utilidad from '../../../../lib/utility'; 
import {  validarPermiso} from '../../../../lib/accesosPerfiles';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import Slide from '@material-ui/core/Slide';
import Avatar from '@material-ui/core/Avatar';
import { blue  } from '@material-ui/core/colors';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import NuevoDescuentoModal from './NuevoDescuentoModal';
import SnackbarSion from "../../../../components/message/SnackbarSion";
import * as ActionMensaje from '../../../../redux/actions/messageAction';

const useStyles = makeStyles((theme) => ({
    root: {
      width: '100%',
      '& > * + *': {
        //marginTop: theme.spacing(2),
      },
    },
    containerPrincipal:{
      paddingLeft:theme.spacing(1),
      paddingRight:theme.spacing(1),
      paddingTop:theme.spacing(4),
      paddingBottom:theme.spacing(2),
    },
    containerGrid:{
      paddingLeft:theme.spacing(6),
      paddingRight:theme.spacing(6),
      paddingTop:theme.spacing(4),
      paddingBottom:theme.spacing(2),
    },
    botones:{
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',  
        marginBottom:theme.spacing(1),
        marginTop:theme.spacing(1),
        marginRight:theme.spacing(1),
        marginLeft:theme.spacing(1),
    },
    appBar: {
        position: 'relative',
        backgroundColor: '#1872b8',
      },
      title: {
        marginLeft: theme.spacing(2),
        flex: 1,
      },
      avatarNombre: {
        color: theme.palette.getContrastText(blue[500]),
        backgroundColor: '#1872b8',
       // boxShadow: '2px 4px 5px #1872b8',
        width: theme.spacing(15),
        height: theme.spacing(15),
      },
      gridContainer:{
        paddingLeft:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingTop:theme.spacing(1),
        paddingBottom:theme.spacing(1),
      },
      submitCargar: {                 
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',
       },
       containerPhoto:{
        display:'flex',
        alignItems:'center',
        justifyContent:'center', 
      },
      table: {
        minWidth: 650,
      },
      submitDetalle: {
        height:'25px',
        background: "#1872b8", 
        boxShadow: '2px 2px 5px #1872b8',
        color:'white'
      },

      margin: {
        margin: '1px',
        paddingLeft:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingTop:theme.spacing(1),
        paddingBottom:theme.spacing(1),
      },
      divCargar: {
        border:'2px', 
       // backgroundColor:'#CAD8DF', 
        borderRadius:'9px',     
        paddingLeft:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingTop:theme.spacing(1),
        paddingBottom:theme.spacing(1),  
        fontWeight: theme.typography.fontWeightRegular,
        '&:hover, &:focus': {
          backgroundColor:'#EEEFF0',
        },
        '&:active': {
          boxShadow: theme.shadows[1],
        },
      },


  }));
    const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
    });


const DetalleDescuentoModal = (props) => {
     const classes = useStyles();
     const dispatch = useDispatch();
      const {namePage, open, handleCloseCancel, ficha, listaAplicaciones, idComisionDetalleSelected, CargarDetalleFrelancers, handleOnGetAplicaciones} = props;
      const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
      const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});     

     const[ openNewDescuento,setOpenNewDescuento ] = useState(false);
     const[subTotal, setSubTotal]= useState(0);    
     useEffect(()=>{    
              if(listaAplicaciones.length > 0){
                  let monto = 0;
                  listaAplicaciones.forEach(function (value) {
                    monto = monto + value.monto;
                  }); 
                setSubTotal(monto);
              }
     },[listaAplicaciones])

     const cerrarModal=()=>{
      setSubTotal(0);
      handleCloseCancel();
     }
     const[listTipoDescuento, setListTipoDescuento]=useState([]);;
     const [openSnackbar, setOpenSnackbar] = useState(false);
     const [mensajeSnackbar, setMensajeSnackbar] = useState("");
     const [tipoSnackbar, settipTSnackbar] = useState(true);

     const[producto, setProducto]=useState('');
     const[monto, setMonto]= useState(0);
     const[cantidad, setCantidad]= useState(1);
     const[descripcion, setDescripcion] = useState('');
     const[proyectoNombre, setProyectoNombre]= useState('');
     const[idProyecto, setIdProyecto]= useState(0); 
     const[errorProducto, setErrorProducto]=useState(false);
     const[errorMonto, setErrorMonto]= useState(false);
     const[errorCantidad, setErrorCantidad]= useState(false);
     const[errorDescripcion, setErrorDescripcion] = useState(false);

     const[idTipoDescuento, setIdTipoDescuento]= useState(0);
     const[errorIdTipoDescuento, setErrorIdTipoDescuento]= useState(false);

    const onChange= (e) => {
            const texfiel = e.target.name;
            const value = e.target.value;
            if (texfiel === "producto") {
                setProducto(value);
                setErrorProducto(!isValidProducto());
                if(idProyecto>0 && producto!== value ){
                  setIdProyecto(0);
                  setProyectoNombre('');
                }
            }
            if (texfiel === "monto") {
                setMonto(value);
                setErrorMonto(!isValidMonto());
            }
            if (texfiel === "descripcion") {
              setDescripcion(value);
              setErrorDescripcion(!isValidDescripcion());
            }
            if (texfiel === "cantidad") {
              setCantidad(value);
              setErrorCantidad(!isValidCantidad());
            }
            if (texfiel === "idTipoDescuento") {
              
              setIdTipoDescuento(value);
              setErrorIdTipoDescuento(!isValidTipoDescuento());
            }

    };
    
    const isValidProducto=()=>{
      return producto.length >3
    }
    const isValidMonto=()=>{
      return monto > 0
    }
    const isValidCantidad =()=>{
      return cantidad >= 1
    }
    const isValidDescripcion=()=>{

      return descripcion.length > 5;
    }
    const isValidTipoDescuento =()=>{
      return idTipoDescuento > 0
    }

    const isValidForm =()=>{
      return  isValidProducto() === true && isValidMonto() === true  && isValidCantidad() === true  && isValidDescripcion() === true 
    }
    

     const AbrirModalDescuentoNew = ()=>{
      listarDescuentos(userName);
      setOpenNewDescuento(true);
      
     }
     const CerrarModalDescuentoNew = ()=>{
        setOpenNewDescuento(false);
        setProducto('');
        setMonto(0);
        setIdProyecto(0);
        setProyectoNombre('');
        setDescripcion('');
        setCantidad(1);
     }
     const limpiarCamposDescuento = ()=>{
      setProducto('');
      setMonto(0);
      setIdProyecto(0);
      setProyectoNombre('');
      setDescripcion('');
      setCantidad(1);
     }

     const buscarProducto =()=>{
        if(producto.length > 0){
              const data={
                usuarioLogin:userName,
                producto: producto        
              };
              requestPost('Aplicaciones/ObtenerProyectoPorProducto',data,dispatch).then((res)=>{                        
                  if(res.code === 0){                    
                    setIdProyecto(res.data.idProyecto);
                    setProyectoNombre(res.data.nombreProyecto);              
                  }else{
                      setIdProyecto(0);
                      setProyectoNombre('');
                      dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
                  }    
              })       
        }

     }
     const confirmarDecuento=()=>{  
       if((idTipoDescuento == utilidad.TIPO_APLICACION_OTROS_ID && idProyecto == 0) || (idTipoDescuento != utilidad.TIPO_APLICACION_OTROS_ID && idProyecto > 0)  ){  
              const data={
                usuarioLogin:userName,
                usuarioId: idUsuario,
                producto: producto,
                monto:parseFloat(monto),
                cantidad:cantidad,
                descripcion:descripcion,
                idProyecto:idProyecto,
                idComisionDetalle:parseInt(idComisionDetalleSelected),
                idTipoDescuento:parseInt(idTipoDescuento)
              };              
              requestPost('Aplicaciones/RegistrarDescuentoComision',data,dispatch).then((res)=>{                        
                  if(res.code === 0){  
                    setOpenNewDescuento(false);   // cerrar modal, actualizar la lista
                    CargarDetalleFrelancers(userName, parseInt(idComisionDetalleSelected));
                    handleOnGetAplicaciones();
                    limpiarCamposDescuento();

                  }else{                       
                      dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
                  }    
              })
              
            }else{
                //aqui no se ejecuto
                setOpenSnackbar(true);
                setMensajeSnackbar('¡Debe ingresar un producto valido para una Cuota');
                settipTSnackbar('warning');
            }



              
     }

     const listarDescuentos = (usuario) => {
        const headers={usuarioLogin:usuario};
        requestGet('Aplicaciones/ObtenerTipoDescuentosGuardian',headers,dispatch).then((res)=>{  
                  
            if(res.code === 0){                 
                setListTipoDescuento(res.data);                            
            }else{
                setListTipoDescuento([])                
            }    
        })   
     };
     const closeSnackbar= (event, reason) => {
      if (reason === 'clickaway') {
        return;
      }
      setOpenSnackbar(false);
    };
  
    return (
        <Fragment>
            <Dialog   fullScreen open={open}   TransitionComponent={Transition}  >
            <AppBar className={classes.appBar}>
                <Toolbar>
                    <IconButton edge="start" color="inherit" onClick={cerrarModal} aria-label="close">
                    <CloseIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                      DETALLE DE APLICACIONES
                    </Typography>
                </Toolbar>
            </AppBar>     
               <Container maxWidth="md" className={classes.containerPrincipal} >                                   
                    <Grid  container item xs={12}  className={classes.gridContainer} >                       
                            <Grid item xs={12} md={3} className={classes.containerPhoto}  >
                                <Avatar alt="perfil"  className={classes.avatarNombre} > <h1> {ficha.nombreFicha !==""? ficha.nombreFicha.charAt(0).toUpperCase(): 'S'.charAt(0).toUpperCase() } </h1> </Avatar> 
                            </Grid>
                            <Grid  item xs={12} md={9} >
                              <Grid  item xs={12} md={12}  >
                                  <Grid  item xs={12} >
                                      <Typography variant="h6" gutterBottom style={{ textTransform: 'uppercase'}}>
                                          {ficha.nombreFicha} 
                                      </Typography>
                                  </Grid>
                              </Grid>
                              <Grid  item xs={12} md={6}  >
                                      <Grid  item xs={12}   >                                           
                                          <Typography variant="subtitle1" gutterBottom style={{ textTransform: 'uppercase'}} >
                                          <b>RANGO:</b>  {ficha.rango} 
                                          </Typography>
                                          <Typography variant="subtitle1" gutterBottom style={{ textTransform: 'uppercase'}} >
                                          <b>CICLO:</b>  {ficha.ciclo} 
                                          </Typography>
                                      </Grid>
                                      <Grid  item xs={12}  >
                                          <Button
                                              type="submit"                            
                                              variant="contained"
                                              color="primary"
                                              className={classes.submitCargar}
                                              /* onClick = {()=> verFicha()}   */                                       
                                              >
                                                VER
                                          </Button>   
                                      </Grid>
                                </Grid>
                                <Grid  item xs={12} md={6} >                              
                                                              
                                </Grid>
                            </Grid>                                                     
                      </Grid>                     
                  </Container>   
                  <Container>
                      <Grid  container >
                            <Grid item xs={12} md={10} >
                            </Grid>
                            <Grid item xs={12} md={2}>
                              {validarPermiso(perfiles, namePage + permiso.CREAR)?
                                <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Desea agregar más descuentos.'}>
                                    <Button
                                        type="submit"                            
                                        variant="contained"
                                        color="primary"
                                        onClick={AbrirModalDescuentoNew}
                                        className={classes.submitCargar}                                   
                                        >
                                          NUEVO DESCUENTO
                                    </Button>   
                                </Tooltip>
                                :
                                <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin Acceso'}>
                                    <Button
                                        type="submit"                            
                                        variant="contained"
                                        color="inherit"                                                                        
                                        >
                                          NUEVO DESCUENTO
                                    </Button>   
                                 </Tooltip>
                              }
                            </Grid>
                      </Grid>
                  </Container>
                  <br />
                  <Container className={classes.containerGrid} > 
                         <Grid  container item xs={12}  >
                                <TableContainer component={Paper}>
                                    <Table className={classes.table} size="medium"  aria-label="a dense table">
                                        <TableHead>
                                        <TableRow>                                          
                                            <TableCell align="center"><b>EMPRESA</b></TableCell> 
                                            <TableCell align="center"><b>PRODUCTO</b></TableCell>
                                            <TableCell align="center"><b>DESCRIPCIÓN</b></TableCell>
                                            <TableCell align="center"><b>MONTO ($us)</b></TableCell>                                                                                                                                                                                   
                                            <TableCell align="center">   </TableCell>
                                        </TableRow>
                                        </TableHead>
                                        <TableBody>
                                          {listaAplicaciones.map((row, index) => (
                                            <TableRow key={index }>
                                                <TableCell align="center"scope="row"> {row.nombreEmpresa} </TableCell>
                                                <TableCell align="center"scope="row"> {row.codigoProducto} </TableCell>
                                                <TableCell align="center"scope="row"> {row.descripcion} </TableCell>
                                                <TableCell align="center">{row.monto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>                                                                                               
                                                <TableCell align="center"scope="row">  </TableCell>                                                                                                                                                                   
                                            </TableRow>
                                          ))}
                                        </TableBody>
                                    </Table>
                                </TableContainer>
                            
                        </Grid> 
                        <br />
                        <Grid  container item xs={12}  >
                            <Grid  item xs={12} md={8} >
                              
                            </Grid>
                            <Grid  item xs={12} md={4} >
                                <Card >
                                    <Typography variant="subtitle1" gutterBottom style={{paddingLeft:'10px', paddingTop:'10px' ,textTransform: 'uppercase'}} >
                                              <b>TOTAL APLICACIÓN ($us):{ ' '} {subTotal.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })} </b>  
                                    </Typography>
                                </Card>
                            </Grid>
                        </Grid>
                   </Container>        
                   
                  
            </Dialog>                        
             <NuevoDescuentoModal open={openNewDescuento} closeHandelModal={CerrarModalDescuentoNew} confirmarDecuento={confirmarDecuento} buscarProducto={buscarProducto} onChange={onChange} producto={producto} monto={monto} descripcion={descripcion} cantidad={cantidad} idProyecto={idProyecto} proyectoNombre={proyectoNombre} errorProducto={errorProducto} errorMonto={errorMonto} errorCantidad={errorCantidad} errorDescripcion={errorDescripcion} isValidForm={isValidForm} idTipoDescuento={idTipoDescuento} errorIdTipoDescuento={errorIdTipoDescuento} listTipoDescuento={listTipoDescuento} />
             <SnackbarSion open={openSnackbar} closeSnackbar={closeSnackbar} tipo={tipoSnackbar} duracion={2000} mensaje={mensajeSnackbar}  /> 
        </Fragment>
    );

};

export default DetalleDescuentoModal;