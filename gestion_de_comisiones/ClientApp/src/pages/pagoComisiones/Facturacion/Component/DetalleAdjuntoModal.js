import React, { Fragment, useState } from "react";
import {
    Button, Dialog, Typography,Grid, Container, Tooltip ,Zoom
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
//import EditIcon from '@material-ui/icons/Edit';
//import DeleteIcon from '@material-ui/icons/Delete';
//import DoneIcon from '@material-ui/icons/Done';

import  imageFac from "../../../../../src/assets/img/facturado2.png";

import EditModal from "./EditModal";
import MessageConfirm from "../../../../components/mesageModal/MessageConfirm";

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


const DetalleAdjuntoModal = (props) => {
     //tipoModal : info, error, warning, success
     const classes = useStyles();
     const dispatch = useDispatch();
      const {namePage, open,  handleCloseConfirm, handleCloseCancel, Ficha, listaDetalleEmpresa, estadoComisionGlobalFacturado, checkdComisionDetalleEmpresa, desCheckdComisionDetalleEmpresa, procesarPdf, AceptarTodo, cancelarTodo } = props;
      const {userName} =useSelector((stateSelector)=>{ return stateSelector.load});
      const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});     
    let cerrarModal = () => {
        handleCloseConfirm();
    };
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

      const onChangeFilePDF= (e, idDetalleEmpresa)=> {
        //var file = e.target.files[0];
        const reader = new FileReader();
        //var url = reader.readAsDataURL(file);
        //console.log(URL.createObjectURL(file));
        reader.onloadend = function (e) {
          procesarPdf(idDetalleEmpresa,reader.result);
         // setAvatar(reader.result);
        //  setNuevoAvatar(true);
        }.bind(this);
      }

      //-------------------------------------------------------------------------------------------------------------------
     const [openModalEdit, setOpenModalEdit]= useState(false);
     const [listEmpresas, setListEmpresas]= useState([]);
     const [idDetalleEmpresaSelected,setIdDetalleEmpresaSelected ]= useState(0);
     const [idEmpresaSelected, setIdEmpresaSelected ]= useState(0);
     const [montoSelected, setMontoSelected ]= useState(0);
     const [nroAutorizacionSelected, setNroAutorizacionSelected ]= useState("");
     const [montoFacturarSelected, setMontoFacturarSelected ]= useState(0);
     const [montoTotalFacturarSelected, setMontoTotalFacturarSelected ]= useState(0);

     const [idComiDetallEmpreSelected, setIdComiDetallEmpreSelected]= useState(0);
     const [siFacturoSelected, setSiFacturoSelected]= useState(false);


     const [openModalCancel, setOpenModalCancel]= useState(false);

     /* const abrirModalEdit=(idDetalleEmpresa)=>{
       ApiObtenerDetalleEmpresa(userName,idDetalleEmpresa );       
       setOpenModalEdit(true);
     }; */
     const onChangeregistroEdit= (e)=> {
        const texfiel = e.target.name;
        const value = e.target.value;
        if (texfiel === "idEmpresaSelected") {
          setIdEmpresaSelected(value);
        }
        if (texfiel === "montoSelected") {
          setMontoSelected(value);
        }
        if (texfiel === "nroAutorizacionSelected") {
          setNroAutorizacionSelected(value);
        }

     };

     const handleCloseConfirmEdit =()=>{
      setOpenModalEdit(false);
     };
     const handleCloseCancelEdit =()=>{
      setOpenModalEdit(false);
     };

     
     const ApiObtenerDetalleEmpresa=(user,idcomisionDetalle )=>{
      const data={
        usuarioLogin:user,
        idComisionDetalleEmpresa:parseInt(idcomisionDetalle)
       };
      
       requestPost('Factura/obtenerCDetalleEmpresa',data,dispatch).then((res)=>{ 
       console.log('detalle Edit  : ', res);
            if(res.code === 0){         
              setListEmpresas(res.data.listEmpresa);
              setIdDetalleEmpresaSelected(res.data.idComisionDetalleEmpresa);
              setIdEmpresaSelected(res.data.idEmpresa);
              setMontoSelected(res.data.monto);
              setMontoFacturarSelected(res.data.montoAFacturar);
              setMontoTotalFacturarSelected(res.data.montoTotalFActurar);
              setNroAutorizacionSelected(res.data.nroAutorizacion);
            }
          })    
     };
     const [openModalSaveConfirmar,setOpenModalSaveConfirmar ] = useState(false);

    /*  const saveCondicional=()=>{
      setOpenModalSaveConfirmar(true);
     } */
     const closeModalConfirmGuardar=()=>{
        setOpenModalSaveConfirmar(false);
        handleCloseConfirm();
     }
     const closeModalCancelarGuardar=()=>{
        setOpenModalSaveConfirmar(false);
    }

    const cancelarFacturaEmpresa=(idComisionDetalleEmpresa, siFacturo)=>{

      setOpenModalCancel(true);
      setIdComiDetallEmpreSelected(idComisionDetalleEmpresa);
      setSiFacturoSelected(siFacturo);
    }
    const confirmCheckCancelModal =()=>{
      
      setOpenModalCancel(false);
      //ejecutar la funcion 
      desCheckdComisionDetalleEmpresa(idComiDetallEmpreSelected, !siFacturoSelected );

    }
    const closeCheckCancelModal=()=>{
        setOpenModalCancel(false);
    }
    const [openModalCancelDesCheck, setopenModalCancelDesCheck]= useState(false);
    const DesChecTodo=()=> {

      setopenModalCancelDesCheck(true)
    }
    
    const confiCheckTodoCancelar=()=>{
      setopenModalCancelDesCheck(false);
      cancelarTodo()

    }     
    const cancelCheckTodo=()=>{
      setopenModalCancelDesCheck(false);
    }

    return (
        <Fragment>
            <Dialog   fullScreen open={open}   TransitionComponent={Transition}  >
            <AppBar className={classes.appBar}>
                <Toolbar>
                    <IconButton edge="start" color="inherit" onClick={handleCloseCancel} aria-label="close">
                    <CloseIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                      DETALLE DE ADJUNTOS
                    </Typography>
                </Toolbar>
            </AppBar>     
               <Container maxWidth="md" className={classes.containerPrincipal} >                          
                    <Grid  container item xs={12}  className={classes.gridContainer} >
                        <Grid item xs={12} md={3} className={classes.containerPhoto}  >
                           <Avatar alt="perfil"  className={classes.avatarNombre} > <h1> {Ficha.nombreFicha !==""? Ficha.nombreFicha.charAt(0).toUpperCase(): 'S'.charAt(0).toUpperCase() } </h1> </Avatar>
                        </Grid>
                        <Grid container item xs={12} md={4}  >
                             <Grid  item xs={12}   >
                                    <Typography variant="h6" gutterBottom style={{ textTransform: 'uppercase'}}>
                                        {Ficha.nombreFicha}
                                    </Typography>
                                    <Typography variant="subtitle1" gutterBottom style={{ textTransform: 'uppercase'}} >
                                    <b>RANGO:</b> {Ficha.rango}
                                    </Typography>
                                    <Typography variant="subtitle1" gutterBottom style={{ textTransform: 'uppercase'}} >
                                    <b>CICLO:</b>  {Ficha.ciclo}
                                    </Typography>
                            </Grid>
                            <Grid  item xs={12}  >
                            <Button
                                type="submit"                            
                                variant="contained"
                                color="primary"
                                className={classes.submitCargar}
                                onClick = {()=> verFicha()}                                         
                                >
                                VER
                                </Button>   
                            </Grid>
                        </Grid>
                        <Grid container item xs={12} md={5} >
                          {estadoComisionGlobalFacturado&&
                            <img src={imageFac} alt={'sion'} style={{width:'100%'}} />
                          }
                        </Grid>
                        <br />
                        </Grid>
                    </Container>   
                    <Container maxWidth="xl" className={classes.containerGrid} >
                        <Grid  container item xs={12}  >

                                <TableContainer component={Paper}>
                                    <Table className={classes.table} size="small" aria-label="a dense table">
                                        <TableHead>
                                        <TableRow>                                          
                                            <TableCell align="center"><b>EMPRESAS</b></TableCell>
                                            <TableCell align="center"><b>VENTAS PERSONALES</b></TableCell>
                                            <TableCell align="center"><b>VENTAS GRUPALES</b></TableCell>
                                            <TableCell align="center"><b>RESIDUAL</b></TableCell>
                                            <TableCell align="right"><b>MONTO (USD)</b></TableCell>
                                            <TableCell align="center"><b>RETENCIÓN</b></TableCell>
                                            <TableCell align="center"><b>NETO (USD)</b></TableCell>
                                            <TableCell align="center" ><b>ARCHIVO</b><PictureAsPdfIcon /><br /><br /><br />   </TableCell>                                          
                                            <TableCell align="center"><b>FACTURADO</b><br />
                                              {validarPermiso(perfiles, namePage + permiso.ACTUALIZAR)?
                                                    <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={estadoComisionGlobalFacturado? 'De seleccionar todos': 'Seleccionar todos'}>
                                                      {estadoComisionGlobalFacturado? 
                                                        <IconButton edge="start" color="inherit"   aria-label="close"  onClick={()=> DesChecTodo()}>
                                                        <CheckBoxIcon style={{ color: green[500] }} />
                                                        </IconButton>
                                                      : 
                                                        <IconButton edge="start" color="inherit"  aria-label="close" onClick={()=> AceptarTodo()}  >
                                                        <CheckBoxOutlineBlankIcon style={{ color: green[500] }} />
                                                        </IconButton>
                                                        } 
                                                    </Tooltip>
                                                    :
                                                      <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin Acceso'}>
                                                        {estadoComisionGlobalFacturado? 
                                                          <IconButton edge="start" color="inherit"   aria-label="close" >
                                                          <CheckBoxIcon color="disabled"  />
                                                          </IconButton>
                                                        : 
                                                          <IconButton edge="start" color="inherit"  aria-label="close" >
                                                          <CheckBoxOutlineBlankIcon color="disabled"  />
                                                          </IconButton>
                                                          } 
                                                      </Tooltip>
                                                } 
                                            </TableCell>                                                                                
                                            <TableCell align="right">   </TableCell>
                                        </TableRow>
                                        </TableHead>
                                        <TableBody>
                                        {listaDetalleEmpresa.map((row, index) => (
                                            <TableRow key={index }>
                                            <TableCell align="center"scope="row"> {row.empresa} </TableCell>
                                            <TableCell align="right">{row.ventasPersonales.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    
                                            <TableCell align="right">{row.ventasGrupales.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    
                                            <TableCell align="right">{row.residual.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    
                                            
                                            <TableCell align="right">{row.monto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    

                                            <TableCell align="right">{row.retencion.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    
                                            <TableCell align="right">{row.montoNeto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    

                                            <TableCell align="center"> 
                                            <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={row.respaldoPath === ""? 'Debe seleccionar un archico (opcional)': 'Tiene archivo Cargado'}>
                                             {row.respaldoPath !== ""?  <CheckCircleOutlineIcon style={{ color: green[500] }} /> :  <HighlightOffIcon color="secondary" /> } 
                                             </Tooltip>
                                             </TableCell>  
                                            
                                            
                                             <TableCell align="center"> 
                                               {validarPermiso(perfiles, namePage + permiso.ACTUALIZAR)?
                                                <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={row.siFacturo === ""? 'Debe seleccionar un archico (opcional)': 'Tiene archivo Cargado'}>
                                                  {row.siFacturo? 
                                                    <IconButton edge="start" color="inherit"   aria-label="close" onClick={()=> cancelarFacturaEmpresa(`${row.idComisionDetalleEmpresa}`, `${row.siFacturo}`)} >
                                                     <CheckBoxIcon style={{ color: green[500] }} />
                                                    </IconButton>
                                                  : 
                                                    <IconButton edge="start" color="inherit"  aria-label="close" onClick={()=> checkdComisionDetalleEmpresa(`${row.idComisionDetalleEmpresa}`, `${row.siFacturo}`)} >
                                                     <CheckBoxOutlineBlankIcon style={{ color: green[500] }} />
                                                    </IconButton>
                                                    } 
                                                </Tooltip>
                                                :
                                                <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Sin acceso'}>
                                                {row.siFacturo? 
                                                  <IconButton edge="start" color="inherit"   aria-label="close"  >
                                                   <CheckBoxIcon color="disabled" />
                                                  </IconButton>
                                                : 
                                                  <IconButton edge="start" color="inherit"  aria-label="close" >
                                                   <CheckBoxOutlineBlankIcon color="disabled" />
                                                  </IconButton>
                                                  } 
                                              </Tooltip>
                                              }
                                             </TableCell>  
                                            
                                            <TableCell align="center">
                                              {!estadoComisionGlobalFacturado&&
                                                 <>
                                                  <label >
                                                    <input style={{display: 'none' ,}} type="file" accept=".pdf" onChange= {(e)=> onChangeFilePDF(e, `${row.idComisionDetalleEmpresa}`)} />  
                                                      <div className={classes.divCargar}>
                                                      {'CARGAR ARCHIVO '} {' '}<CloudUploadIcon color="action"  style={{marginLeft:'5px'}} />        
                                                      </div>                                                                                                                  
                                                  </label>  
                                                
                                                  </> 
                                              }

                                               {/* <IconButton aria-label="delete" className={classes.margin} onClick={()=> abrirModalEdit(`${row.idComisionDetalleEmpresa}`)} >
                                                  <EditIcon fontSize="inherit" />
                                                </IconButton> */}

                                                {/* <IconButton aria-label="delete" className={classes.margin} onClick={()=> openDeleteModal(`${row.idComisionDetalleEmpresa}`)} >
                                                  <DeleteIcon fontSize="inherit" />
                                                </IconButton> */}

                                            </TableCell>   
                                            </TableRow>
                                        ))}
                                        </TableBody>
                                    </Table>
                                </TableContainer>
                                <TablePagination
                                    rowsPerPageOptions={[10,25,35]}
                                    component="div"
                                    count={listaDetalleEmpresa.length}
                                    rowsPerPage={rowsPerPage}
                                    page={page}
                                    onChangePage={handleChangePage}
                                    onChangeRowsPerPage={handleChangeRowsPerPage}
                                    />
                        </Grid>

                      </Container>       
                   
                  
            </Dialog> 
             <EditModal 
              open={openModalEdit} 
              handleCloseConfirmEdit={handleCloseConfirmEdit}
              handleCloseCancelEdit={handleCloseCancelEdit}
              listEmpresas={listEmpresas}
              idDetalleEmpresaSelected={idDetalleEmpresaSelected}
              idEmpresaSelected={idEmpresaSelected}
              montoSelected={montoSelected}
              onChangeregistroEdit={onChangeregistroEdit}
              nroAutorizacionSelected={nroAutorizacionSelected}
              
              />
              <MessageConfirm open={openModalSaveConfirmar} titulo={'Confirmar facturacion'} subTituloModal={'facturado'} tipoModal={'info'} mensaje={'esta seguro que desea procesar data'} handleCloseConfirm={closeModalConfirmGuardar} handleCloseCancel={closeModalCancelarGuardar}  />
              <MessageConfirm open={openModalCancel} titulo={'Cancelar facturacion'} subTituloModal={'facturado'} tipoModal={'info'} mensaje={'desea Cancelar la factura empresa'} handleCloseConfirm={confirmCheckCancelModal} handleCloseCancel={closeCheckCancelModal}  />
              <MessageConfirm open={openModalCancelDesCheck} 
                 titulo={'Esta seguro!'} 
                 subTituloModal={''} tipoModal={'info'} 
                 mensaje={'Con Cancelar todas las facturas'} 
                 handleCloseConfirm={confiCheckTodoCancelar} 
                 handleCloseCancel={cancelCheckTodo}  />


        </Fragment>
    );

};

export default DetalleAdjuntoModal;