import React, { Fragment } from "react";
import {
    Button, Dialog, DialogContent, Typography,Grid
} from "@material-ui/core";
import { Alert, AlertTitle } from '@material-ui/lab';
import { makeStyles } from '@material-ui/core/styles';
import CancelIcon from '@material-ui/icons/Cancel';
import CheckCircleIcon from '@material-ui/icons/CheckCircle';

const useStyles = makeStyles((theme) => ({
    root: {
      width: '100%',
      '& > * + *': {
        //marginTop: theme.spacing(2),
      },
    },
    botones:{
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',  
        marginBottom:theme.spacing(1),
        marginTop:theme.spacing(1),
        marginRight:theme.spacing(1),
        marginLeft:theme.spacing(1),
    },

    botonesSecondary:{
        background: "#f44336", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',  
        marginBottom:theme.spacing(1),
        marginTop:theme.spacing(1),
        marginRight:theme.spacing(1),
        marginLeft:theme.spacing(1),
    }
  }));

let MessageConfirm = ({ open, titulo,subTituloModal, tipoModal, mensaje, handleCloseConfirm, handleCloseCancel }) => {
     //tipoModal : info, error, warning, success
     const classes = useStyles();

    let cerrarModal = () => {
        handleCloseConfirm();
    };

    return (
        <Fragment>
            <Dialog
                fullWidth={true}
                open={open}
                aria-labelledby="customized-dialog-title"
            >
                <DialogContent>
                    <div className={classes.root}>
                        <Alert severity={tipoModal}>
                            <AlertTitle>{titulo}</AlertTitle>
                            {/* <strong>{subTituloModal}</strong> */}
                            <br />
                            <Typography variant="caption" display="block" gutterBottom>
                                <strong>NOTA :</strong> {mensaje} 
                            </Typography>
                            <br />
                        </Alert>
                    </div>
                    <Grid  container item xs={12}  justify="flex-end"  >
                        <Button onClick={cerrarModal} variant="contained"  className={classes.botones} color="secondary">
                            <CheckCircleIcon/> Aceptar
                        </Button>
                    </Grid>
                </DialogContent>
            </Dialog>
        </Fragment>
    );

};

export default MessageConfirm;