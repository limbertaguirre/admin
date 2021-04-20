import React  from 'react';
import Typography from '@material-ui/core/Typography';
import problem from '../../assets/imageError/problem.png'
const  NotFoundLoad =()=>  {       
    return (
         <>            
            <Typography variant="h6" gutterBottom>
                No se encontró la   página.
            </Typography>         
            <div style={{ width: '100%', display: 'flex', alignItems: 'center'}} >
                <img src={process.env.PUBLIC_URL + problem } alt="" style={{width: '75%', alignContent:'center',}} />
            </div>                       
         </>
    );
}
export default  NotFoundLoad;

