import React, * as GeneralReact from "react";
import * as Redux from "react-redux";
import PropTypes from "prop-types";
import * as Core from "@material-ui/core";
import * as CoreStyles from "@material-ui/core/styles";
import * as GeneralIcons from "@material-ui/icons";
import clsx from "clsx";
import * as Actions from "../../../../redux/actions/PagosGestorAction";
import * as ActionMensaje from "../../../../redux/actions/messageAction";
import { Row } from "react-flexbox-grid";
import { Button } from "bootstrap";
import MessageTransferConfirm from "../../../../components/mesageModal/MessageTransferConfirm";
import { formatearNumero } from "../../../../lib/utility";

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Core.Slide direction="up" ref={ref} {...props} />;
});

//-------------------------- PROPIEDADES DE LA TABLA ----------------------------------------
function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

const StyledBreadcrumb = Core.withStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.grey[100],
    height: theme.spacing(3),
    color: theme.palette.grey[800],
    fontWeight: theme.typography.fontWeightRegular,
    "&:hover, &:focus": {
      backgroundColor: theme.palette.grey[300],
    },
    "&:active": {
      boxShadow: theme.shadows[1],
      backgroundColor: Core.emphasize(theme.palette.grey[300], 0.12),
    },
  },
}))(Core.Chip);

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

