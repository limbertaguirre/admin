import  React, {useEffect , useState } from 'react';
import {TextField, Button, Dialog , DialogActions , DialogContent ,
  DialogTitle , Slide , Table, TableContainer,TableHead,TableRow,Paper,TableBody , TableCell, Typography , Grid} from '@material-ui/core';
import { event } from 'jquery';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

const ModalTipoIncentivo = (props) => {
  const { open, clickBotonCancelar , clickBotonAceptar, montoTotalIncentivo, totalUsuariosBenificiados, listaIncentivo } = props
  return (
    <div>
      <Dialog
        open={open}
        TransitionComponent={Transition}
        keepMounted
        aria-describedby="alert-dialog-slide-description"
        onBackdropClick={null}
        maxWidth="xs"
      >
        <DialogTitle align="center">{"Pago de incentivo"}</DialogTitle>
        <DialogContent>
          <Grid container>
            <TableContainer component={Paper}>
              <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                <TableHead>
                  <TableRow>
                    <TableCell>Nombre completo</TableCell>
                    <TableCell align="right">Cedula identidad</TableCell>
                    <TableCell align="right">Cuenta</TableCell>
                    <TableCell align="right">Monto</TableCell>
                    <TableCell align="right">Estado Pago</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {listaIncentivo.map((row) => (
                    <TableRow
                      key={row.name}
                      sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                    >
                      <TableCell>{row.nombreCompleto} </TableCell>
                      <TableCell align="right">{row.cedulaIdentidad}</TableCell>
                      <TableCell align="right">{row.cuentaBanco}</TableCell>
                      <TableCell align="right">{row.montoTotalNeto}</TableCell>
                      <TableCell align="right">{row.pagado}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
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