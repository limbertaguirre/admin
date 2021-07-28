import React, {useEffect,useState}  from 'react';
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import HomeIcon from '@material-ui/icons/Home';
import UsuarioRolTable from './components/UsuariosRolesTable';
import { requestPost, requestGet } from "../../service/request";
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
    const [idRolSelected,setIdRolSelected] = useState(0);
    const [roles,setRoles] = useState([]);
    const [usuarios,setUsuarios] = useState([]);
    const [idUserSelected,setIdUserSelected] = useState(0);
    const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});

    const getRoles=()=>{
      const headerData={usuarioLogin:userName};
       requestGet('Rol/GetRoles',headerData,dispatch).then((res)=>{ 
            if(res.code === 0){                 
              setRoles(res.data);
            }
          })    
     };

     const getUsuarios=()=>{
      const headerData={usuarioLogin:userName};
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
      getUsuarios();
     },[]);

     const handleClickSelectedRol= (e) =>{
      setIdRolSelected(e.target.value);
    }

    const HandleCliclSelectedUsurio= (e) =>{
      setIdUserSelected(e.target.value);
    }


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
                        onChange={HandleCliclSelectedUsurio}
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
                        onChange={handleClickSelectedRol}
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

                <Button variant="contained" color="primary">
                  Editar rol
                </Button>
                <Button variant="contained" color="secondary">
                  Eliminar rol
                </Button>
              </Grid>
             </Grid>
           </Card>
            <hr/>
           <Card>
            <Grid container spacing={3} className={style.gridContainer}>
              <Grid item xs={12}>

              </Grid>
            </Grid>
           </Card>
        <UsuarioRolTable />
        </Container>
      </>
    );

}

export default SetRol;