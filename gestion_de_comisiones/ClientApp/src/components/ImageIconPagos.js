import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import {
    Home,
    LocalAtm,
    BrokenImage,
    Receipt,
    ExitToApp,
    Lock,
    LockOpen,
    Person,
    LocalMall,   
} from '@material-ui/icons';
import ErrorOutlineIcon from '@material-ui/icons/ErrorOutline';
const useStyles = makeStyles((theme) => ({
    imgStyle: {
        fontSize:24,
        // margin: '10px'
    }

    }));

const ImageIconPagos = ({name})=>{
    const classes = useStyles();
    
    switch (name) {
        case 'sionpay':
            return  <img  height="20" src={require('../assets/icons/sionpay_large.png')} />;
        case 'transfer':
            return  <img  height="20" src={require('../assets/icons/tranfer.png')} />;
        case 'cheque':
            return  <img  height="20" src={require('../assets/icons/cheque.png')} />;
        case 'ninguno':
            return  <ErrorOutlineIcon  className={classes.imgStyle}/>;     
        default:
            return <ErrorOutlineIcon  className={classes.imgStyle}/>;
    }
}

export default ImageIconPagos;