import React, {useEffect,useState}  from 'react';
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import HomeIcon from '@material-ui/icons/Home';
import UsuariosRolesListView from './components/UsuariosRolesListView';
import AsignarRolesDetailView from './components/AsignarRolesDetailView';
import { requestPost, requestGet } from "../../service/request";
import AsignarRolesLookupDetailView from './components/AsignarRolesLookupDetailView';
import {Container, 
  Tooltip ,
  Fade,
  Card, 
  Button, 
  Grid,  
  Typography,
  Chip } from "@material-ui/core";
import AddIcon from '@material-ui/icons/Add';
import * as Action from '../../redux/actions/messageAction';
  
import * as permiso from '../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../lib/accesosPerfiles';
import {useSelector, useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import { set } from 'date-fns';


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
      botonAsignar: {                 
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',
       }
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


     const handleRolParent= (id) =>{
      setIdRolSelected(id);
    }

    const handleUserParent= (id) =>{
      setIdUserSelected(id);
    }
     
    const newUsuarioRol=()=>{
      //0: Nuevo asignacion de rol
      setIdUserSelected(0);
      setIdRolSelected(0);
      setOperation(0);
      // reloadData(0,0);
      setOpen(true);
    }

    const editUsuarioRol=(idUsuario, idRol)=>{
      //1: Editar rol de usuario
      setIdUserSelected(idUsuario);
      setIdRolSelected(idRol);
      setOperation(1);
      setOpen(true);
    }


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
                dispatch(Action.showMessage({ message: 'Operaci??n completada exitosamente', variant: "success" }));
                // setOperation(2);
                setOpen(false);
            }
            else{
              dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }
        })

    }

    const handleCloseCancelParent= ()=>{
      setOpen(false);
      // setOperation(2);
    }

    const reloadData=(usuario, rol)=>{
      getRoles();
      getUsuarios();
      setIdRolSelected(rol);
      setIdUserSelected(usuario);
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
         let response =  requestPost('Usuario/GetUsuariosForSelect',bodyData,dispatch);
       
         response.then((res)=>{ 
              if(res.code === 0){                          
                setUsuarios(res.data);
              }
          })    
    };

    

    useEffect(()=>{
      
      if (operation != 2) {
        reloadData(idUserSelected,idRolSelected);
      }

    },[operation]);

    useEffect(()=>{
      if (!open) {
        setOperation(2);
      }
    },[open])

    useEffect(()=>{
      getUsuariosRol();
    },[]);

    return (
      <>
        <Container maxWidth="xl">
          <div className="col-xl-12 col-lg-12 d-none d-lg-block"  style={{ paddingLeft: "0px", paddingRight: "0px" }}>
              <Breadcrumbs aria-label="breadcrumb">
                        <StyledBreadcrumb key={1} component="a" label="Configuraciones"icon={<HomeIcon fontSize="small" />}  />
                        <StyledBreadcrumb key={2} component="a" label="Usuarios"  />
                        <StyledBreadcrumb key={3} label="Asignaci??n de roles" />
              </Breadcrumbs>
          </div>
          <br/>
          <Typography variant="h4" gutterBottom className={style.etiqueta} >
             {'Asignaci??n de roles'}
          </Typography>
          <Card>
            <Grid container spacing={3}  justify="space-between" className={style.gridContainer}>
              <Grid item>
              <Typography variant="h5" gutterBottom className={style.etiqueta} >
                {'Listado de usuarios con rol'}
              </Typography>
              </Grid>
              <Grid item>
                {validarPermiso(perfiles, props.location.state.namePagina + permiso.CREAR)?
                  <Tooltip TransitionComponent={Fade} TransitionProps={{ timeout: 600 }} title="Asignar rol a un nuevo usuario.">
                    <Button variant="contained" color="primary" className={style.botonAsignar} onClick={()=>{newUsuarioRol()}}>
                        <AddIcon/> Asignar rol
                    </Button>
                 </Tooltip>
                :
                <Tooltip TransitionComponent={Fade} TransitionProps={{ timeout: 600 }} title="Sin acceso, para asignar roles.">
                  <Button variant="contained" color={"disabled"}  >
                     <AddIcon/> Asignar rol 
                  </Button>
                 </Tooltip>
                }
              </Grid>
            </Grid>
          </Card>
          <UsuariosRolesListView 
            handleEditOperation={editUsuarioRol} 
            usuariosRol={usuariosRol} 
            handleData={getUsuariosRol}
            Actualizar={validarPermiso(perfiles, props.location.state.namePagina + permiso.CREAR)}
            Eliminar={validarPermiso(perfiles, props.location.state.namePagina + permiso.ELIMINAR)}
          />
        </Container>
        <AsignarRolesLookupDetailView 
          open ={open}

          operation={operation}

          idUserSelected={idUserSelected}  handleRol={handleRolParent} usuarios={usuarios}
          idRolSelected={idRolSelected} handleUser={handleUserParent} roles={roles}

          handleCloseConfirmParent ={handleCloseConfirmParent}
          handleCloseCancelParent ={handleCloseCancelParent}
          
        />
      </>
    );

}

export default SetRol;