import React, {useEffect,useState}  from 'react';
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import {useSelector, useDispatch} from 'react-redux';
import CheckIcon from '@material-ui/icons/Check';
import DeleteIcon from '@material-ui/icons/Delete';
import CloseIcon from '@material-ui/icons/Close';
import AddIcon from '@material-ui/icons/Add';
import { requestPost, requestGet } from "../../../service/request";
import {Container, 
    InputAdornment,
    Tooltip ,
    Zoom, 
    Dialog,
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
    


const AsignarRolesDetailView=(props)=>{
    const { handleUser,handleRol,idUserSelected,idRolSelected,handleOperationParent,operationParent} = props;
    const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
    const [roles,setRoles] = useState([]);
    const [usuarios,setUsuarios] = useState([]);

    const useStyles = makeStyles((theme) => ({
        root: {
          flexGrow: 1,
        },
        gridContainer: {
          padding: theme.spacing(2),
        },
        
      }));
      
    let style= useStyles();
    const dispatch = useDispatch();

    const getRoles=()=>{
        const headerData={usuarioLogin:userName};
        requestGet('Rol/GetRoles',headerData,dispatch).then((res)=>{ 
            if(res.code === 0){                 
            setRoles(res.data);
            }
        })    
    };
  
    const getUsuarios=(operation)=>{
    const headerData={usuarioLogin:userName, operation:operation};
        requestGet('Usuario/GetUsuariosForSelect',headerData,dispatch).then((res)=>{ 
            if(res.code === 0){                 
            setUsuarios(res.data);
            }
        })    
    };

    useEffect(()=>{
    getRoles();
    },[]);

    useEffect(()=>{
    getUsuarios(operationParent);
    },[]);

    // useEffect(()=>{
    //    setIdRol(idRolSelected);
    // //    setIdUser(idUserSelected);

    // });
    

    

    return (
    <Grid container spacing={3} className={style.gridContainer}>
        <Grid item xs={6}>
            <FormControl variant="outlined" fullWidth>
            <InputLabel id="lb-usuario">Usuario</InputLabel>
            <Select
                labelId="lb-usuario"
                id="sl-usuario"
                name="usuario"
                label="Usuaario"
                value={idUserSelected}
                onChange={(e)=>{handleUser(e.target.value)}}
                disabled={operationParent===1}

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
        </Grid>
        <Grid item xs={6}>
            <FormControl variant="outlined" fullWidth>
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
            </FormControl>
        </Grid>
        <Grid item xs={12}>
            {
            operationParent===0?
            <>
            <Button variant="contained" color="primary">
                <CheckIcon/> Guardar
            </Button>
            <Button variant="contained" color="secondary"  onClick={()=>{handleOperationParent(2)}}>
            <CloseIcon/> Cancelar
            </Button>
            </>
            :
            <>
            <Button variant="contained" color="secondary">
                <DeleteIcon/> Eliminar
            </Button>
            <Button variant="contained" color="secondary" onClick={()=>{handleOperationParent(2)}}>
                <CloseIcon/> Cancelar
            </Button>
            </>
            }
        
        
        </Grid>
    </Grid>
    )
}

export default AsignarRolesDetailView;