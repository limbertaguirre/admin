import React, { useEffect, useState}  from 'react';
import { emphasize, makeStyles } from '@material-ui/core/styles';

import * as permiso from '../../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../../lib/accesosPerfiles';



import CheckBoxIcon from '@material-ui/icons/CheckBox';
import CheckBoxOutlineBlankIcon from '@material-ui/icons/CheckBoxOutlineBlank';
import ErrorIcon from '@material-ui/icons/Error';
import {Container, InputAdornment,Tooltip ,Zoom, Dialog,Card, DialogContent, Button, Grid, TextField, Typography, FormGroup, FormControlLabel,Checkbox,FormControl, InputLabel, Select, FormHelperText,MenuItem } from "@material-ui/core";

import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TablePagination from '@material-ui/core/TablePagination';
import Paper from '@material-ui/core/Paper';
import IconButton from '@material-ui/core/IconButton';
import VisibilityIcon from '@material-ui/icons/Visibility';
import VisibilityOffIcon from '@material-ui/icons/VisibilityOff';


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
    submitDetalleInactivo: {
        height:'25px',
    },
    cardVacio: {
          height:'400px',
          display:'flex',
          alignItems:'center',
          justifyContent:'center', 
      },
  }));


 const GridAplicaciones =(props)=> {
    let style= useStyles();
    const {aplicacionesList, selecionarDetalleFrelances, txtBusqueda} = props;
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
        const[totalRetencion, setTotalRetencion]= useState(0);
        const[totalDescuento, setTotalDescuento]= useState(0);
        const[totalNeto, setTotalNeto]= useState(0);
        useEffect(()=>{    
                 if(aplicacionesList.length > 0){
                     let ptotalBruto=0;
                     let ptotalRetencion= 0;
                     let ptotalDescuento=0;
                     let ptotalNeto=0;

                     aplicacionesList.forEach(function (value) {
                      ptotalBruto = ptotalBruto + value.montoBruto;
                      ptotalRetencion= ptotalRetencion + value.montoRetencion;
                      ptotalDescuento = ptotalDescuento + value.montoAplicacion;
                      ptotalNeto= ptotalNeto + value.montoNeto;
                     }); 
                     setTotalBruto(ptotalBruto);
                     setTotalRetencion(ptotalRetencion);
                     setTotalDescuento(ptotalDescuento);
                     setTotalNeto(ptotalNeto);
                 }
        },[aplicacionesList])
        


    return (
      <>
        {aplicacionesList.length>0? 
               <Grid>
               <TableContainer component={Paper}>
                    <Table className={style.table} size="medium" aria-label="a dense table">
                        <TableHead>
                        <TableRow>
                            <TableCell align="center"><b>#</b></TableCell>
                            <TableCell align="center"><b>Nombre completo</b></TableCell>
                            <TableCell align="center"><b>Cédula identidad</b></TableCell>
                            <TableCell align="center"><b>Nro. Cuenta</b></TableCell>
                            <TableCell align="center"><b>Banco</b></TableCell>
                            <TableCell align="center"><b>Monto Bruto ($us.)</b></TableCell>
                            <TableCell align="center"><b>Retención ($us.)</b></TableCell>
                            <TableCell align="center"><b>Descuento ($us.)</b></TableCell>
                            <TableCell align="center"><b>Monto Total Neto ($us.)</b></TableCell>
                            <TableCell align="center"></TableCell>
                        </TableRow>
                        </TableHead>
                        <TableBody>
                        { stableSort(aplicacionesList, getComparator(order, orderBy))
                         .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row, index) => (
                            <TableRow key={index +1 }>
                            <TableCell align="center"scope="row"> {contadorPage + index + 1} </TableCell>
                            <TableCell align="left">{row.nombre}</TableCell>
                            <TableCell align="center">{row.ci}</TableCell>
                            <TableCell align="center">{row.cuentaBancaria}</TableCell>
                            <TableCell align="center">{row.nombreBanco}</TableCell>   
                            <TableCell align="center">{row.montoBruto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</TableCell>   
                            <TableCell align="center">{row.montoRetencion.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</TableCell>   
                            <TableCell align="center">{row.montoAplicacion.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2,})}</TableCell>                              
                            <TableCell align="center">{row.montoNeto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</TableCell>    
                            <TableCell align="center">
                                {row.montoAplicacion > 0? 
                                     <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Ver descuento detallado.'}>                                        
                                         <IconButton edge="start" color="inherit" aria-label="close"   onClick = {()=> selecionarDetalleFrelances(`${row.idComisionDetalle}`)} >
                                            <VisibilityIcon color="primary"  style={{ fontSize: 30 }} />
                                         </IconButton>
                                    </Tooltip>
                                    :
                                    <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'No tiene descuentos.'}>                                       
                                         <IconButton edge="start" color="inherit"   aria-label="close"   onClick = {()=> selecionarDetalleFrelances(`${row.idComisionDetalle}`)}  >
                                            <VisibilityOffIcon color="disabled" style={{ fontSize: 30 }} />
                                         </IconButton>
                                    </Tooltip>
                                  }

                            </TableCell>   
                            </TableRow>
                        ))}

                          <TableRow key={100000000000000}>
                            <TableCell align="center"><b></b></TableCell>
                              <TableCell align="right"><b>TOTAL</b></TableCell>
                              <TableCell align="center"><b></b></TableCell>
                              <TableCell align="center"><b></b></TableCell>
                              <TableCell align="center"><b></b></TableCell>
                              <TableCell align="center"><b> {totalBruto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })}</b> </TableCell>
                              <TableCell align="center"><b> {totalRetencion.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })} </b></TableCell>
                              <TableCell align="center"><b> {totalDescuento.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })} </b></TableCell>
                              <TableCell align="center"><b> {totalNeto.toLocaleString("de-DE", { minimumFractionDigits: 2, maximumFractionDigits: 2, })} </b></TableCell>
                              <TableCell align="center"></TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
                <TablePagination
                    rowsPerPageOptions={[10, 30, 45]}
                    component="div"
                    count={aplicacionesList.length}
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
export default GridAplicaciones;