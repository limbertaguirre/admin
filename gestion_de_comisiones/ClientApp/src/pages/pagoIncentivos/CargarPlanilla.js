/* eslint-disable react-hooks/exhaustive-deps */
import React, {useEffect, useState  } from 'react';
import { Table, Grid, TableContainer, Button , TableBody , TableCell, TableHead, TableRow, FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";
import readXlsxFile from 'read-excel-file'
import { requestPost ,requestGet } from '../../service/request';
import {useDispatch, useSelector} from 'react-redux';
import * as ActionMensaje from "../../redux/actions/messageAction";
import ComboTipoIncentivoPago from './ComboTipoIncentivo';
import ModalCargarPlanilla from './MensajeModalCargarPlanilla';
const PagoIncentivo = (props)=> {
  const dispatch = useDispatch()
  const {userName, idUsuario} = useSelector((stateSelector)=>{ return stateSelector.load});
  const changeInputFile = (event) => {
    console.log(event.target.files)
    const schema = {
      'N°': {
        prop: 'nro',
        type: Number
      },
      'Usuario': {
        prop: 'usuario',
        type: String
      },
      'Empresa': {
        prop: 'empresa',
        type: String
      },
      'Id_Empresa': {
        prop: 'idEmpresa',
        type: Number
      },
      'Nombre_cliente': {
        prop: 'nombreCliente',
        type: String
      },
      'CI_Cliente': {
        prop: 'ciCliente',
        type: String
      },
      'Cuenta_Sion_Pay': {
        prop: 'cuentaSionPay',
        type: String
      },
      'Monto en $us': {
        prop: 'monto',
        type: Number
      },
      'CIUDAD': {
        prop: 'ciudad',
        type: String
      },
      'PAIS': {
        prop: 'pais',
        type: String
      },
      'DETALLE': {
        prop: 'detalle',
        type: String
      }
    }
    readXlsxFile(event.target.files[0], {
      schema,
      transformData(data) {
        return data.filter(row => row.filter(column => column !== null).length > 0)
      }
    })
    .then(({rows, errors})=>{
      setDatosExcel(rows)
      console.table(rows)
      console.log(errors)
    })
  }

  const cargarPlanilla = () => {
    let data = {
      DatosClientes: datosExcel,
      IdCiclo : 1 ,
      UsuarioNombre : 'srios'
    }
    requestPost('IncentivoSionPay/CargarPlanillaExcel',data, dispatch)
      .then((response)=>{
        setEstadoCargarPlanilla(response.code)
        setModalCargarPlanilla(true)
      })
      .catch((error)=>{
        console.log(error)
    })
  }
  useEffect(()=>{
    obtenerCiclos();
    obtenerTipoIncentivoPago();
  },[])
  const obtenerCiclos = () => {
    const data={usuarioLogin:userName};
      requestGet('IncentivoSionPay/ObtenerCiclos',data,dispatch).then((res)=>{
        if(res.code === 0){
          setListaCiclo(res.data);
        }else{
            setListaCiclo([]);
            dispatch(ActionMensaje.showMessage({ message: res.message, variant: "info" }));
        }
      })
  }
  const [datosExcel, setDatosExcel] = useState()
  const [ ciclo , setCiclo ] = useState('')
  const [ listaCiclo , setListaCiclo ] = useState()
  const [ listaTipoIncentivo , setListaTipoIncentivo ] = useState()
  const [ modalCargarPlanilla, setModalCargarPlanilla] = useState(false)
  const [ estadoCargarPlanilla, setEstadoCargarPlanilla] = useState(0)
  const handleChangeCiclo = (event) =>{
    setCiclo(event.target.value);
  }

  const obtenerTipoIncentivoPago = () => {
    const data={usuarioLogin:userName};
    requestGet('IncentivoSionPay/ObtenerTipoIncentivo',data,dispatch).then((res)=>{
      if(res.code === 0){
        setListaTipoIncentivo(res.data);
      }else{
          setListaCiclo([]);
          dispatch(ActionMensaje.showMessage({ message: res.message, variant: "info" }));
      }
    })
  }
  const cerrarModal = () => {
    setModalCargarPlanilla(false)
  }
  return(
    <>
    <Grid container spacing={3}>
        <Grid item xs={12}>
          <FormControl fullWidth variant="outlined">
            <InputLabel id="demo-simple-select-label">Seleccione el ciclo</InputLabel>
            <Select
              labelId="demo-simple-select-label"
              id="demo-simple-select"
              value={ciclo}
              label="Seleccione el ciclo"
              onChange={handleChangeCiclo}
            >
              { listaCiclo &&
                listaCiclo.map((ciclo,index)=>(
                  <MenuItem key={index} value={ciclo.idCiclo}>{ciclo.nombre}</MenuItem>
                  )
                )
              }
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <input type="file" id='contained-button-file'  accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" onChange={ changeInputFile} style={{ display: 'none'}}/>
          <label htmlFor="contained-button-file">
            <Button variant="contained" component="span">
              Cargar archivo Excel
            </Button>
          </label>
        </Grid>
    </Grid>
      { datosExcel && (
        <TableContainer >
        <Table aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell> Nro</TableCell>
              <TableCell align="left"> Usuario </TableCell>
              <TableCell align="left">Empresa</TableCell>
              <TableCell align="left"> Id Empresa </TableCell>
              <TableCell align="left"> Nombre cliente</TableCell>
              <TableCell align="left"> CI cliente</TableCell>
              <TableCell align="left"> Cuenta SionPay</TableCell>
              <TableCell align="left"> Monto ($us)</TableCell>
              <TableCell align="left"> Ciudad </TableCell>
              <TableCell align="left"> Pais </TableCell>
              <TableCell align="left"> Detalle </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {datosExcel.map((row) => (
              <TableRow
                key={row.nro}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell> {row.nro} </TableCell>
                <TableCell> {row.usuario} </TableCell>
                <TableCell align="left">{row.empresa}</TableCell>
                <TableCell align="left">{row.idEmpresa}</TableCell>
                <TableCell align="left">{row.nombreCliente}</TableCell>
                <TableCell align="left">{row.ciCliente}</TableCell>
                <TableCell align="left">{row.cuentaSionPay}</TableCell>
                <TableCell align="left">{row.monto}</TableCell>
                <TableCell align="left">{row.ciudad}</TableCell>
                <TableCell align="left">{row.pais}</TableCell>
                <TableCell align="left">{row.detalle}</TableCell>
                <TableCell>
                  <ComboTipoIncentivoPago listaIncentivo = {listaTipoIncentivo}/>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      )}
      <Button
        variant="contained"
        color="primary"
        disabled={ datosExcel ? false : true}
        onClick={ cargarPlanilla }> Cargar Datos</Button>
      <ModalCargarPlanilla 
        open = { modalCargarPlanilla }
        titulo = { 'Cargar Planilla Mensaje'}
        tipoModal = { estadoCargarPlanilla === 0 ? 'success' : 'warning' }
        mensaje = { estadoCargarPlanilla === 0 ? 'Planilla cargada correctamente' : 'Ocurrio un problema al cargar la planilla'} 
        handleCloseConfirm = { cerrarModal} 
      />
    </>
  )

}

export default PagoIncentivo;