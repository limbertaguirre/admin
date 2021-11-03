import React, * as GeneralReact from "react";
import * as Redux from "react-redux";
import PropTypes from "prop-types";
import * as Core from "@material-ui/core";
import * as CoreStyles from "@material-ui/core/styles";
import * as GeneralIcons from "@material-ui/icons";
import clsx from "clsx";

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Core.Slide direction="up" ref={ref} {...props} />;
});

//-------------------------- PROPIEDADES DE LA TABLA ----------------------------------------
function createData(
  nombreCompleto,
  ci,
  nroCuenta,
  banco,
  monto,
  formaPago,
  estadoPago
) {
  return { nombreCompleto, ci, nroCuenta, banco, monto, formaPago, estadoPago };
}

const rows = [
  createData("ELIOT HUMEREZ", "1234567", "1234567EH", "FASSIL", 50,2,1),
  createData("GRACIELA GUTIERREZ", 452, 25.0, 51, 4.9,1,0),
  createData("MARWIN NU�EZ", 262, 16.0, 24, 6.0,1,0),
  createData("JUAN MANUEL JUSTINIANO", 159, 6.0, 24, 4.0,1,0),
  createData("LUIS VACA", 356, 16.0, 49, 3.9,1,0),
  createData("MAR�A LOPEZ", 408, 3.2, 87, 6.5,1,0),
  createData("LIMBERT AGUIRRE", 237, 9.0, 37, 4.3,1,0),
  createData("JHON DOE", 375, 0.0, 94, 0.0,1,0),
  createData("PEDRO DOMINGO", 518, 26.0, 65, 7.0,1,0),
  createData("MARWIL VILLALPANDO", 392, 0.2, 98, 0.0,1,0),
  createData("SERGIO RIOS", 318, 0, 81, 2.0,1,0),
  createData("ROLANDO GONZALES", 360, 19.0, 9, 37.0,1,0),
  createData("ABRAHAM TIRADO", 437, 18.0, 63, 4.0,1,0),
];

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
    id: "nombreCompleto",
    numeric: false,
    disablePadding: true,
    label: "NOMBRE COMPLETO",
      },
  { id: "ci", numeric: false, disablePadding: false, label: "C�DULA DE IDENTIDAD" },
  { id: "nroCuenta", numeric: false, disablePadding: false, label: "Nro. CUENTA" },
  { id: "banco", numeric: false, disablePadding: false, label: "BANCO" },
  { id: "montoReal", numeric: false, disablePadding: false, label: "MONTO TOTAL (Bs.)" },
  { id: "formaPago", numeric: false, disablePadding: false, label: "FORMA PAGO" },
  { id: "estado", numeric: false, disablePadding: false, label: "ESTADO" },
];

function EnhancedTableHead(props) {
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
    <Core.TableHead>
      <Core.TableRow>
        <Core.TableCell padding="checkbox">
          <Core.Checkbox
            indeterminate={numSelected > 0 && numSelected < rowCount}
            checked={rowCount > 0 && numSelected === rowCount}
            onChange={onSelectAllClick}
            inputProps={{ "aria-label": "select all desserts" }}
          />
        </Core.TableCell>
        {headCells.map((headCell) => (
          <Core.TableCell
            key={headCell.id}
            align={headCell.numeric ? "right" : "left"}
            padding={headCell.disablePadding ? "none" : "normal"}
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
                  {order === "desc" ? "sorted descending" : "sorted ascending"}
                </span>
              ) : null}
            </Core.TableSortLabel>
          </Core.TableCell>
        ))}
      </Core.TableRow>
    </Core.TableHead>
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
        ></Core.Typography>
      )}

      {numSelected > 0}
    </Core.Toolbar>
  );
};

EnhancedTableToolbar.propTypes = {
  numSelected: PropTypes.number.isRequired,
};