const useHeaderStyles = CoreStyles.makeStyles((theme) => ({
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

function EnhancedTableHead(props) {
  const style = useHeaderStyles();
  const {
    classes,
    onSelectAllClick,
    order,
    orderBy,
    numSelected,
    rowCount,
    onRequestSort,
  } = props;
  const createSortHandler = (property) => (event) => {
    onRequestSort(event, property);
  };
  return (
    <>
      <br />
      <Core.TableHead className={style.headerTable}>
        <Core.TableRow>
          <Core.TableCell padding="Checkbox">
            <Core.Checkbox
              indeterminate={numSelected > 0 && numSelected < rowCount}
              checked={rowCount > 0 && numSelected === rowCount}
              onChange={onSelectAllClick}
              inputProps={{ "aria-label": "select all desserts" }}
            />
          </Core.TableCell>
          {headCells.map((headCell) => (
            <Core.TableCell
              className={style.headerRow}
              key={headCell.id}
              align={headCell.numeric ? "left" : "center"}
              padding={headCell.disablePadding ? "right" : "center"}
              sortDirection={orderBy === headCell.id ? order : false}
            >
              <Core.TableSortLabel
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
              </Core.TableSortLabel>
            </Core.TableCell>
          ))}
        </Core.TableRow>
      </Core.TableHead>
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

const useToolbarStyles = CoreStyles.makeStyles((theme) => ({
  root: {
    paddingLeft: theme.spacing(2),
    paddingRight: theme.spacing(1),
  },
  highlight:
    theme.palette.type === "light"
      ? {
          color: theme.palette.secondary.main,
          backgroundColor: CoreStyles.lighten(
            theme.palette.secondary.light,
            0.85
          ),
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
    <Core.Toolbar
      className={clsx(classes.root, {
        [classes.highlight]: numSelected > 0,
      })}
    >
      {numSelected > 0 ? (
        <Core.Typography
          className={classes.title}
          color="inherit"
          variant="subtitle1"
          component="div"
        >
          {numSelected} seleccionados
        </Core.Typography>
      ) : (
        <Core.Typography
          className={classes.title}
          variant="h6"
          id="tableTitle"
          component="div"
        >
          Listado de transferencias
        </Core.Typography>
      )}

      {
        numSelected > 0
        //? (<Core.Tooltip title="Aceptar transferencias"><Core.IconButton aria-label="next">Aceptar<GeneralIcons.NavigateNext fontSize={"large"}/></Core.IconButton></Core.Tooltip>) :
        //(<Core.Tooltip title="Filter list"><Core.IconButton aria-label="filter list"><GeneralIcons.FilterList /></Core.IconButton></Core.Tooltip>)
        //''
      }
    </Core.Toolbar>
  );
};

EnhancedTableToolbar.propTypes = {
  numSelected: PropTypes.number.isRequired,
};

const useStyles = CoreStyles.makeStyles((theme) => ({
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
    minWidth: 750,
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

const GridTransferencia = (props) => {
  const classes = useStyles();
  const dispatch = Redux.useDispatch();
  const {
    idCiclo,
    list,
    empresaId,
    openModalFullScreen,
    closeFullScreenModal,
    seleccionarTodo,
    selected,
    setSelected,
    data,
  } = props;
  const [order, setOrder] = React.useState("asc");
  const [orderBy, setOrderBy] = React.useState("docDeIdentidad");
  const [dense, setDense] = React.useState(false);
  const { userName, idUsuario } = Redux.useSelector((stateSelector) => {
    return stateSelector.load;
  });
  const [openModalConfirmation, setOpenModalConfirmation] =
    React.useState(false);
  const [totalPagar, setTotalPagar] = React.useState(data?.montoTotal);
  const [totalMontoRechazados, setTotalMontoRechazados] = React.useState(0);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    //Aqui seleccionamos todos.
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
  const [check_in, setCheck_in] = React.useState(true);

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
    let response = await Actions.handleConfirmarPagosTransferencias(
      userN,
      usuarioId,
      cicloId,
      list,
      idEmpresa,
      dispatch
    );
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
    let t = 0;
    if (!isItemSelected) {
      let s = parseFloat(totalPagar) + parseFloat(data.importePorEmpresa);
      if (totalMontoRechazados > 0) {
        t =
          parseFloat(totalMontoRechazados) - parseFloat(data.importePorEmpresa);
      }
      setTotalMontoRechazados(t.toFixed(2));
      setTotalPagar(s.toFixed(2));
    } else {
      let s = parseFloat(totalPagar) - parseFloat(data.importePorEmpresa);
      let t =
        parseFloat(totalMontoRechazados) + parseFloat(data.importePorEmpresa);
      setTotalMontoRechazados(t.toFixed(2));
      setTotalPagar(s.toFixed(2));
    }
  };

  return (
    <Core.Dialog
      fullScreen
      open={openModalFullScreen}
      onClose={closeFullScreenModal}
      TransitionComponent={Transition}
    >
      <Core.AppBar className={classes.appBar}>
        <Core.Toolbar>
          <Core.IconButton
            edge="start"
            color="inherit"
            onClick={closeFullScreenModal}
            aria-label="close"
          >
            <GeneralIcons.Close />
          </Core.IconButton>
          <Core.Typography variant="h6" className={classes.appBar}>
            CONFIRMAR TRANSFERENCIA
          </Core.Typography>
        </Core.Toolbar>
      </Core.AppBar>
      {/* --------------------------------------------------------------CABECERA-------------------------------------------------------------------- */}

      <Core.Card style={{ overflow: "initial" }}>
        <Core.Grid container className={classes.gridContainer}>
          <Core.Grid item xs={12} md={4}></Core.Grid>
          <Core.Grid item xs={12} md={4} className={classes.containerSave}>
            {/* {statusBusqueda&&
            <Core.TextField
              label="Buscar freelancer"
              type={"text"}
              variant="outlined"
              placeholder={"Buscar por carnet identidad"}
              name="txtBusqueda"
              value={txtBusqueda}
              //onChange={onChangeSelectCiclo}
              fullWidth
              onKeyPress={(ev) => {
                if (ev.key === "Enter") {
                  //buscarFreelanzer();
                }
              }}
              InputProps={{
                startAdornment: (
                  <Core.InputAdornment position="start">
                    <GeneralIcons.Search />
                  </Core.InputAdornment>
                ),
              }}
            />
            } */}
          </Core.Grid>
          <Core.Grid item xs={12} md={4} className={classes.containerCargar}>
            <Core.Button
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
            </Core.Button>
          </Core.Grid>
        </Core.Grid>
      </Core.Card>
      <br />

      <Core.Grid container className={classes.gridContainer}>
        <Core.Grid item xs={12} className={classes.containerSave}>
          <Core.Paper className={classes.paper}>
            <EnhancedTableToolbar numSelected={selected.length} />
            <Core.TableContainer>
              <Core.Table
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
                <Core.TableBody>
                  {stableSort(list, getComparator(order, orderBy)).map(
                    (row, index) => {
                      const isItemSelected = isSelected(
                        row.idComisionDetalleEmpresa
                      );
                      const labelId = `enhanced-table-checkbox-${index}`;

                      return (
                        <Core.TableRow
                          hover
                          onClick={(event) => handleClick(event, row)}
                          role="checkbox"
                          aria-checked={isItemSelected}
                          tabIndex={-1}
                          key={row.nombreDeCliente}
                          selected={isItemSelected}
                        >
                          <Core.TableCell padding="Checkbox">
                            <Core.Checkbox
                              disabled={
                                row.idEstadoComisionDetalleEmpresa === 2
                                  ? true
                                  : false
                              }
                              checked={isItemSelected}
                              value={isItemSelected}
                              inputProps={{ "aria-labelledby": labelId }}
                            />
                          </Core.TableCell>
                          <Core.TableCell
                            component="th"
                            id={labelId}
                            scope="row"
                          >
                            {row.nombreDeCliente}
                          </Core.TableCell>
                          <Core.TableCell align="center">
                            {row.docDeIdentidad}
                          </Core.TableCell>
                          <Core.TableCell align="left">
                            {row.nombreBanco}
                          </Core.TableCell>
                          <Core.TableCell align="left">
                            {row.nroDeCuenta}
                          </Core.TableCell>
                          <Core.TableCell align="left">
                            {row.importePorEmpresa}
                          </Core.TableCell>
                          <Core.TableCell align="center">
                            {row.empresa}
                          </Core.TableCell>
                          <Core.TableCell align="center">
                            {row.idEstadoComisionDetalleEmpresa === 2 ? (
                              <Core.Chip
                                label="Pagado"
                                color="primary"
                                variant="default"
                              />
                            ) : row.idEstadoComisionDetalleEmpresa === 1 ? (
                              <Core.Chip
                                label="Pendiente"
                                color="secondary"
                                variant="default"
                              />
                            ) : (
                              <Core.Chip
                                label="Rechazado"
                                color="secondary"
                                variant="default"
                              />
                            )}
                          </Core.TableCell>
                        </Core.TableRow>
                      );
                    }
                  )}
                  <Core.TableRow key={100000000000000}>
                    <Core.TableCell align="center">
                      <b></b>
                    </Core.TableCell>
                    <Core.TableCell align="right"></Core.TableCell>
                    <Core.TableCell align="center">
                      <b> </b>
                    </Core.TableCell>
                    <Core.TableCell align="center">
                      <b> </b>
                    </Core.TableCell>
                    <Core.TableCell align="center">
                      <b>{"TOTAL: "} </b>
                    </Core.TableCell>
                    <Core.TableCell align="left">
                      <b>{formatearNumero(data.montoTotal)}</b>
                    </Core.TableCell>
                    <Core.TableCell align="center"></Core.TableCell>
                  </Core.TableRow>
                </Core.TableBody>
              </Core.Table>
            </Core.TableContainer>
          </Core.Paper>
        </Core.Grid>
      </Core.Grid>

      <MessageTransferConfirm
        open={openModalConfirmation}
        titulo={<b>DETALLE DE TRANSFERENCIA</b>}
        subTituloModal={""}
        // tipoModal={"info"}
        mensaje={{
          confirmados: selected.length,
          montoAPagar: formatearNumero(totalPagar),
          rechazados: list.length - selected.length,
          montoAPagarRechazados: formatearNumero(totalMontoRechazados),
          totalLista: list.length,
          montoTotal: formatearNumero(data.montoTotal),
        }}
        handleCloseConfirm={confirmarModal}
        handleCloseCancel={closeModalMessage}
      />
    </Core.Dialog>
  );
  //-------------------------------------------------------------------------------------
};
export default GridTransferencia;
