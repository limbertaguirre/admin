import React from 'react';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';
import ErrorOutlineIcon from '@material-ui/icons/ErrorOutline';
import ErrorIcon from '@material-ui/icons/Error';
import InfoIcon from '@material-ui/icons/Info';
import { BrokenImage } from '@material-ui/icons';


const Icons = ({name})=>{     
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