const useStyles = CoreStyles.makeStyles((theme) => ({
  root: {
    width: "100%",
  },
  appBar: {
    position: "relative",
    backgroundColor: "#1872b8",
    marginRight: theme.spacing(4),
  },
  paper: {
    width: "100%",
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
  containerSave: {
    paddingLeft: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingTop: theme.spacing(1),
    paddingBottom: theme.spacing(1),

    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  gridContainer: {
    paddingLeft: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingTop: theme.spacing(1),
    paddingBottom: theme.spacing(1),
  },
}));
//-----------------------------------------------------------------------------------------------

const GridTransferencia = (props) => {
  const classes = useStyles();
  const dispatch = Redux.useDispatch();
  const { openModalFullScreen, closeFullScreenModal } = props;
  const [txtBusqueda, setTxtBusqueda] = React.useState("");
  const [idCiclo, setIdCiclo] = React.useState(0);
  const [idCicloSelected, setIdCicloSelected] = React.useState(0);
  const [statusBusqueda, setStatusBusqueda] = React.useState(false);
  const [openSnackbar, setOpenSnackbar] = React.useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = React.useState("");
  const [tipoSnackbar, settipTSnackbar] = React.useState(true);
  const [listaComisionesAPagar, setListaComisionesAPagar] = React.useState([]);
  const { userName, idUsuario } = Redux.useSelector((stateSelector) => {
    return stateSelector.load;
  });

  const generarSnackBar = (mensaje, tipo) => {
    setOpenSnackbar(true);
    setMensajeSnackbar(mensaje);
    settipTSnackbar(tipo);
  };
  const onChangeSelectCiclo = (e) => {
    const texfiel = e.target.name;
    const value = e.target.value;
    if (texfiel === "idCiclo") {
      setIdCiclo(value);
    }
    if (texfiel === "txtBusqueda") {
      setTxtBusqueda(value);
    }
  };
  const buscarFreelanzer = () => {
    if (txtBusqueda != "") {
      buscarFrelancerPorCi();
    } else {
      generarSnackBar("¡Introduzca carnet de identidad!", "info");
    }
  };
  async function cargarComisionesPagos(userNa, cicloId) {
    // let respuesta = await Actions.ObtenerComisionesPagos(
    //   userNa,
    //   cicloId,
    //   dispatch
    // );
    // console.log("comisiones pagos: ", respuesta);
    // if (respuesta && respuesta.code == 0) {
    //   setListaComisionesAPagar(respuesta.data);
    //   setStatusBusqueda(true);
    // } else {
    //   dispatch(
    //     ActionMensaje.showMessage({
    //       message: respuesta.message,
    //       variant: "error",
    //     })
    //   );
    // }
  }

  async function buscarFrelancerPorCi() {
    // let response = await Actions.buscarPorCarnetFormaPago(
    //   userName,
    //   idCiclo,
    //   txtBusqueda,
    //   dispatch
    // );
    // if (response && response.code == 0) {
    //   console.log("response busca ", response);
    //   let data = response.data;
    //   setListaComisionesAPagar(data);
    // }
  }
  //-------------------------------- TABLA CHECK ----------------------------------------
  const [order, setOrder] = React.useState("asc");
  const [orderBy, setOrderBy] = React.useState("ci");
  const [selected, setSelected] = React.useState([]);
  const [page, setPage] = React.useState(0);
  const [dense, setDense] = React.useState(false);
  // const [rowsPerPage, setRowsPerPage] = React.useState(5);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = rows.map((n) => n.nombreCompleto);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event, nombreCompleto) => {
    const selectedIndex = selected.indexOf(nombreCompleto);
    let newSelected = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, nombreCompleto);
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

  const isSelected = (nombreCompleto) =>
    selected.indexOf(nombreCompleto) !== -1;

  // const emptyRows = rowsPerPage - Math.min(rowsPerPage, rows.length - page * rowsPerPage);

  return (
    <div className={classes.root}>
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
              TRANSFERENCIA
            </Core.Typography>
          </Core.Toolbar>
        </Core.AppBar>
        <Core.Card>
          <Core.Grid container className={classes.gridContainer}>
            <Core.Grid item xs={12} md={4}></Core.Grid>
            <Core.Grid item xs={12} md={3} className={classes.containerSave}>
              {
                // statusBusqueda && (
                <Core.TextField
                  label="Buscar freelancer"
                  type={"text"}
                  variant="outlined"
                  placeholder={"CÉDULA DE IDENTIDAD"}
                  name="txtBusqueda"
                  value={txtBusqueda}
                  onChange={onChangeSelectCiclo}
                  fullWidth
                  onKeyPress={(ev) => {
                    if (ev.key === "Enter") {
                      buscarFreelanzer();
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
                // )
              }
            </Core.Grid>
          </Core.Grid>
        </Core.Card>

        <Core.Grid container className={classes.gridContainer}>
          <Core.Grid item xs={12}>
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
                    rowCount={rows.length}
                  />
                  <Core.TableBody>
                    {stableSort(rows, getComparator(order, orderBy))
                      // .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                      .map((row, index) => {
                        const isItemSelected = isSelected(row.nombreCompleto);
                        const labelId = `enhanced-table-checkbox-${index}`;

const GridTransferencia = () => {
  return (
                      <Core.TableRow
                        hover
                        onClick={(event) => handleClick(event, row.name)}
                        role="checkbox"
                        aria-checked={isItemSelected}
                        tabIndex={-1}
                        key={row.name}
                        selected={isItemSelected}
                      >
                        <Core.TableCell padding="checkbox">
                          <Core.Checkbox
                            checked={isItemSelected}
                            inputProps={{ "aria-labelledby": labelId }}
                          />
                        </Core.TableCell>
                        <Core.TableCell
                          component="th"
                          id={labelId}
                          scope="row"
                          padding="none"
                        >
                          {row.name}
                        </Core.TableCell>
                        <Core.TableCell align="right">{row.calories}</Core.TableCell>
                        <Core.TableCell align="right">{row.fat}</Core.TableCell>
                        <Core.TableCell align="right">{row.carbs}</Core.TableCell>
                        <Core.TableCell align="right">{row.protein}</Core.TableCell>
                      </Core.TableRow>
                    );
                  })}
                {emptyRows > 0 && (
                  <Core.TableRow style={{ height: (dense ? 33 : 53) * emptyRows }}>
                    <Core.TableCell colSpan={6} />
                  </Core.TableRow>
                )}
              </Core.TableBody>
            </Core.Table>
          </Core.TableContainer>
          
        </Core.Paper>
      </Core.Dialog>
    </div>
  );
  //-------------------------------------------------------------------------------------
};
export default GridTransferencia;
