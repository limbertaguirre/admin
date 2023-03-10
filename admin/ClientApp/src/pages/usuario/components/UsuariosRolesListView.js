import React,{ useState} from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import { requestPost } from '../../../service/request';
import {useSelector, useDispatch} from 'react-redux';
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
const deleteConfirmMessage =(u, r)=>`¿Está seguro de eliminar el rol de "${r}" para el usuario "${u}"?`;


export default function UsuariosRolesListView(props) {
  const {handleEditOperation,usuariosRol,handleData, Actualizar, Eliminar} = props;
  const classes = useStyles();
  const dispatch = useDispatch();
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [page, setPage] = useState(0);
  const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
  const [modalConfirmOpen, setModalConfirmOpen] =useState(false);
  const [idUsuarioRolSelected,setIdUsuarioRolSelected] = useState(0);
  const [rowSelected, setRowSelected] = useState({}); 

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

   const handleDeleteUsuarioRolOperation=(row)=>{
     setModalConfirmOpen(true);
     setIdUsuarioRolSelected(row.usuarioRolId);
     setRowSelected(row);
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
                        {Actualizar ?
                        <Tooltip title="Editar" aria-label="editar">
                          <IconButton  
                                variant="contained"
                                color="primary"
                                onClick = {(e)=>{ handleEditOperation(row.usuarioId, row.rolId)}}                            
                              >
                            <EditIcon />
                        </IconButton >  
                        </Tooltip>
                        :  
                         <Tooltip title="Sin permiso para editar" >
                            <IconButton     variant="contained"    >
                              <EditIcon />
                            </IconButton > 
                          </Tooltip> 
                        }

                        {Eliminar? 
                        <Tooltip title="Eliminar" >
                        <IconButton 
                                variant="contained"
                                color="secondary"
                                onClick = {()=>{handleDeleteUsuarioRolOperation(row)}}                            
                           >
                            <DeleteIcon/>
                        </IconButton >  
                        </Tooltip>
                        :
                          <Tooltip title="Sin permiso para eliminar" >
                            <IconButton variant="contained" color={"action"}>
                                <DeleteIcon/>
                            </IconButton >  
                          </Tooltip>
                        }
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
      titulo={'Mensaje de confirmación'} 
      tipoModal={"error"} 
      mensaje={deleteConfirmMessage(rowSelected.usuario, rowSelected.rol)} 
      handleCloseConfirm={handleDeleteUsuarioRolConfirm} 
      handleCloseCancel={handleDeleteUsuarioRolCancel}  />
     </>
  );
}
