import React  from 'react';
import Typography from '@material-ui/core/Typography';
import WarningIcon from '@material-ui/icons/Warning';

const  NotFoundLoad =()=>  {       
    return (
         <>            
            <Typography variant="h6" gutterBottom>
                Sitio sin Acceso <WarningIcon />
            </Typography>         
            <br />
            
         
            <div style={{ width: '100%', display: 'flex',  alignContent:'center', justifyContent: 'center'}} >
               
            </div>                       
         </>
    );
}
export default  NotFoundLoad;