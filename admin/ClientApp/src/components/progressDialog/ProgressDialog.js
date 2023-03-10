import React from 'react';
import Backdrop from '@material-ui/core/Backdrop';
import CircularProgress from '@material-ui/core/CircularProgress';
import { makeStyles } from '@material-ui/core/styles';
import { useSelector } from 'react-redux';

const useStyles = makeStyles((theme) => ({
  backdrop: {
    zIndex: theme.zIndex.drawer + 10000,
    color: '#fff',
  },
}));

export default function ProgressDialog() {
    const classes = useStyles();
    const {loading} = useSelector((stateSelector) => stateSelector.requestloading);
    
    return (
       
        <Backdrop className={classes.backdrop} open={loading} >
            <CircularProgress color="inherit" />
        </Backdrop>
        
    );
}