import React,{useEffect,useState} from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import Button from '@material-ui/core/Button';
import { requestPost, requestGet } from "../../../service/request";
import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';
import {useSelector, useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';


const columns = [
    {id: 'usuarioRolId', label: 'ID', minWidth: 170 },
    {id: 'usuario', label: 'Usuario', minWidth: 170 },
    {id: 'nombres', label: 'Nombres', minWidth: 100 },
    {id: 'apellidos',label: 'Apellidos',minWidth: 170},
    {id: 'usuarioId',label: 'Usuario ID',minWidth: 170},
    {id: 'rolId',label: 'Rol ID',minWidth: 170},
    {id: 'rol',label: 'Rol',minWidth: 170},
    {id: 'actions', label: 'Acciones', minWidth:170}

];

function createData(usuarioRolId,usuario,nombres,apellidos,usuarioId,rolId,rol) {
  return { usuarioRolId,usuario,nombres,apellidos,usuarioId,rolId,rol };
}

const rows = [
  createData(1, 'jaflores', "nombre1", 'apellido1',7,5,'Desarrollador'),
  createData(2, 'jaflores', "nombre1", 'apellido1',7,5,'Desarrollador'),
  createData(3, 'jaflores', "nombre1", 'apellido1',7,5,'Desarrollador'),
];

const useStyles = makeStyles({
  root: {
    width: '100%',
  },
  container: {
    maxHeight: 440,
  },
});



export default function UsuarioRolTable() {
  const classes = useStyles();
  const dispatch = useDispatch();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const [usuariosRol,setUsuariosRol] = useState([]);
  const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  const getUsuariosRol=()=>{
    const headerData={usuarioLogin:userName};
    requestGet('Usuario/GetUsuariosRol',headerData,dispatch).then((res)=>{ 
      if(res.code === 0){                 
        setUsuariosRol(res.data);
      }
    })   
  }

  useEffect(()=>{
    getUsuariosRol();
   },[]);

  return (
    <Paper className={classes.root}>
      <TableContainer className={classes.container}>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              {columns.map((column) => (
                <TableCell
                  key={column.id}
                  align={column.align}
                  style={{ minWidth: column.minWidth }}
                >
                  {column.label}
                </TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row) => {
              return (
                <TableRow hover role="checkbox" tabIndex={-1} key={row.usuarioRolId}>
                  {columns.map((column) => {
                    const value = row[column.id];
                    return (
                        column.id != 'actions'?
                      <TableCell key={column.id} align={column.align}>
                        {value}
                      </TableCell>
                      :
                      <TableCell key={column.id} align={column.align}>
                        <>
                        <Button
                                type="submit"
                                // fullWidth
                                variant="contained"
                                color="primary"
                                // onClick = {onHandleClose}                            
                           >
                            Editar
                        </Button>  
                        <Button
                                type="submit"
                                // fullWidth
                                variant="contained"
                                color="secondary"
                                // onClick = {onHandleClose}                            
                           >
                            Eliminar
                        </Button>  
                        </>
                      </TableCell>
                    );
                  })
                  
                  }
                    
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
      {/* <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        count={rows.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      /> */}
    </Paper>
  );
}
