import React from "react";
import * as Redux from "react-redux";
import PropTypes from "prop-types";
import {
  Slide,
  TableHead,
  TableRow,
  TableCell,
  TableSortLabel,
  Checkbox,
  Dialog,
  AppBar,
  Toolbar,
  Chip,
  makeStyles,
  lighten,
  Typography,
  Card,
  Table,
  Paper,
  Grid,
  Button,
  TableContainer,
  TableBody,
  IconButton,
} from "@material-ui/core";
import { Close } from "@material-ui/icons";
import clsx from "clsx";
import * as ActionMensaje from "../../../../redux/actions/messageAction";
import MessageTransferConfirm from "./MessageTransferConfirm";
import { formatearNumero } from "../../../../lib/utility";
import { requestPost } from "../../../../service/request";

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function getComparator(order, orderBy) {
  return order === "desc"
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

function stableSort(array, comparator) {
  const stabilizedThis = array.map((el, index) => [el, index]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) return order;
    return a[1] - b[1];
  });
  return stabilizedThis.map((el) => el[0]);
}

const headCells = [
  {
    id: "nombreDeCliente",
    numeric: false,
    disablePadding: true,
    label: <b>Nombre completo</b>,
  },
  {
    id: "docDeIdentidad",
    numeric: false,
    disablePadding: true,
    label: <b>Cédula identidad</b>,
  },
  {
    id: "nombreBanco",
    numeric: false,
    disablePadding: true,
    label: <b>Banco</b>,
  },
  {
    id: "nroDeCuenta",
    numeric: false,
    disablePadding: true,
    label: <b>Nro. de Cuenta</b>,
  },

  {
    id: "importePorEmpresa",
    numeric: true,
    disablePadding: false,
    label: <b>Monto ($us.)</b>,
  },
  {
    id: "empresa",
    numeric: false,
    disablePadding: true,
    label: <b>Empresa</b>,
  },
  {
    id: "idEstadoComisionDetalleEmpresa",
    numeric: false,
    disablePadding: true,
    label: <b>Estado</b>,
  },
];

const useHeaderStyles = makeStyles((theme) => ({
  headerTable: {
    background: "#1872b8",
    boxShadow: "2px 1px 5px #1872b8",
  },
  headerRow: {
    color: "white",
    paddingBottom: "13px",
    paddingTop: "13px",
  },
}));

function EnhancedTableHead({
  classes,
  onSelectAllClick,
  order,
  orderBy,
  numSelected,
  rowCount,
  onRequestSort,
}) {
  const style = useHeaderStyles();

  const createSortHandler = (property) => (event) => {
    onRequestSort(event, property);
  };
  return (
    <>
      <br />
      <TableHead className={style.headerTable}>
        <TableRow>
          <TableCell padding="Checkbox">
            <Checkbox
              indeterminate={numSelected > 0 && numSelected < rowCount}
              checked={rowCount > 0 && numSelected === rowCount}
              onChange={onSelectAllClick}
              inputProps={{ "aria-label": "select all desserts" }}
            />
          </TableCell>
          {headCells.map((headCell) => (
            <TableCell
              className={style.headerRow}
              key={headCell.id}
              align={headCell.numeric ? "left" : "center"}
              padding={headCell.disablePadding ? "right" : "center"}
              sortDirection={orderBy === headCell.id ? order : false}
            >
              <TableSortLabel
                active={orderBy === headCell.id}
                direction={orderBy === headCell.id ? order : "asc"}
                onClick={createSortHandler(headCell.id)}
                style={{ color: "white" }}
              >
                {headCell.label}
                {orderBy === headCell.id ? (
                  <span className={classes.visuallyHidden}>
                    {order === "desc"
                      ? "sorted descending"
                      : "sorted ascending"}
                  </span>
                ) : null}
              </TableSortLabel>
            </TableCell>
          ))}
        </TableRow>
      </TableHead>
    </>
  );
}

EnhancedTableHead.propTypes = {
  classes: PropTypes.object.isRequired,
  numSelected: PropTypes.number.isRequired,
  onRequestSort: PropTypes.func.isRequired,
  onSelectAllClick: PropTypes.func.isRequired,
  order: PropTypes.oneOf(["asc", "desc"]).isRequired,
  orderBy: PropTypes.string.isRequired,
  rowCount: PropTypes.number.isRequired,
};

