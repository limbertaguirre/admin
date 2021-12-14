/* eslint-disable react-hooks/exhaustive-deps */
import React, {useEffect, useState  } from 'react';
import { makeStyles, Table, Grid, TableContainer, Button , TableBody , TableCell, TableHead, TableRow, FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";
import readXlsxFile from 'read-excel-file'
import { requestPost ,requestGet } from '../../service/request';
import {useDispatch, useSelector} from 'react-redux';
import * as ActionMensaje from "../../redux/actions/messageAction";
import ComboTipoIncentivoPago from './ComboTipoIncentivo';
import ModalCargarPlanilla from './MensajeModalCargarPlanilla';
const useStyles = makeStyles((theme) => ({
  errorRow: {
    background: 'darksalmon'
  },
  normalRow:{
    background: 'white'
  }
}));
const PagoIncentivo = (props)=> {
  const style = useStyles()
  const dispatch = useDispatch()
  const {userName, idUsuario} = useSelector((stateSelector)=>{ return stateSelector.load});
  const ESTADO_ERROR_PLANILLA = 1;
  const changeInputFile = (event) => {
    console.log(event.target.files)
    const schema = {
      'N°': {
        prop: 'nro',
        type: Number,
        required: true
      },
      'Usuario': {
        prop: 'usuario',
        type: String,
        required: true
      },
      'Empresa': {
        prop: 'empresa',
        type: String,
        required: true
      },
      'Id_Empresa': {
        prop: 'idEmpresa',
        type: Number,
        required: true
      },
      'Nombre_cliente': {
        prop: 'nombreCliente',
        type: String,
        required: true
      },
      'CI_Cliente': {
        prop: 'ciCliente',
        type: String,
        required: true
      },
      'Cuenta_Sion_Pay': {
        prop: 'cuentaSionPay',
        type: String,
        required: true
      },
      'Monto en $us': {
        prop: 'monto',
        type: (value) => {
          if(isNaN(parseFloat(value))){
            throw new Error('El monto tiene que ser de tipo numero')
          }
          if(parseFloat(value)<=0){
            throw new Error('El monto tiene que ser mayor a 0')
          }
          return value
        },
        required: true
      },
      'CIUDAD': {
        prop: 'ciudad',
        type: String,
        required: true
      },
      'PAIS': {
        prop: 'pais',
        type: String,
        required: true
      },
      'DETALLE': {
        prop: 'detalle',
        type: String,
        required: true
      }
    }
    readXlsxFile(event.target.files[0], {
      schema,
      transformData(data) {
        return data.filter(row => row.filter(column => column !== null).length > 0)
      }
    })
    .then(({rows, errors})=>{
      console.table(rows)
      console.log(errors)
      if(errors.length > 0){
        alert('Ocurrio un problema al cargar la planilla, verifique los datos')
      }else{
        setDatosExcel(rows.map( (item)=>{
          return { ...item, idTipoIncentivoPago : '', estadoFila : 0}
        }))
      }
      console.log('cambio')
    })
  }
  const verificarTipoIncentivoSeleccionado = (datosClientes) =>{
    let filasObservadas = []
    for (let i = 0; i < datosClientes.length; i+=1) {
      if (datosExcel[i].idTipoIncentivoPago == '')
        filasObservadas.push(i+1)
    }
    console.log(filasObservadas)
    return filasObservadas.length === 0
  }
  const cargarPlanilla = () => {
    let data = {
      DatosClientes: datosExcel,
      IdCiclo : ciclo ,
      UsuarioNombre : userName
    }
    let newDatosExcel = [...datosExcel]
    newDatosExcel[1].estadoFila = 1
    setDatosExcel(newDatosExcel)

    if(ciclo == '' ){
      setEstadoCargarPlanilla(ESTADO_ERROR_PLANILLA)
      setModalCargarPlanilla(true)
      setMensajeErrorModal("No tiene seleccionado un ciclo")
    }
    if (verificarTipoIncentivoSeleccionado(datosExcel)){
      requestPost('IncentivoSionPay/CargarPlanillaExcel',data, dispatch)
      .then((response)=>{
        console.log(response)
        setEstadoCargarPlanilla(response.code)
        setModalCargarPlanilla(true)
      })
      .catch((error)=>{
        console.log(error)
      })
    }else{
      setEstadoCargarPlanilla(ESTADO_ERROR_PLANILLA)
      setModalCargarPlanilla(true)
      setMensajeErrorModal("Seleccione un tipo de incentivo en cada fila")
    }
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
  const [ mensajeErrorModal , setMensajeErrorModal] = useState("")
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
  const handleChangeTipoIncentivo = (event,index) =>{
    let newDatosExcel = [...datosExcel]
    newDatosExcel[index].idTipoIncentivoPago = event.target.value
    setDatosExcel(newDatosExcel)
    console.table(datosExcel)
  }
  const onClickInputFile = (event) =>{
    event.target.value = null
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
          <input type="file" id='contained-button-file' onClick={ onClickInputFile } accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" onChange={ changeInputFile} style={{ display: 'none'}}/>
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
              <TableCell align="left">Empresa</TableCell>
              <TableCell align="left"> Id Empresa </TableCell>
              <TableCell align="left"> Nombre cliente</TableCell>
              <TableCell align="left"> CI cliente</TableCell>
              <TableCell align="left"> Cuenta SionPay</TableCell>
              <TableCell align="left"> Monto ($us)</TableCell>
              <TableCell align="left"> Ciudad </TableCell>
              <TableCell align="left"> Pais </TableCell>
              <TableCell align="left"> Detalle </TableCell>
              <TableCell align="left"> Tipo incentivo </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {datosExcel.map((row,index) => (
              <TableRow
                key={index}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                className={ row.estadoFila === 0 ? style.normalRow : style.errorRow }
              >
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
                  <ComboTipoIncentivoPago
                    listaIncentivo = {listaTipoIncentivo}
                    handleChangeTipoIncentivo={ (e)=> handleChangeTipoIncentivo (e,index) }
                    valorTipoIncentivo={ row.idTipoIncentivoPago }
                  />
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
        mensaje = { estadoCargarPlanilla === 0 ? 'Planilla cargada correctamente' : 'Ocurrio un problema al cargar la planilla : ' + mensajeErrorModal } 
        handleCloseConfirm = { cerrarModal}
      />
    </>
  )

}

export default PagoIncentivo;