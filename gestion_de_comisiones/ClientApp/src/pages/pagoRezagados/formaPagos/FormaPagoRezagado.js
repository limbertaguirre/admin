import React from "react";

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
  submitPendiente: {
    background: "#E29020",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
  },
  submitAprobado: {
    background: "#197608",
    boxShadow: "2px 4px 5px #1872b8",
    color: "white",
  },
  submitSAVE: {
    background: "#1872b8",
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
}));

const FormaPagoRezagado = ({ location }) => {
  const style = useStyles();
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
            label="Gestión de rezagado"
            icon={<HomeIcon fontSize="small" />}
          />
          <StyledBreadcrumb key={2} component="a" label="Pago de comisiones" />
          <StyledBreadcrumb key={3} label="Forma de pagos" />
        </Breadcrumbs>
      </div>
      <br />
      <Typography variant="h4" gutterBottom>
        {"Forma de pagos"}
      </Typography>
      <Grid container item xs={12} justify="flex-end">
        {pendienteFormaPago && (
          <img src={imageFac} alt={"sion"} style={{ width: "100px" }} />
        )}
      </Grid>
      {autorizadorObjeto.autorizador && (
        <Card>
          <Grid container className={style.gridContainer}>
            <Grid item xs={12} md={2} className={style.containerCargar}>
              {autorizadorObjeto.comisionAutorizada ? (
                <Button
                  type="submit"
                  fullWidth
                  variant="contained"
                  className={style.submitAprobado}
                >
                  <>
                    {"PAGO APROBADO "}
                    <CheckCircleOutlineIcon />{" "}
                  </>
                </Button>
              ) : (
                <Button
                  type="submit"
                  fullWidth
                  variant="contained"
                  className={style.submitPendiente}
                >
                  <>
                    {"PENDIENTE APROBACION "}
                    <HelpOutlineIcon />{" "}
                  </>
                </Button>
              )}
            </Grid>
          </Grid>
        </Card>
      )}

      <Card>
        <Grid container className={style.gridContainer}>
          <Grid item xs={12} md={3} className={style.containerSave}>
            {statusBusqueda && (
              <>
                {validarPermiso(
                  perfiles,
                  location.state.namePagina + permiso.CREAR
                ) ? (
                  <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    className={style.submitSAVE}
                    onClick={() => verificarConfirmarFomaPago()}
                  >
                    <SaveIcon style={{ marginRight: "5px" }} /> CERRAR FORMA
                    PAGO
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
                      <SaveIcon style={{ marginRight: "5px" }} /> CERRAR FORMA
                      PAGO
                    </Button>
                  </Tooltip>
                )}
              </>
            )}
          </Grid>
          <Grid item xs={12} md={4} className={style.containerSave}>
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
                      <SearchIcon />
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
                {ciclos.map((value, index) => (
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
              onClick={() => handleOnGetAplicaciones()}
            >
              {"CARGAR "} <CloudUploadIcon style={{ marginLeft: "12px" }} />
            </Button>
          </Grid>
        </Grid>
      </Card>

      <SnackbarSion
        open={openSnackbar}
        closeSnackbar={closeSnackbar}
        tipo={tipoSnackbar}
        duracion={2000}
        mensaje={mensajeSnackbar}
      />
      <GridFormaPagos
        listaComisionesAPagar={listaComisionesAPagar}
        listaComisionPaginacionNueva={listaComisionPaginacionNueva}
        setListaComisionPaginacionNueva={setListaComisionPaginacionNueva}
        selecionarDetalleFrelances={selecionarDetalleFrelances}
        seleccionarTipoFiltroBusqueda={seleccionarTipoFiltroBusqueda}
        idCiclo={idCiclo}
        pendienteFormaPago={pendienteFormaPago}
        permisoActualizar={validarPermiso(
          perfiles,
          location.state.namePagina + permiso.ACTUALIZAR
        )}
        permisoCrear={validarPermiso(
          perfiles,
          location.state.namePagina + permiso.CREAR
        )}
      />
      <TipoPagosModal
        open={openTipoPago}
        closeHandelModal={cerrarModalTipoPagoModal}
        confirmarTipoPago={confirmarTipoPago}
        listTipoPagos={listTipoPagos}
        idtipoPagoSelect={idtipoPagoSelect}
        handleChangeRadio={handleChangeRadio}
      />
      <VistaListaAutorizados
        open={openModalAutorizadores}
        objList={autorizadorObjeto}
        nameComboSeleccionado={nameComboSeleccionado}
        closeHandelModal={cerrarModalListaAutorizadosConfirm}
        confirmarModalAutorizacion={confirmarModalAutorizacion}
      />
      <ConfirmarCierrePagoModal
        open={openCierrePagoModal}
        closeHandelModal={cancelarModalConfirmarCierre}
        confirmarPago={confirmarCierrePagoModal}
        listado={listadoConfirm}
        habilitado={habilitadoCierrePago}
        listadoSeleccionado={listadoSeleccionado}
      />
    </>
  );
};

export default FormaPagoRezagado;