const useToolbarStyles = makeStyles((theme) => ({
  root: {
    paddingLeft: theme.spacing(2),
    paddingRight: theme.spacing(1),
  },
  highlight:
    theme.palette.type === "light"
      ? {
          color: theme.palette.secondary.main,
          backgroundColor: lighten(theme.palette.secondary.light, 0.85),
        }
      : {
          color: theme.palette.text.primary,
          backgroundColor: theme.palette.secondary.dark,
        },
  title: {
    flex: "1 1 100%",
  },
}));

const EnhancedTableToolbar = (props) => {
  const classes = useToolbarStyles();
  const { numSelected } = props;

  return (
    <Toolbar
      className={clsx(classes.root, {
        [classes.highlight]: numSelected > 0,
      })}
    >
      {numSelected > 0 ? (
        <Typography
          className={classes.title}
          color="inherit"
          variant="subtitle1"
          component="div"
        >
          {numSelected} seleccionados
        </Typography>
      ) : (
        <Typography
          className={classes.title}
          variant="h6"
          id="tableTitle"
          component="div"
        >
          Listado de transferencias
        </Typography>
      )}

      {numSelected > 0}
    </Toolbar>
  );
};

EnhancedTableToolbar.propTypes = {
  numSelected: PropTypes.number.isRequired,
};

