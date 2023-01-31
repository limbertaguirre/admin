

import React from 'react';
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';

function Alert(props) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}


const  SnackbarSion =({open,closeSnackbar,tipo,duracion, mensaje })=>  {  
     //ejemplo tipo : error, warning,info, success
     //ejemplo duracion: 6000
    return (
         <>    
            <Snackbar open={open} autoHideDuration={duracion} onClose={closeSnackbar}>
              <Alert onClose={closeSnackbar} severity={tipo}>
               {mensaje}
              </Alert>
            </Snackbar>        
         </>
    );
}
export default  SnackbarSion;

