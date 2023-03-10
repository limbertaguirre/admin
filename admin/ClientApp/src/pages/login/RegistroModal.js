import React,{ useEffect, useState} from 'react';
import { Dialog, DialogContent, Button, Grid } from "@material-ui/core"
import { makeStyles } from '@material-ui/core/styles';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import AssignmentIndIcon from '@material-ui/icons/AssignmentInd';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import esLocale from "date-fns/locale/es";
import { useSelector,useDispatch } from "react-redux";
import * as moment from "moment";
import "moment/locale/es";
import * as Action from '../../redux/actions/LoginAction';
import { verificaAlfanumerico } from "../../lib/expresiones";
import Slide from '@material-ui/core/Slide';

const useStyles = makeStyles((theme) => ({
    icono: {
        width: '40px',
        height: '40px',
    },
    contentTitle: {
        textAlign: 'center',
    },
    contentButton: {
        textAlign: 'center',
    },
    contentDialog: {
         background: '#bgfg55',
        width: '70%',
    },
    TextFiel: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
    },
    formControl: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
    },

}));

const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
  });

const RegistroModal = ({ open, txtUsuario, onHandleClose, accion }) => {
    
    const style = useStyles();
    const dispatch = useDispatch();
    const {listAreas, listSucursales } = useSelector((stateSelector) =>{ return stateSelector.load});

    const [usuarioName, setUsuarioName] = useState("");
    const [nombre, setNombre] = useState("");
    const [apellido, setApellido] = useState("");
    const [telefono, setTelefono] = useState("");
    const [corporativo, setCorporativo]= useState("");
    const [fechaNacimiento, setFechaNacimiento]= useState(moment().format("YYYY/MM/DD"));
    const [area, setArea]= useState(0);
    const [sucursal, setSucursal]= useState(0);
    const [usuarioNameError, setUsuarioNameError] = useState(false);
    const [nombreError, setNombreError] = useState(false);
    const [apellidoError, setApellidoError] = useState(false);
    const [telefonoError, setTelefonoError] = useState(false);
    const [corporativoError, setCorporativoError]= useState(false);
    const [fechaNacimientoError, setFechaNacimientoError]= useState(false);
    const [areaError, setAreaError]= useState(false);
    const [sucursalError, setSucursalError]= useState(false);
   
    const isValidUsuarioName = (usuarioName) => {   
        return  usuarioName.length >= 3 && verificaAlfanumerico(usuarioName);
    };
    const isValidNombre = (nombre) => {    
        return  nombre.length >= 3 && verificaAlfanumerico(nombre);
    };
    const isValidApellido = (apellido) => {    
        return  apellido.length >= 3 && verificaAlfanumerico(nombre);
    };
    const isValidTelefono = (telefono) => {    
        return  telefono.length >= 3;
    };
    const isValidCorporativo = (corporativo) => {    
        if(corporativo.length >= 1){
          //aqui validar y devolver false para q interprete el componente
            if(corporativo.length >= 2){
                return true
            }else{
                return false
            }
        }else{
            return  true
        }
        
    };
    const isValidArea = (area) => {  
        return  area >= 1;
    };
    const isValidSucursal = (sucursal) => {    
        return  sucursal >= 1 ;
    };
    const isValidFechaNacimiento = (fechaNac) => {   
        let fechaValida= moment().subtract(17,'y').format("YYYY/MM/DD") 
        let fechanacimiento= moment(fechaNac).format("YYYY/MM/DD");
        if(fechanacimiento <= fechaValida){
            return  true;
        }else{
            return  false;
        }
    };

    const _onChangeregistro= (e) => {
            const texfiel = e.target.name;
            const value = e.target.value;
            if (texfiel === "usuarioName") {
                setUsuarioName(value);
                setUsuarioNameError(!isValidUsuarioName(value));
            }
            if (texfiel === "nombre") {
                setNombre(value);
                setNombreError(!isValidNombre(value));
            }
            if (texfiel === "apellido") {
                setApellido(value);
                setApellidoError(!isValidApellido(value));
            }
            if (texfiel === "telefono") {
                setTelefono(value);
                setTelefonoError(!isValidTelefono(value));
            }
            if (texfiel === "corporativo") {
                setCorporativo(value);
                setCorporativoError(!isValidCorporativo(value));
            }
            if (texfiel === "area") {
                setArea(value);
                setAreaError(!isValidArea(value));
            }
            if (texfiel === "sucursal") {
                setSucursal(value);
                setSucursalError(!isValidSucursal(value));
            }
            
      };
    useEffect(()=>{
        setUsuarioName(txtUsuario);
    },[txtUsuario]);

    useEffect(()=>{
        dispatch(Action.cargarAreas());
        dispatch(Action.cargarSucursales());
    },[])
    const _onChangeFechaNacimiento= (date) => {
         setFechaNacimiento(moment(date).format("YYYY/MM/DD"));
         setFechaNacimientoError(!isValidFechaNacimiento(date));
    }
    const isFormValid=()=> {
        return isValidUsuarioName(usuarioName) && isValidNombre(nombre) && 
                 isValidApellido(apellido) && isValidTelefono(telefono) && 
                 isValidCorporativo(corporativo) && isValidFechaNacimiento(fechaNacimiento) &&
                  isValidArea(area) && isValidSucursal(sucursal);
      }
    const registrarUsuarioNuevo= () => {
        dispatch(Action.registrarUsuario(usuarioName.trim(),nombre.trim(),apellido.trim(),telefono, corporativo,fechaNacimiento,area,sucursal));
    }

    return (
        <Dialog
            disableEscapeKeyDown
            disableBackdropClick
            open={open}
            onClose={onHandleClose}
            TransitionComponent={Transition}
        >
        <DialogContent >
                    <Grid item xs={12} className={style.contentTitle} >
                      <AssignmentIndIcon className={style.icono} />
                    </Grid>
                    <Grid item xs={12} className={style.contentTitle}  >
                        <Typography variant="h4" gutterBottom>
                           REGISTRATE
                        </Typography>
                    </Grid>
                       <TextField
                            id="usuarioName"
                            label="Usuario"
                            type={'text'}
                            variant="outlined"
                            name="usuarioName"
                            disabled
                            value={usuarioName}
                            onChange={_onChangeregistro}
                            className={style.TextFiel}
                            error={usuarioNameError}
                            helperText={ usuarioNameError &&
                            "El usuario es requerido, no cumple con los requisitos"
                            }
                            required
                            fullWidth
                            inputProps={{
                            maxLength: 20,
                            }}
                        />

                        <TextField
                            id="nombre"
                            label="Nombre"
                            type={'text'}
                            variant="outlined"
                            name="nombre"
                            value={nombre}
                            onChange={_onChangeregistro}
                            className={style.TextFiel}
                            error={nombreError}
                            helperText={ nombreError &&
                            "El nombre es requerido, no cumple con los requisitos"
                            }
                            required
                            fullWidth
                            inputProps={{
                            maxLength: 20,
                            }}
                        />
                        <TextField
                            id="apellido"
                            label="Apellido"
                            type={'text'}
                            variant="outlined"
                            name="apellido"
                            value={apellido}
                            onChange={_onChangeregistro}
                            className={style.TextFiel}
                            error={apellidoError}
                            helperText={ apellidoError &&
                            "El apellido es requerido, no cumple con los requisitos"
                            }
                            required
                            fullWidth
                            inputProps={{
                            maxLength: 20,
                            }}
                        />
                        <TextField
                            className={style.TextFiel}
                            error={telefonoError}
                            helperText={telefonoError && "El telefono es requerido"}
                            id="telefono"
                            label="Telefono"
                            name="telefono"
                            type="number"
                            InputProps={{
                               // inputComponent: NumberFormatCustom,
                                maxLength: 15,
                            }}
                            disabled={false}
                            onChange={_onChangeregistro}
                            value={telefono}
                            variant="outlined" 
                            required
                            fullWidth
                        />
                        <TextField
                            id="corporativo"
                            label="Corporativo"
                            type={'number'}
                            variant="outlined"
                            name="corporativo"
                            value={corporativo}
                            placeholder=" Ejemplo : 123"
                            onChange={_onChangeregistro}
                            className={style.TextFiel}
                            error={corporativoError}
                            helperText={ corporativoError &&
                            "Numero de corporativo incorrecto"
                            }                            
                            fullWidth
                            InputProps={{
                                startAdornment: (
                                  <InputAdornment position="start">
                                     {'#'}
                                  </InputAdornment>
                                ),
                              }}
                            inputProps={{
                            maxLength: 10,
                            }}
                        />
                         <MuiPickersUtilsProvider utils={DateFnsUtils} locale={esLocale}>
                            <KeyboardDatePicker
                                    fullWidth
                                    autoOk
                                    variant="inline"
                                    inputVariant="outlined"
                                    className={style.TextFiel}
                                    label="Fecha de Nacimiento"
                                    format="yyyy/MM/dd"
                                    value={fechaNacimiento}
                                    error={fechaNacimientoError}
                                    helperText={fechaNacimientoError &&'Ingrese un a??o de nacimiento valido'}
                                    InputAdornmentProps={{ position: "start" }}
                                    invalidDateMessage={'Formato de fecha no v??lido'}
                                    onChange={_onChangeFechaNacimiento}
                            />
                        </MuiPickersUtilsProvider>
                         <FormControl variant="outlined"  
                          fullWidth
                          error={areaError} 
                          className={style.formControl}>
                            <InputLabel id="demo-simple-select-outlined-labelarea">Area</InputLabel>
                            <Select
                                labelId="demo-simple-select-outlined-labelarea"
                                id="demo-simple-select-outlined"
                                value={area}
                                name="area"
                                onChange={_onChangeregistro}
                                label="Area"
                                >
                                <MenuItem value={0}>
                                    <em>Seleccione tu Area</em>
                                </MenuItem>
                                {listAreas.map((value,index)=> ( <MenuItem key={index} value={value.idArea}>{value.nombre}</MenuItem> ))}                             
                            </Select>
                            <FormHelperText>{areaError&&'Seleccione una area de trabajo'}</FormHelperText>
                        </FormControl>
                        <FormControl  variant="outlined"  
                           fullWidth  
                           error={sucursalError} 
                           className={style.formControl}>
                            <InputLabel id="demo-simple-select-outlined-labelsucursal">Sucursal</InputLabel>
                            <Select
                                labelId="demo-simple-select-outlined-labelsucursal"
                                id="demo-simple-select-outlined"
                                value={sucursal}
                                name="sucursal"
                                onChange={_onChangeregistro}
                                label="Sucursal"
                                >
                                <MenuItem value={0}>
                                    <em>Seleccione una sucursal</em>
                                </MenuItem>
                                {listSucursales.map((value,index)=> ( <MenuItem key={index} value={value.idSucursal}>{value.nombre}</MenuItem> ))}                               
                            </Select>
                            <FormHelperText>{sucursalError&&'Seleccione una sucursal'}</FormHelperText>
                        </FormControl>
                    <Grid container item xs={12}  >
                        <Grid item xs={6} >
                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                color="secondary"
                                className={style.submit}
                                onClick = {onHandleClose}                            
                           >
                            CANCELAR
                        </Button>     
                        </Grid>   
                        <Grid item xs={6}  >
                            <Button 
                               onClick={registrarUsuarioNuevo}
                               variant="contained"
                               fullWidth
                               disabled={!isFormValid()}
                               color="secondary"
                                >
                                REGISTRATE
                            </Button>
                        </Grid> 
                    </Grid>
                
            </DialogContent>
        </Dialog>
    );
}
export default RegistroModal;