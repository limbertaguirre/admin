import React, { Fragment } from "react";
import {
  Paper,
  Button,
  Dialog,
  DialogContent,
  DialogTitle,
  DialogContentText,
  Typography,
  Grid,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableBody,
  TableCell,
} from "@material-ui/core";
import { Alert, AlertTitle } from "@material-ui/lab";
import { makeStyles } from "@material-ui/core/styles";
import CancelIcon from "@material-ui/icons/Cancel";
import CheckCircleIcon from "@material-ui/icons/CheckCircle";

const useStyles = makeStyles((theme) => ({
  root: {
    width: "100%",
    "& > * + *": {},
  },
  botones: {
    background: "#1872b8",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
    marginBottom: theme.spacing(1),
    marginTop: theme.spacing(1),
    marginRight: theme.spacing(1),
    marginLeft: theme.spacing(1),
  },

  botonesSecondary: {
    background: "#f44336",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
    marginBottom: theme.spacing(1),
    marginTop: theme.spacing(1),
    marginRight: theme.spacing(1),
    marginLeft: theme.spacing(1),
  },
  dialogContainer: {
    "& .MuiPaper-root": {
      [theme.breakpoints.down("lg")]: {
        minWidth: "740px",
      },
      [theme.breakpoints.down("md")]: {
        minWidth: "500px",
      },
      [theme.breakpoints.down("xs")]: {
        minWidth: "550px",
      },
    },
  },
  dialgoTitle: {
    backgroundColor: "#1872b8",
    boxShadow: "2px 1px 5px #1872b8",
    color: "white",
    "& .MuiTypography-root": {
      color: "white",
    },
  },
  table: {
    minWidth: 200,
  },
  textcontent: {
    color: "black",
  },
}));

let MessageTransferConfirm = ({
  open,
  titulo,
  subTituloModal,
  mensaje,
  handleCloseConfirm,
  handleCloseCancel,
}) => {
  const classes = useStyles();
  let cerrarModal = () => {
    handleCloseConfirm();
  };
  if (mensaje !== null) {
    return (
      <Dialog
        fullWidth={true}
        open={open}
        maxWidth="sm"
        aria-labelledby="customized-dialog-title"
        className={classes.dialogContainer}
      >
        <DialogTitle className={classes.dialgoTitle}>{titulo}</DialogTitle>

        <DialogContent>
          <div className={classes.root}>
            <DialogContentText>
              <Typography
                variant="body1"
                gutterBottom
                className={classes.textcontent}
              >
                {subTituloModal}
              </Typography>
              <TableContainer component={Paper}>
                <Table className={classes.table} aria-label="simple table">
                  <TableHead>
                    <TableRow>
                      <TableCell>
                        <b>{""}</b>
                      </TableCell>
                      <TableCell align="center">
                        <b>CANTIDAD (ACI)</b>
                      </TableCell>
                      <TableCell align="center">
                        <b>MONTOS</b>
                      </TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    <TableRow>
                      <TableCell>
                        <b>POR CONFIRMAR: </b>
                      </TableCell>
                      <TableCell align="center">
                        {mensaje.confirmados}
                      </TableCell>
                      <TableCell align="center">
                        {mensaje.montoAPagar}
                      </TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>
                        <b>POR RECHAZAR: </b>
                      </TableCell>
                      <TableCell align="center">{mensaje.rechazados}</TableCell>
                      <TableCell align="center">
                        {mensaje.montoAPagarRechazados}
                      </TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>
                        <b>TOTALES: </b>
                      </TableCell>
                      <TableCell align="center">{mensaje.totalLista}</TableCell>
                      <TableCell align="center">{mensaje.montoTotal}</TableCell>
                    </TableRow>
                  </TableBody>
                </Table>
              </TableContainer>
              <Typography> ACI = Asesor Comercial Independiente</Typography>
            </DialogContentText>
          </div>
          <Grid container item xs={12} justify="flex-end">
            <Button
              onClick={handleCloseCancel}
              variant="contained"
              color="primary"
              className={classes.botonesSecondary}
            >
              <CancelIcon /> Cancelar
            </Button>
            <Button
              onClick={cerrarModal}
              variant="contained"
              className={classes.botones}
              color="secondary"
            >
              <CheckCircleIcon /> Aceptar
            </Button>
          </Grid>
        </DialogContent>
      </Dialog>
    );
  } else return "Ocurrió un error inesperado al obtener datos.";
};
export default MessageTransferConfirm;