const useStyles = makeStyles((theme) => ({
  root: {
    width: "100%",
    marginTop: theme.spacing(2),
  },
  appBar: {
    transition: theme.transitions.create(["margin", "width"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    background: "linear-gradient(90deg, #2E3B55, #1872b8)",
    position: "relative",
  },
  paper: {
    width: "100%",
    marginBottom: theme.spacing(2),
  },
  table: {
    maxWidth: "100%",
  },
  visuallyHidden: {
    border: 0,
    clip: "rect(0 0 0 0)",
    height: 1,
    margin: -1,
    overflow: "hidden",
    padding: 0,
    position: "absolute",
    top: 20,
    width: 1,
    color: "white",
  },
  gridContainer: {
    paddingLeft: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingTop: theme.spacing(1),
    paddingBottom: theme.spacing(1),
    marginTop: theme.spacing(1),
  },
  containerSave: {
    paddingLeft: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingTop: theme.spacing(1),
    paddingBottom: theme.spacing(1),

    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  containerCargar: {
    paddingLeft: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingTop: theme.spacing(1),
    paddingBottom: theme.spacing(1),

    display: "flex",
    flexDirection: "row",
    alignContent: "center",
    alignItems: "center",
    justifyContent: "center",
  },
  submitCargar: {
    background: "#1872b8",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
  },
  headerTable: {
    background: "#1872b8",
    boxShadow: "2px 1px 5px #1872b8",
  },
}));
//-----------------------------------------------------------------------------------------------

const GridTransferencia = ({
  handleConfirmarPagosTransferencias,
  idCiclo,
  list,
  empresaId,
  openModalFullScreen,
  closeFullScreenModal,
  seleccionarTodo,
  selected,
  setSelected,
  data,
  idComision,
}) => {
  const classes = useStyles();
  const dispatch = Redux.useDispatch();
  const [order, setOrder] = React.useState("asc");
  const [orderBy, setOrderBy] = React.useState("docDeIdentidad");
  const [dense, setDense] = React.useState(false);
  const { userName, idUsuario } = Redux.useSelector((stateSelector) => {
    return stateSelector.load;
  });
  const [openModalConfirmation, setOpenModalConfirmation] =
    React.useState(false);
  const [totalPagar, setTotalPagar] = React.useState(
    parseFloat(data?.montoTotal).toFixed(2)
  );
  const [totalMontoRechazados, setTotalMontoRechazados] = React.useState(0);
  data.montoTotal = data.montoTotal.replace(",", ".");

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      setTotalPagar(data.montoTotal);
      setTotalMontoRechazados(0);
      seleccionarTodo();
      return;
    }
    setTotalPagar(0);
    setTotalMontoRechazados(data.montoTotal);
    setSelected([]);
  };

  const handleClick = (event, freelacerObject) => {
    let idComisionDetalleEmpresa = freelacerObject.idComisionDetalleEmpresa;
    const selectedIndex = selected.indexOf(idComisionDetalleEmpresa);
    let newSelected = [];
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, idComisionDetalleEmpresa);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }
    setSelected(newSelected);
    handleSum(freelacerObject);
  };

  const isSelected = (idComisionDetalleEmpresa) =>
    selected.indexOf(idComisionDetalleEmpresa) !== -1;

  const error = (message) => {
    dispatch(ActionMensaje.showMessage({ message: message, variant: "info" }));
  };

  const closeModalMessage = () => {
    setOpenModalConfirmation(false);
  };
  const abrirModalCormarPagos = () => {
    setOpenModalConfirmation(true);
  };

  const confirmarModal = () => {
    if (idCiclo && idCiclo !== 0) {
      prosesarConfirmarTransferencia(
        userName,
        idUsuario,
        idCiclo,
        selected,
        empresaId
      );
    }
  };

  async function prosesarConfirmarTransferencia(
    userN,
    usuarioId,
    cicloId,
    list,
    idEmpresa
  ) {
    // let response = await handleConfirmarPagosTransferencias(
    //   userN,
    //   usuarioId,
    //   cicloId,
    //   list,
    //   idEmpresa,
    //   dispatch
    // );
    let url = "/gestionPagosRezagados/ConfirmarPagosRezagadosTransferencias";
    let response = requestPost(url, {
      user: userName,
      cicloId,
      comisionId: idComision,
      empresaId: idEmpresa,
      confirmados: list,
    });
    if (response && response.code == 0) {
      setOpenModalConfirmation(false);
      dispatch(
        ActionMensaje.showMessage({
          message: response.message,
          variant: "success",
        })
      );
      closeFullScreenModal();
    } else {
      dispatch(
        ActionMensaje.showMessage({
          message: response.message,
          variant: "error",
        })
      );
    }
  }

  const handleSum = (data) => {
    const isItemSelected = isSelected(data.idComisionDetalleEmpresa);
    let s = 0;
    let t = 0;
    if (!isItemSelected) {
      s = parseFloat(totalPagar) + parseFloat(data.importePorEmpresa);
      t = parseFloat(totalMontoRechazados) - parseFloat(data.importePorEmpresa);
      setTotalMontoRechazados(t.toFixed(2));
      setTotalPagar(s.toFixed(2));
    } else {
      s = parseFloat(totalPagar) - parseFloat(data.importePorEmpresa);
      t = parseFloat(totalMontoRechazados) + parseFloat(data.importePorEmpresa);
      setTotalMontoRechazados(t.toFixed(2));
      setTotalPagar(s.toFixed(2));
    }
  };
  const cerrarVolverCero = () => {
    setTotalMontoRechazados(0);
    closeFullScreenModal();
  };
  function addFormat(nStr) {
    nStr += "";
    var x = nStr.split(".");
    var x1 = x[0];
    var x2 = x.length > 1 ? "," + x[1] : "";
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
      x1 = x1.replace(rgx, "$1" + "." + "$2");
    }
    return x1 + x2;
  }
  const modalSum = (s1, s2, val) => {
    let suma1 = 0;
    let suma2 = 0;
    let ad = "";
    if (val == 0) {
      suma1 = s1 - s2;
      ad = addFormat(suma1.toFixed(2));
      return ad;
    } else if (val == 1) {
      suma2 = s1 - (s1 - s2);
      ad = addFormat(suma2.toFixed(2));
      return ad;
    } else return "valor no válido";
  };

  return (
    <Dialog
      fullScreen
      open={openModalFullScreen}
      onClose={closeFullScreenModal}
      TransitionComponent={Transition}
    >
      <AppBar className={classes.appBar}>
        <Toolbar>
          <IconButton
            edge="start"
            color="inherit"
            onClick={cerrarVolverCero}
            aria-label="close"
          >
            <Close />
          </IconButton>
          <Typography variant="h6" className={classes.appBar}>
            CONFIRMAR TRANSFERENCIA PARA {data.list[0].empresa}
          </Typography>
        </Toolbar>
      </AppBar>
      <Card style={{ overflow: "initial" }}>
        <Grid container className={classes.gridContainer}>
          <Grid item xs={12} md={4}></Grid>
          <Grid item xs={12} md={4} className={classes.containerSave}></Grid>
          <Grid item xs={12} md={4} className={classes.containerCargar}>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submitCargar}
              onClick={() =>
                selected.length > 0
                  ? abrirModalCormarPagos()
                  : error(
                      "¡Al menos, debe seleccionar una cuenta para continuar con la transferencia!"
                    )
              }
            >
              {"Confirmar transferencias "}{" "}
            </Button>
          </Grid>
        </Grid>
      </Card>
      <br />

      <Grid container className={classes.gridContainer}>
        <Grid item xs={12} className={classes.containerSave}>
          <Paper className={classes.paper}>
            <EnhancedTableToolbar numSelected={selected.length} />
            <TableContainer>
              <Table
                className={classes.table}
                aria-labelledby="tableTitle"
                size={dense ? "small" : "medium"}
                aria-label="enhanced table"
              >
                <EnhancedTableHead
                  classes={classes}
                  numSelected={selected.length}
                  order={order}
                  orderBy={orderBy}
                  onSelectAllClick={handleSelectAllClick}
                  onRequestSort={handleRequestSort}
                  rowCount={list.length}
                />
                <TableBody>
                  {stableSort(list, getComparator(order, orderBy)).map(
                    (row, index) => {
                      const isItemSelected = isSelected(
                        row.idComisionDetalleEmpresa
                      );
                      const labelId = `enhanced-table-checkbox-${index}`;

                      return (
                        <TableRow
                          hover
                          onClick={(event) => handleClick(event, row)}
                          role="checkbox"
                          aria-checked={isItemSelected}
                          tabIndex={-1}
                          key={row.nombreDeCliente}
                          selected={isItemSelected}
                        >
                          <TableCell padding="Checkbox">
                            <Checkbox
                              disabled={
                                row.idEstadoComisionDetalleEmpresa === 2
                                  ? true
                                  : false
                              }
                              checked={isItemSelected}
                              value={isItemSelected}
                              inputProps={{ "aria-labelledby": labelId }}
                            />
                          </TableCell>
                          <TableCell component="th" id={labelId} scope="row">
                            {" "}
                            {row.nombreDeCliente}
                          </TableCell>
                          <TableCell align="center">
                            {" "}
                            {row.docDeIdentidad}
                          </TableCell>
                          <TableCell align="left"> {row.nombreBanco}</TableCell>
                          <TableCell align="left">{row.nroDeCuenta}</TableCell>
                          <TableCell align="left">
                            {formatearNumero(
                              parseFloat(row.importePorEmpresa).toFixed(2)
                            )}
                          </TableCell>
                          <TableCell align="center">{row.empresa}</TableCell>
                          <TableCell align="center">
                            {row.idEstadoComisionDetalleEmpresa === 2 ? (
                              <Chip
                                label="Pagado"
                                color="primary"
                                variant="default"
                              />
                            ) : row.idEstadoComisionDetalleEmpresa === 1 ? (
                              <Chip
                                label="Pendiente"
                                color="secondary"
                                variant="default"
                              />
                            ) : (
                              <Chip
                                label="Rechazado"
                                color="secondary"
                                variant="default"
                              />
                            )}
                          </TableCell>
                        </TableRow>
                      );
                    }
                  )}
                  <TableRow key={100000000000000}>
                    <TableCell align="center">
                      <b></b>
                    </TableCell>
                    <TableCell align="right"></TableCell>
                    <TableCell align="center">
                      <b> </b>
                    </TableCell>
                    <TableCell align="center">
                      <b> </b>
                    </TableCell>
                    <TableCell align="center">
                      <b>{"TOTAL: "} </b>
                    </TableCell>
                    <TableCell align="left">
                      <b>{addFormat(data.montoTotal)}</b>
                    </TableCell>
                    <TableCell align="center"></TableCell>
                  </TableRow>
                </TableBody>
              </Table>
            </TableContainer>
          </Paper>
        </Grid>
      </Grid>

      {data && (
        <MessageTransferConfirm
          open={openModalConfirmation}
          titulo={<b>DETALLE DE TRANSFERENCIA</b>}
          subTituloModal={
            <b>
              Ciclo: {data.list[0].glosa}
              <br />
              Empresa: {data.list[0].empresa}
            </b>
          }
          mensaje={{
            confirmados: selected.length,
            montoAPagar: modalSum(
              parseFloat(data.montoTotal),
              parseFloat(totalMontoRechazados),
              0
            ),
            rechazados: list.length - selected.length,
            montoAPagarRechazados:
              list.length - selected.length
                ? modalSum(
                    parseFloat(data.montoTotal),
                    parseFloat(totalMontoRechazados),
                    1
                  )
                : 0.0,
            totalLista: list.length,
            montoTotal: addFormat(data.montoTotal),
          }}
          handleCloseConfirm={confirmarModal}
          handleCloseCancel={closeModalMessage}
        />
      )}
    </Dialog>
  );
};
export default GridTransferencia;
