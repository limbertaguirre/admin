import React,{useState} from 'react';
import { Dialog, DialogContent, Button, Grid } from "@material-ui/core"
import { makeStyles } from '@material-ui/core/styles';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';
import CancelIcon from '@material-ui/icons/Cancel';
import { TextField, Typography, InputAdornment, IconButton } from "@material-ui/core";
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';

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
        // background: green[500],
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



const RegistroModal = ({ open, mensaje, onHandleClose, accion }) => {
    const style = useStyles();
    const [usuarioName, setUsuarioName] = useState("");
    const [nombre, setNombre] = useState("");
    const [apellido, setApellido] = useState("");
    const [telefono, setTelefono] = useState("");
    const [corporativo, setCorporativo]= useState("");
    const [fechaNacimiento, setFechaNacimiento]= useState("");
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

    const _onChangeregistro= (e) => {
            const texfiel = e.target.name;
            const value = e.target.value;
            
            if (texfiel === "usuarioName") {
                setUsuarioName(value);
                console.log('escribiendo :', value);
            }
            if (texfiel === "nombre") {
                setNombre(value);
                console.log('escribiendo :', value);
            }
            if (texfiel === "apellido") {
                setApellido(value);
                console.log('escribiendo :', value);
            }
            if (texfiel === "telefono") {
                setTelefono(value);
                console.log('escribiendo :', value);
            }
            if (texfiel === "corporativo") {
                setCorporativo(value);
                console.log('escribiendo :', value);
            }
            console.log('escribiendo :', value);
            console.log('escribiend otexfiel :', e);
            if (texfiel === "area") {
                setArea(value);
                console.log('escribiendo :', value);
            }
            if (texfiel === "sucursal") {
                setSucursal(value);
                console.log('escribiendo :', value);
            }
            
      };


    return (
        <Dialog
            disableEscapeKeyDown
            disableBackdropClick
            open={open}
            onClose={onHandleClose}
        >
            <DialogContent >

                    <br/>
                    <Grid item xs={12} className={style.contentTitle} >
                       {accion?<CheckCircleOutlineIcon className={style.icono} />
                        :<CancelIcon className={style.icono}/> }
                    </Grid>
                    
                    <Grid item xs={12} className={style.contentTitle}  >
                       {mensaje}
                    </Grid>
                    <br/>

                       <TextField
                            id="usuarioName"
                            label="Nombre Usuario"
                            type={'text'}
                            variant="outlined"
                            name="usuarioName"
                            value={usuarioName}
                            onChange={_onChangeregistro}
                            className={style.TextFiel}
                            error={usuarioNameError}
                            helperText={ usuarioNameError &&
                            "El usuario no cumple con los requisitos"
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
                            "El apellido es requerido"
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
                            "El apellido son requeridos"
                            }
                            required
                            fullWidth
                            inputProps={{
                            maxLength: 20,
                            }}
                        />
                        <TextField
                            className={style.boxAmount}
                            error={telefonoError}
                            helperText={telefonoError && "Telefono obligatorio incorrecto"}
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
                            fullWidth
                        />

                         <FormControl variant="outlined"  fullWidth  className={style.formControl}>
                            <InputLabel id="demo-simple-select-outlined-label">Are</InputLabel>
                            <Select
                                labelId="demo-simple-select-outlined-label"
                                id="demo-simple-select-outlined"
                                value={area}
                                name="area"
                                onChange={_onChangeregistro}
                                label="Area"
                                
                                >
                                <MenuItem value={0}>
                                    <em>Seleccione un area</em>
                                </MenuItem>
                                <MenuItem value={10}>UIT</MenuItem>
                                <MenuItem value={20}>CONTABOLIDAD</MenuItem>
                                <MenuItem value={30}>FINANSAS</MenuItem>                               
                            </Select>
                            <FormHelperText>{areaError&&'Seleccione una area de trabajo'}</FormHelperText>
                        </FormControl>
                        <FormControl  variant="outlined"  fullWidth   className={style.formControl}>
                            <InputLabel id="demo-simple-select-outlined-label">Sucursal</InputLabel>
                            <Select
                                labelId="demo-simple-select-outlined-label"
                                id="demo-simple-select-outlined"
                                value={sucursal}
                                name="sucursal"
                                onChange={_onChangeregistro}
                                label="Sucursal"
                                >
                                <MenuItem value={0}>
                                    <em>Seleccione una sucursal</em>
                                </MenuItem>
                                <MenuItem value={10}>Canhoto</MenuItem>
                                <MenuItem value={20}>Ambasador</MenuItem>
                                <MenuItem value={30}>Mansion</MenuItem>
                               
                            </Select>
                            <FormHelperText>{sucursalError&&'Seleccione una sucursal'}</FormHelperText>
                        </FormControl>
                
                        

                    <Grid item xs={12} className={style.contentButton} >
                        <Button onClick={onHandleClose} color={accion? 'primary':'secondary'} autoFocus>
                            OK
                        </Button>
                    </Grid>
                
            </DialogContent>
        </Dialog>
    );
}
export default RegistroModal;