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
    label: "Nombre completo",
  },
  {
    id: "docDeIdentidad",
    numeric: false,
    disablePadding: true,
    label: "Cédula identidad",
  },
  { id: "nombreBanco", numeric: false, disablePadding: true, label: "Banco" },
  {
    id: "nroDeCuenta",
    numeric: false,
    disablePadding: true,
    label: "Nro. de Cuenta",
  },

  {
    id: "importePorEmpresa",
    numeric: true,
    disablePadding: false,
    label: "Monto ($us.)",
  },
  {
    id: "empresa",
    numeric: false,
    disablePadding: true,
    label: "Empresa",
  },
  {
    id: "idEstadoComisionDetalleEmpresa",
    numeric: false,
    disablePadding: true,
    label: "Estado",
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
  } = props;
  const [order, setOrder] = React.useState("asc");
  const [orderBy, setOrderBy] = React.useState("docDeIdentidad");
  const [selected, setSelected] = React.useState([]);
  const [dense, setDense] = React.useState(false);
  const [txtBusqueda, setTxtBusqueda] = React.useState("");
  // const [idCiclo, setIdCiclo] = React.useState(0);
  const [idCicloSelected, setIdCicloSelected] = React.useState(0);
  const [statusBusqueda, setStatusBusqueda] = React.useState(false);
  const [openSnackbar, setOpenSnackbar] = React.useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = React.useState("");
  const [tipoSnackbar, settipTSnackbar] = React.useState(true);
  const [listaComisionesAPagar, setListaComisionesAPagar] = React.useState([]);
  const { userName, idUsuario } = Redux.useSelector((stateSelector) => {
    return stateSelector.load;
  });
  const [openModalConfirmation, setOpenModalConfirmation] =
    React.useState(false);

  // const generarSnackBar = (mensaje, tipo) => {
  //   setOpenSnackbar(true);
  //   setMensajeSnackbar(mensaje);
  //   settipTSnackbar(tipo);
  // };

  /*  const onChangeSelectCiclo = (e) => {
      const texfiel = e.target.nombreCompleto;
      const value = e.target.value;
      if (texfiel === "idCiclo") {
        setIdCiclo(value);
      }
      if (texfiel === "txtBusqueda") {
        setTxtBusqueda(value);
      }
    }; */

  // const buscarFreelanzer = () => {
  //   if (txtBusqueda != "") {
  //     buscarFrelancerPorCi();
  //   } else {
  //     generarSnackBar("¡Introduzca carnet de identidad!", "info");
  //   }
  // };

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    //Aqui seleccionamos todos.
    if (event.target.checked) {
      const newSelecteds = list.map((n) => n.idComisionDetalleEmpresa);
      setSelected(newSelecteds);

      console.log("que es esto?, setSelected: " , newSelecteds)
      return;
    }
    setSelected([]);
  };

  const handleClick = (event, idComisionDetalleEmpresa) => {
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
      //handleOnGetPagos();
    } else {
      dispatch(
        ActionMensaje.showMessage({
          message: response.message,
          variant: "error",
        })
      );
    }
  }

  const handleOnGetPagos = () => {
    if (idCiclo && idCiclo !== 0) {
      setIdCicloSelected(idCiclo);
      cargarComisionesPagos(userName, idCiclo);
    } else {
      // generarSnackBar('¡Debe Seleccionar un ciclo para cargar las comisiones!','warning')
      /*  setOpenSnackbar(true);
      setMensajeSnackbar('¡Debe Seleccionar un ciclo para cargar las comisiones!');
      settipTSnackbar('warning'); */
    }
  };
  async function cargarComisionesPagos(userNa, cicloId) {
    let respuesta = await Actions.ObtenerComisionesPagos(
      userNa,
      cicloId,
      dispatch
    );
    console.log('cargarComisionesPagos: ',respuesta);
    if (respuesta && respuesta.code == 0) {
      setListaComisionesAPagar(respuesta.data);
      setStatusBusqueda(true);
    } else {
      dispatch(
        ActionMensaje.showMessage({
          message: respuesta.message,
          variant: "error",
        })
      );
    }
  }

  // const importe = list.map((row)=> [row.idComisionDetalleEmpresa, row.importePorEmpresa]);
  // console.log("Listado? importe: ",importe)

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
          <Core.Typography variant="h6" className={classes.title}>
            CONFIRMAR TRANSFERENCIA
          </Core.Typography>
        </Core.Toolbar>
      </Core.AppBar>
      {/* --------------------------------------------------------------CABECERA-------------------------------------------------------------------- */}
      <div
        style={{ paddingLeft: "0px", paddingRight: "0px", paddingTop: "10px" }}
      >
        <Core.Breadcrumbs aria-label="breadcrumb">
          <StyledBreadcrumb
            key={1}
            component="a"
            label="Confirmar selección"
            icon={<GeneralIcons.Home fontSize="small" />}
          />
        </Core.Breadcrumbs>
      </div>
      <br />

      <Core.Card style={{ overflow: "initial" }}>
        <Core.Grid container className={classes.gridContainer}>
          <Core.Grid item xs={12} md={4}></Core.Grid>
          <Core.Grid item xs={12} md={4} className={classes.containerSave}>
            {/* {statusBusqueda&& */}
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
            {/* } */}
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
                          onClick={(event) =>
                            handleClick(event, row.idComisionDetalleEmpresa)
                          }
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
                          <Core.TableCell align="left">
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
                          {/* <Core.TableCell align="center">{row.formaPago}</Core.TableCell> */}
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
                </Core.TableBody>
              </Core.Table>
            </Core.TableContainer>
          </Core.Paper>
        </Core.Grid>
      </Core.Grid>
      
      <MessageTransferConfirm 
        open={openModalConfirmation} 
        titulo={"CONFIRMAR"} 
        subTituloModal={"PAGO POR TRANSFERENCIA"} 
        tipoModal={"info"}
        mensaje={<div>Se realizara el pago por transferencia de: {selected.length} cuentas de Acesor Comercial Independiente (ACI)<br/>
                      Con un monto de:   {console.log("ESTE ES EL SELECCIONADO: ", selected)}<br/>
                      Para la empresa: {empresaId}<br/>
                </div>
                  
                  
        }
        handleCloseConfirm={confirmarModal}
        handleCloseCancel={closeModalMessage}
      />
    </Core.Dialog>
  );
  //-------------------------------------------------------------------------------------
};
export default GridTransferencia;