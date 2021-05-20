

import React,{useState, useEffect, useReducer }  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { Dialog, DialogContent, Button, Grid } from "@material-ui/core"
import { makeStyles } from '@material-ui/core/styles';
import { verificaAlfanumerico } from "../../lib/expresiones";
import { useSelector,useDispatch } from "react-redux";
import * as Action from '../../redux/actions/usuarioAction';
import CheckPagina from './CheckPagina';
import CheckPermiso from './CheckPermiso';
import HistoryModel from './HistoryModel';
//-----------------------transferencia
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';
import Paper from '@material-ui/core/Paper';
//-----------------------------------------------
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Divider from '@material-ui/core/Divider';
import SearchIcon from '@material-ui/icons/Search';
const useStyles = makeStyles((theme) => ({
    icono: {
        width: '40px',
        height: '40px',
    },
    contentTitle: {
        textAlign: 'rigth',
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
        width: '50%',
    },
    TextFielDescripcion: {
      marginBottom: theme.spacing(1),
      marginTop: theme.spacing(1),
      width: '100%',
    },
    TextFielBusqueda:{
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
        width: '40%',
    },
    formControl: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
    },
    //-------------------------------
  //--estilo de transferencia
    root: {
        margin: 'auto',
        width: '100%',
      },
      paper: {
        width: '100%',
        height: '700px',
        overflow: 'auto',
      },
      button: {
        margin: theme.spacing(0.5, 0),
      },
      ///--------------------------------
      heading: {
        fontSize: theme.typography.pxToRem(15),
        flexBasis: '33.33%',
        flexShrink: 0,
      },
      rootAcordion: {
        width: '100%',
      },
      acordionStilo:{
        backgroundColor: 'rgba(0, 0, 0, .03)',
       
      }
      
}));
//---------funciona para transferencia--------------
    function not(a, b) {
        return a.filter((value) => b.indexOf(value) === -1);
    }
    
    function intersection(a, b) {
       
        return a.filter((value) => b.indexOf(value) !== -1);
    }
//--------------------------

