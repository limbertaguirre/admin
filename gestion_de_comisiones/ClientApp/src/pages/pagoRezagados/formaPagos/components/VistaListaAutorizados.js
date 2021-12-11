import React, { useDebugValue, useState } from "react";
import {
  Button,
  Dialog,
  DialogContent,
  Typography,
  Grid,
  TextField,
  FormLabel,
  FormControlLabel,
  FormControl,
  Radio,
  RadioGroup,
  FormHelperText,
  Tooltip,
  Zoom,
} from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import Alert from "@material-ui/lab/Alert";
import MenuItem from "@material-ui/core/MenuItem";
import MenuList from "@material-ui/core/MenuList";
//import LogoSion from '../../../../assets/icons/LogoSION.sgv'

import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import Avatar from "@material-ui/core/Avatar";
import ImageIcon from "@material-ui/icons/Image";
import WorkIcon from "@material-ui/icons/Work";
import BeachAccessIcon from "@material-ui/icons/BeachAccess";
import DoneAllIcon from "@material-ui/icons/DoneAll";
import ErrorOutlineIcon from "@material-ui/icons/ErrorOutline";

import MessageConfirm from "../../../../components/mesageModal/MessageConfirm";

const useStyles = makeStyles((theme) => ({
  rootTituloConfir: {
    width: "100%",
    "& > * + *": {
      marginTop: theme.spacing(2),
    },
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    backgroundColor: "#E5F3FA",
    borderRadius: "3px",
    marginBottom: theme.spacing(1),
  },
  rootTituloPendi: {
    width: "100%",
    "& > * + *": {
      marginTop: theme.spacing(2),
    },
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    backgroundColor: "#E5F3FA",
    borderRadius: "3px",
    marginBottom: theme.spacing(1),
  },
  divRadio: {
    width: "100%",
    "& > * + *": {
      marginTop: theme.spacing(2),
    },
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    //backgroundColor:'#E3F2F7',
    borderRadius: "3px",
    marginBottom: theme.spacing(1),
  },
  group: {
    margin: `${theme.spacing.unit}px 0`,
  },
  botones: {
    background: "#1872b8",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
    marginBottom: theme.spacing(2),
    marginTop: theme.spacing(2),
    marginRight: theme.spacing(1),
    marginLeft: theme.spacing(1),
  },
  TextFiel: {
    marginBottom: theme.spacing(1),
    marginTop: theme.spacing(1),
    marginRight: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingLeft: theme.spacing(1),
    //width:'98%'
  },
  Titulo: {
    marginBottom: theme.spacing(2),
    marginTop: theme.spacing(2),
    marginRight: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingLeft: theme.spacing(1),
    //width:'98%'
  },
  titleDescripcion: {
    color: "red",
  },
  rootList: {
    width: "100%",
    maxWidth: 360,
    backgroundColor: theme.palette.background.paper,
  },
}));

const MODAL_CONFIG = {
  titulo: "Estas seguro",
  subTituloModal: "Estas seguro con aprobar el pago.",
  tipoModal: "warning",
  mensaje: "Al aprobar no hay marcha atras el anular autorizacion.",
};

const VistaListaAutorizados = ({
  open,
  closeHandelModal,
  objList,
  nameComboSeleccionado,
  confirmarModalAutorizacion,
}) => {
  const classes = useStyles();

  const [openModalConfirm, setOpenModalConfirm] = useState(false);

  const openModalConfirmacion = () => {
    setOpenModalConfirm(true);
  };
  const CloseModalConfirmacion = () => {
    setOpenModalConfirm(false);
  };
  const confirmarModalAutorizarOk = () => {
    setOpenModalConfirm(false);
    confirmarModalAutorizacion(
      objList.idComision,
      objList.idAutorizacionComision
    );
  };

  return (
    <>
      <Dialog
        fullWidth={true}
        open={open}
        aria-labelledby="customized-dialog-title"
      >
        <DialogContent>
          <div
            className={
              objList.comisionAutorizada
                ? classes.rootTituloConfir
                : classes.rootTituloPendi
            }
          >
            <Typography
              variant="subtitle1"
              className={classes.Titulo}
              gutterBottom
            >
              {objList.comisionAutorizada ? (
                <b>CONFIRMADO</b>
              ) : (
                <b>PENDIENTE A CONFIRMACION</b>
              )}
            </Typography>
          </div>
          <div>
            <Typography variant="overline" display="block" gutterBottom>
              LISTA APROBACIONES PARA EL CICLO <b> {nameComboSeleccionado} </b>
            </Typography>
          </div>
          <br />
          <Grid container>
            <div className={classes.divRadio}>
              <List className={classes.rootList}>
                {objList.autorizadores.map((valu, index) => (
                  <ListItem key={index}>
                    <ListItemText
                      primary={
                        valu.nombre.toUpperCase() +
                        " " +
                        valu.apellido.toUpperCase()
                      }
                      secondary={valu.area}
                    />
                    <ListItemAvatar>
                      {valu.aprobado ? (
                        <DoneAllIcon style={{ color: "#1D7C0E" }} />
                      ) : (
                        <Tooltip
                          disableFocusListener
                          disableTouchListener
                          TransitionComponent={Zoom}
                          title={"Pendiente a aprobacion"}
                        >
                          <ErrorOutlineIcon color="disabled" />
                        </Tooltip>
                      )}
                    </ListItemAvatar>
                  </ListItem>
                ))}
              </List>
            </div>
            <div>
              {!objList.comisionAutorizada && (
                <Typography variant="overline" display="block" gutterBottom>
                  ¿Está segura que desea autorizar para pagar el ciclo?
                </Typography>
              )}
            </div>
          </Grid>
          <Grid container item xs={12} justify="flex-end">
            <Button
              onClick={closeHandelModal}
              variant="contained"
              color="primary"
              className={classes.botones}
            >
              {objList.comisionAutorizada ? "CERRAR" : "CANCELAR"}
            </Button>
            {!objList.comisionAutorizada && (
              <Button
                onClick={openModalConfirmacion}
                variant="contained"
                color="primary"
                className={classes.botones}
              >
                Autorizar
              </Button>
            )}
          </Grid>
        </DialogContent>
      </Dialog>
      <MessageConfirm
        open={openModalConfirm}
        {...MODAL_CONFIG}
        handleCloseConfirm={confirmarModalAutorizarOk}
        handleCloseCancel={CloseModalConfirmacion}
      />
    </>
  );
};
export default VistaListaAutorizados;
