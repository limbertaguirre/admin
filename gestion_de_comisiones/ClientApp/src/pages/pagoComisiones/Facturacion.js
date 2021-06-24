import React, {useEffect, useState}  from 'react';
import BorderWrapper from 'react-border-wrapper'
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';
import { Dialog,Card, DialogContent, Button, Grid, TextField, Typography, FormGroup, FormControlLabel,Checkbox,FormControl, InputLabel, Select, FormHelperText,MenuItem } from "@material-ui/core";
import CloudUploadIcon from '@material-ui/icons/CloudUpload';
import SnackbarSion from "../../components/message/SnackbarSion";

import * as permiso from '../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import { requestPost, requestGet } from "../../service/request";

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
    }

  }));


 const Facturacion =(props)=> {    
     
  let history = useHistory();
  let style= useStyles();
  const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
  useEffect(()=>{  try{  
     verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
     }catch (err) {  verificarAcceso(perfiles, 'none', history); }
  },[])
  const {userName} =useSelector((stateSelector)=>{ return stateSelector.load});
  const dispatch = useDispatch();
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = useState("");
  const [tipoSnackbar, settipTSnackbar] = useState(false);

  const[ciclos, setCiclos]= useState([]);
  const[idCiclo, setIdCiclo]= useState(0);
  const[listaComisionesP, setListaComisionesP]= useState()

  useEffect(()=>{  
    obtenerCiclos();
  },[]);

  const obtenerCiclos=()=>{
    const data={usuarioLogin:userName };
     requestGet('Factura/ObtenerCiclos',data,dispatch).then((res)=>{ 
      console.log('ciclos : ', res);
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

  };

    const cargarComisiones=()=>{
      if(idCiclo != 0){

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
                  <Grid item xs={12} md={7} >

                  </Grid>
                    <Grid item xs={12} md={3} className={style.containerCiclo}>
                               <FormControl  variant="outlined"  
                                fullWidth  
                                //error={cicloError} 
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
                                {/*  <FormHelperText>{cicloError&&'Seleccione un ciclo'}</FormHelperText> */}
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


          <SnackbarSion open={openSnackbar} closeSnackbar={closeSnackbar} tipo={tipoSnackbar} duracion={2000} mensaje={mensajeSnackbar} />   

      </>
    );

}
export default Facturacion;