const  Roles =()=>  {       
    const style = useStyles();
    const dispatch = useDispatch();
    const [rolName, setRolName] = useState("");
    const [rolNameError, setRolNameError] = useState(false);
    const [rolDescripcion, setRolDescripcion] = useState("");
    const [rolDescripcionError, setRolDescripcionError] = useState(false);
    const [txtBusqueda, setTxtBusqueda] = useState("");
    const [txtBusquedaError, setTxtBusquedaError] = useState(false);

    const {listModulos, listPermisos } = useSelector((stateSelector) =>{ return stateSelector.usuario});
    const [lModulos, setLModulos] = useState([]);
    const [lPermisos, setLPermisos] = useState([]);




    const isValidRolName = (usuarioName) => {   
        return  usuarioName.length >= 5 && verificaAlfanumerico(usuarioName);
    };
    const isValidRolDescripcion = (descripcion) => {   
      return  descripcion.length >= 5 && verificaAlfanumerico(descripcion);
    };
    const isValidtxtBusqueda = (busqueda) => {   
      return  busqueda.length >= 5;
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
        if (texfiel === "txtBusqueda") {
          setTxtBusqueda(value);
         // setTxtBusquedaError(!isValidtxtBusqueda(value));
        }
        console.log('ingreso ',value)
    };
    useEffect(()=>{
        dispatch(Action.getPaginas());
    },[]);
    useEffect(()=>{
      setLModulos(listModulos);
     // console.log('permisos , ', lPermisos);
  },[lModulos, lPermisos ]);

    //----------------------------------------------------------------------------------
    const [checked, setChecked] = React.useState([]);

    const [left, setLeft] = React.useState([]);
    const [right, setRight] = React.useState([]);
  
    const leftChecked = intersection(checked, left);
   const rightChecked = intersection(checked, right);
  
    const handleToggle = (value, nombre) => () => {
      console.log('onchange ini------------------------------: ');
          const currentIndex = checked.indexOf(value);
          const newChecked = [...checked];
          console.log('currentIndex : ', currentIndex);
          console.log('newChecked : ', newChecked);

          if (currentIndex === -1) {
            var value1= { id_pagina:value, nombre:nombre};
            newChecked.push(value1);
          } else {
            newChecked.splice(currentIndex, 1);
          }
         setChecked(newChecked);
      console.log('onchange ini------------------------------: ');
    };
  
    const handleAllRight = () => {
      console.log(left)
      setRight(right.concat(left));
      setLeft([]);
    };
  
    const handleCheckedRight = () => {
     // setRight(right.concat(leftChecked));
    //  setLeft(not(left, leftChecked));
    //  setChecked(not(checked, leftChecked));
    };
  
    const handleCheckedLeft = () => { 
    //  setLeft(left.concat(rightChecked));
    //  setRight(not(right, rightChecked));
    //  setChecked(not(checked, rightChecked));
    };
  
    const handleAllLeft = () => {
      setLeft(left.concat(right));
      setRight([]);
    };
    useEffect(()=>{
     // console.log('el checked', checked);
   },[checked, ]);

     const checkeselecionado = (valuesid) => {
        
          const tupla= checked.find(x => x.id_pagina === parseInt(valuesid));
          
          if(tupla !== undefined){
            console.log('ingreso undefine :', tupla);
            return true
          }
    };
    //---------------------------------------------------------------------------
    //const appReducer = (state, Action) =>{
      //  switch(Action.type){
      //        case 'ADD_PERMISO':{
        //        historico: [...state.historico, Action.payload];
         //     }
       //  }
  //   }
    const incializador ={
     historico: [
        {
          idmodulo:0,
          nombreModulo:'',
          paginas:[
                { 
                  idPagina:0,
                  nombrePagina:'',
                  permisos:[
                            { 
                              idPermiso:0,
                              permiso:''
                            }
                  ]
                }
          ]
        }
      ]
    }
    const incioHistori =[
         {
           idmodulo:0,
           nombreModulo:'',
           paginas:[
                 { 
                   idPagina:0,
                   nombrePagina:'',
                   permisos:[
                             { 
                               idPermiso:0,
                               permiso:''
                             }
                   ]
                 }
           ]
         }
       ]
     
   // const [historial, setHistorial] = useReducer(appReducer ,incializador);
    const [pageSelected, setPageSelected]= useState({id_pagina:0, nombre: ''})
    const [moduloSelected, setModuloSelected]= useState({idModulo:0, nombre: ''})
    const [listHisotrico, setListHisotrico]= useState([])
    const [listPaginaAgregadas, setPListPaginaAgregadas]= useState([])
    const [componente, setComponente] = useState(false);

    const selecionoPagina = (pagina,idModulo, nombreModulo) => {
     // console.log("agregara en el modulo", nombreModulo)
      console.log("se agrego en raiz", pagina)
      setModuloSelected({idModulo:idModulo, nombre: nombreModulo})
      setLPermisos([]);
      setTimeout(1000);
      setPageSelected(pagina);
      setLPermisos(listPermisos);//se habilita los permisos visibles

    };
    const desSelecionoPagina = (paginadelecte, idModulo, nombreModulo) => {
          
          quitarPagina(paginadelecte.id_pagina, paginadelecte.nombre, idModulo, nombreModulo);
         setPageSelected({id_pagina:0, nombre: ''});
         setLPermisos([]);//se deshabilita los permisos visibles

    };
//-----------------------------------------------------------------------
//-- componente permiso
      const selecionoPermiso = (permiso) => {
        console.log("se agrego  permiso", permiso)
        addCalculoHisotorico(permiso, moduloSelected.idModulo, moduloSelected.nombre, pageSelected.id_pagina,pageSelected.nombre);
       
      };
      const desSelecionoPermiso = (permisoDelete) => {
        //  console.log("se elimino permiso", permisoDelete );
          eliminarPermisoCheck(permisoDelete, moduloSelected.idModulo, moduloSelected.nombre, pageSelected.id_pagina, pageSelected.nombre);
      };
      useEffect(()=>{
  
        console.log('lista add hisotrico', listHisotrico);
     },[listPaginaAgregadas, listHisotrico ]);
  
     useEffect(()=>{
      
   },[ moduloSelected, lPermisos]);

    const addCalculoHisotorico =(permiso, idModulo,nombreModulo, idPagina, nombrePagina)=>{
       const backupHistory = [...listHisotrico];
       console.log('inicia con : ', backupHistory);
        let objmoduloBk = listHisotrico.filter(x => x.idModulo != parseInt(idModulo));//--------------
        let objmodulo = listHisotrico.filter(x => x.idModulo == parseInt(idModulo));
        const moBK = [...objmoduloBk];

       // console.log(' modulo hisotico elejido',objmodulo);
        if(objmodulo.length == 0){
          // console.log('no existe modulo hisotico',objmodulo);
           const addNew= {
                        idModulo:idModulo,
                        nombreModulo:nombreModulo,
                        paginas:[
                              { 
                                idPagina:idPagina,
                                nombrePagina:nombrePagina,
                                permisos:[
                                          { 
                                            idPermiso:permiso.id_permiso,
                                            permiso:permiso.permiso
                                          }
                                ]
                              }
                        ]
                      };
            moBK.push(addNew);
            backupHistory.push(addNew);
            setListHisotrico(moBK);
        }else  {
         // console.log('existe modulo hisotico',objmodulo);
          let objpaginaBK = objmodulo[0].paginas.filter(x => x.idPagina != parseInt(idPagina));//pagina backup----------------------
          let objpagina = objmodulo[0].paginas.filter(x => x.idPagina == parseInt(idPagina));
          const paBK= [...objpaginaBK];
        //  console.log('pagina backups', objpaginaBK  );
        //  console.log('pagina select', objpagina);
           if(objpagina.length > 0){ 
                let objPermisoBK= objpagina[0].permisos.filter(x => x.idPermiso != parseInt(permiso.id_permiso));//--------------------
                let objPermiso= objpagina[0].permisos.filter(x => x.idPermiso == parseInt(permiso.id_permiso));
                const peBK= [...objPermisoBK];
             //   console.log('permiso tiene', objPermisoBK);
               // console.log('permiso nuevo', objPermiso);
                  
                  if(objPermiso.length == 0){//add
                   // console.log('permiso nuevo', permiso);
                    peBK.push({idPermiso: permiso.id_permiso, permiso:permiso.permiso});
                    const page= { 
                      idPagina:idPagina,
                      nombrePagina:nombrePagina,
                      permisos:peBK
                    }
                    paBK.push(page);
                    const mode={
                          idModulo:idModulo,
                          nombreModulo:nombreModulo,
                          paginas:paBK
                    }
                    moBK.push(mode);
                    setListHisotrico(moBK);
                    console.log('permiso agregado al primer objeto',moBK);
                  }else{
                    console.log('eliminar aqui');
                  }
            }else{
             // console.log('nueva pagina ,para el modulo, nombre pagina :', nombrePagina );

                    const newPage=  { 
                              idPagina:idPagina,
                              nombrePagina:nombrePagina,
                              permisos:[ {  idPermiso:permiso.id_permiso, permiso:permiso.permiso }  ]
                           };
                  paBK.push(newPage);
                  const newModulo={
                              idModulo:idModulo,
                              nombreModulo:nombreModulo,
                              paginas:paBK
                            }
                  moBK.push(newModulo);
                  setListHisotrico(moBK);
                 // console.log('otra modulo + backup', moBK);
 
            }
        }
       
    }

    const eliminarPermisoCheck =(permiso, idModulo,nombreModulo, idPagina, nombrePagina)=>{

       let objmoduloBk = listHisotrico.filter(x => x.idModulo != parseInt(idModulo));//--------------
        let objmodulo = listHisotrico.filter(x => x.idModulo == parseInt(idModulo));
        const moBK = [...objmoduloBk];
        if(objmodulo.length >= 1){
              let objpaginaBK = objmodulo[0].paginas.filter(x => x.idPagina != parseInt(idPagina));//pagina backup----------------------
              let objpagina = objmodulo[0].paginas.filter(x => x.idPagina == parseInt(idPagina));
              const paBK= [...objpaginaBK];
    
              if(objpagina.length > 0){ 
                
                  let objPermisoBK= objpagina[0].permisos.filter(x => x.idPermiso != parseInt(permiso.id_permiso));
                  let objPermiso= objpagina[0].permisos.filter(x => x.idPermiso == parseInt(permiso.id_permiso));
                  const peBK= [...objPermisoBK];

                  console.log('pagina delete objPermisoBK', objPermisoBK);
                  console.log('pagina delete objPermiso', objPermiso);
                  if(objPermiso.length > 0){
                        const page= { 
                          idPagina:idPagina,
                          nombrePagina:nombrePagina,
                          permisos:peBK
                        }
                        paBK.push(page);
                        console.log('cero paBK 11: ', paBK);
                        const mode={
                              idModulo:idModulo,
                              nombreModulo:nombreModulo,
                              paginas:paBK
                        }
                       
                     if(peBK.length > 0){ 
                       //se add el backup y  
                         moBK.push(mode);                 
                        setListHisotrico(moBK);
                     }else{
                       //aqui lo aqgrega sin la pagina ya que la pagina no tiene permiso
                       console.log('cero paBK 22: ', paBK);
                       console.log('cero paBK 22: ', objpaginaBK);
                       const modesinVacio={
                        idModulo:idModulo,
                        nombreModulo:nombreModulo,
                        paginas:objpaginaBK
                         }
                        console.log('cero : ', modesinVacio);
                        moBK.push(modesinVacio); 
                        setListHisotrico(moBK);

                     }
                    
                   }
              }
           
        }
    }

    const quitarPagina=(PidPagina, PnombrePagina, pidModulo, pnombreModulo)=>{

      let objmoduloBk = listHisotrico.filter(x => x.idModulo != parseInt(pidModulo));//--------------
      let objmodulo = listHisotrico.filter(x => x.idModulo == parseInt(pidModulo));
      const moBK = [...objmoduloBk];
    
        if(objmodulo.length >= 1){
            let objpaginaBK = objmodulo[0].paginas.filter(x => x.idPagina != parseInt(PidPagina));//pagina backup----------------------
            let objpagina = objmodulo[0].paginas.filter(x => x.idPagina == parseInt(PidPagina));
            
            if(objpagina.length > 0){ 

                  const modesinVacio={
                    idModulo:pidModulo,
                    nombreModulo:pnombreModulo,
                    paginas:objpaginaBK
                    }
                    console.log('cero : ', modesinVacio);
                    moBK.push(modesinVacio); 
                    setListHisotrico(moBK);
                    console.log('paginas total : ', objpaginaBK);
                    if(objpaginaBK.length <= 0){
                      //aqui solo se agregara los modulos consu paginas
                      console.log('paginas total resul : ', objpaginaBK);
                     setListHisotrico(objmoduloBk);
                    }
            }

        }

    }

    const onchangeRegistrarRol= ()=>{

          dispatch(Action.registrarRoles(rolName, rolDescripcion, listHisotrico ));

    }




    return (
         <>            
            <Typography variant="h6" gutterBottom>
                GESTION DE ROLES
            </Typography>   
            <Grid item xs={12} className={style.contentTitle} >
                         <TextField
                            id="rolName"
                            label="Nuevo Rol"
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
                            id="rolDescripcion"
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
            <br /> 
            <Grid item xs={12} className={style.contentTitle} >
                         <TextField
                            id="txtBusqueda"
                            label="Buscar Modulo"
                            type={'text'}
                            variant="outlined"
                            name="txtBusqueda"
                            value={txtBusqueda}
                            onChange={_onChangeregistro}
                            className={style.TextFielBusqueda}
                            error={txtBusquedaError}
                            helperText={ txtBusquedaError &&
                            "El campo es requerido"
                            }
                            InputProps={{
                              startAdornment: (
                                <InputAdornment position="start">
                                  <SearchIcon />
                                </InputAdornment>
                              ),
                            }}
                            
                        />      


           </Grid>
            <br />
            <Grid item xs={12} >

                <Grid container spacing={2} alignItems="center" className={style.root}>
            
                   
                </Grid>

            </Grid>
 
            <Grid container xs={12} spacing={1} alignItems="center" className={style.root}>
                <Grid item xs={4} >
                  <List dense component="div" role="list"  >
                      <Paper className={style.paper}>
                          
                            {lModulos.map((value, index) => {
                              return (
                                  <div className={style.rootAcordion}>
                                    <Accordion square   >
                                      <AccordionSummary
                                        expandIcon={<ExpandMoreIcon />}
                                        aria-controls="panel1bh-content"
                                        id="panel1bh-header"
                                        style={{width:'100%'}}
                                        className={style.acordionStilo}
                                      >
                                        <div >                                        
                                             <Typography className={style.heading} >
                                                <b>{'Modulo : '}</b>
                                                { value.nombre}
                                             </Typography>
                                        </div>
                                      </AccordionSummary>
                                      <AccordionDetails>
                                             <List dense component="div" role="list">
                                                  {value.listmodulos.map((value2,index) => {
                                                  const labelId = `transfer-list-item-${value.idModulo}-label`;
                                                  return (
                                                    <ListItem key={value2.id_pagina} role="listitem" button 
                                                    //onClick={handleToggle(value.id_pagina,value.nombre )}
                                                    >
                                                      <CheckPagina modulo={value} pagina={value2} labelId={labelId} selecionoPagina={selecionoPagina} desSelecionoPagina={desSelecionoPagina} />
                                                    </ListItem>
                                                  );
                                                })}
                                                <ListItem />
                                              </List>
                                      </AccordionDetails>
                                    </Accordion>                                                                                        
                                  </div>
                              );
                            })}
                          
                      </Paper>    
                    </List>            
                </Grid>
                <Grid item xs={4} >
                <Paper className={style.paper}>
                <List dense component="div" role="list">
                  <Typography variant="h6" gutterBottom>
                  {'  '} Asignar permiso a  : <b>{pageSelected.nombre}</b> 
                  </Typography> 
                  <Divider />
              
                             <>
                             {lPermisos.map((value2,index) => {
                             const labelId = `transfer-list-item-${value2.id_permiso}-label`;
                             return (
                               <ListItem key={value2.id_permiso} role="listitem" button 
                               //onClick={handleToggle(value.id_pagina,value.nombre )}
                               > 
                                <CheckPermiso permiso={value2} labelId={labelId} selecionoPermiso={selecionoPermiso} desSelecionoPermiso={desSelecionoPermiso} />
                                 
                               </ListItem>
                              );
                             })}
                           </>
                           
                  <ListItem />
                  </List>
                </Paper>
                </Grid>
                <Grid item xs={4} >
                <Paper className={style.paper}>
                <List dense component="div" role="list">
                  <Typography variant="h6" gutterBottom>
                  {'  '} Perfiles seleccionados  : <b>{}</b> 
                  </Typography> 
                  <Divider />
              
                     <HistoryModel listHisotrico={listHisotrico} />
                           
                  <ListItem />
                  </List>
                </Paper>
                </Grid>

            </Grid>
            <Grid item xs={12} className={style.contentTitle} >
            <Button variant="contained" color="primary" onClick={onchangeRegistrarRol} >
              REGISTRAR ROL
            </Button>

           </Grid>

         </>
    );
}
export default  Roles;

