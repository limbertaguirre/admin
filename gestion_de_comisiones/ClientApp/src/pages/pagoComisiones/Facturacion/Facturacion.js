import React, {useEffect, useState}  from 'react';
import BorderWrapper from 'react-border-wrapper'
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';
import {Container, InputAdornment, Dialog,Card, DialogContent, Button, Grid, TextField, Typography, FormGroup, FormControlLabel,Checkbox,FormControl, InputLabel, Select, FormHelperText,MenuItem } from "@material-ui/core";
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import SnackbarSion from "../../../components/message/SnackbarSion";

import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import { requestPost, requestGet } from "../../../service/request";
import SearchIcon from '@material-ui/icons/Search';
import SaveIcon from '@material-ui/icons/Save';

import GridComisiones from './Component/GridComisiones';
import DetalleAdjuntoModal from './Component/DetalleAdjuntoModal';

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
  const {userName} =useSelector((stateSelector)=>{ return stateSelector.load});
  const dispatch = useDispatch();
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = useState("");
  const [tipoSnackbar, settipTSnackbar] = useState(true);

  const[ciclos, setCiclos]= useState([]);
  const[idCiclo, setIdCiclo]= useState(0);
  const[listaComisionesPendientes, setListaComisionesPendientes]= useState([]);
  const [txtBusqueda, setTxtBusqueda] = useState("");
  const[idDetalleComisionSelect, setIdDetalleComisionSelect ]= useState(0);
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
            console.log(value);
        }
        if (texfiel === "txtBusqueda") {
             setTxtBusqueda(value);
        }
  };

    const cargarComisiones=()=>{
      if(idCiclo != 0){
         obtenerComisiones(userName, idCiclo);
      }else{
        setOpenSnackbar(true);
        setMensajeSnackbar('¡Debe Seleccionar un permiso!');
        settipTSnackbar('warning');
      }
      
    }

    function handleClick(event) {
        event.preventDefault();
        console.info('You clicked a breadcrumb.');
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
     
     const guardarFactura=()=>{

     }
     const CerrarFactura=()=>{
       
     }

    const buscarClientepornombre=(ev)=>{
      console.log('enter');
     // if(txtBusqueda === ""){
        ApiBuscarPorNombre(userName, idCiclo,txtBusqueda)
     // }
      
    }

  //detalle

  const selecionarDetalleFrelances=(idDetalleComision)=>{
      console.log('selecionado iddetalle: ', idDetalleComision);
     /*  const location = {
        pathname: '/facturacion/detalle/adjunto',
        state: {
            namePagina: namePage,
            idDetalleComision: idDetalleComision
          }
      } 
     
      history.push(location); */
      setIdDetalleComisionSelect(idDetalleComision);
      setOpen(true);

    }
   const handleCloseConfirm=()=>{
     setOpen(false);
   }
   const handleCloseCancel=()=>{
      setOpen(false);
   }

    return (
      <>      
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
                  <Grid item xs={12} md={4} className={style.containerSave} >
                     {listaComisionesPendientes.length>0&&
                      <>
                           <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            className={style.submitSAVE}
                            onClick = {()=> CerrarFactura()}                                         
                            >
                            <SaveIcon />  {' '} GUARDAR 
                            </Button> 
                           <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            className={style.submitSAVE}
                            onClick = {()=> guardarFactura()}                                         
                            >
                             <SaveIcon />{' '} CERRAR FACTURA
                            </Button> 
                        </>
                       }     
                  </Grid>
                  <Grid item xs={12} md={3} className={style.containerSave}>
                  {listaComisionesPendientes.length>0&&
                        <TextField
                          label="BUSCAR CLIENTE"
                          type={'text'}
                          variant="outlined"
                          placeholder={'Buscar cliente...'}
                          name="txtBusqueda"                    
                          value={txtBusqueda}
                          onChange={onChange}
                          onKeyPress={(ev) => {
                            if (ev.key === 'Enter') {
                              buscarClientepornombre();
                            }
                          }}
                        // className={styles.TextFielBusqueda}
                        //  error={txtBusquedaError}
                        // helperText={ txtBusquedaError && "El campo es requerido" }
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
                             CARGAR {' '} <CloudUploadIcon />
                            </Button>   
                    </Grid>
                  </Grid>
                </Card>
            <br />           
            <GridComisiones listaComisionesPendientes={listaComisionesPendientes} selecionarDetalleFrelances={selecionarDetalleFrelances} />
            <SnackbarSion open={openSnackbar} closeSnackbar={closeSnackbar} tipo={tipoSnackbar} duracion={2000} mensaje={mensajeSnackbar} txtBusqueda={txtBusqueda} />   
            <DetalleAdjuntoModal open={open} handleCloseConfirm={handleCloseConfirm} handleCloseCancel={handleCloseCancel} />
      </>
    );

}
export default Facturacion;