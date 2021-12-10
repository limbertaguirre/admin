import {
  Paper,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  IconButton,
  Grid,
  Select,
  FormControl,
  InputLabel,
  MenuItem,
  Button,
  Typography,
  makeStyles,
  Breadcrumbs,
  emphasize,
  Dialog,
  DialogContent,
  DialogActions,
  DialogTitle,
  Chip,
  withStyles,
  FormControlLabel,
  Switch,
  Menu,
} from "@material-ui/core";
import Home from "@material-ui/icons/Home";
import ListIcon from "@material-ui/icons/List";
import React from "react";
import useReporteCiclo from "../../hooks/useReporteCiclo";
import { formatearNumero } from "../../lib/utility";

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

const ReporteCiclo = () => {
  const styles = useStyles();
  const {
    ciclo,
    setCiclo,
    items,
    ciclos,
    detailItems,
    getDetailReportInfo,
    totalSionPay,
    totalTransferencia,
    getReportInfo,
    closeDialog,
    open,
    selectedDetailItemData,
    downloadPdfReport,
    downloadExcelReport,
    activarComisiones,
    activarRezagados,
    setActivarComisiones,
    setActivarRezagados,
    onClickExportButton,
    closeExportMenu,
    exportAnchorEl,
  } = useReporteCiclo();
  return (
    <div>
      <div
        className="col-xl-12 col-lg-12 d-none d-lg-block"
        style={{ paddingLeft: "0px", paddingRight: "0px" }}
      >
        <Breadcrumbs aria-label="breadcrumb">
          <StyledBreadcrumb
            component="a"
            label="Reporte por ciclo"
            icon={<Home fontSize="small" />}
          />
        </Breadcrumbs>
      </div>
      <br />
      <Typography variant="h4" gutterBottom>
        Reporte por ciclo
      </Typography>
      <Paper elevation={2} className={styles.cardPaper}>
        <Grid container spacing={2}>
          <Grid item xs={12} sm={4}>
            <FormControl variant="outlined" fullWidth>
              <InputLabel id="ciclo-label-id">Ciclo</InputLabel>
              <Select
                labelId="ciclo-label-id"
                value={ciclo}
                label="Ciclo"
                onChange={(e, value) => setCiclo(e.target.value)}
              >
                <MenuItem value="">
                  <em>Seleccione un ciclo</em>
                </MenuItem>
                {ciclos.map((ciclo) => (
                  <MenuItem key={ciclo.idCiclo} value={ciclo.idCiclo}>
                    {ciclo.nombre}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </Grid>

          <Grid item xs={12} sm={4} className={styles.buttonsGrid}>
            <Button
              variant="contained"
              color="primary"
              size="small"
              onClick={getReportInfo}
            >
              Cargar
            </Button>
            {items.length > 0 && (
              <>
                <Button
                  aria-controls="export-menu"
                  aria-haspopup="true"
                  variant="contained"
                  color="secondary"
                  size="small"
                  style={{ marginLeft: 4 }}
                  onClick={onClickExportButton}
                >
                  Exportar
                </Button>
                <Menu
                  id="export-menu"
                  anchorOrigin={{
                    vertical: "bottom",
                    horizontal: "center",
                  }}
                  transformOrigin={{
                    vertical: "top",
                    horizontal: "center",
                  }}
                  anchorEl={exportAnchorEl}
                  getContentAnchorEl={null}
                  open={Boolean(exportAnchorEl)}
                  onClose={closeExportMenu}
                >
                  <MenuItem onClick={downloadExcelReport}>
                    Exportar a Excel
                  </MenuItem>
                  <MenuItem onClick={downloadPdfReport}>
                    Exportar a Pdf
                  </MenuItem>
                </Menu>
              </>
            )}
          </Grid>
          <Grid item xs={12} sm={4}>
            <FormControlLabel
              control={
                <Switch
                  checked={activarComisiones}
                  onChange={(e) => setActivarComisiones(e.target.checked)}
                  name="comisiones"
                />
              }
              style={{ marginBottom: 0 }}
              label="Pago por comision"
            />
            <FormControlLabel
              control={
                <Switch
                  checked={activarRezagados}
                  onChange={(e) => setActivarRezagados(e.target.checked)}
                  name="rezagados"
                />
              }
              style={{ marginBottom: 0 }}
              label="Pago por rezagados"
            />
          </Grid>
        </Grid>
        {items.length > 0 && (
          <Grid container spacing={2} className={styles.amountGrid}>
            <Grid item xs={12} sm={4}>
              <Typography>
                <b>Total Sion Pay: </b>
                {formatearNumero(totalSionPay)}
              </Typography>
            </Grid>
            <Grid item xs={12} sm={4}>
              <Typography>
                <b>Total Transferencia: </b>
                {formatearNumero(totalTransferencia)}
              </Typography>
            </Grid>
            <Grid item xs={12} sm={4}>
              <Typography>
                <b>Total General: </b>
                {formatearNumero(totalSionPay + totalTransferencia)}
              </Typography>
            </Grid>
          </Grid>
        )}
      </Paper>
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
                <TableCell variant="head">NOMBRE</TableCell>
                <TableCell variant="head">DOC. IDENTIDAD</TableCell>
                <TableCell variant="head">CUENTA DE BANCO</TableCell>
                <TableCell variant="head">CUENTA SIONPAY</TableCell>
                <TableCell variant="head">MONTO</TableCell>
                <TableCell variant="head">TIPO DE PAGO</TableCell>
                <TableCell variant="head"></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.map((item, index) => (
                <TableRow key={item.idComisionDetalle}>
                  <TableCell>{index + 1}</TableCell>
                  <TableCell>{item.nombres + " " + item.apellidos}</TableCell>
                  <TableCell>{item.ci}</TableCell>
                  <TableCell>{item.cuentaBancaria}</TableCell>
                  <TableCell>{item.nroCuenta}</TableCell>
                  <TableCell align="right">
                    {formatearNumero(item.montoNeto)}
                  </TableCell>
                  <TableCell>{item.tipoPago}</TableCell>
                  <TableCell>
                    <IconButton
                      color="secondary"
                      onClick={() =>
                        getDetailReportInfo(item.idComisionDetalle)
                      }
                    >
                      <ListIcon />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
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
                {selectedDetailItemData.ci}
              </Typography>
              <Typography>
                <b>Nombre: </b>
                {selectedDetailItemData.nombres +
                  " " +
                  selectedDetailItemData.apellidos}
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

export default ReporteCiclo;
