import  React, {useEffect , useState } from 'react';
import {TextField, Button, Dialog , DialogActions , DialogContent ,
  DialogTitle , Slide , Table, TableContainer,TableHead,TableRow,Paper,TableBody , TableCell} from '@material-ui/core';
import { event } from 'jquery';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

const ModalTipoIncentivo = (props) => {
  const { open, clickBotonCancelar , clickBotonAceptar, listaTipoIncentivo , pasarDescripcion } = props
  
  const [ descripcionValue , setDescripcionValue] = useState("")
  const handleChange = (event) =>{
    setDescripcionValue(event.target.value)
    pasarDescripcion(event.target.value)
  }

  return (
    <div>
      <Dialog
        open={open}
        TransitionComponent={Transition}
        keepMounted
        aria-describedby="alert-dialog-slide-description"
        onBackdropClick={null}
      >
        <DialogTitle>{"Registrar tipo de incentivo"}</DialogTitle>
        <DialogContent>
          <TextField onChange={handleChange} value={descripcionValue} fullWidth id="outlined-basic" label="Descripcion" variant="outlined" />
        </DialogContent>
        <DialogActions>
          <Button onClick={clickBotonCancelar}>Atras</Button>
          <Button onClick={clickBotonAceptar}>Registrar</Button>  
        </DialogActions>
        <TableContainer  style={{ height: '250px' }} >
          <Table size="small" aria-label="a dense table">
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Descripcion</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {listaTipoIncentivo.map((row) => (
                <TableRow
                  key={row.name}
                  sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                >
                  <TableCell>{row.idTipoIncentivo}</TableCell>
                  <TableCell>{row.descripcion}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Dialog>
    </div>
  );
}

export default ModalTipoIncentivo