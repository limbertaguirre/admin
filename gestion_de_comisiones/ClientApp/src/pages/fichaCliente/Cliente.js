import React, {useEffect, useState}  from 'react';
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';

import * as permiso from '../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import * as ActionCliente from '../../redux/actions/clienteAction';

import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TablePagination from '@material-ui/core/TablePagination';
import Paper from '@material-ui/core/Paper';

import CancelIcon from '@material-ui/icons/Cancel';
import CheckCircleIcon from '@material-ui/icons/CheckCircle';
import { green, red } from '@material-ui/core/colors';
import SearchIcon from '@material-ui/icons/Search';
import { TextField, Typography, InputAdornment, Grid, Button } from "@material-ui/core";

const StyledBreadcrumb = withStyles((theme) => ({
    root: {
      backgroundColor: theme.palette.grey[100],
      height: theme.spacing(3),
      color: theme.palette.grey[800],
      fontWeight: theme.typography.fontWeightRegular,
      '&:hover, &:focus': {
        backgroundColor: theme.palette.grey[300],
      },
      '&:active': {
        boxShadow: theme.shadows[1],
        backgroundColor: emphasize(theme.palette.grey[300], 0.12),
      },
    },
  }))(Chip); 

  const useStyles = makeStyles((theme) => ({
    table: {
      minWidth: 650,
    },
    submit: {
      //  width:'30px',
        height:'25px',
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white'
    },
    contentButton: {
        textAlign: 'center',
    },
    TextFielBusqueda:{
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
        width: '100%',
    },
    submitBusqueda: {                 
          background: "#1872b8", 
          boxShadow: '2px 4px 5px #1872b8',
          color:'white',
          
      },
      containerBusqueda:{
        paddingLeft:theme.spacing(1),
        paddingTop:theme.spacing(2),
      }

  }));

 const Cliente =(props)=> {
    let history = useHistory();
    const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
    useEffect(()=>{  try{  
       verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
       }catch (err) {  verificarAcceso(perfiles, 'none', history); }
    },[])
    const styles = useStyles();
    const dispatch = useDispatch();
    const [rowsPerPage, setRowsPerPage] = useState(30);
    const [page, setPage] = useState(0);
    const {listClientes} = useSelector((stateSelector) =>{ return stateSelector.cliente});  
    const [txtBusqueda, setTxtBusqueda] = useState("");
    const [txtBusquedaError, setTxtBusquedaError] = useState(false);

    useEffect(()=>{ 
       dispatch(ActionCliente.obtenerClientes());
     },[])

     const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };    
    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };
    const selecionarCliente=(idcliente)=>{
       console.log('click', idcliente);
       
       /*  const location = {
          pathname: '/cliente/ver/ficha',
          state: {
              namePagina: props.location.state.namePagina,
              idCliente: idcliente
            }
        } */
        history.push("/cliente/ficha");
    };

    const _onChangeregistro= (e) => {
        const texfiel = e.target.name;
        const value = e.target.value;
        if (texfiel === "txtBusqueda") {
          setTxtBusqueda(value);
          console.log(value);
        }

    };
    const BuscarCliente=()=>{
        if(txtBusqueda === ""){
            dispatch(ActionCliente.obtenerClientes());
        }else{
          dispatch(ActionCliente.buscarClientesXnombre(txtBusqueda));
        }
    }

     
    return (
      <>
          <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}>
              <Breadcrumbs aria-label="breadcrumb">                    
                        <StyledBreadcrumb key={2} component="a" label="Cliente" icon={<HomeIcon fontSize="small" />} />                        
              </Breadcrumbs>
           </div>           
           <br/>
           <br/>
                <Grid container xs={12}> 
                <Grid item xs={12} md={10}  className={styles.contentTitle} >
                    <TextField
                    label="Buscar Clientes"
                    type={'text'}
                    variant="outlined"
                    name="txtBusqueda"                    
                    value={txtBusqueda}
                    onChange={_onChangeregistro}
                    className={styles.TextFielBusqueda}
                    error={txtBusquedaError}
                    helperText={ txtBusquedaError &&
                    "El campo es requerido"
                    }
                    InputProps={{
                        startAdornment: (
                        <InputAdornment position="start">
                            <SearchIcon />
                        </InputAdornment>
                        ),
                    }}                    
                  />      
                </Grid>
                <Grid item  xs={12} md={2} className={styles.containerBusqueda}  >
                        <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={styles.submitBusqueda}
                        onClick = {()=> BuscarCliente()}                                         
                        >
                         BUSCAR {' '} <SearchIcon />
                        </Button>   
                </Grid>
                </Grid>
            <br />
            <div>
               
                <TableContainer component={Paper}>
                    <Table className={styles.table} size="small" aria-label="a dense table">
                        <TableHead>
                        <TableRow>
                            <TableCell align="center">ID</TableCell>
                            <TableCell align="right">Nombre completo</TableCell>
                            <TableCell align="right">Cedula identidad</TableCell>
                            <TableCell align="right">Nro Cuenta</TableCell>
                            <TableCell align="right">Banco</TableCell>
                            <TableCell align="center">Estado cliente</TableCell>
                            <TableCell align="right">   </TableCell>
                        </TableRow>
                        </TableHead>
                        <TableBody>
                        {listClientes.map((row) => (
                            <TableRow key={row.idFicha  }>
                            <TableCell align="center"scope="row"> {row.idFicha} </TableCell>
                            <TableCell align="center">{row.nombreCompleto}</TableCell>
                            <TableCell align="right">{row.ci}</TableCell>
                            <TableCell align="right">{row.cuentaBancaria}</TableCell>
                            <TableCell align="right">{row.nombreBanco}</TableCell>   
                            <TableCell align="center">
                                {row.estado === 1? <CheckCircleIcon  style={{ color: green[500], fontSize: 30 }} />
                                : <CancelIcon  style={{ color: red[500],fontSize: 30 }} />
                                 }                               
                            </TableCell>   
                            <TableCell align="center">
                                         <Button
                                            type="submit"
                                            fullWidth
                                            variant="contained"
                                            color="primary"
                                            className={styles.submit}
                                            onClick = {()=> selecionarCliente(`${row.idFicha}`)}                                         
                                        >
                                            Ver ficha
                                        </Button>   
                            </TableCell>   
                            </TableRow>
                        ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <TablePagination
                    rowsPerPageOptions={[30, 60, 75]}
                    component="div"
                    count={listClientes.length}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onChangePage={handleChangePage}
                    onChangeRowsPerPage={handleChangeRowsPerPage}
                    />
           </div>

      </>
    );

}

export default Cliente;