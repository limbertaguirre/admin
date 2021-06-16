import React, { Fragment } from "react";
import {
    Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Paper, Typography,Grid
} from "@material-ui/core";
import EmojiObjectsIcon from "@material-ui/icons/EmojiObjects";

let MessageConfirm = ({ open, titulo, mensaje, handleCloseConfirm, handleCloseCancel }) => {

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
                <DialogTitle >{titulo}</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                         {/*         <Paper
                                elevation={0}
                                style={{
                                    background: "#ffecb3",
                                    marginTop: 16,
                                    marginBottom: 16,
                                    paddingLeft: 8,
                                }}
                                >
                                <Grid container justify="flex-start" alignItems="center">
                                    <Grid item xs={1}>
                                    <EmojiObjectsIcon />
                                    </Grid>
                                    <Grid item xs={10}>
                                    <Typography
                                        variant="subtitle2"
                                        gutterBottom
                                        style={{ marginTop: 15, marginBottom: 5 }}
                                        display="block"
                                    >
                                        {mensaje}
                                    </Typography>
                                    </Grid>
                                </Grid>
                                </Paper>   */}
                                {mensaje}              
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCloseCancel} color="primary">
                        Cancelar
                    </Button>
                    <Button onClick={cerrarModal} color="primary">
                        Aceptar
                    </Button>
                </DialogActions>
            </Dialog>
        </Fragment>
    );

};

export default MessageConfirm;