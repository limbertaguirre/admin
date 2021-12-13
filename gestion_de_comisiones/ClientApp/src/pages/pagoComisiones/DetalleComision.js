import React, { useState, Fragment } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import Chip from '@material-ui/core/Chip';
import VisibilityIcon from '@material-ui/icons/Visibility';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { ObtenerDetallePago } from '../../redux/actions/PagosGestorAction';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import { Typography } from '@material-ui/core';
import DoneIcon from '@material-ui/icons/Done';
import CloseIcon from '@material-ui/icons/Close';
import PlaylistAddCheckIcon from '@material-ui/icons/PlaylistAddCheck';

const DetalleComision = ({ idComisionDetalle, nombreFreelance, docId }) => {
    const dispatch = useDispatch();
    const { userName } = useSelector((stateSelector) => { return stateSelector.load });
    const [open, setOpen] = useState(false);
    const [listaDetalleEmpresa, setListaDetalleEmpresa] = useState(null);

    const handleClickOpen = async (idDetalleComision) => {
        let respuesta = await ObtenerDetallePago(userName, idDetalleComision, dispatch);
        if (respuesta && respuesta.code == 0) {
            setListaDetalleEmpresa(respuesta.data);
            setOpen(true);
        } else {
            alert('error');

        }
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <Fragment>
            <Chip label="Detalle" color="primary" icon={<VisibilityIcon />} onClick={() => { handleClickOpen(idComisionDetalle) }} />
            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title"><b>{"Detalle de comision por empresa"}</b></DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {listaDetalleEmpresa && (
                            <>
                                <Typography variant="subtitle2" gutterBottom>
                                    <b>Nombre: </b> {nombreFreelance}
                                </Typography>
                                <Typography variant="subtitle2" gutterBottom>
                                    <b>Cedula Identidad: </b> {docId}
                                </Typography>
                                <List>
                                    <ListItem>
                                        <ListItemIcon>
                                            <PlaylistAddCheckIcon />
                                        </ListItemIcon>
                                        <ListItemText
                                            primary={<b>Empresa</b>}
                                            secondary={null}
                                            primaryTypographyProps={{ variant: "subtitle2" }}
                                        />
                                        <ListItemSecondaryAction>
                                            <Typography align="right" variant="subtitle2" gutterBottom>
                                                <b>Monto</b>
                                            </Typography>
                                        </ListItemSecondaryAction>
                                    </ListItem>
                                    {listaDetalleEmpresa.map((item) => {
                                        return (<ListItem>
                                            <ListItemIcon>
                                                {item.estaPagado ? <DoneIcon style={{ color: "green" }} /> : <CloseIcon color="secondary" />}
                                            </ListItemIcon>
                                            <ListItemText
                                                primary={item.nombreEmprea}
                                                secondary={null}
                                                primaryTypographyProps={{ variant: "subtitle2" }}
                                            />
                                            <ListItemSecondaryAction>
                                                <Typography align="right" variant="subtitle2" gutterBottom>
                                                    {item.montoComision}
                                                </Typography>
                                            </ListItemSecondaryAction>
                                        </ListItem>)
                                    })}
                                </List>
                            </>)}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Cerrar
                    </Button>
                </DialogActions>
            </Dialog>
        </Fragment>

    );
}

export default DetalleComision;
