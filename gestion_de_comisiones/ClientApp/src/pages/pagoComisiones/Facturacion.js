import React, {useEffect, useState}  from 'react';
import BorderWrapper from 'react-border-wrapper'
import { emphasize, withStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';

import * as permiso from '../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import { requestPost, requestGet } from "../../service/request";

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


 const Facturacion =(props)=> {    
     
  let history = useHistory();
  const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
  useEffect(()=>{  try{  
     verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
     }catch (err) {  verificarAcceso(perfiles, 'none', history); }
  },[])
  const {userName} =useSelector((stateSelector)=>{ return stateSelector.load});
  const dispatch = useDispatch();
  const[ciclos, setCiclos]= useState();
 
  useEffect(()=>{  
    obtenerCiclos();
  },[]);

  const obtenerCiclos=()=>{
    const data={usuarioLogin:userName };
     requestGet('Factura/ObtenerCiclos',data,dispatch).then((res)=>{ 
      console.log('ciclos : ', res);
          if(res.code === 0){                 
               
          }else{
             // dispatch(Action.showMessage({ message: res.message, variant: "error" }));
          }    
        })    
   };
  

    function handleClick(event) {
        event.preventDefault();
        console.info('You clicked a breadcrumb.');
    }
     
    return (
      <>
           <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
              <Breadcrumbs aria-label="breadcrumb">
                        <StyledBreadcrumb key={1} component="a" label="Gestion de pagos"icon={<HomeIcon fontSize="small" />}  />
                        <StyledBreadcrumb key={2} component="a" label="Pago de comisiones"  />
                        <StyledBreadcrumb key={3} label="Facturacion"  onClick={handleClick}/>
              </Breadcrumbs>
           </div>
           <br/>
           <br/>
           <BorderWrapper
                borderColour="#00bcf1"
                borderWidth="4px"
                borderRadius="29px"
                borderType="solid"
                innerPadding="30px"
                topElement={ <h2>Facturacion</h2>}
                topPosition={0.05}
                topOffset="22px"
                topGap="4px"                
                rightPosition={0.1}
                rightOffset="22px"
                rightGap="4px"
                >
                
                <div style={{width:'100%'}}>

              
               
                   <h1> en contruccion </h1>
                        <p>aqui se cargara las comisiones</p>
                                      
                </div>
            </BorderWrapper>
      </>
    );

}
export default Facturacion;