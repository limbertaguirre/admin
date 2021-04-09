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
    LocalMall
} from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
    imgStyle: {
        fontSize:24,
        // margin: '10px'
    }

    }));

const ImageIcons = ({name})=>{
    const classes = useStyles();
    
    switch (name) {
        case 'home':
            return <Home className={classes.imgStyle}/>;
        case 'money':
            return <LocalAtm  className={classes.imgStyle}/>;
        case 'producto':
            return <LocalMall  className={classes.imgStyle}/>;
        case 'vendido':
            return <Receipt  className={classes.imgStyle}/>;
        case 'salir':
            return <ExitToApp  className={classes.imgStyle}/>;
        case 'cambiarContraseña':
            return <Lock  className={classes.imgStyle}/>;
        case 'cambiarContraseña2':
            return <LockOpen  className={classes.imgStyle}/>;
        case 'persona':
            return <Person  className={classes.imgStyle}/>;
        default:
            return <BrokenImage  className={classes.imgStyle}/>;
    }
}

export default ImageIcons;