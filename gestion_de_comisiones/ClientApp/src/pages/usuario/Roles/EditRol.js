

import React,{useState, useEffect }  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { Dialog, DialogContent, Button, Grid } from "@material-ui/core"
import { makeStyles, emphasize, withStyles  } from '@material-ui/core/styles';
import { useSelector,useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import * as Action from '../../../redux/actions/usuarioAction';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';
import AddCircleOutlineIcon from '@material-ui/icons/AddCircleOutline';
import CardRol from './component/CardRol';
import EditOutlinedIcon from '@material-ui/icons/EditOutlined';
import { verificaAlfanumerico } from "../../../lib/expresiones";

import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';

import EditAcordionModulo from './component/EditAcordionModulo';

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
      rootAcordion: {
        width: '100%',
      },
    gridNewRol: {
        display: 'flex',
        alignItems:"flex-end",
        justifyContent: 'flex-end',
    },
    contentTitle: {
        textAlign: 'rigth',
    },
    TextFiel: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
        width: '50%',
    },
    TextFielDescripcion: {
      marginBottom: theme.spacing(1),
      marginTop: theme.spacing(1),
      width: '100%',
    },

}));

const  EditRol =(props)=>  {       
    const style = useStyles();
    const dispatch = useDispatch();
    const history = useHistory();
    const {objetoRol } = useSelector((stateSelector) =>{ return stateSelector.usuario});
    const [idRol, setIdRol] = useState(0)
    const [rolName, setRolName] = useState("");
    const [rolNameError, setRolNameError] = useState(false);
    const [rolDescripcion, setRolDescripcion] = useState("");
    const [rolDescripcionError, setRolDescripcionError] = useState(false);
    const [hisotryModules, setHisotryModules]= useState([]);
    const [allModules, setAllModules]= useState([]);

 
    useEffect(()=>{
        setIdRol(props.location.state.idRol)
        setRolName(objetoRol.nombre);
        setRolDescripcion(objetoRol.descripcion);
        setHisotryModules(objetoRol.listModulos);
        setAllModules(objetoRol.listModulos);
     },[objetoRol]);
    const isValidRolName = (usuarioName) => {   
        return  usuarioName.length >= 5 && verificaAlfanumerico(usuarioName);
    };
    const isValidRolDescripcion = (descripcion) => {   
      return  descripcion.length >= 5 && verificaAlfanumerico(descripcion);
    };

    const _onChangeregistro= (e) => {
        const texfiel = e.target.name;
        const value = e.target.value;
        if (texfiel === "rolName") {
            setRolName(value);
            setRolNameError(!isValidRolName(value));
        }
        if (texfiel === "rolDescripcion") {
          setRolDescripcion(value);
          setRolDescripcionError(!isValidRolDescripcion(value));
        }


    };
    useEffect(()=>{
      console.log(' local allModules : ',allModules)
   
    },[allModules]);
    const selecionoPermiso = (idModulo,nombreModulo, pagina, permiso, estado) =>{
     agregarPerfil(idModulo,nombreModulo, pagina, permiso, estado);

  /*   console.log('click selecionar true idModulo :', idModulo);
    console.log('click selecionar true nombreModulo :', nombreModulo);
    console.log('click selecionar true pagina : ', pagina);
    console.log('click selecionar true permiso : ', permiso);
    console.log('click selecionar true estado : ', estado); */
    }
    const desSelecionoPermiso = (idModulo,nombreModulo, pagina, permiso, estado)=>{
     /*  console.log('click selecionar false idModulo :', idModulo);
      console.log('click selecionar false nombreModulo :', nombreModulo);
      console.log('click selecionar false pagina : ', pagina);
      console.log('click selecionar false permiso : ', permiso);
      console.log('click selecionar false estado : ', estado); */
    }

    const agregarPerfil=(idModulo,nombreModulo, pagina, permiso, estado)=>{
      let objmoduloBk = allModules.filter(x => x.idModulo != parseInt(idModulo));//--------------
      let objmodulo = allModules.filter(x => x.idModulo == parseInt(idModulo));
      const moBK = [...objmoduloBk];
                  if(objmodulo.length == 0){
                        const addNew= {
                                  idModulo:idModulo,
                                  nombre:nombreModulo,
                                  listmodulos:[
                                        { 
                                          id_pagina:pagina.id_pagina,
                                          nombre:pagina.nombre,
                                          permisos:[
                                                    { 
                                                      id_permiso:permiso.id_permiso,
                                                      permiso:permiso.permiso,
                                                      estado:estado
                                                    }
                                          ]
                                        }
                                  ]
                                };
                      moBK.push(addNew);
                      setAllModules(moBK);
                      console.log('new modulo', moBK)
                  }else{
                        console.log('exite modulo');
                        let objpaginaBK = objmodulo[0].listmodulos.filter(x => x.id_pagina != parseInt(pagina.id_pagina));//pagina backup----------------------
                        let objpagina = objmodulo[0].listmodulos.filter(x => x.id_pagina == parseInt(pagina.id_pagina));
                        const paBK= [...objpaginaBK];
                        console.log('exite pagina bk :', objpaginaBK);
                        console.log('exite pagina :',objpagina);
                        if(objpagina.length > 0){ 
                            //verificamos permiso
                            let objPermisoBK= objpagina[0].permisos.filter(x => x.id_permiso != parseInt(permiso.id_permiso));//--------------------
                            let objPermiso= objpagina[0].permisos.filter(x => x.id_permiso == parseInt(permiso.id_permiso));
                            const peBK= [...objPermisoBK];
                            console.log('exite permisos bk :',objPermisoBK);
                            console.log('exite permiso :',objPermiso);
                            console.log('exite permiso :',objPermiso.length);
                            if(objPermiso.length >= 0){//add

                              peBK.push({id_permiso: permiso.id_permiso,
                                       permiso:permiso.permiso,
                                       estado:estado });
                                    const pageUpdate= { 
                                        id_pagina:pagina.id_pagina,
                                        nombre:pagina.nombre,
                                        permisos:peBK
                                        };
                                    paBK.push(pageUpdate);
                                    const moduloUpdate={
                                          idModulo:idModulo,
                                          nombre:nombreModulo,
                                          listmodulos:paBK
                                    }
                                    moBK.push(moduloUpdate);
                                    console.log('updale all : ',moBK);
                                    setAllModules(moBK);
                            }
                        }
                  }
    }

    return (
         <>    
          <br/>
            <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
              <Breadcrumbs aria-label="breadcrumb">
                    <div onClick={()=> history.goBack()}> <StyledBreadcrumb key={1}  component="a" label="Gestion de Roles"icon={<HomeIcon fontSize="small" />}  />  </div>                        
                    <div><StyledBreadcrumb key={2} component="a" label="Editar Rol "icon={<EditOutlinedIcon fontSize="small" />}  /></div>
              </Breadcrumbs>
           </div>
           <br/>
           <div className={style.root}>
                <Grid container spacing={3}>
                    <Grid item xs={6}>
                        <Typography variant="h6" gutterBottom>
                            Editar de Roles
                        </Typography>  
                        </Grid>
                    <Grid item xs={6} className={style.gridNewRol} >
                        <Button variant="contained" 
                            /* color="primary" */ 
                            style={{background: "#1872b8", boxShadow: '2px 4px 5px #999'}}
                            /* onClick={()=>  history.goBack()} */
                             >                            
                            {' '}{' Procesar Cambios'}
                        </Button>
                    </Grid>  
                <Grid item xs={12}>

                    
                </Grid>
                </Grid>
                <Grid item xs={12} className={style.contentTitle} >
                         <TextField
                           
                            label="Rol"
                            type={'text'}
                            variant="outlined"
                            name="rolName"
                            value={rolName}
                            onChange={_onChangeregistro}
                            className={style.TextFiel}
                            error={rolNameError}
                            helperText={ rolNameError &&
                            "El campo es requerido"
                            }
                            required
                            //fullWidth
                            inputProps={{
                            maxLength: 20,
                            }}
                        />      
                </Grid>
                <Grid item xs={12} className={style.contentTitle} >
                        <TextField
                            label="Descripcion"
                            type={'text'}
                            variant="outlined"
                            name="rolDescripcion"
                            value={rolDescripcion}
                            onChange={_onChangeregistro}
                            className={style.TextFielDescripcion}
                            error={rolDescripcionError}
                            helperText={ rolDescripcionError &&
                            "El campo es requerido"
                            }
                            required
                            fullWidth                                    
                        />      
                </Grid>               
                <div className={style.rootAcordion}>
                    {hisotryModules.map((value, index)=>{                               
                        return( 
                                 <EditAcordionModulo modulo={value} selecionoPermiso={selecionoPermiso} desSelecionoPermiso={desSelecionoPermiso} />
                        )
                     })}                           
                </div>                                 
            </div>                  
         </>
    );
}
export default  EditRol;

