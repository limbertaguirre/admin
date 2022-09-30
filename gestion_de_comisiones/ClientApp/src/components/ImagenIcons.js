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
import PaymentIcon from '@material-ui/icons/Payment';
import AccountBalanceIcon from '@material-ui/icons/AccountBalance';
import SettingsIcon from '@material-ui/icons/Settings';
import ControlPointDuplicateIcon from '@material-ui/icons/ControlPointDuplicate';
import SecurityIcon from '@material-ui/icons/Security';
import AssignmentIndIcon from '@material-ui/icons/AssignmentInd';
import AccountBalanceWalletIcon from '@material-ui/icons/AccountBalanceWallet';
import PeopleAltIcon from '@material-ui/icons/PeopleAlt';
import SupervisedUserCircleIcon from '@material-ui/icons/SupervisedUserCircle';
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
        case 'pago':
            return <PaymentIcon  className={classes.imgStyle}/>;
        case 'gestionPago':
            return <AccountBalanceIcon  className={classes.imgStyle}/>;
        case 'config':
            return <SettingsIcon  className={classes.imgStyle}/>;
        case 'rol':
            return <ControlPointDuplicateIcon  className={classes.imgStyle}/>;
        case 'gestionClienteIcon':
            return <AssignmentIndIcon  className={classes.imgStyle}/>;
        case 'gestionSeguridadIcon':
            return <SecurityIcon  className={classes.imgStyle}/>;
        case 'gestionRolesIcon':
            return <SupervisedUserCircleIcon  className={classes.imgStyle}/>;
        case 'gestionPagoIcon':
            return <PaymentIcon  className={classes.imgStyle}/>;
        case 'pagoComisionesIcon':
            return <AccountBalanceWalletIcon  className={classes.imgStyle}/>;
        case 'fichaClientIcon':
            return <PeopleAltIcon  className={classes.imgStyle}/>;   
        default:
            return <BrokenImage  className={classes.imgStyle}/>;
    }
}

export default ImageIcons;