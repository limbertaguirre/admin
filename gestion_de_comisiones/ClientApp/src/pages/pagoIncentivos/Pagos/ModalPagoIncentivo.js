import  React, {useEffect , useState } from 'react';
import {TextField, Button, Dialog , DialogActions , DialogContent ,
  DialogTitle , Slide , Table, TableContainer,TableHead,TableRow,Paper,TableBody , TableCell, Typography , Grid} from '@material-ui/core';
import { event } from 'jquery';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

const ModalTipoIncentivo = (props) => {
  const { open, clickBotonCancelar , clickBotonAceptar, montoTotalIncentivo, totalUsuariosBenificiados, nombreCiclo, nombreIncentivo } = props
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
        <DialogTitle align="center">{"Confirmar pago de incentivo"}</DialogTitle>
        <DialogContent>
          <Grid container>
            <Grid item xs={6}>
              <Typography align="right"><b>Ciclo:&nbsp;</b></Typography>
            </Grid>
            <Grid item xs={6}>
              <Typography>{nombreCiclo}</Typography>    
            </Grid>
            <Grid item xs={6}>
              <Typography align="right"><b>Incentivo:&nbsp; </b></Typography>    
            </Grid>
            <Grid item xs={6}>
              <Typography>{nombreIncentivo}</Typography>    
            </Grid>
            <Grid item xs={6}>
              <Typography align="right"><b>Total ACI:&nbsp; </b></Typography>    
            </Grid>
            <Grid item xs={6}>
              <Typography>{totalUsuariosBenificiados}</Typography>    
            </Grid>
            <Grid item xs={6}>
              <Typography align="right"><b>Monto total:&nbsp; </b></Typography>    
            </Grid>
            <Grid item xs={6}>
              <Typography>{ montoTotalIncentivo}</Typography>    
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={clickBotonCancelar} variant="contained" color="secondary">Cancelar</Button>
          <Button onClick={clickBotonAceptar} variant="contained" color="primary">Confirmar pago</Button>  
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default ModalTipoIncentivo