import React from "react";
import {
  Chip,
  makeStyles,
  withStyles,
  Breadcrumbs,
  Typography,
  Grid,
  Button,
  Tooltip,
  Card,
  TextField,
  InputLabel,
  FormControl,
  MenuItem,
  IconButton,
  Menu,
  Select,
  emphasize,
  InputAdornment,
  Zoom,
} from "@material-ui/core";
import { validarPermiso } from "../../../lib/accesosPerfiles";
import { MoreVert, Home, Save, Search, CloudUpload } from "@material-ui/icons";
import * as permiso from "../../../routes/permiso";
import usePagoRezagado from "../../../hooks/usePagoRezagado";
import SnackbarSion from "../../../components/message/SnackbarSion";
import TransferenciasDialog from "./components/TransferenciasDialog";
import GridPagos from "./components/GridPagos";
import MessageConfirm from "../../../components/mesageModal/MessageConfirm";

const StyledBreadcrumb = withStyles((theme) => ({
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
}))(Chip);

const useStyles = makeStyles((theme) => ({
  etiqueta: {
    marginBottom: theme.spacing(1),
    marginTop: theme.spacing(1),
    marginRight: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingLeft: theme.spacing(1),
  },
  submitCargar: {
    background: "#1872b8",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
  },
  submitSAVE: {
    background: "#f44336",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
    marginLeft: theme.spacing(1),
  },
  gridContainer: {
    paddingLeft: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingTop: theme.spacing(1),
    paddingBottom: theme.spacing(1),
  },
  containerCiclo: {
    paddingLeft: theme.spacing(1),
    paddingRight: theme.spacing(1),
    paddingTop: theme.spacing(1),
    paddingBottom: theme.spacing(1),
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
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
    flexDirection: "column",
    alignContent: "center",
    alignItems: "center",
    justifyContent: "center",
  },
  containerPaymentMethods: {
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
}));

const PagoRezagado = ({ location }) => {
  const style = useStyles();
  console.log(location);
  const {
    idCiclo,
    openTransferenciasDialog,
    handleCloseTransferencias,
    empresasTransferencias,
    handleOnGetPagos,
    statusBusqueda,
    perfiles,
    permiso,
    confirmarCierrePagos,
    txtBusqueda,
    onChangeSelectCiclo,
    buscarFreelanzer,
    listCiclo,
    handlePages,
    handleClose,
    abrirModal,
    handleClickOpenTransferencias,
    openSnackbar,
    closeSnackbar,
    tipoSnackbar,
    mensajeSnackbar,
    listaComisionesAPagar,
    listaComisionPaginacionNueva,
    setListaComisionPaginacionNueva,
    selecionarDetalleFrelances,
    seleccionarTipoFiltroBusqueda,
    openModalConfirm,
    nameComboSeleccionado,
    subtitulo,
    confirmarModal,
    CloseModalConfirmacion,
    open,
    seleccionarNombreCombo,
    anchorEl,
    idComision,
  } = usePagoRezagado();
  return (
    <>
      <div
        className="col-xl-12 col-lg-12 d-none d-lg-block"
        style={{ paddingLeft: "0px", paddingRight: "0px" }}
      >
        <Breadcrumbs aria-label="breadcrumb">
          <StyledBreadcrumb
            key={1}
            component="a"
            label="Pagos Rezagados"
            icon={<Home fontSize="small" />}
          />
        </Breadcrumbs>
      </div>
      <br />
      <Typography variant="h4" gutterBottom>
        Pagos Rezagados
      </Typography>

      {empresasTransferencias && (
        <TransferenciasDialog
          cicloId={idCiclo}
          idComision={idComision}
          openDialog={openTransferenciasDialog}
          closeTransferenciasDialog={handleCloseTransferencias}
          empresas={empresasTransferencias}
          recargarCicloActual={handleOnGetPagos}
        />
      )}

      <Card>
        <Grid container className={style.gridContainer}>
          {statusBusqueda ? (
            <>
              <Grid item xs={12} sm={3} md={3} className={style.containerSave}>
                {statusBusqueda && (
                  <>
                    {
                      // validarPermiso(
                      //   perfiles,
                      //   location.state.namePagina + permiso.CREAR
                      // )
                      true ? (
                        <Button
                          type="submit"
                          variant="contained"
                          color="primary"
                          className={style.submitCargar}
                          onClick={() => null}
                        >
                          <Save style={{ marginRight: "5px" }} /> CERRAR PAGOS
                          REZAGADOS
                        </Button>
                      ) : (
                        <Tooltip
                          disableFocusListener
                          disableTouchListener
                          TransitionComponent={Zoom}
                          title={"Sin Acceso"}
                        >
                          <Button variant="contained">
                            {" "}
                            <Save style={{ marginRight: "5px" }} /> CERRAR FORMA
                            PAGO
                          </Button>
                        </Tooltip>
                      )
                    }
                  </>
                )}
              </Grid>
            </>
          ) : (
            <>
              <Grid
                item
                xs={12}
                sm={4}
                md={4}
                className={style.containerSave}
              ></Grid>
            </>
          )}
          <Grid item xs={12} md={3} className={style.containerSave}>
            {statusBusqueda && (
              <TextField
                label="Buscar freelancer"
                type={"text"}
                variant="outlined"
                placeholder={"Buscar por carnet identidad"}
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
                    <InputAdornment position="start">
                      <Search />
                    </InputAdornment>
                  ),
                }}
              />
            )}
          </Grid>

          <Grid item xs={12} md={3} className={style.containerCiclo}>
            <FormControl
              variant="outlined"
              fullWidth
              className={style.TextFiel}
            >
              <InputLabel id="demo-simple-select-outlined-labelciclo">
                CICLO #{" "}
              </InputLabel>
              <Select
                labelId="demo-simple-select-outlined-labelciclo"
                value={idCiclo}
                name="idCiclo"
                onChange={onChangeSelectCiclo}
                label="CICLO # "
              >
                <MenuItem value={0}>
                  <em>Seleccione un ciclo</em>
                </MenuItem>
                {listCiclo.map((value, index) => (
                  <MenuItem
                    key={index}
                    onClick={() => seleccionarNombreCombo(`${value.nombre}`)}
                    value={value.idCiclo}
                  >
                    {value.nombre}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </Grid>
          <Grid item xs={12} md={2} className={style.containerCargar}>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={style.submitCargar}
              onClick={() => handleOnGetPagos()}
            >
              {"CARGAR "} <CloudUpload style={{ marginLeft: "12px" }} />
            </Button>
          </Grid>

          {statusBusqueda && (
            <>
              {validarPermiso(
                perfiles,
                location.state.namePagina + permiso.CREAR
              ) && (
                <>
                  <Grid
                    item
                    xs={12}
                    sm={1}
                    md={1}
                    lg={1}
                    className={style.containerPaymentMethods}
                  >
                    <IconButton
                      aria-label="more"
                      aria-controls="long-menu"
                      aria-haspopup="true"
                      onClick={handlePages}
                    >
                      <MoreVert />
                    </IconButton>
                    <Menu
                      id="long-menu"
                      anchorEl={anchorEl}
                      keepMounted
                      open={open}
                      onClose={handleClose}
                      PaperProps={{
                        style: {
                          maxHeight: 48 * 4.5,
                          width: "50ch",
                        },
                      }}
                    >
                      {/* <MenuItem key="1" onClick={() => abrirModal()}>
                        PAGAR SION PAY
                      </MenuItem> */}
                      <MenuItem
                        key="2"
                        onClick={() => handleClickOpenTransferencias()}
                      >
                        GENERAR ARCHIVO PARA TRANSFERENCIA
                      </MenuItem>
                    </Menu>
                  </Grid>
                </>
              )}
            </>
          )}
        </Grid>
      </Card>
      <SnackbarSion
        open={openSnackbar}
        closeSnackbar={closeSnackbar}
        tipo={tipoSnackbar}
        duracion={2000}
        mensaje={mensajeSnackbar}
      />
      <GridPagos
        listaComisionesAPagar={listaComisionesAPagar}
        listaComisionPaginacionNueva={listaComisionPaginacionNueva}
        setListaComisionPaginacionNueva={setListaComisionPaginacionNueva}
        selecionarDetalleFrelances={selecionarDetalleFrelances}
        seleccionarTipoFiltroBusqueda={seleccionarTipoFiltroBusqueda}
        idCiclo={idCiclo}
        // permisoActualizar={true}
        // permisoCrear={true}
        permisoActualizar={validarPermiso(
          perfiles,
          location.state.namePagina + permiso.ACTUALIZAR
        )}
        permisoCrear={validarPermiso(
          perfiles,
          location.state.namePagina + permiso.CREAR
        )}
      />
      <MessageConfirm
        open={openModalConfirm}
        titulo={"Confirmación de pagos por SION PAY."}
        nombreCiclo={nameComboSeleccionado}
        subTituloModal={subtitulo}
        tipoModal={"warning"}
        mensaje={"¿Desea confirmar el pago a través de SION PAY? "}
        handleCloseConfirm={confirmarModal}
        handleCloseCancel={CloseModalConfirmacion}
      />
    </>
  );
};

export default PagoRezagado;
