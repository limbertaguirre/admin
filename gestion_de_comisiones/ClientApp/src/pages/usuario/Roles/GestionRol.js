

import React,{ useEffect, useState }  from 'react';
import {  Typography } from "@material-ui/core";
import {  Button, Grid, Container } from "@material-ui/core"
import { makeStyles, emphasize, withStyles  } from '@material-ui/core/styles';
import { useSelector,useDispatch } from "react-redux";
import {requestGet, requestPost} from '../../../service/request'; 
import { useHistory } from "react-router-dom";
import * as Action from '../../../redux/actions/usuarioAction';
import * as ActionMensaje from '../../../redux/actions/messageAction';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';
import AddCircleOutlineIcon from '@material-ui/icons/AddCircleOutline';
import CardRol from './component/CardRol';
import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';

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
    root: {
       flexGrow: 1,
      },
    gridNewRol: {
        display: 'flex',
        alignItems:"flex-end",
        justifyContent: 'flex-end',
    },
    contentMenu: {
        display: "flex",
        width: "100%",
        marginTop: 10,
        flexDirection: "row",
        flexWrap: "wrap",
        justifyContent: "center",
        alignContent: "stretch",
        alignItems: "center",
      },

}));

const  GestionRol =(props)=>  {      
    const style = useStyles();
    const dispatch = useDispatch();
    let history = useHistory();
    const [namePage, setNamePage]= useState('');
    const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
    useEffect(()=>{  try{  
      setNamePage(props.location.state.namePagina);
       verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
       }catch (err) {  verificarAcceso(perfiles, 'none', history); }
    },[])

    const [listaModulos, setListaModulos]= useState([]);
    const {userName} =useSelector((stateSelector)=>{ return stateSelector.load});

    const obtenerModulos = () =>{ 
            const headers={userLogin:userName};
            requestGet('Rol/ObtenerRolesAllModules',headers,dispatch).then((res)=>{ 
              if(res.code === 0){        
                setListaModulos(res.data)        
                     
              }else{
                  dispatch(ActionMensaje.showMessage({ message: res.message, variant: "error" }));
              }   
            })    
     }

     useEffect(()=>{        
          obtenerModulos();
     },[ ]);

    const redirecionarEditRol=(idRol)=>{
        dispatch(Action.ObtenerRolModulos(idRol));
        const location = {
          pathname: '/gestion/edit/rol',
          state: {idRol: idRol, permisoActualizar:validarPermiso(perfiles, namePage + permiso.ACTUALIZAR) }
        }
        history.push(location);
    }
    useEffect(()=>{        
    },[listaModulos ]);

    return (
         <>    
           <Container>
            <br/>
              <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
                <Breadcrumbs aria-label="breadcrumb">
                          <StyledBreadcrumb key={1} component="a" label="Gestión de Roles"icon={<HomeIcon fontSize="small" />}  />              
                </Breadcrumbs>
              </div>
            <br/>
              <div className={style.root}>
                    <Grid container spacing={3}>
                        <Grid item xs={6}>
                            <Typography variant="h6" gutterBottom>
                              Gestión de Roles
                            </Typography>  
                            </Grid>
                        <Grid item xs={6} className={style.gridNewRol} >
                            {validarPermiso(perfiles, namePage + permiso.CREAR)&& 
                              <Button variant="contained" 
                                  color="primary" 
                                  onClick={()=> history.push("/gestion/nuevo/roles")} >
                                  <AddCircleOutlineIcon />
                                  {' '}{' NUEVO ROL'}
                              </Button>
                            }
                        </Grid>
                    </Grid>
                </div>                     
              <br /> 
                <Grid item xs={12} >
                  <div className={style.contentMenu}>
                      {listaModulos.map((value,index)=>(
                          <CardRol key={index}  modulo={value} redirecionarEditRol={redirecionarEditRol} actualizar={validarPermiso(perfiles, namePage + permiso.ACTUALIZAR)} />
                      ))}
                  </div>
                </Grid>          
            </Container>
         </>
    );
}
export default  GestionRol;

