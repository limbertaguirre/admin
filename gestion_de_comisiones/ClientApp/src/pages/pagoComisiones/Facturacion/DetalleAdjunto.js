import React, {useEffect, useState}  from 'react';

import { Breadcrumbs, Chip, emphasize, withStyles } from "@material-ui/core";
import HomeIcon from "@material-ui/icons/Home";

import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';

const StyledBreadcrumb = withStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.grey[100],
    height: theme.spacing(3),
    color: theme.palette.grey[800],
    fontWeight: theme.typography.fontWeightRegular,
    "&:hover, &:focus": {
      backgroundColor: theme.palette.grey[300],
    },
    "&:active": {
      boxShadow: theme.shadows[1],
      backgroundColor: emphasize(theme.palette.grey[300], 0.12),
    },
  },
}))(Chip);

const DetalleAdjunto = (props) => {

    let history = useHistory();
  /*   const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
    useEffect(()=>{  try{  
       verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
       }catch (err) {  verificarAcceso(perfiles, 'none', history); }
    },[]) */


  return (
    <>
      <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}>
 
      </div>
      <br />
  
      <p>Detalle de Adjunto</p>
    </>
  );
};
export default DetalleAdjunto;