import React, { useState, useEffect}  from 'react';
import { makeStyles } from '@material-ui/core/styles';
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

    const [order, setOrder] = useState('asc');
    const [orderBy, setOrderBy] = React.useState('calories');

        function descendingComparator(a, b, orderBy) {
            if (b[orderBy] < a[orderBy]) {
            return -1;
            }
            if (b[orderBy] > a[orderBy]) {
            return 1;
            }
            return 0;
        }
        function getComparator(order, orderBy) {
            return order === 'desc'
              ? (a, b) => descendingComparator(a, b, orderBy)
              : (a, b) => -descendingComparator(a, b, orderBy);
        }
        function stableSort(array, comparator) {            
            const stabilizedThis = array.map((el, index) => [el, index]);
            stabilizedThis.sort((a, b) => {
              const order = comparator(a[0], b[0]);
              if (order !== 0) return order;
              return a[1] - b[1];
            });
            return stabilizedThis.map((el) => el[0]);
          }


        const [rowsPerPage, setRowsPerPage] = useState(30);
        const [page, setPage] = useState(0);
        const [contadorPage, setContadorPage]= useState(0)

        const handleChangePage = (event, newPage) => {
            setPage(newPage);
        };    
        const handleChangeRowsPerPage = (event) => {
            setRowsPerPage(parseInt(event.target.value, 10));
            setPage(0);
        };
        useEffect(()=>{     
            setContadorPage( page * rowsPerPage);
         },[page, rowsPerPage]);

         const[totalBruto, setTotalBruto]= useState(0);
         const[totalDescuento, setTotalDescuento]= useState(0);
         const[totalRetencion, setTotalRetencion]= useState(0);         
         const[totalNeto, setTotalNeto]= useState(0);

         useEffect(()=>{    
                  if(listaComisionesPendientes.length > 0){
                      let ptotalBruto=0;
                      let ptotalRetencion= 0;
                      let ptotalDescuento=0;
                      let ptotalNeto=0;
 
                      listaComisionesPendientes.forEach(function (value) {
                        ptotalBruto = ptotalBruto + value.montoBruto;
                        ptotalDescuento = ptotalDescuento + value.montoAplicacion;
                        ptotalRetencion= ptotalRetencion + value.montoRetencion;                      
                        ptotalNeto= ptotalNeto + value.montoNeto;
                      }); 
                      setTotalBruto(ptotalBruto);
                      setTotalDescuento(ptotalDescuento);
                      setTotalRetencion(ptotalRetencion);                      
                      setTotalNeto(ptotalNeto);
                  }
         },[listaComisionesPendientes])

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
                            <TableCell align="center"><b>DESCUENTO</b></TableCell>                                                                                                            
                            <TableCell align="center"><b>MONTO RETENCION (15.5%)</b></TableCell> 
                            <TableCell align="center"><b>MONTO A PAGAR</b></TableCell>   
                            <TableCell align="center"><b>FACTURO?</b></TableCell>  
                            <TableCell align="right">    </TableCell>
                        </TableRow>
                        </TableHead>
                        <TableBody>
                        {stableSort(listaComisionesPendientes, getComparator(order, orderBy))
                         .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row, index) => (
                            <TableRow key={index + 1 }>
                            <TableCell align="center"scope="row"> {contadorPage + index +1} </TableCell>
                            <TableCell align="left">{row.nombre}</TableCell>
                            <TableCell align="center">{row.ci}</TableCell>
                            <TableCell align="center">{row.factura === "True"? <CheckBoxIcon color="disabled" /> : <CheckBoxOutlineBlankIcon color="disabled"/>}</TableCell>   
                            <TableCell align="right">{row.montoBruto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</TableCell>                                   
                            <TableCell align="right">{row.montoAplicacion.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</TableCell>
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
                            <TableRow key={100000000000000 }>
                                <TableCell align="center"scope="row"> {''} </TableCell>
                                <TableCell align="left"> <b>{''}</b></TableCell>
                                <TableCell align="center"><b>{'TOTAL'}</b></TableCell>
                                <TableCell align="center">{''}</TableCell>   
                                <TableCell align="right"><b>{totalBruto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</b></TableCell>                                   
                                <TableCell align="right"><b>{totalDescuento.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</b></TableCell>
                                <TableCell align="right"><b>{totalRetencion.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</b></TableCell>    
                                <TableCell align="right"><b>{totalNeto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</b></TableCell> 
                                <TableCell align="center"> {''}  </TableCell> 
                                <TableCell align="center"> {''}  </TableCell> 
                            </TableRow>
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