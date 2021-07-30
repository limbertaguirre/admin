import React, {useEffect,useState}  from 'react';
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import HomeIcon from '@material-ui/icons/Home';
import UsuariosRolesListView from './components/UsuariosRolesListView';
import AsignarRolesDetailView from './components/AsignarRolesDetailView';
import { requestPost, requestGet } from "../../service/request";
import AsignarRolesLookupDetailView from './components/AsignarRolesLookupDetailView';
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
import CheckIcon from '@material-ui/icons/Check';
import DeleteIcon from '@material-ui/icons/Delete';
import CloseIcon from '@material-ui/icons/Close';
import AddIcon from '@material-ui/icons/Add';
import * as Action from '../../redux/actions/messageAction';
  
import * as permiso from '../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../lib/accesosPerfiles';
import {useSelector, useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';


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

 const SetRol =(props)=> {

  
    let history = useHistory();

    const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
    useEffect(()=>{  
      try{  
       verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
      }
      catch (err) 
      {
        verificarAcceso(perfiles, 'none', history);
      }
    },[])

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
    const [operation,setOperation] = useState(2);//0: new item; 1: edit item; 2:none;
    const [open,setOpen] = useState(false);
    const [idRolSelected,setIdRolSelected] = useState(0);
    const [idUserSelected,setIdUserSelected] = useState(0);
    const [roles,setRoles] = useState([]);
    const [usuarios,setUsuarios] = useState([]);
    const [reloadListView, setReloadListView] = useState(false);
    const [usuariosRol,setUsuariosRol] =useState([]);
    const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});


     /*AsignarRolesLookupDetailView */

     const handleRolParent= (id) =>{
      setIdRolSelected(id);
    }

    const handleUserParent= (id) =>{
      setIdUserSelected(id);
    }
     
    const newUsuarioRol=()=>{
      //0: Nuevo asignacion de rol
      setOperation(0);
      reloadData(0,0);
      setOpen(true);
    }

    const editUsuarioRol=(idUsuario, idRol)=>{
      //1: Editar rol de usuario
      setOperation(1);
      reloadData(idUsuario,idRol);
      setOpen(true);


    }



    /*UsuariosRolesListView */

    const getUsuariosRol=()=>{
      const headerData={usuarioLogin:userName};
      requestGet('Usuario/GetUsuariosRol',headerData,dispatch).then((res)=>{ 
        if(res.code === 0){                 
          setUsuariosRol(res.data);
        }
      })   
    }



    const handleCloseConfirmParent=()=>{
      
      let body={
        usuarioId: idUserSelected, 
        rolId: idRolSelected,
        userOperationId: idUsuario,
        userOperationUsername: userName
      };
      
        requestPost('Usuario/SetRol',body,dispatch).then((res)=>{ 
            if(res.code === 0){

                getUsuariosRol();
                dispatch(Action.showMessage({ message: 'Operación completada exitosamente', variant: "success" }));
                setOpen(false);
            }
            else{
              dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }
        }) 
    }

    const reloadData=(usuario, rol)=>{
      getRoles();
      getUsuarios();
      setIdRolSelected(usuario);
      setIdUserSelected(rol);
    }

    const getRoles=()=>{
        const headerData={usuarioLogin:userName};
        requestGet('Rol/GetRoles',headerData,dispatch).then((res)=>{ 
            if(res.code === 0){                 
            setRoles(res.data);
            }
        })    
    };

    const getUsuarios=()=>{
      const bodyData={idUsuario:idUsuario,usuarioLogin:userName, operation:operation};
          requestPost('Usuario/GetUsuariosForSelect',bodyData,dispatch).then((res)=>{ 
              if(res.code === 0){                 
              setUsuarios(res.data);
              }
          })    
    };

    const handleCloseCancelParent= ()=>{
      setOpen(false);
      setOperation(2);
    }

    useEffect(()=>{
      reloadData(idUserSelected,idRolSelected);

    },[operation]);

    useEffect(()=>{
      getUsuariosRol();
    },[]);

    return (
      <>
        <Container maxWidth="xl">
          <div className="col-xl-12 col-lg-12 d-none d-lg-block"  style={{ paddingLeft: "0px", paddingRight: "0px" }}>
              <Breadcrumbs aria-label="breadcrumb">
                        <StyledBreadcrumb key={1} component="a" label="Gestión de seguridad"icon={<HomeIcon fontSize="small" />}  />
                        <StyledBreadcrumb key={2} component="a" label="Gestión de usuarios"  />
                        <StyledBreadcrumb key={3} label="Asinación de roles" />
              </Breadcrumbs>
          </div>
          <br/>
          <Typography variant="h4" gutterBottom className={style.etiqueta} >
             {'Asinación de roles'}
          </Typography>
          <Card>
            <Grid container spacing={3}  justify="space-between" className={style.gridContainer}>
              <Grid item>
              <Typography variant="h5" gutterBottom className={style.etiqueta} >
                {'Listado de usuarios con rol'}
              </Typography>
              </Grid>
              <Grid item>
              <Button variant="contained" color="primary" onClick={()=>{newUsuarioRol()}}>
                  <AddIcon/> Asignar rol
              </Button>
              </Grid>
            </Grid>
          </Card>
          <UsuariosRolesListView 
            handleEditOperation={editUsuarioRol} 
            usuariosRol={usuariosRol} 
            handleData={getUsuariosRol}
          />
        </Container>
        <AsignarRolesLookupDetailView 
          open ={open}
          idUserSelected={idUserSelected}
          idRolSelected={idRolSelected}
          usuarios={usuarios}
          roles={roles}
          
          handleCloseConfirmParent ={handleCloseConfirmParent}
          handleCloseCancelParent ={handleCloseCancelParent}
          operation={operation}
          handleRol={handleRolParent}
          handleUser={handleUserParent}
           />
      </>
    );

}

export default SetRol;