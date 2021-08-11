import React, {useEffect, useState}  from 'react';

import {Container, Chip, InputAdornment, Dialog,Card, DialogContent, Button,
   Grid, TextField, Typography, FormGroup, FormControlLabel,Checkbox,FormControl,
    InputLabel, Select, FormHelperText,MenuItem, Breadcrumbs } from "@material-ui/core";
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

  useEffect(()=>{  try{   
      verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
      }catch (err) {  verificarAcceso(perfiles, 'none', history); }
  },[]);
  const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
  const dispatch = useDispatch();
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = useState("");
  const [tipoSnackbar, settipTSnackbar] = useState(true);

  const[ciclos, setCiclos]= useState([]);
  const[idCiclo, setIdCiclo]= useState(0);
  const[listaComisionesCerrados, setListaComisionesCerrados]= useState([]);
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
    if(idCiclo && idCiclo != 0){       
      const data={
        usuarioLogin:userName,
        idCiclo: idCiclo
       };
      requestPost('Aplicaciones/ObtenerAplicaciones',data,dispatch).then((res)=>{ 
          console.log('getCiclos response => ', res);
          if(res.code === 0){  
              setListaComisionesCerrados(res.data);                          
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

  function handleClick(event) {
    event.preventDefault();
    console.info('You clicked a breadcrumb.');
}

const onChangeSelectCiclo= (e) => {
  const texfiel = e.target.name;
  const value = e.target.value;
  if (texfiel === "idCiclo") {
      setIdCiclo(value);
      console.log(value);
  }
  if (texfiel === "txtBusqueda") {
       //setTxtBusqueda(value);
  }
};

 const selecionarDetalleFrelances = (comisionDetalleId)=>{
    console.log('iddetalle :', comisionDetalleId);
 }

  return (
    <>
           
      <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
              <Breadcrumbs aria-label="breadcrumb">
                        <StyledBreadcrumb key={1} component="a" label="Gestion de pagos"icon={<HomeIcon fontSize="small" />}  />
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
                     
                     {/*   <>
                       
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
                            
                        </> */}
                          
                  </Grid>
                  <Grid item xs={12} md={4} className={style.containerSave}>
                 
                       {/*  <TextField
                          label="Buscar freelancer"
                          type={'text'}
                          variant="outlined"
                          placeholder={'Buscar por carnet identidad'}
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
                        />       */}
                      
            </Grid>

            <Grid item xs={12} md={3} className={style.containerCiclo}>
                        <FormControl  variant="outlined"  
                        fullWidth                       
                        className={style.TextFiel}
                        >
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
                              {ciclos.map((value,index)=> ( <MenuItem key={index} value={value.idCiclo}>{value.nombre}</MenuItem> ))}  
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

      <GridAplicaciones aplicacionesList={listaComisionesCerrados}   />

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

