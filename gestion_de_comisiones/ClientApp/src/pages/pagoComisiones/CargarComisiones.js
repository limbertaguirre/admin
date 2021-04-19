import React from 'react';

import {Breadcrumbs,Chip,emphasize, withStyles} from '@material-ui/core';
import HomeIcon from '@material-ui/icons/Home';

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

const CargarComisiones =()=> {
  
    


    return (
      <>
           <Breadcrumbs aria-label="breadcrumb">
                    <StyledBreadcrumb key={1} component="a" label="Gestion de pagos"icon={<HomeIcon fontSize="small" />}  />
                    <StyledBreadcrumb key={2} component="a" label="Pago de comisiones"  />
                    <StyledBreadcrumb key={3} label="Prorrateo" />
           </Breadcrumbs>
        <br />
        <h1>Cargar Comisiones</h1>

        <p>This is a simple example of a React component.</p>

        
      </>
    );
  
}
export default CargarComisiones;