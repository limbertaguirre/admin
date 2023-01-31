import React, { Fragment,useState } from "react";
import { makeStyles } from '@material-ui/core/styles';
import FormHelperText from '@material-ui/core/FormHelperText';
import SaveIcon from '@material-ui/icons/Save';
import CancelIcon from '@material-ui/icons/Cancel';
import {
    Dialog,
    DialogTitle,
    DialogContent,    
    DialogActions,
    Button,
    FormControl, 
    InputLabel, 
    Select,
    MenuItem,
     } from "@material-ui/core";
    

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
    },
    formControl: {
        marginBottom: theme.spacing(1),
    }
  }));

const AsignarRolesLookupDetailView = (props) => {
     //tipoModal : info, error, warning, success
     const style= useStyles(); 
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
    const [rolError,setRolError] = useState(false);
    const TITLE_OPERATION=operation===0?'Nueva asignación de rol':'Editar asignación de rol ';

    const handleConfirmModal = () => {
        handleCloseConfirmParent();
    };

    const handleCancelModal=()=>{
        handleCloseCancelParent();
    }
      
       

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
                        name="usuario"
                        label="Usuaario"
                        value={idUserSelected}
                        onChange={(e)=>{handleUser(e.target.value)}}
                        disabled={operation===1} // op 1 edit
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
                    <Button disabled={!isFormValid()} onClick={handleConfirmModal} variant="contained" color="secondary" className={style.botones}>
                        <SaveIcon/> Aceptar
                    </Button>

                    <Button onClick={handleCancelModal} variant="contained" color="primary" className={style.botones} >
                        <CancelIcon /> Cancelar
                    </Button>
            </DialogActions>           
            </Dialog>
        </Fragment>
    );
};
export default AsignarRolesLookupDetailView;