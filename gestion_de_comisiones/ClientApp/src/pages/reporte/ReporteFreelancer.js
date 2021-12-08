import {
  CircularProgress,
  TextField,
  Chip,
  emphasize,
  withStyles,
  makeStyles,
  Breadcrumbs,
  Table,
  TableCell,
  TableRow,
  TableBody,
  TableHead,
  TableContainer,
  Typography,
  Paper,
  Grid,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
} from "@material-ui/core";
import { Home, List } from "@material-ui/icons";
import { Autocomplete } from "@material-ui/lab";
import React from "react";
import { formatearNumero } from "../../lib/utility";
import useReporteFreelancer from "../../hooks/useReporteFreelancer";

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
  cardPaper: {
    padding: theme.spacing(2),
    marginBottom: theme.spacing(2),
  },
  amountGrid: {
    marginTop: theme.spacing(1),
  },
  buttonsGrid: {
    display: "flex",
    flexDirection: "row",
    justifyContent: "flex-start",
    alignItems: "center",
  },
  buttonRight: {
    marginLeft: theme.spacing(2),
  },
}));

const ReporteFreelancer = () => {
  const styles = useStyles();
  const {
    clients,
    selectedClient,
    setSelectedClient,
    items,
    detailItems,
    open,
    closeDialog,
    montoTotal,
    getDetailReportInfo,
    getReportInfo,
    searchFreelancer,
    selectedDetailItemData,
    openAutocompleteInput,
    closeAutocompleteInput,
    openAutocomplete,
    loadingAutcomplete,
    downloadReportExcel,
    downloadReportPdf,
  } = useReporteFreelancer();
  return (
    <div>
      <div
        className="col-xl-12 col-lg-12 d-none d-lg-block"
        style={{ paddingLeft: "0px", paddingRight: "0px" }}
      >
        <Breadcrumbs aria-label="breadcrumb">
          <StyledBreadcrumb
            component="a"
            label="Reporte por freelancer"
            icon={<Home fontSize="small" />}
          />
        </Breadcrumbs>
      </div>
      <br />
      <Typography variant="h4" gutterBottom>
        Reporte por freelancer
      </Typography>
      <Paper elevation={2} className={styles.cardPaper}>
        <Grid container spacing={2}>
          <Grid item xs={12} sm={7}>
            <Autocomplete
              id="asynchronous-demo"
              className="mt-3"
              open={openAutocomplete}
              fullWidth
              limitTags={50}
              onOpen={openAutocompleteInput}
              onClose={closeAutocompleteInput}
              getOptionSelected={(option, value) =>
                option.idFicha === value.idFicha
              }
              getOptionLabel={(option) =>
                `${option.nombres} ${option.apellidos} (${option.ci})`
              }
              options={clients}
              loading={loadingAutcomplete}
              value={selectedClient}
              filterOptions={(options, state) => {
                const inputValue = state.inputValue;
                let opts = inputValue.split(" ");
                return options.filter((option) => {
                  for (let i = 0; i < opts.length; i++) {
                    const element = opts[i];
                    if (
                      option.nombres
                        .toLowerCase()
                        .indexOf(element.toLowerCase()) !== -1 ||
                      option.apellidos
                        .toLowerCase()
                        .indexOf(element.toLowerCase()) !== -1 ||
                      option.ci.toLowerCase().indexOf(element.toLowerCase()) !==
                        -1
                    ) {
                      return true;
                    }
                  }
                  return false;
                });
              }}
              onChange={(e, newValue) => setSelectedClient(newValue)}
              renderInput={(params) => (
                <TextField
                  {...params}
                  label="Seleccione un Freelancer"
                  variant="outlined"
                  fullWidth
                  FormHelperTextProps={{
                    style: { color: "black" },
                  }}
                  InputProps={{
                    ...params.InputProps,
                    fullWidth: true,
                    onChange: (e) => searchFreelancer(e.target.value),
                    endAdornment: (
                      <React.Fragment>
                        {loadingAutcomplete ? (
                          <CircularProgress color="inherit" size={20} />
                        ) : null}
                        {params.InputProps.endAdornment}
                      </React.Fragment>
                    ),
                  }}
                />
              )}
            />
          </Grid>
          <Grid item xs={12} sm={5} className={styles.buttonsGrid}>
            <Button
              variant="contained"
              color="primary"
              size="small"
              onClick={getReportInfo}
            >
              Cargar
            </Button>
            <Button
              variant="contained"
              size="small"
              onClick={downloadReportExcel}
              className={styles.buttonRight}
            >
              Excel
            </Button>
            <Button
              variant="contained"
              color="secondary"
              size="small"
              onClick={downloadReportPdf}
              className={styles.buttonRight}
            >
              PDF
            </Button>
          </Grid>
        </Grid>
      </Paper>
      {selectedClient && items.length > 0 && (
        <Paper elevation={2} className={styles.cardPaper}>
          <Typography>
            <b>Nombres: </b>
            {selectedClient.nombres}
          </Typography>
          <Typography>
            <b>Apellidos: </b>
            {selectedClient.apellidos}
          </Typography>
          <Typography>
            <b>CI: </b>
            {selectedClient.ci}
          </Typography>
          <Typography>
            <b>Email: </b>
            {selectedClient.correoElectronico}
          </Typography>
        </Paper>
      )}
      <TableContainer component={Paper} elevation={2}>
        {items.length === 0 ? (
          <div
            style={{
              width: "100%",
              height: 400,
              marginBottom: 0,
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
            }}
          >
            <Typography style={{ textAlign: "center" }}>
              No se encontraron resultados, seleccione y cargue un ciclo.
            </Typography>
          </div>
        ) : (
          <Table size="small">
            <TableHead>
              <TableRow>
                <TableCell variant="head">#</TableCell>
                <TableCell variant="head">CICLO</TableCell>
                <TableCell variant="head">TIPO DE PAGO</TableCell>
                <TableCell variant="head">CUENTA SIONPAY</TableCell>
                <TableCell variant="head">CUENTA DE BANCO</TableCell>
                <TableCell variant="head">MONTO</TableCell>
                <TableCell variant="head"></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((item, index) => (
                <TableRow key={item.idComisionDetalle}>
                  <TableCell>{index + 1}</TableCell>
                  <TableCell>{item.ciclo}</TableCell>
                  <TableCell>{item.tipoPago}</TableCell>
                  <TableCell>{item.nroCuenta}</TableCell>
                  <TableCell>{item.cuentaBancaria}</TableCell>
                  <TableCell align="right">
                    {formatearNumero(item.montoNeto)}
                  </TableCell>
                  <TableCell>
                    <IconButton
                      color="secondary"
                      onClick={() =>
                        getDetailReportInfo(item.idComisionDetalle)
                      }
                    >
                      <List />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
              <TableRow>
                <TableCell colSpan={5} align="right">
                  <b>TOTAL:</b>
                </TableCell>
                <TableCell align="right">
                  <b>{formatearNumero(montoTotal)}</b>
                </TableCell>
              </TableRow>
            </TableBody>
          </Table>
        )}
      </TableContainer>
      <Dialog
        open={open}
        maxWidth="sm"
        fullWidth
        onClose={closeDialog}
        aria-labelledby="detalle-title"
        aria-describedby="detalle-description"
      >
        <DialogTitle id="detalle-title">Detalle por empresa</DialogTitle>
        <DialogContent>
          {selectedDetailItemData && (
            <>
              <Typography>
                <b>CI: </b>
                {selectedClient.ci}
              </Typography>
              <Typography>
                <b>Nombre: </b>
                {selectedClient.nombres + " " + selectedClient.apellidos}
              </Typography>
              <Typography>
                <b>Tipo de Pago: </b>
                {selectedDetailItemData.tipoPago}
              </Typography>
            </>
          )}

          <Table size="small">
            <TableHead>
              <TableRow>
                <TableCell variant="head">#</TableCell>
                <TableCell variant="head">Empresa</TableCell>
                <TableCell variant="head">Tipo Comision</TableCell>
                <TableCell variant="head">Monto</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {detailItems.map((item, index) => (
                <TableRow key={item.idComisionDetalleEmpresa}>
                  <TableCell>{index + 1}</TableCell>
                  <TableCell>{item.nombreEmpresa}</TableCell>
                  <TableCell>{item.tipoComision}</TableCell>
                  <TableCell align="right">
                    {formatearNumero(item.montoNeto)}
                  </TableCell>
                </TableRow>
              ))}
              <TableRow>
                <TableCell colSpan={3} align="right">
                  <b>TOTAL:</b>
                </TableCell>
                <TableCell align="right">
                  <b>
                    {selectedDetailItemData
                      ? formatearNumero(selectedDetailItemData.montoNeto)
                      : ""}
                  </b>
                </TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </DialogContent>
        <DialogActions>
          <Button onClick={closeDialog} color="primary" autoFocus>
            Cerrar
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};

export default ReporteFreelancer;
