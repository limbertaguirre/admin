/* eslint-disable react-hooks/exhaustive-deps */
import React, {useEffect, useState  } from 'react';
import { makeStyles, Link, Table, Grid, TableContainer, Button , TableBody , TableCell, TableHead, TableRow, FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";
import readXlsxFile from 'read-excel-file'
import { requestPost ,requestGet } from '../../../service/request';
import {useDispatch, useSelector} from 'react-redux';
import * as ActionMensaje from "../../../redux/actions/messageAction";
import ComboTipoIncentivoPago from './ComboTipoIncentivo';
import ComboTipoPago from './ComboTipoPago';
import ModalCargarPlanilla from './MensajeModalCargarPlanilla';
import ModalTipoIncentivo from './ModalTipoIncentivo';
import CloudUploadOutlinedIcon from '@material-ui/icons/CloudUploadOutlined';
import CloudDownloadOutlinedIcon from '@material-ui/icons/CloudDownloadOutlined';
import AddCircleOutlineOutlinedIcon from '@material-ui/icons/AddCircleOutlineOutlined';
const useStyles = makeStyles((theme) => ({
  errorRow: {
    background: 'darksalmon'
  },
  normalRow:{
    background: 'white'
  },
  botonVerde:{
    background: '#197e30',
    color: 'white',
    "&:hover": {
      background: '#197e30',
      color: 'white',
    }
  }
}));
const CargarPlanilla = ()=> {
  const style = useStyles()
  const dispatch = useDispatch()
  const {userName} = useSelector((stateSelector)=>{ return stateSelector.load});
  const ESTADO_ERROR_PLANILLA = 1;
  const FILA_OBSERVADA = 1;
  const ID_TIPO_PAGO_SION_PAY = 1;
  const changeInputFile = (event) => {
    const schema = {
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
          return { ...item, idTipoIncentivoPago : '', estadoFila : 0 , idTipoPago: ''}
        }))
      }
    })
  }
  const verificarTipoIncentivoSeleccionado = (datosClientes) =>{
    let filasObservadas = []
    for (let i = 0; i < datosClientes.length; i+=1) {
      if (datosExcel[i].idTipoIncentivoPago == '')
        filasObservadas.push(i+1)
    }
    return filasObservadas.length === 0
  }
  const cargarPlanilla = () => {
    let data = {
      DatosClientes: datosExcel,
      IdCiclo : ciclo ,
      UsuarioNombre : userName
    }
    if(ciclo == '' ){
      setEstadoCargarPlanilla(ESTADO_ERROR_PLANILLA)
      setModalCargarPlanilla(true)
      setMensajeErrorModal("No tiene seleccionado un ciclo")
      return
    }
    let newDatosExcel = [...datosExcel]
    newDatosExcel.map((item)=>{
      item.estadoFila = 0
    })
    setDatosExcel(newDatosExcel)
    if (verificarTipoIncentivoSeleccionado(datosExcel)){
      requestPost('IncentivoSionPay/CargarPlanillaExcel',data, dispatch)
      .then((response)=>{
        setEstadoCargarPlanilla(response.code)
        setModalCargarPlanilla(true)
        if(response.code === 1 ){
          setMensajeErrorModal(response.message)
            response.data.map((item,index) =>{
              if(item.observada === true){
                let newDatosExcel = [...datosExcel]
                newDatosExcel[index].estadoFila = FILA_OBSERVADA
                setDatosExcel(newDatosExcel)
              }else{
                let newDatosExcel = [...datosExcel]
                newDatosExcel[index].estadoFila = 0
                setDatosExcel(newDatosExcel)
              }
            })
        }
      })
      .catch((error)=>{
        setEstadoCargarPlanilla(ESTADO_ERROR_PLANILLA)
        setModalCargarPlanilla(true)
        setMensajeErrorModal(error)
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
    obtenerTipoPago();
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
  const [ listaTipoIncentivo , setListaTipoIncentivo ] = useState([])
  const [ listaTipoPago , setListaTipoPago ] = useState()
  const [ modalCargarPlanilla, setModalCargarPlanilla] = useState(false)
  const [ estadoCargarPlanilla, setEstadoCargarPlanilla] = useState(0)
  const [ mensajeErrorModal , setMensajeErrorModal] = useState("")
  const [ openTipoIncentivoModal , setOpenTipoIncentivoModal] = useState(false)
  const [ valorTipoIncentivoTodos , setValorIncentivoTodos] = useState("")
  const [ valorTipoPagoTodos , setValorPagoTodos] = useState("")
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
  const obtenerTipoPago = () => {
    const data={usuarioLogin:userName};
    requestGet('IncentivoSionPay/ObtenerTipoPagos',data,dispatch).then((res)=>{
      if(res.code === 0){
        setListaTipoPago(res.data);
      }else{
        setListaTipoPago([]);
        dispatch(ActionMensaje.showMessage({ message: res.message, variant: "info" }));
      }
    })
  }
  const cerrarModal = () => {
    setModalCargarPlanilla(false)
  }
  const handleChangeTipoIncentivo = (event,index, cambiarTodos) =>{
    let newDatosExcel = [...datosExcel]
    if(cambiarTodos === true){
      setValorIncentivoTodos(event.target.value)
      newDatosExcel.map((item)=>{
        item.idTipoIncentivoPago = event.target.value
      })
      setDatosExcel(newDatosExcel)
    }else{
      newDatosExcel[index].idTipoIncentivoPago = event.target.value
      setDatosExcel(newDatosExcel)
    }
  }
  const handleChangeTipoPago = (event, index, cambiarTodos) =>{
    let newDatosExcel = [...datosExcel]
    if(cambiarTodos === true){
      setValorPagoTodos(event.target.value)
      newDatosExcel.map((item)=>{
        item.idTipoPago = event.target.value
      })
      setDatosExcel(newDatosExcel)
    }else{
      newDatosExcel[index].idTipoPago = event.target.value
      setDatosExcel(newDatosExcel)
    }
  }
  const onClickInputFile = (event) =>{
    event.target.value = null
  }
  const registrarTipoIncentivo = ()=>{
    setOpenTipoIncentivoModal(true)
  }
  const clickBotonCancelar = () =>{
    setOpenTipoIncentivoModal(false)
  }
  const clickBotonAceptar = () =>{
    if(descripcionTipoIncentivo === ""){
      alert("Debe ingresar una descripcion")
      return
    }
    let data={
      Descripcion : descripcionTipoIncentivo,
      Usuario : userName
    }
    requestPost('IncentivoSionPay/RegistroTipoIncentivoPago',data,dispatch)
    .then((response)=>{
      if(response.code === 0){
        obtenerTipoIncentivoPago()
      }else{
        dispatch(ActionMensaje.showMessage({ message: response.message, variant: "info" }));
      }
    })
  }
  const [ descripcionTipoIncentivo, setDescripcionTipoIncentivo] = useState("")


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
        <Grid item xs={3}>
          <input type="file" id='contained-button-file' onClick={ onClickInputFile } accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" onChange={ changeInputFile} style={{ display: 'none'}}/>
          <label htmlFor="contained-button-file">
            <Button variant="contained" component="span" endIcon={<CloudUploadOutlinedIcon/>} className={ style.botonVerde}>
              Cargar archivo Excel
            </Button>
          </label>
        </Grid>
        <Grid item xs={3}>
          <Link href="/FormatoPlanillaIncentivoSionPay.xlsx" target="_blank" rel="noopener" download variant="body2">
            <Button variant="contained" component="span" color="primary" endIcon={<CloudDownloadOutlinedIcon/>}>
              Descargar formato planilla Excel
            </Button>
          </Link>
        </Grid>
        <Grid item xs={6}>
          <Button variant="contained" component="span" onClick={ registrarTipoIncentivo } endIcon={<AddCircleOutlineOutlinedIcon/>} color="primary" >
            Registrar tipo incentivo
          </Button>
        </Grid>
    </Grid>
      { datosExcel && (
        <TableContainer >
        <Table aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell align="left"></TableCell>
              <TableCell align="left"></TableCell>
              <TableCell align="left"> </TableCell>
              <TableCell align="left"> </TableCell>
              <TableCell align="left"> </TableCell>
              <TableCell align="left">  </TableCell>
              <TableCell align="left">  </TableCell>
              <TableCell align="left">  </TableCell>
              <TableCell align="left">  </TableCell>
              <TableCell align="left" style={{minWidth: '230px'}}>
                <ComboTipoPago
                  listaTipoPago = {listaTipoPago}
                  handleChangeTipoPago={ (e)=> handleChangeTipoPago (e,1,true) }
                  valorTipoPago = { valorTipoPagoTodos === '' ? ID_TIPO_PAGO_SION_PAY : valorTipoPagoTodos  }
                  labelTipoPago = { 'Aplicar tipo de pago en todas las filas'}
                />
              </TableCell>
              <TableCell align="left" style={{minWidth: '300px'}}>
                <ComboTipoIncentivoPago
                    listaIncentivo = {listaTipoIncentivo}
                    handleChangeTipoIncentivo={ (e)=> handleChangeTipoIncentivo (e,1,true) }
                    valorTipoIncentivo={valorTipoIncentivoTodos}
                    labelTipoIncentivoPago={'Aplicar incentivo en todas las filas'}
                />
              </TableCell>
            </TableRow>
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
              <TableCell align="left"> Tipo Pago </TableCell>
              <TableCell align="left"> Tipo incentivo</TableCell>
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
                <TableCell align="right">{row.idEmpresa}</TableCell>
                <TableCell align="left">{row.nombreCliente}</TableCell>
                <TableCell align="left">{row.ciCliente}</TableCell>
                <TableCell align="left">{row.cuentaSionPay}</TableCell>
                <TableCell align="right">{row.monto}</TableCell>
                <TableCell align="left">{row.ciudad}</TableCell>
                <TableCell align="left">{row.pais}</TableCell>
                <TableCell align="left">{row.detalle}</TableCell>
                <TableCell>
                <ComboTipoPago
                    listaTipoPago = {listaTipoPago}
                    handleChangeTipoPago={ (e)=> handleChangeTipoPago (e,index, false) }
                    valorTipoPago ={row.idTipoPago === '' ? ID_TIPO_PAGO_SION_PAY : row.idTipoPago  }
                    labelTipoPago = {'Elige el tipo de pago'}
                  />
                </TableCell>
                <TableCell>
                  <ComboTipoIncentivoPago
                    listaIncentivo = {listaTipoIncentivo}
                    handleChangeTipoIncentivo={ (e)=> handleChangeTipoIncentivo (e,index,false) }
                    valorTipoIncentivo={ row.idTipoIncentivoPago }
                    labelTipoIncentivoPago={'Elige el incentivo'}
                  />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      )}
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Button
            variant="contained"
            color="primary"
            disabled={ datosExcel ? false : true}
            onClick={ cargarPlanilla }> Cargar Datos</Button>
        </Grid>
      </Grid>
      <ModalCargarPlanilla
        open = { modalCargarPlanilla }
        titulo = { 'Cargar Planilla Mensaje'}
        tipoModal = { estadoCargarPlanilla === 0 ? 'success' : 'warning' }
        mensaje = { estadoCargarPlanilla === 0 ? 'Planilla cargada correctamente' : 'Ocurrio un problema al cargar la planilla : ' + mensajeErrorModal } 
        handleCloseConfirm = { cerrarModal}
      />
      <ModalTipoIncentivo
        open={ openTipoIncentivoModal}
        clickBotonCancelar={ clickBotonCancelar }
        clickBotonAceptar={ clickBotonAceptar }
        listaTipoIncentivo = { listaTipoIncentivo}
        pasarDescripcion = { setDescripcionTipoIncentivo}
      />
    </>
  )

}

export default CargarPlanilla;