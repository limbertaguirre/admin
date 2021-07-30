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
import { requestPost, requestGet } from '../../../service/request';
import Button from '@material-ui/core/Button';
import * as permiso from '../../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../../lib/accesosPerfiles';
import {useSelector, useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import * as ActionMessage from '../../../redux/actions/messageAction';
import MessageConfirm from '../../../components/mesageModal/MessageConfirm';


const useStyles = makeStyles({
  // root: {
  //   width: '100%',
  // },
  // container: {
  //   maxHeight: 440,
  // },
});
const columns = [
  {id: 'usuario', label: 'Usuario', minWidth: 170 },
  {id: 'nombres', label: 'Nombres', minWidth: 100 },
  {id: 'apellidos',label: 'Apellidos',minWidth: 170},
  {id: 'rol',label: 'Rol',minWidth: 170},
  {id: 'actions', label: 'Acciones', minWidth:170}

];
const deleteConfirmMessage='Estas seguro eliminar el rol para este usuario?'


export default function UsuariosRolesListView(props) {
  const {handleEditOperation,usuariosRol,handleData} = props;
  const classes = useStyles();
  const dispatch = useDispatch();
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [page, setPage] = useState(0);
  const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
  const [modalConfirmOpen, setModalConfirmOpen] =useState(false);
  const [idUsuarioRolSelected,setIdUsuarioRolSelected] = useState(0);

  const onChangePage =()=>{

  }

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

   const handleDeleteUsuarioRolOperation=(idUsuarioRol)=>{
     setModalConfirmOpen(true);
     setIdUsuarioRolSelected(idUsuarioRol);
   }

   const handleDeleteUsuarioRolCancel=()=>{
    setIdUsuarioRolSelected(0);
    setModalConfirmOpen(false);
   }

   const handleDeleteUsuarioRolConfirm = ()=>{
    const body ={
      usuarioRolId:idUsuarioRolSelected,
      operationUserId:idUsuario,
      operationLogin: userName
    }
     requestPost('Usuario/DeleteUsuarioRol',body,dispatch)
      .then((res)=>
      { 
        if(res.code === 0){

            handleData();
            dispatch(ActionMessage.showMessage({ message: 'Operación completada exitosamente', variant: "success" }));
        }
        else{
          dispatch(ActionMessage.showMessage({ message: res.message, variant: "error" }));
        }
      });
      
      setModalConfirmOpen(false);
   }

  return (
    <>
    <Paper>
      <TableContainer>
        <Table stickyHeader aria-label="sticky table" size="small">
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
            {(rowsPerPage > 0
            ? usuariosRol.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
            : usuariosRol
          ).map((row) => {
              return (
                <TableRow hover key={row.usuarioRolId}>
                  <TableCell>{row.usuario}</TableCell>
                  <TableCell>{row.nombres}</TableCell>
                  <TableCell>{row.apellidos}</TableCell>
                  <TableCell>{row.rol}</TableCell>
                  <TableCell>
                        <>
                        <Tooltip title="Editar" aria-label="editar">
                        <IconButton  
                                // fullWidth
                                variant="contained"
                                color="primary"
                                onClick = {(e)=>{ handleEditOperation(row.usuarioId, row.rolId)}}                            
                              >
                            <EditIcon />
                        </IconButton >  
                        </Tooltip>
                        <Tooltip title="Eliminar" aria-label="eliminar">
                        <IconButton 
                                // fullWidth
                                variant="contained"
                                color="secondary"
                                onClick = {()=>{handleDeleteUsuarioRolOperation(row.usuarioRolId)}}                            
                           >
                            <DeleteIcon/>
                        </IconButton >  
                        </Tooltip>
                        </>
                      </TableCell>
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
       <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        count={usuariosRol.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onChangePage={handleChangePage}
        onChangeRowsPerPage={handleChangeRowsPerPage}
      />
    </Paper>
     <MessageConfirm 
      open={modalConfirmOpen} 
      titulo={'Confirmación'} 
      tipoModal={"info"} 
      mensaje={deleteConfirmMessage} 
      handleCloseConfirm={handleDeleteUsuarioRolConfirm} 
      handleCloseCancel={handleDeleteUsuarioRolCancel}  />
     </>
  );
}
