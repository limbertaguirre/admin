

import React,{useState, useEffect}  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { Dialog, DialogContent, Button, Grid } from "@material-ui/core"
import { makeStyles } from '@material-ui/core/styles';
import { verificaAlfanumerico } from "../../lib/expresiones";
import { useSelector,useDispatch } from "react-redux";
import * as Action from '../../redux/actions/usuarioAction';
import CheckPagina from './CheckPagina';
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
        width: '500px',
        height: '300px',
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
        console.log('ejecuata a :', a );
        console.log('ejecuata b:', b );
        return a.filter((value) => b.indexOf(value) !== -1);
    }
//--------------------------

const  Roles =()=>  {       
    const style = useStyles();
    const dispatch = useDispatch();
    const [rolName, setRolName] = useState("");
    const [rolNameError, setRolNameError] = useState(false);
    const {listModulos } = useSelector((stateSelector) =>{ return stateSelector.usuario});
    const [lModulos, setLModulos] = useState([]);




    const isValidRolName = (usuarioName) => {   
        return  usuarioName.length >= 5 && verificaAlfanumerico(usuarioName);
    };
    const _onChangeregistro= (e) => {
        const texfiel = e.target.name;
        const value = e.target.value;
        if (texfiel === "rolName") {
            setRolName(value);
            setRolNameError(!isValidRolName(value));
        }
    };
    useEffect(()=>{
        dispatch(Action.getPaginas());
    },[]);
    useEffect(()=>{
      setLModulos(listModulos);
  },[lModulos]);

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
            console.log('onchange -1');
          } else {
            newChecked.splice(currentIndex, 1);
            console.log('onchange else');
          }
          // console.log('final newChecked : ', newChecked);
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
  
      console.log('el checked', checked);
     // console.log('lista izquierda :', leftChecked);
     // console.log('lista derecha :', leftChecked);
   },[checked, leftChecked ]);

     const checkeselecionado = (valuesid) => {
        
          const tupla= checked.find(x => x.id_pagina === parseInt(valuesid));
          
          if(tupla !== undefined){
            console.log('ingreso undefine :', tupla);
            return true
          }
    };
    //---------------------------------------------------------------------------
    const [pageSelected, setPageSelected]= useState({id_pagina:0, nombre: ''})

    const selecionoPagina = (pagina) => {
    
      console.log("se agrego en raiz", pagina)
    };
    const desSelecionoPagina = (paginadelecte) => {
         console.log("se elimino en raiz", paginadelecte )
    };

  

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
            <br /> 
            <Grid item xs={12} >

                <Grid container spacing={2} alignItems="center" className={style.root}>
            
                   
                </Grid>

            </Grid>
 
            <Grid container spacing={2} alignItems="center" className={style.root}>
                <Grid item >
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
                                                  const labelId = `transfer-list-item-${value.id_pagina}-label`;
                                        
                                                  return (
                                                    <ListItem key={value2.id_pagina} role="listitem" button 
                                                    //onClick={handleToggle(value.id_pagina,value.nombre )}
                                                    >
                                                      <CheckPagina pagina={value2} labelId={labelId} selecionoPagina={selecionoPagina} desSelecionoPagina={desSelecionoPagina} />
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
                <Grid item >

                </Grid>
            </Grid>

         </>
    );
}
export default  Roles;

