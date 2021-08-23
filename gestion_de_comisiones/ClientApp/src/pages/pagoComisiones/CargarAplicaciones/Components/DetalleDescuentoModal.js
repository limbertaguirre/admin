import React, { Fragment, useState, useEffect } from "react";
import {
    Button, Dialog, Typography,Grid, Container, Tooltip ,Zoom, Card
} from "@material-ui/core";
import {useSelector,useDispatch} from 'react-redux';
import { requestPost } from "../../../../service/request";
import * as permiso from '../../../../routes/permiso'; 
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
import TablePagination from '@material-ui/core/TablePagination';
import Paper from '@material-ui/core/Paper';

import CheckBoxIcon from '@material-ui/icons/CheckBox';
import CheckBoxOutlineBlankIcon from '@material-ui/icons/CheckBoxOutlineBlank';
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import PictureAsPdfIcon from '@material-ui/icons/PictureAsPdf';

import HighlightOffIcon from '@material-ui/icons/HighlightOff';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';
import { green } from '@material-ui/core/colors';


import  imageFac from "../../../../../src/assets/img/facturado2.png";
import NuevoDescuentoModal from './NuevoDescuentoModal';
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
      const {open, handleCloseCancel, ficha, listaAplicaciones, idComisionDetalleSelected, CargarDetalleFrelancers, handleOnGetAplicaciones} = props;
      const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
      const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});     

     const[ openNewDescuento,setOpenNewDescuento ] = useState(false);



    const verFicha = () => {
      
    };

      //tabla
      const [rowsPerPage, setRowsPerPage] = useState(10);
      const [page, setPage] = useState(0);
      const handleChangePage = (event, newPage) => {
          setPage(newPage);
      };    
      const handleChangeRowsPerPage = (event) => {
          setRowsPerPage(parseInt(event.target.value, 10));
          setPage(0);
      };
     const [idDetalleEmpresaSelected,setIdDetalleEmpresaSelected ]= useState(0);
     const [idEmpresaSelected, setIdEmpresaSelected ]= useState(0);
 

     const [idComiDetallEmpreSelected, setIdComiDetallEmpreSelected]= useState(0);
     const [siFacturoSelected, setSiFacturoSelected]= useState(false);


     const onChangeregistroEdit= (e)=> {
        const texfiel = e.target.name;
        const value = e.target.value;
        if (texfiel === "idEmpresaSelected") {
          setIdEmpresaSelected(value);
        }
       

     };
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
  
     //------------------------------------
     // nuevo descuento
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

    const onChange= (e) => {
            const texfiel = e.target.name;
            const value = e.target.value;
            if (texfiel === "producto") {
                setProducto(value);
                setErrorProducto(!isValidProducto());
                if(idProyecto>0 && producto!=value ){
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
    const isValidForm =()=>{
      return  isValidProducto() === true && isValidMonto() === true  && isValidCantidad() === true  && isValidDescripcion() === true 
    }
    

     const AbrirModalDescuentoNew = ()=>{
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
                    console.log('data : ', res);    
                    setIdProyecto(res.data.idProyecto);
                    setProyectoNombre(res.data.nombreProyecto);              
                  }else{
                      setIdProyecto(0);
                      setProyectoNombre('');
                      dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
                  }    
              })   
    
            console.log('llmar api buscar');
        }else{ 
            console.log('ingrese algun caracter');
        }
     }
     const confirmarDecuento=()=>{      
              const data={
                usuarioLogin:userName,
                usuarioId: idUsuario,
                producto: producto,
                monto:parseFloat(monto),
                cantidad:cantidad,
                descripcion:descripcion,
                idProyecto:idProyecto,
                idComisionDetalle:parseInt(idComisionDetalleSelected)
              };           
              console.log('data register descuento :', data);
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
     }
  
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
             <NuevoDescuentoModal open={openNewDescuento} closeHandelModal={CerrarModalDescuentoNew} confirmarDecuento={confirmarDecuento} buscarProducto={buscarProducto} onChange={onChange} producto={producto} monto={monto} descripcion={descripcion} cantidad={cantidad} idProyecto={idProyecto} proyectoNombre={proyectoNombre} errorProducto={errorProducto} errorMonto={errorMonto} errorCantidad={errorCantidad} errorDescripcion={errorDescripcion} isValidForm={isValidForm} />

        </Fragment>
    );

};

export default DetalleDescuentoModal;