import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';
import ErrorOutlineIcon from '@material-ui/icons/ErrorOutline';
import ErrorIcon from '@material-ui/icons/Error';
import InfoIcon from '@material-ui/icons/Info';

import { BrokenImage } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
    imgStyle: {
        fontSize:24,
    }
    }));

const Icons = ({name})=>{
    const classes = useStyles();    
    switch (name) {
        case 'info':
            return <InfoIcon />;
        case 'success':
            return <CheckCircleOutlineIcon  />;
        case 'error':
            return <ErrorIcon  />;
        case 'warning':
            return <ErrorOutlineIcon  />;        
        default:
            return <BrokenImage  />;
    }
}
export default Icons;