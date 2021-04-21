import React  from 'react';
import Typography from '@material-ui/core/Typography';
import problema3 from '../../assets/imageError/problema3.png'
const  NotFoundLoad =()=>  {       
    return (
         <>            
            <Typography variant="h6" gutterBottom>
                No se encontró la   página.
            </Typography>         
            <br />
            
         
            <div style={{ width: '100%', display: 'flex',  alignContent:'center', justifyContent: 'center'}} >
                <img src={process.env.PUBLIC_URL + problema3 } alt="" style={{width: '80%'}} />
            </div>                       
         </>
    );
}
export default  NotFoundLoad;

