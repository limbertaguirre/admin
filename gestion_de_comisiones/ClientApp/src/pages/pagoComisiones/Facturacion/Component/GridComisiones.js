import React, { useState}  from 'react';
import { makeStyles } from '@material-ui/core/styles';
// import * as permiso from '../../../../routes/permiso'; 
// import { verificarAcceso, validarPermiso} from '../../../../lib/accesosPerfiles'; 
import CheckBoxIcon from '@material-ui/icons/CheckBox';
import CheckBoxOutlineBlankIcon from '@material-ui/icons/CheckBoxOutlineBlank';
import ErrorIcon from '@material-ui/icons/Error';
import {Tooltip ,Zoom ,Card, Button, Grid } from "@material-ui/core";

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
                            <TableCell align="center"><b>NRO</b></TableCell>
                            <TableCell align="center"><b>NOMBRE COMPLETO</b></TableCell>
                            <TableCell align="center"><b>CEDULA IDENTIDAD</b></TableCell>
                            <TableCell align="center"><b>PRESENTA FACTURA?</b></TableCell>
                            <TableCell align="center"><b>MONTO BRUTO</b></TableCell>                           
                            <TableCell align="center"><b>FACTURO?</b></TableCell>                            
                            <TableCell align="center"><b>MONTO RETENCION (15.5%)</b></TableCell> 
                            <TableCell align="center"><b>MONTO NETO</b></TableCell> 
                            <TableCell align="right">    </TableCell>
                        </TableRow>
                        </TableHead>
                        <TableBody>
                        {listaComisionesPendientes.map((row, index) => (
                            <TableRow key={index + 1 }>
                            <TableCell align="center"scope="row"> {index +1} </TableCell>
                            <TableCell align="left">{row.nombre}</TableCell>
                            <TableCell align="center">{row.ci}</TableCell>
                            <TableCell align="center">{row.factura === "True"? <CheckBoxIcon color="disabled" /> : <CheckBoxOutlineBlankIcon color="disabled"/>}</TableCell>   
                            <TableCell align="right">{row.montoBruto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</TableCell>                                   
                            <TableCell align="right">{row.montoRetencion.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    
                            <TableCell align="right">{row.montoNeto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell> 
                            <TableCell align="center">  {row.estadoFacturoId === 2?  <CheckBoxIcon color="disabled" /> : <CheckBoxOutlineBlankIcon color="disabled"  /> }  </TableCell>                         
                            <TableCell align="center">
                                   {row.factura === "True"&& 
                                        <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={row.estadoDetalleFacturaNombre}>
                                            <Button
                                            type="submit"
                                            fullWidth
                                            variant="contained"
                                            color="primary"
                                            alt={row.estadoDetalleFacturaNombre}
                                            className={style.submitDetalle}
                                            onClick = {()=> selecionarDetalleFrelances(`${row.idComisionDetalle}`, `${row.estadoFacturoId}`)}                                         
                                            >
                                            Detalle
                                            </Button>  
                                        </Tooltip> 
                                    }
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
                  <ErrorIcon style={{marginRight:'5px'}} />  {' No hay qué mostrar, selecione y cargue un ciclo'}
               </Card> 
            }
      </>
    );
}
export default GridComisiones;