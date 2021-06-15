import React, {useEffect, useState}  from 'react';
import BorderWrapper from 'react-border-wrapper'
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';

import * as permiso from '../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import * as ActionCliente from '../../redux/actions/clienteAction';
import * as moment from "moment";
import "moment/locale/es";
import { Dialog, DialogContent, Button, Grid, TextField, Typography, FormGroup, FormControlLabel,Checkbox,FormControl, InputLabel, Select, FormHelperText,MenuItem } from "@material-ui/core";
import { MuiPickersUtilsProvider, KeyboardDatePicker } from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import esLocale from "date-fns/locale/es";
import Avatar from '@material-ui/core/Avatar';
import CameraAltIcon from '@material-ui/icons/CameraAlt';
import { requestPost } from "../../service/request";

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
    TextFiel: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
        marginRight:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingLeft:theme.spacing(1),
        //width:'98%'
    },
    fotoSise: {
      width: theme.spacing(15),
      height: theme.spacing(15),
    },
    divCenter: {     
     /*  '& > *': {
        margin: theme.spacing(1),
      }, */
      display:'flex',
      flexDirection:'column',
      alignContent:'center',
      alignItems:'center',
      justifyContent:'center',
    },
    submitCamara: {                 
      background: "#1872b8", 
      boxShadow: '2px 4px 5px #1872b8',
      color:'white',      
    },

}));


 const Ficha = (props)=> {    
     
  let history = useHistory();
  const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
/*   useEffect(()=>{  try{  
     verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
     }catch (err) {  verificarAcceso(perfiles, 'none', history); }
  },[]) */
  const dispatch = useDispatch();
  const style = useStyles();
  const {objCliente, listPaises, listCiudades, listBajas, listBancos} = useSelector((stateSelector) =>{ return stateSelector.cliente});  
  const {userName} =useSelector((stateSelector)=>{ return stateSelector.load});
  
  useEffect(()=>{ 
    console.log('paramet : ', props.location.state.idCliente);
    dispatch(ActionCliente.listaPaises());
    dispatch(ActionCliente.obtenerBajas());
    dispatch(ActionCliente.obtenerBancos());
   // dispatch(ActionCliente.obtenerClienteXId(parseInt(props.location.state.idCliente)));
    obtenerCliente(parseInt(props.location.state.idCliente));
    dispatch(ActionCliente.obtenerCiudadesPorPais(objCliente.idPais));
  },[])

    const regresarPage=()=>{  
        dispatch(ActionCliente.InicializarCliente());      
        history.goBack();        
    }
    const[idFicha, setIdFicha]= useState(0);
    const [codigo, setCodigo]= useState("");
    const [fechaRegistro, setFechaRegistro]= useState(moment().format("YYYY/MM/DD"));
    const [nombre, setNombre]= useState("");
    const [apellido, setApellido]= useState("");
    const [ci, setCi]= useState("");
    const [telOficina, setTelOficina]=useState(0);
    const [telMovil, setTelMovil] = useState(0);
    const [telFijo, setTelFijo] = useState(0);
    const [direccion, setDireccion]= useState("");
    const [idCiudad,setIdCiudad]= useState(0);
    const [idPais, setIdPais]= useState(0);
    const [correoElectronico, setCorreoElectronico]= useState("");
    const [fechaNacimiento, setFechaNacimiento] = useState(moment().format("YYYY/MM/DD"))
    const [codigoPatrocinador, setCodigoPatrocinador] = useState("");
    const [nombrePatrocinador, setNombrePatrocinador]= useState("");
    const [nivel, setNivel]= useState("");
    const [comentario, setComentario]=useState("");

    const [idBanco, setIdBanco]=useState(0);
    const [cuentaBancaria, setCuentaBancaria]= useState("");
    const [codigoBanco, setCodigoBanco]= useState(0);

    const [razonSocial, setRazonSocial]= useState("");
    const [nit, setNit]= useState("");

    const [fechaBaja, setFechaBaja]= useState(moment().format("YYYY/MM/DD"));
    const [idTipoBaja, setIdTipoBaja]= useState(0);
    const [motivoBaja, setMotivoBaja]= useState("");
    
    const[checkTieneCuenta, setCheckTieneCuenta]= useState(false);
    const[checkTieneFactura, setCheckTieneFactura]= useState(false);
    const[checkTieneBaja, setCheckTieneBaja]= useState(false);

    const obtenerCliente=(idCliente)=>{
      const data={usuarioLogin:userName, idCliente: idCliente };
      requestPost('Cliente/IdObtenerCliente',data,dispatch).then((res)=>{ 
        console.log('nuevo obtener : ', res);
            if(res.code === 0){  
               let data= res.data;
               setIdPais(data.idPais);
               setIdCiudad(data.idCiudad);
               setCodigo(data.codigo);
               setFechaRegistro(data.fechaRegistro);
               setNombre(data.nombre);
               setApellido(data.apellido);
               setCi(data.ci);
               setTelOficina(data.telOficina);
               setTelMovil(data.telMovil);
               setTelFijo(data.telFijo === null? 0 : data.telFijo );
               setDireccion(data.direccion);
               setCorreoElectronico(data.correoElectronico);
               setFechaNacimiento(data.fechaNacimiento);
               
               setCodigoPatrocinador(data.codigoPatrocinador);
               setNombrePatrocinador(data.nombrePatrocinador);
               setNivel(data.nivel);
         
               setComentario(data.comentario === null? "": data.comentario );
               
               setCheckTieneBaja(data.idFichaTipoBajaDetalle>0);
               setFechaBaja(data.fechaBaja);
               setIdTipoBaja(data.idTipoBaja);
               setMotivoBaja(data.motivoBaja);
         
               setRazonSocial(data.razonSocial === null? "": data.razonSocial);
               setNit(data.nit === null? "" : data.nit);
               setCheckTieneFactura(data.nit != null);
               
               setIdBanco(data.idBanco);
               setCuentaBancaria(data.cuentaBancaria === null? "":data.cuentaBancaria );
               setCodigoBanco(data.codigoBanco);
               setCheckTieneCuenta(data.tieneCuentaBancaria);
                           
            }else{
               // dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }    
          })   
    };
    useEffect(()=>{ 

      dispatch(ActionCliente.obtenerCiudadesPorPais(idPais));
    },[idPais])



    const _onChangeregistro= (e) => {
      const texfiel = e.target.name;
      const value = e.target.value;
      if (texfiel === "codigo") {
           setCodigo(value);
      }
      if (texfiel === "nombre") {
        setNombre(value);
      }
      if (texfiel === "apellido") {
        setApellido(value);
      }
      if (texfiel === "ci") {
        setCi(value);
      }
      if (texfiel === "telOficina") {
        setTelOficina(value);
      }
      if (texfiel === "telMovil") {
        setTelMovil(value);
      }
      if (texfiel === "telFijo") {
        setTelFijo(value);
      }
      if (texfiel === "direccion") {
        setDireccion(value);
      }
      if (texfiel === "idPais") {
        setIdPais(value);
        dispatch(ActionCliente.obtenerCiudadesPorPais(value));
        setIdCiudad(0);
      }
      if (texfiel === "idCiudad") {
        setIdCiudad(value);
      }

      if (texfiel === "correoElectronico") {
        setCorreoElectronico(value);
      }

      if (texfiel === "codigoPatrocinador") {
        setCodigoPatrocinador(value);
      }
      if (texfiel === "nombrePatrocinador") {
        setNombrePatrocinador(value);
      }

      if (texfiel === "nivel") {
        setNivel(value);
      }
      if (texfiel === "comentario") {
        setComentario(value);
      }
      if (texfiel === "idTipoBaja") {
        setIdTipoBaja(value);
      }
      if (texfiel === "motivoBaja") {
        setMotivoBaja(value);
      }

      if (texfiel === "razonSocial") {
        setRazonSocial(value);
      }
      if (texfiel === "nit") {
        setNit(value);
      }
      
      if (texfiel === "idBanco") {
        setIdBanco(value);
      }
      if (texfiel === "cuentaBancaria") {
        setCuentaBancaria(value);
      }
      if (texfiel === "codigoBanco") {
        setCodigoBanco(value);
      }
      

      
    };
    const _onChangeFechaRegistro= (date) => {
    setFechaRegistro(moment(date).format("YYYY/MM/DD"));
      
    }
    const _onChangeFechaNacimiento= (date) => {
      setFechaNacimiento(moment(date).format("YYYY/MM/DD"));        
    }
    const _onChangeFechaBaja= (date) => {
      setFechaBaja(moment(date).format("YYYY/MM/DD"));        
    }
    const handleChangeCheck = (event) => {
        let checkFiel= event.target.name;
        let value= event.target.checked;
        console.log(checkFiel);
        console.log(value);
        if(checkFiel === 'checkTieneCuenta'){
          setCheckTieneCuenta(value);
        }
        if(checkFiel === 'checkTieneFactura'){
          setCheckTieneFactura(value);
        }
        if(checkFiel === 'checkTieneBaja'){
          setCheckTieneBaja(value);
        }
    };
    
    const editarPerfil=()=>{
       console.log('en proceso cambiar perfil');
    };
     
    return (
      <>
           <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
              <Breadcrumbs aria-label="breadcrumb">
                      <div  onClick={regresarPage} >   <StyledBreadcrumb key={2}  component="a" label="Cliente" icon={<HomeIcon fontSize="small" />} /> </div>  
                        <StyledBreadcrumb key={2} component="a" label="Ficha cliente"  />
                       
              </Breadcrumbs>
           </div>
           <br/>
         
         
          <h3> ficha Cliente </h3>
          <Grid container item xs={12}  >
            <Grid container item xs={6} >
               <Grid item xs={6} >
                       <TextField                            
                            label=" codigo"
                            type={'text'}
                            variant="outlined"
                            disabled
                            name="codigo"
                            value={codigo}
                            placeholder="Codigo de cliente"
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid>
               <Grid item xs={6}>
                         <MuiPickersUtilsProvider utils={DateFnsUtils} locale={esLocale}>
                            <KeyboardDatePicker
                                    fullWidth
                                    autoOk
                                    disabled
                                    variant="inline"
                                    inputVariant="outlined"
                                    className={style.TextFiel}
                                    label="Fecha de registro"
                                    format="yyyy/MM/dd"
                                    value={fechaRegistro}
                                   // error={fechaNacimientoError}
                                    //helperText={fechaNacimientoError &&'Ingrese un año de nacimiento valido'}
                                    InputAdornmentProps={{ position: "start" }}
                                    invalidDateMessage={'Formato de fecha no válido'}
                                    onChange={_onChangeFechaRegistro}
                            />
                        </MuiPickersUtilsProvider>
               </Grid> 
               <Grid item xs={6} >
                        <TextField                            
                            label="Nombre"
                            type={'text'}
                            variant="outlined"
                            name="nombre"
                            value={nombre}
                            placeholder="Nombre Cliente"
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid> 
               <Grid item xs={6} >
                        <TextField                            
                            label="Apellido"
                            type={'text'}
                            variant="outlined"
                            name="apellido"
                            value={apellido}
                            placeholder="Apellido"
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid> 
               <Grid item xs={6} >
                       <TextField                            
                            label="C.I."
                            type={'text'}
                            variant="outlined"
                            name="ci"
                            value={ci}
                            placeholder="Carnet de Identidad"
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid>
               <Grid item xs={6}>
                          <TextField                            
                            label="telOficina"
                            type={'number'}
                            variant="outlined"
                            name="telOficina"
                            value={telOficina}                            
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid>  
               <Grid item xs={6} >
                       <TextField                            
                            label="telMovil"
                            type={'number'}
                            variant="outlined"
                            name="telMovil"
                            value={telMovil}                           
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid>
               <Grid item xs={6}>
                          <TextField                            
                            label="telFijo"
                            type={'number'}
                            variant="outlined"
                            name="telFijo"
                            value={telFijo}                            
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid>      
               <Grid item xs={12} >
                       <TextField                            
                            label="Dirección"
                            type={'text'}
                            variant="outlined"
                            name="direccion"
                            value={direccion}
                            placeholder="Dirección"
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid>
               <Grid item xs={6} >
                        <FormControl  variant="outlined"  
                           fullWidth  
                           //error={CiudadError} 
                           className={style.TextFiel}
                           >
                            <InputLabel id="demo-simple-select-outlined-labelciudad">Ciudad</InputLabel>
                            <Select
                                labelId="demo-simple-select-outlined-labelciudad"
                                id="demo-simple-select-outlined"
                                value={idCiudad}
                                name="idCiudad"
                                onChange={_onChangeregistro}
                                label="Ciudad"
                                >
                                <MenuItem value={0}>
                                    <em>Seleccione una Ciudad</em>
                                </MenuItem>
                                {listCiudades.map((value,index)=> ( <MenuItem key={index} value={value.idCiudad}>{value.nombre}</MenuItem> ))}  
                            </Select>
                           {/*  <FormHelperText>{sucursalError&&'Seleccione una ciudad'}</FormHelperText> */}
                        </FormControl>
               </Grid>
               <Grid item xs={6}>
                        <FormControl  variant="outlined"  
                           fullWidth  
                           //error={sucursalError} 
                           className={style.TextFiel}
                           >
                            <InputLabel id="demo-simple-select-outlined-labelpais">Pais</InputLabel>
                            <Select
                                labelId="demo-simple-select-outlined-labelpais"
                                id="demo-simple-select-outlined"
                                value={idPais}
                                name="idPais"
                                onChange={_onChangeregistro}
                                label="Pais"
                                >
                                <MenuItem value={0}>
                                    <em>Seleccione un Pais</em>
                                </MenuItem>
                                {listPaises.map((value,index)=> ( <MenuItem key={index} value={value.idPais}>{value.nombre}</MenuItem> ))}  
                            </Select>
                           {/*  <FormHelperText>{sucursalError&&'Seleccione un Pais'}</FormHelperText> */}
                        </FormControl>
               </Grid>  
               <Grid item xs={12} >
                       <TextField                            
                            label="Correo electronico"
                            type={'text'}
                            variant="outlined"
                            name="correoElectronico"
                            value={correoElectronico}
                            placeholder="Correo electronico"
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid>    
              <Grid item xs={12}>
                        <MuiPickersUtilsProvider utils={DateFnsUtils} locale={esLocale}>
                            <KeyboardDatePicker
                                    fullWidth
                                    autoOk
                                    variant="inline"
                                    inputVariant="outlined"
                                    className={style.TextFiel}
                                    label="Fecha de nacimiento"
                                    format="yyyy/MM/dd"
                                    value={fechaNacimiento}
                                   // error={fechaNacimientoError}
                                    //helperText={fechaNacimientoError &&'Ingrese un año de nacimiento valido'}
                                    InputAdornmentProps={{ position: "start" }}
                                    invalidDateMessage={'Formato de fecha no válido'}
                                    onChange={_onChangeFechaNacimiento}
                            />
                        </MuiPickersUtilsProvider>
                </Grid>
                <Grid container >
                    <Grid item xs={4} >
                          <TextField                            
                                label="Cod patrocinador"
                                type={'text'}
                                variant="outlined"
                                disabled
                                name="codigoPatrocinador"
                                value={codigoPatrocinador}                           
                                className={style.TextFiel}
                                onChange={_onChangeregistro}
                              // error={corporativoError}
                              /*  helperText={ corporativoError &&
                                "campo requerido"
                                }   */                          
                                fullWidth                             
                            />
                  </Grid>
                  <Grid item xs={8}>
                              <TextField                            
                                label="Nombre patrocinador"
                                type={'text'}
                                variant="outlined"
                                disabled
                                name="nombrePatrocinador"
                                value={nombrePatrocinador}                            
                                className={style.TextFiel}
                                onChange={_onChangeregistro}
                              // error={corporativoError}
                              /*  helperText={ corporativoError &&
                                "campo requerido"
                                }   */                          
                                fullWidth                             
                            />
                  </Grid>      
                </Grid>
                <Grid item xs={12} >
                       <TextField                            
                            label="Nivel"
                            type={'text'}
                            disabled
                            variant="outlined"
                            name="nivel"
                            value={nivel}
                            placeholder="Nivel"
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid>   
            </Grid>   
            <Grid item xs={6}  >
                <Grid container className={style.divCenter}>
                       <Grid item xs={6} >
                       <Avatar alt="perfil" src={"https://pbs.twimg.com/media/Dfk08xnUEAUxTLR?format=jpg&name=360x360"} className={style.fotoSise} />
                       </Grid>
                       <Grid item xs={6} >
                       <Button
                          type="submit"                          
                          variant="contained"
                          color="primary"
                          className={style.submitCamara}
                          onClick = {()=> editarPerfil()}                                         
                          >
                          <CameraAltIcon />
                        </Button>  
                        </Grid>
               </Grid> 
               <Grid item xs={12}   >
                        <TextField                            
                            label="Comentario"
                            type={'text'}
                            variant="outlined"
                            name="comentario"
                            value={comentario}
                            multiline
                            rows={3}
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
               </Grid> 
               <Grid item xs={12}  className={style.divCenter}  >
                 <FormGroup row  >
                      <FormControlLabel
                        control={<Checkbox checked={checkTieneCuenta} onChange={handleChangeCheck} name="checkTieneCuenta" color="primary" />}
                        label="Tiene Cuenta"
                      />
                      <FormControlLabel
                        control={ <Checkbox checked={checkTieneFactura} onChange={handleChangeCheck} name="checkTieneFactura" color="primary" /> }
                        label="Tiene Factura?"
                      />
                       <FormControlLabel
                        control={ <Checkbox checked={checkTieneBaja} onChange={handleChangeCheck} name="checkTieneBaja" color="primary" /> }
                        label="Dado de baja?"
                      />                                         
                  </FormGroup>   
              </Grid> 
              {checkTieneCuenta&&
              <Grid container >
                  <Grid item xs={12}  >
                      <Grid item xs={6}  >
                        <FormControl  variant="outlined"  
                                fullWidth  
                                //error={CiudadError} 
                                className={style.TextFiel}
                                >
                                  <InputLabel id="demo-simple-select-outlined-labelbanco">Banco</InputLabel>
                                  <Select
                                      labelId="demo-simple-select-outlined-labelbanco"
                                      id="demo-simple-select-outlined"
                                      value={idBanco}
                                      name="idBanco"
                                      onChange={_onChangeregistro}
                                      label="Banco"
                                      >
                                      <MenuItem value={0}>
                                          <em>Seleccione el Banco</em>
                                      </MenuItem>
                                      {listBancos.map((value,index)=> ( <MenuItem key={index} value={value.idBanco}>{value.nombre}</MenuItem> ))}  
                                  </Select>
                                {/*  <FormHelperText>{sucursalError&&'Seleccione una ciudad'}</FormHelperText> */}
                              </FormControl>
                        </Grid>
                    </Grid>
                    <Grid item xs={6} >
                       <TextField                            
                            label="Cuenta Bancaria"
                            type={'text'}
                            variant="outlined"
                            name="cuentaBancaria"
                            value={cuentaBancaria}
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
                    </Grid>
                    <Grid item xs={6} >
                         <TextField                            
                            label="Cuenta Banco"
                            type={'text'}
                            variant="outlined"
                            name="codigoBanco"
                            value={codigoBanco}
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
                      
                    </Grid>

               </Grid>
               }

              {checkTieneFactura&& 
              <Grid container >
                  <Grid item xs={6}  >
                        <TextField                            
                            label="Razón  Social"
                            type={'text'}
                            variant="outlined"
                            name="razonSocial"
                            value={razonSocial}
                            placeholder="Codigo de cliente"
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
                  </Grid>
                  <Grid item xs={6}  >
                        <TextField                            
                            label="NIT"
                            type={'text'}
                            variant="outlined"
                            name="nit"
                            value={nit}
                            className={style.TextFiel}
                            onChange={_onChangeregistro}
                           // error={corporativoError}
                           /*  helperText={ corporativoError &&
                            "campo requerido"
                            }   */                          
                            fullWidth                             
                        />
                  </Grid>
              </Grid>
              }

              {checkTieneBaja&&
                <Grid container   >
                    <Grid item xs={6}  >
                          <MuiPickersUtilsProvider utils={DateFnsUtils} locale={esLocale}>
                              <KeyboardDatePicker
                                      fullWidth
                                      autoOk
                                      variant="inline"
                                      inputVariant="outlined"
                                      className={style.TextFiel}
                                      label="Fecha de baja"
                                      format="yyyy/MM/dd"
                                      value={fechaBaja}
                                    // error={fechaNacimientoError}
                                      //helperText={fechaNacimientoError &&'Ingrese un año de nacimiento valido'}
                                      InputAdornmentProps={{ position: "start" }}
                                      invalidDateMessage={'Formato de fecha no válido'}
                                      onChange={_onChangeFechaBaja}
                              />
                          </MuiPickersUtilsProvider>
                    </Grid>
                    <Grid item xs={6}  >
                    <FormControl  variant="outlined"  
                            fullWidth  
                            //error={CiudadError} 
                            className={style.TextFiel}
                            >
                              <InputLabel id="demo-simple-select-outlined-labelbaja">Tipo de baja</InputLabel>
                              <Select
                                  labelId="demo-simple-select-outlined-labelbaja"
                                  id="demo-simple-select-outlined"
                                  value={idTipoBaja}
                                  name="idTipoBaja"
                                  onChange={_onChangeregistro}
                                  label="Tipo de baja"
                                  >
                                  <MenuItem value={0}>
                                      <em>Seleccione una baja</em>
                                  </MenuItem>
                                  {listBajas.map((value,index)=> ( <MenuItem key={index} value={value.idTipoBaja}>{value.nombre}</MenuItem> ))}  
                              </Select>
                            {/*  <FormHelperText>{sucursalError&&'Seleccione una ciudad'}</FormHelperText> */}
                          </FormControl>

                    </Grid>
                    <Grid item xs={12}  >
                           <TextField                            
                              label="Motivo de baja"
                              type={'text'}
                              variant="outlined"
                              name="motivoBaja"
                              value={motivoBaja}
                              multiline
                              rows={3}
                              className={style.TextFiel}
                              onChange={_onChangeregistro}
                            // error={corporativoError}
                            /*  helperText={ corporativoError &&
                              "campo requerido"
                              }   */                          
                              fullWidth                             
                          />
                    </Grid>
                  </Grid>
               }

            </Grid> 
          </Grid>
  
      </>
    );

}
export default Ficha;
