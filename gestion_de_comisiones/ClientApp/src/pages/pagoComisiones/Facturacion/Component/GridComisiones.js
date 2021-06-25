import React, { useState}  from 'react';
import { emphasize, makeStyles } from '@material-ui/core/styles';

import * as permiso from '../../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../../lib/accesosPerfiles';



import CheckBoxIcon from '@material-ui/icons/CheckBox';
import CheckBoxOutlineBlankIcon from '@material-ui/icons/CheckBoxOutlineBlank';
import ErrorIcon from '@material-ui/icons/Error';
import {Container, InputAdornment, Dialog,Card, DialogContent, Button, Grid, TextField, Typography, FormGroup, FormControlLabel,Checkbox,FormControl, InputLabel, Select, FormHelperText,MenuItem } from "@material-ui/core";

import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TablePagination from '@material-ui/core/TablePagination';
import Paper from '@material-ui/core/Paper';

  const useStyles = makeStyles((theme) => ({

    table: {
      minWidth: 650,
    },
    submitDetalle: {
        height:'25px',
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white'
    },
    cardVacio: {
          height:'400px',
          display:'flex',
          alignItems:'center',
          justifyContent:'center', 
      },
  }));


 const GridComisiones =(props)=> {
    let style= useStyles();
    const {listaComisionesPendientes, selecionarDetalleFrelances, txtBusqueda} = props;
 
        //tabla
        const [rowsPerPage, setRowsPerPage] = useState(30);
        const [page, setPage] = useState(0);

        const handleChangePage = (event, newPage) => {
            setPage(newPage);
        };    
        const handleChangeRowsPerPage = (event) => {
            setRowsPerPage(parseInt(event.target.value, 10));
            setPage(0);
        };


    return (
      <>
        {listaComisionesPendientes.length>0? 
               <Grid>
               <TableContainer component={Paper}>
                    <Table className={style.table} size="medium" aria-label="a dense table">
                        <TableHead>
                        <TableRow>
                            <TableCell align="center"><b>ID</b></TableCell>
                            <TableCell align="right"><b>Nombre completo</b></TableCell>
                            <TableCell align="right"><b>Cedula identidad</b></TableCell>
                            <TableCell align="right"><b>Nro Cuenta</b></TableCell>
                            <TableCell align="right"><b>Banco</b></TableCell>
                            <TableCell align="center"><b>Monto Total Bruto</b></TableCell>
                            <TableCell align="center"><b>Factura</b></TableCell>
                            <TableCell align="center"><b>Facturo (15%)</b></TableCell>
                            <TableCell align="center"><b>Monto Total Neto</b></TableCell>
                            <TableCell align="right"> Adjuntos  </TableCell>
                        </TableRow>
                        </TableHead>
                        <TableBody>
                        {listaComisionesPendientes.map((row, index) => (
                            <TableRow key={index }>
                            <TableCell align="center"scope="row"> {index} </TableCell>
                            <TableCell align="center">{row.nombre}</TableCell>
                            <TableCell align="right">{row.ci}</TableCell>
                            <TableCell align="right">{row.cuentaBancaria}</TableCell>
                            <TableCell align="right">{row.nombreBanco}</TableCell>   
                            <TableCell align="right">{row.montoBruto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</TableCell>   
                            <TableCell align="right">{row.factura == "True"? <CheckBoxIcon color="primary" /> : <CheckBoxOutlineBlankIcon color="primary" />}</TableCell>   
                            <TableCell align="center"> {row.facturaDescuento == "True"?  <CheckBoxIcon color="primary" /> : <CheckBoxOutlineBlankIcon color="primary" /> } </TableCell>  
                            <TableCell align="right">{row.montoNeto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    
                            <TableCell align="center">
                                         <Button
                                            type="submit"
                                            fullWidth
                                            variant="contained"
                                            color="primary"
                                            className={style.submitDetalle}
                                            onClick = {()=> selecionarDetalleFrelances(`${row.idComisionDetalle}`)}                                         
                                        >
                                            Detalle
                                        </Button>   
                            </TableCell>   
                            </TableRow>
                        ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <TablePagination
                    rowsPerPageOptions={[10, 30, 45]}
                    component="div"
                    count={listaComisionesPendientes.length}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onChangePage={handleChangePage}
                    onChangeRowsPerPage={handleChangeRowsPerPage}
                    />
               </Grid>
               :<Card className={style.cardVacio}>                    
                  <ErrorIcon /> {' '} {' No hay qué mostrar, selecione y cargue un ciclo'}
               </Card> 
            }
      </>
    );
}
export default GridComisiones;