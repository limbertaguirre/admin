import React, * as GeneralReact from "react";
import * as Redux from "react-redux";
import PropTypes from "prop-types";
import * as Core from "@material-ui/core";
import * as CoreStyles from "@material-ui/core/styles";
import * as GeneralIcons from "@material-ui/icons";
import clsx from "clsx";
import * as Actions from '../../../../redux/actions/PagosGestorAction';
import * as ActionMensaje from '../../../../redux/actions/messageAction';
import { Row } from "react-flexbox-grid";

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
  { id: "docDeIdentidad", numeric: false, disablePadding: true, label: "Cédula identidad" },
  { id: "nroDeCuenta", numeric: false, disablePadding: true, label: "Cuenta banco" },
  // { id: "banco", numeric: false, disablePadding: true, label: "Banco" },
  { id: "idComisionDetalleEmpresa", numeric: true, disablePadding: false, label: "Comision detalle" },
  { id: "empresa", numeric: false, disablePadding: true, label: "Empresa" },
  { id: "estado", numeric: false, disablePadding: false, label: "Estado" },
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
            align={headCell.numeric ? "left" : "center"}
            padding={headCell.disablePadding ? "center" : "left"}
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
        >
          Listado de transferencias
        </Core.Typography>
      )}

      {numSelected > 0 
      // ? (
      //   <Core.Tooltip title="Delete">
      //     <Core.IconButton aria-label="delete">
      //       <GeneralIcons.Delete />
      //     </Core.IconButton>
      //   </Core.Tooltip>
      // ) : (
      //   <Core.Tooltip title="Filter list">
      //     <Core.IconButton aria-label="filter list">
      //       <GeneralIcons.FilterList />
      //     </Core.IconButton>
      //   </Core.Tooltip>
      // )
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
  gridContainer:{
    paddingLeft:theme.spacing(1),
    paddingRight:theme.spacing(1),
    paddingTop:theme.spacing(1),
    paddingBottom:theme.spacing(1),
    marginTop:theme.spacing(1)
  },
  containerSave: {
    paddingLeft: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingTop: theme.spacing(1),
    paddingBottom: theme.spacing(1),
    // marginTop: theme.spacing(6),
    marginBlock: theme.spacing(4),

    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
}));
//-----------------------------------------------------------------------------------------------

const GridTransferencia = (props) => {
  const classes = useStyles();
  const dispatch = Redux.useDispatch();
  const { list,empresaId, openModalFullScreen, closeFullScreenModal } = props;
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
    const texfiel = e.target.nombreCompleto;
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
  const [orderBy, setOrderBy] = React.useState("docDeIdentidad");
  const [selected, setSelected] = React.useState([]);
  const [dense, setDense] = React.useState(false);
  
  // React.useEffect(()=>{ 
  //   handleSelectAllClick()
  // },[])

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked ) {
      const newSelecteds = list.map((n) => n.nombreDeCliente);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event, nombreDeCliente) => {
    const selectedIndex = selected.indexOf(nombreDeCliente);
    let newSelected = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, nombreDeCliente);
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

  const isSelected = (nombreDeCliente) => selected.indexOf(nombreDeCliente) !== -1;

  

  return (
    <Core.Dialog
      fullScreen
      open={openModalFullScreen}
      onClose={closeFullScreenModal}
      TransitionComponent={Transition}
    >
      <Core.Grid container className={classes.gridContainer}>
        <Core.Grid item xs={12}>
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
        </Core.Grid>
        {/* <Core.Grid item xs={12} md={4}></Core.Grid>
        <Core.Grid item xs={12} md={4} className={classes.containerSave}>
          <Core.Card>
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
          </Core.Card>
        </Core.Grid> */}
        <Core.Grid item xs={12} className={classes.containerSave}>
          <div className={classes.root}>
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
                        const isItemSelected = isSelected(row.nombreDeCliente);
                        const labelId = `enhanced-table-checkbox-${index}`;

                        return (
                          <Core.TableRow
                            hover
                            onClick={(event) => handleClick(event, row.nombreDeCliente)}
                            role="checkbox"
                            aria-checked={isItemSelected}
                            tabIndex={-1}
                            key={row.nombreDeCliente}
                            selected={isItemSelected}
                          >
                            <Core.TableCell padding="checkbox">
                              <Core.Checkbox
                                checked={isItemSelected}
                                inputProps={{ "aria-labelledby": labelId }}
                              />
                            </Core.TableCell>
                            <Core.TableCell component="th" id={labelId} scope="row" >{row.nombreDeCliente}</Core.TableCell>
                            <Core.TableCell align="center">{row.docDeIdentidad}</Core.TableCell>
                            <Core.TableCell align="center">{row.nroDeCuenta}</Core.TableCell>
                            <Core.TableCell align="center">{row.idComisionDetalleEmpresa}</Core.TableCell>
                            <Core.TableCell align="center">{row.empresa}</Core.TableCell>
                            {/* <Core.TableCell align="center">{row.formaPago}</Core.TableCell> */}
                            <Core.TableCell align="center">{row.estado? 
                                <Core.Chip label="Pagado" color="primary" variant="outlined" />
                              :  <Core.Chip label="por pagar"  color="secondary" variant="outlined"   />
                            }</Core.TableCell> 
                          </Core.TableRow>
                        );
                      }
                    )}
                  </Core.TableBody>
                </Core.Table>
              </Core.TableContainer>
            </Core.Paper>
          </div>
        </Core.Grid> 
      </Core.Grid>
    </Core.Dialog>
  );
  //-------------------------------------------------------------------------------------
};
export default GridTransferencia;
