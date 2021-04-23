import React from 'react';
import { emphasize, withStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
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


 const FormaPago =()=> {
    

     
    return (
      <>
          <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}>
              <Breadcrumbs aria-label="breadcrumb">
                        <StyledBreadcrumb key={1} component="a" label="Gestion de pagos"icon={<HomeIcon fontSize="small" />}  />
                        <StyledBreadcrumb key={2} component="a" label="Pago de comisiones"  />
                        <StyledBreadcrumb key={3} label="Forma de pagos" />
              </Breadcrumbs>
           </div>           
           <br/>
           <br/>
           <h1> Forma de Pagos </h1>
           <p>aqui se cargara las comisiones</p>

      </>
    );

}

export default FormaPago;