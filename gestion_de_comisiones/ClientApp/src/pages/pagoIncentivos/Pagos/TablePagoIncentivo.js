import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';

const columns = [
  { id: 'nombre', label: 'Nombre\u00a0Completo', minWidth: 100 },
  {
    id: 'carnet',
    label: 'Carnet identidad',
    minWidth: 170,
    format: (value) => value.toLocaleString('en-US'),
  },
  {
    id: 'cuenta',
    label: 'Cuenta',
    minWidth: 170,
    format: (value) => value.toLocaleString('en-US'),
  },
  {
    id: 'monto',
    label: 'Monto',
    minWidth: 100,
    align: 'right'
  },
  {
    id: 'tipoIncentivo',
    label: 'Tipo incentivo',
    minWidth: 170,
  },
  {
    id: 'formaPago',
    label: 'Forma de Pago',
    minWidth: 170,
  },
];

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
  }
});

export default function TablePagoIncentivo(props) {
  const { listaIncentivo } = props
  const classes = useStyles();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  return (
    <Paper className={classes.root}>
      <TableContainer className={classes.container}>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow >
              {columns.map((column) => (
                <TableCell
                  key={column.id}
                  align={column.align}
                  style={{ minWidth: column.minWidth }}
                  className={classes.tableHeaderPlanilla}
                >
                  {column.label}
                </TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {listaIncentivo.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row,index) => {
              return (
                <TableRow>
                  <TableCell>{row.nombreCompleto} </TableCell>
                  <TableCell>{row.cedulaIdentidad} </TableCell>
                  <TableCell>{row.cuentaBanco} </TableCell>
                  <TableCell align='right'>{row.montoTotalNeto.toFixed(2)} </TableCell>
                  <TableCell>{row.tipoIncentivo} </TableCell>
                  <TableCell>{row.tipoPago} </TableCell>
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        count={listaIncentivo.length}
        rowsPerPage={rowsPerPage}
        labelRowsPerPage="Filas por pagina"
        page={page}
        onChangePage={handleChangePage}
        onChangeRowsPerPage={handleChangeRowsPerPage}
      />
    </Paper>
  );
}
