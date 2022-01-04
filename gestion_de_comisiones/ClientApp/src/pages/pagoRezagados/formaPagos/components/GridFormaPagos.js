import React, { useEffect, useState } from "react";
import { makeStyles, withStyles, emphasize } from "@material-ui/core/styles";
import ErrorIcon from "@material-ui/icons/Error";
import { Tooltip, Zoom, Card, Grid } from "@material-ui/core";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TablePagination from "@material-ui/core/TablePagination";
import Paper from "@material-ui/core/Paper";
import IconButton from "@material-ui/core/IconButton";
import configEstadoTable from "../../../../lib/configEstadoTable.json";
import FilterListIcon from "@material-ui/icons/FilterList";
import { useSelector, useDispatch } from "react-redux";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import ImageIconPagos from "../../../../components/ImageIconPagos";
import { requestPost } from "../../../../service/request";

const StyledMenu = withStyles({
  paper: {
    border: "1px solid #d3d4d5",
  },
})((props) => (
  <Menu
    elevation={0}
    getContentAnchorEl={null}
    anchorOrigin={{
      vertical: "bottom",
      horizontal: "center",
    }}
    transformOrigin={{
      vertical: "top",
      horizontal: "center",
    }}
    {...props}
  />
));

const StyledMenuItem = withStyles((theme) => ({
  root: {
    "&:focus": {
      backgroundColor: theme.palette.primary.main,
      "& .MuiListItemIcon-root, & .MuiListItemText-primary": {
        color: theme.palette.common.white,
      },
    },
  },
}))(MenuItem);

const useStyles = makeStyles((theme) => ({
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
      backgroundColor: emphasize(theme.palette.grey[300], 0.12),
    },
  },
  table: {
    // minWidth: 650,
    maxWidth: "100%",
  },
  submitDetalle: {
    height: "25px",
    background: "#1872b8",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
  },
  submitDetalleInactivo: {
    height: "25px",
  },
  cardVacio: {
    height: "400px",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  headerTable: {
    background: "#1872b8",
    boxShadow: "2px 1px 5px #1872b8",
  },
  headerRow: {
    color: "white",
    paddingBottom: "13px",
    paddingTop: "13px",
  },
  backgroundSionPay: {
    //  background: '#ACE1FB'
  },
  backgroundTransferencia: {
    // background: "#8FB8CD"
  },
  backgroundCheque: {
    background: "#EBF0AE",
  },
  backgroundNinguno: {
    background: "#EFF3F5",
  },
  altoCeldas: {
    paddingBottom: "2px",
    paddingTop: "2px",
  },
}));

