import React, { Fragment,useEffect,useState } from "react";
import EmojiObjectsIcon from "@material-ui/icons/EmojiObjects";
import { Alert, AlertTitle } from '@material-ui/lab';
import { makeStyles } from '@material-ui/core/styles';
import {useSelector, useDispatch} from 'react-redux';
import { requestPost, requestGet } from "../../../service/request";
import FormHelperText from '@material-ui/core/FormHelperText';
import Autocomplete from '@material-ui/lab/Autocomplete';
import {Container, 
    InputAdornment,
    Tooltip ,
    Zoom, 
    Dialog,
    DialogTitle,
    DialogContent,
    DialogContentText,
    DialogActions,
    Card, 
    Button, 
    Grid, 
    TextField, 
    Typography,
    FormControl, 
    InputLabel, 
    Select,
    MenuItem,
    sMenuItem,
    Chip } from "@material-ui/core";
    

const useStyles = makeStyles((theme) => ({
    root: {
      width: '100%',
      '& > * + *': {
        //marginTop: theme.spacing(2),
      },
    },
    botones:{
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',  
        marginBottom:theme.spacing(1),
        marginTop:theme.spacing(1),
        marginRight:theme.spacing(1),
        marginLeft:theme.spacing(1),
    }
  }));

const AsignarRolesLookupDetailView = (props) => {
     //tipoModal : info, error, warning, success
     const { 
        open, 
        idUserSelected,
        idRolSelected,
        usuarios,
        roles,
        handleCloseConfirmParent, 
        handleCloseCancelParent,
        operation,
        handleRol,
        handleUser
    } =props;
    const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
    
    const [rolError,setRolError] = useState(false);
    const TITLE_OPERATION=operation===0?'Nueva asignación':'Editar asignación';

    const handleConfirmModal = () => {
        handleCloseConfirmParent();
    };

    const handleCancelModal=()=>{
        handleCloseCancelParent();
    }

    const useStyles = makeStyles((theme) => ({
        root: {
          flexGrow: 1,
        },
        gridContainer: {
          padding: theme.spacing(2),
        },
        formControl: {
            marginBottom: theme.spacing(1),
        }
      }));
      
    const style= useStyles();
    const dispatch = useDispatch();

    const isFormValid =()=>{
        return (idRolSelected >0 && idUserSelected >0)
    }

    return (
        <Fragment>
            <Dialog
                fullWidth             
                open={open}
                aria-labelledby="customized-dialog-title"
            >
            <DialogTitle id="form-dialog-title">{TITLE_OPERATION}</DialogTitle>
            <DialogContent>
                    <FormControl variant="outlined" fullWidth className={style.formControl}>
                    <InputLabel id="lb-usuario">Usuario</InputLabel>
                        <Select
                        labelId="lb-usuario"
                        id="sl-usuario"
                        name="usuario"
                        label="Usuaario"
                        value={idUserSelected}
                        onChange={(e)=>{handleUser(e.target.value)}}
                        disabled={operation===1}
                        >
                            <MenuItem value={0}>
                                <em>Seleccione el usuario</em>
                            </MenuItem>
                            {usuarios.map((v, i)=> (
                                <MenuItem key={'user_'+v.idUsuario} value={v.idUsuario}>
                                <em>{v.login}</em>
                                </MenuItem>
                            ))}                    
                        </Select>
                        
                    </FormControl>

                    <FormControl variant="outlined" fullWidth className={style.formControl}  error={rolError} >
                        <InputLabel id="lb-rol">Rol</InputLabel>
                        <Select
                            labelId="lb-rol"
                            id="sl-rol"
                            name="rol"
                            label="Rol"
                            value={idRolSelected}
                            onChange={(e)=>{handleRol(e.target.value)}}
                            >
                            <MenuItem value={0}>
                                <em>Seleccione el rol</em>
                            </MenuItem>
                            {roles.map((v, i)=> (
                                <MenuItem key={'rol_'+v.idRol} value={v.idRol}>
                                <em>{v.nombre}</em>
                                </MenuItem>
                            ))}
                        </Select>
                        <FormHelperText>{rolError&&'Seleccione una rol'}</FormHelperText>
                    </FormControl>
                
            </DialogContent>  
            <DialogActions>
                    <Button disabled={!isFormValid()} onClick={handleConfirmModal} variant="contained" color="primary" className={style.botones}>
                        Aceptar
                    </Button>

                    <Button onClick={handleCancelModal} variant="contained" color="secondary" className={style.botones} >
                        Cancelar
                    </Button>
            </DialogActions>           
            </Dialog>
        </Fragment>
    );

};

export default AsignarRolesLookupDetailView;