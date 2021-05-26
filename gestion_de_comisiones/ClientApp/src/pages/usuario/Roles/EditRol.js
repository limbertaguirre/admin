

import React,{useState, useEffect }  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { Dialog, DialogContent, Button, Grid } from "@material-ui/core"
import { makeStyles, emphasize, withStyles  } from '@material-ui/core/styles';
import { useSelector,useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import * as Action from '../../../redux/actions/usuarioAction';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';
import AddCircleOutlineIcon from '@material-ui/icons/AddCircleOutline';
import CardRol from './component/CardRol';
import EditOutlinedIcon from '@material-ui/icons/EditOutlined';
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

const useStyles = makeStyles((theme) => ({
    root: {
       flexGrow: 1,
      },
    gridNewRol: {
        display: 'flex',
        alignItems:"flex-end",
        justifyContent: 'flex-end',
    },
}));

const  EditRol =(props)=>  {       
    const style = useStyles();
    const dispatch = useDispatch();
    const history = useHistory();
    const {globalModules } = useSelector((stateSelector) =>{ return stateSelector.usuario});
    const [idRol, setIdRol] = useState(0)
     useEffect(()=>{
         setIdRol(props.location.state.idRol)
         
     },[]);
     useEffect(()=>{
       // if(idRol != 0){
            console.log(' local d : ',idRol)
            dispatch(Action.ObtenerRolModulos(props.location.state.idRol));
       // } 
    },[]);

    return (
         <>    
          <br/>
            <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
              <Breadcrumbs aria-label="breadcrumb">
                    <div onClick={()=> history.goBack()}> <StyledBreadcrumb key={1}  component="a" label="Gestion de Roles"icon={<HomeIcon fontSize="small" />}  />  </div>                        
                    <div><StyledBreadcrumb key={2} component="a" label="Editar Rol "icon={<EditOutlinedIcon fontSize="small" />}  /></div>
              </Breadcrumbs>
           </div>
           <br/>
           <div className={style.root}>
                <Grid container spacing={3}>
                    <Grid item xs={6}>
                        <Typography variant="h6" gutterBottom>
                            Editar de Roles
                        </Typography>  
                        </Grid>
                   {/*  <Grid item xs={6} className={style.gridNewRol} >
                        <Button variant="contained" 
                            color="primary" 
                            onClick={()=>  history.goBack()} >
                            <AddCircleOutlineIcon />
                            {' '}{' volver'}
                        </Button>
                    </Grid>  */}
                <Grid item xs={12}>

                    
                </Grid>
                </Grid>
            </div>        
          

         </>
    );
}
export default  EditRol;