const GridFormaPagos = ({
  listaComisionesAPagar,
  listaComisionPaginacionNueva,
  setListaComisionPaginacionNueva,
  selecionarDetalleFrelances,
  seleccionarTipoFiltroBusqueda,
  idCiclo,
  pendienteFormaPago,
  permisoActualizar,
  permisoCrear,
  idComision,
}) => {
  let style = useStyles();
  const dispatch = useDispatch();
  const { userName, idUsuario } = useSelector((stateSelector) => {
    return stateSelector.load;
  });

  const [order, setOrder] = useState("asc");
  const [orderBy, setOrderBy] = React.useState("calories");

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

  const [rowsPerPage, setRowsPerPage] = useState(30);
  const [page, setPage] = useState(0);
  const [contadorPage, setContadorPage] = useState(0);
  const handleChangePage = (event, newPage) => {
    setPage(newPage);
    setListaComisionPaginacionNueva(false);
  };
  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };
  useEffect(() => {
    setContadorPage(page * rowsPerPage);
  }, [page, rowsPerPage]);

  const [totalBruto, setTotalBruto] = useState(0);
  const [totalRetencion, setTotalRetencion] = useState(0);
  const [totalDescuento, setTotalDescuento] = useState(0);
  const [totalNeto, setTotalNeto] = useState(0);
  useEffect(() => {
    if (listaComisionesAPagar.length > 0) {
      let ptotalBruto = 0;
      let ptotalRetencion = 0;
      let ptotalDescuento = 0;
      let ptotalNeto = 0;

      listaComisionesAPagar.forEach(function (value) {
        ptotalBruto = ptotalBruto + value.montoBruto;
        ptotalRetencion = ptotalRetencion + value.montoRetencion;
        ptotalDescuento = ptotalDescuento + value.montoAplicacion;
        ptotalNeto = ptotalNeto + value.montoNeto;
      });
      setTotalBruto(ptotalBruto);
      setTotalRetencion(ptotalRetencion);
      setTotalDescuento(ptotalDescuento);
      setTotalNeto(ptotalNeto);
    }
  }, [listaComisionesAPagar]);

  function verificarTipo(tipo) {
    if (parseInt(tipo) === configEstadoTable.tipoPago.sionPay) {
      return style.backgroundSionPay;
    } else if (parseInt(tipo) === configEstadoTable.tipoPago.transferencia) {
      return style.backgroundTransferencia;
    } else if (parseInt(tipo) === configEstadoTable.tipoPago.cheque) {
      return style.backgroundCheque;
    } else {
      return style.backgroundNinguno;
    }
  }
  useEffect(() => {
    if (listaComisionPaginacionNueva) {
      setPage(0);
      setRowsPerPage(30);
      setContadorPage(page * rowsPerPage);
    }
  }, [listaComisionPaginacionNueva, rowsPerPage, page, contadorPage]);

  const [anchorEl, setAnchorEl] = React.useState(false);
  const open = Boolean(anchorEl);
  const [listFormaPago, setListFormaPago] = useState([]);

  const handleOpenFilter = (event) => {
    ApiListarTiposPagos(event.currentTarget);
  };

  async function ApiListarTiposPagos(event) {
    let url = "/formasPagosRezagados/GetFormaPagosDisponibles";
    let body = {
      usuarioLogin: userName,
      idCiclo,
      comisionId: idComision,
    };
    let respuesta = await requestPost(url, body, dispatch);
    if (respuesta && respuesta.code == 0) {
      setListFormaPago(respuesta.data);
      setAnchorEl(event);
    }
  }

  const handleCloseFilter = () => {
    setAnchorEl(null);
  };
  const seleccionarTipo = (idtipo) => {
    seleccionarTipoFiltroBusqueda(idtipo);
    setAnchorEl(null);
  };

  return (
    <>
      <br />
      {listaComisionesAPagar.length > 0 ? (
        <>
          <Grid container>
            <Grid item xs></Grid>
            <Grid item>
              <div className={style.root}>
                <Tooltip
                  disableFocusListener
                  disableTouchListener
                  TransitionComponent={Zoom}
                  title={"Seleccione una forma de pago."}
                >
                  <IconButton
                    aria-controls="fade-menu"
                    className={style.altoCeldas}
                    edge="start"
                    color="inherit"
                    aria-label="close"
                    onClick={handleOpenFilter}
                  >
                    Filtro <FilterListIcon />
                  </IconButton>
                </Tooltip>
                <StyledMenu
                  id="customized-menu"
                  anchorEl={anchorEl}
                  keepMounted
                  open={open}
                  onClose={handleCloseFilter}
                >
                  {listFormaPago.map((row, index) => (
                    <StyledMenuItem
                      key={index}
                      onClick={() => seleccionarTipo(`${row.idTipoPago}`)}
                    >
                      <ListItemIcon>
                        <ImageIconPagos name={row.icono} />
                      </ListItemIcon>
                      <ListItemText>
                        {row.nombre} {" - "} {"(" + row.cantidad + ")"}
                      </ListItemText>
                    </StyledMenuItem>
                  ))}
                </StyledMenu>
              </div>
              <br />
            </Grid>
          </Grid>

          <Grid container>
            <Grid item xs={12}>
              <TableContainer component={Paper}>
                <Table
                  className={style.table}
                  size="small"
                  aria-label="a dense table"
                >
                  <TableHead className={style.headerTable}>
                    <TableRow>
                      <TableCell align="center" className={style.headerRow}>
                        <b>#</b>
                      </TableCell>
                      <TableCell align="center" className={style.headerRow}>
                        <b>Nombre completo</b>
                      </TableCell>
                      <TableCell align="center" className={style.headerRow}>
                        <b>Cédula identidad</b>
                      </TableCell>
                      <TableCell align="center" className={style.headerRow}>
                        <b>Cuenta Banco</b>
                      </TableCell>
                      <TableCell align="center" className={style.headerRow}>
                        <b>Banco</b>
                      </TableCell>
                      <TableCell align="center" className={style.headerRow}>
                        <b>Monto Total Neto ($us.)</b>
                      </TableCell>
                      <TableCell align="center" className={style.headerRow}>
                        <b>Forma Pago</b>{" "}
                      </TableCell>
                      <TableCell
                        align="center"
                        className={style.headerRow}
                      ></TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {stableSort(
                      listaComisionesAPagar,
                      getComparator(order, orderBy)
                    )
                      .slice(
                        page * rowsPerPage,
                        page * rowsPerPage + rowsPerPage
                      )
                      .map((row, index) => (
                        <TableRow
                          key={index + 1}
                          className={verificarTipo(`${row.idTipoPago}`)}
                        >
                          <TableCell align="center" scope="row">
                            {" "}
                            {contadorPage + index + 1}{" "}
                          </TableCell>
                          <TableCell align="left">{row.nombre}</TableCell>
                          <TableCell align="center">{row.ci}</TableCell>
                          <TableCell align="center">
                            {row.cuentaBancaria}
                          </TableCell>
                          <TableCell align="center">
                            {row.nombreBanco}
                          </TableCell>
                          <TableCell align="center">
                            {row.montoNeto != null
                              ? row.montoNeto.toLocaleString("de-DE", {
                                  minimumFractionDigits: 2,
                                  maximumFractionDigits: 2,
                                })
                              : 0.0}
                          </TableCell>
                          <TableCell align="center">
                            {row.tipoPagoDescripcion}
                          </TableCell>
                          <TableCell align="center">
                            {row.idListaFormasPago > 0 ? (
                              <>
                                {permisoActualizar === true &&
                                  pendienteFormaPago === false && (
                                    <Tooltip
                                      disableFocusListener
                                      disableTouchListener
                                      TransitionComponent={Zoom}
                                      title={"Agregar un tipo de pagos"}
                                    >
                                      <IconButton
                                        className={style.altoCeldas}
                                        edge="start"
                                        color="inherit"
                                        aria-label="close"
                                        onClick={() =>
                                          selecionarDetalleFrelances(
                                            `${row.idComisionDetalle}`,
                                            `${row.ci}`,
                                            `${row.idTipoPago}`
                                          )
                                        }
                                      >
                                        <img
                                          width="22"
                                          height="22"
                                          src={require("../../../../assets/icons/tipopago1.png")}
                                        />
                                      </IconButton>
                                    </Tooltip>
                                  )}
                              </>
                            ) : (
                              <>
                                {permisoCrear === true &&
                                  pendienteFormaPago === false && (
                                    <Tooltip
                                      disableFocusListener
                                      disableTouchListener
                                      TransitionComponent={Zoom}
                                      title={"Sin tipo de pago"}
                                    >
                                      <IconButton
                                        className={style.altoCeldas}
                                        edge="start"
                                        color="inherit"
                                        aria-label="close"
                                        onClick={() =>
                                          selecionarDetalleFrelances(
                                            `${row.idComisionDetalle}`,
                                            `${row.ci}`,
                                            `${row.idTipoPago}`
                                          )
                                        }
                                      >
                                        <img
                                          width="22"
                                          height="22"
                                          src={require("../../../../assets/icons/tipopago2.png")}
                                        />
                                      </IconButton>
                                    </Tooltip>
                                  )}
                              </>
                            )}
                          </TableCell>
                        </TableRow>
                      ))}

                    <TableRow key={100000000000000}>
                      <TableCell align="center">
                        <b></b>
                      </TableCell>
                      <TableCell align="right">
                        <b></b>
                      </TableCell>
                      <TableCell align="center">
                        <b> </b>{" "}
                      </TableCell>
                      <TableCell align="center">
                        <b> </b>
                      </TableCell>
                      <TableCell align="center">
                        <b>TOTAL </b>
                      </TableCell>
                      <TableCell align="center">
                        <b>
                          {" "}
                          {totalNeto.toLocaleString("de-DE", {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2,
                          })}{" "}
                        </b>
                      </TableCell>
                      <TableCell align="center"></TableCell>
                    </TableRow>
                  </TableBody>
                </Table>
              </TableContainer>
              <TablePagination
                rowsPerPageOptions={[10, 30, 45]}
                component="div"
                count={listaComisionesAPagar.length}
                rowsPerPage={rowsPerPage}
                page={page}
                onChangePage={handleChangePage}
                onChangeRowsPerPage={handleChangeRowsPerPage}
              />
            </Grid>
          </Grid>
        </>
      ) : (
        <Card className={style.cardVacio}>
          <ErrorIcon /> {" No hay qué mostrar, selecione y cargue un ciclo"}
        </Card>
      )}
    </>
  );
};
export default GridFormaPagos;
