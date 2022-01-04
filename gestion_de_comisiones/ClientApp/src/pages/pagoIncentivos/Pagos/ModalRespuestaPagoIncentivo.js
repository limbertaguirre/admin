import  React, {useEffect , useState } from 'react';
import { makeStyles, TextField, Button, Dialog , DialogActions , DialogContent ,
  DialogTitle , Slide , Table, TableContainer,TableHead,TableRow,Paper,TableBody , TableCell, Typography , Grid} from '@material-ui/core';
import { event } from 'jquery';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

const useStyles = makeStyles({
  root: {
    width: '100%',
  },
  container: {
    maxHeight: 440,
  },
  tableHeaderPlanilla:{
    background : "#1872b8",
    color: 'white'
  },
  filaNoPagado:{
    background: '#ffb1b1'
  },
  filaPagado:{
    background: '#b5ffc9'
  }
});

const ModalTipoIncentivo = (props) => {
  const { open, clickBotonCancelar , clickBotonAceptar, montoTotalIncentivo, totalUsuariosBenificiados, listaIncentivo } = props;
  const classes = useStyles();
  return (
    <div>
      <Dialog
        open={open}
        TransitionComponent={Transition}
        keepMounted
        aria-describedby="alert-dialog-slide-description"
        onBackdropClick={null}
      >
        <DialogTitle align="center">{"Resultado pago de incentivo"}</DialogTitle>
        <DialogContent>
          <Grid container>
            <Grid item xs={12}>
              <TableContainer component={Paper}>
                <Table size="small" aria-label="a dense table">
                  <TableHead>
                    <TableRow >
                      <TableCell className={classes.tableHeaderPlanilla}>Nombre completo</TableCell>
                      <TableCell className={classes.tableHeaderPlanilla}>Cedula identidad</TableCell>
                      <TableCell className={classes.tableHeaderPlanilla} >Cuenta</TableCell>
                      <TableCell className={classes.tableHeaderPlanilla} align="right">Monto</TableCell>
                      <TableCell className={classes.tableHeaderPlanilla} >Estado Pago</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {listaIncentivo.map((row) => (
                      <TableRow
                        key={row.name}
                        sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        className={ row.pagado ? classes.filaPagado : classes.filaNoPagado}
                      >
                        <TableCell>{row.nombreCompleto} </TableCell>
                        <TableCell>{row.cedulaIdentidad}</TableCell>
                        <TableCell>{row.cuentaSionPay}</TableCell>
                        <TableCell align="right">{row.montoTotalNeto.toFixed(2)}</TableCell>
                        <TableCell>{row.pagado ? "Pagado" : "No pagado" }</TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </TableContainer>
            </Grid>
            <Grid item xs={12}>
            </Grid>
          </Grid>

        </DialogContent>
        <DialogActions>
          {/* <Button onClick={clickBotonCancelar} variant="contained" color="secondary">Cancelar</Button> */}
          <Button onClick={clickBotonAceptar} variant="contained" color="primary">Aceptar</Button>  
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default ModalTipoIncentivo