import React, {useEffect, useState  } from 'react';
import { makeStyles, Link, Table, Grid, TableContainer, Button , TableBody , TableCell, TableHead, TableRow, FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";
import { requestPost ,requestGet } from '../../../service/request';
import {useDispatch, useSelector} from 'react-redux';
import * as ActionMensaje from "../../../redux/actions/messageAction";
import TablePagoIncentivo from './TablePagoIncentivo';
import ModalTipoIncentivo from './ModalPagoIncentivo';
import ModalRespuestaPagoIncentivo from './ModalRespuestaPagoIncentivo';

const useStyles = makeStyles((theme) => ({
  errorRow: {
    background: 'darksalmon'
  }
}))

const PagoIncentivo = () =>{

  const [ ciclo, setCiclo ] = useState("")
  const [ incentivo, setIncentivo ] = useState("")
  const [ listaCiclo, setListaCiclo ]= useState([])
  const [ listaTipoIncentivo, setListaTipoIncentivo ]= useState([])
  const [ listaIncentivo, setListaIncentivo ]= useState([])
  const [ montoTotalIncentivo, setMontoTotalIncentivo ] = useState(0)
  const [ nombreCiclo, setNombreCiclo ] = useState("")
  const [ nombreTipoIncentivo, setNombreTipoIncentivo ] = useState("")
  const [ modalOpen, setModalOpen ] = useState(false)
  const [ modalOpenRespuestaPago, setModalOpenRespuestaPago] = useState(false)
  const [ listaIncentivoRespuestaPago, setListaIncentivoRespuestaPago] = useState([])

  const style = useStyles()
  const dispatch = useDispatch()
  const {userName} = useSelector((stateSelector)=>{ return stateSelector.load});
  useEffect(()=>{
    obtenerCiclos()
  } ,[])
  const obtenerCiclos = ()=>{
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
  const obtenerIncentivoPorCiclo = (idCiclo) =>{
    const data={ 
      usuarioLogin:userName , 
      nroCicloMensual: parseInt(idCiclo)
    };
    requestGet('IncentivoSionPay/ObtenerTipoIncentivosSegunCicloMensual',data,dispatch).then((res)=>{
      console.log(res)
      if(res.code === 0){
        setListaTipoIncentivo(res.data);
        res.data.length === 0 && dispatch(ActionMensaje.showMessage({ message: "No se encontraron incentivos en este ciclo", variant: "info" }));
      }else{
        setListaTipoIncentivo([]);
        dispatch(ActionMensaje.showMessage({ message: res.message, variant: "info" }));
      }
    })
  }
  const obtenerListaTipoIncentivosAPagar = (idCiclo, idTipoIncentivo) =>{
    const data = {
      usuarioLogin: userName,
      nroCicloMensual: idCiclo,
      idTipoIncentivo: idTipoIncentivo
    }
    requestGet('IncentivoSionPay/ObtenerIncentivosPagar',data,dispatch).then((res)=>{
      console.table(res.data)
      if(res.code === 0){
        setListaIncentivo(res.data);
        console.table(res.data)
        obtenerMontoTotalIncentivo(res.data)
      }else{
        setListaIncentivo([]);
        dispatch(ActionMensaje.showMessage({ message: res.message, variant: "info" }));
      }
    })
  }
  const obtenerMontoTotalIncentivo = (listaIncentivo) =>{
    let montoTotal = 0
    for (let item of listaIncentivo){
      montoTotal += item.montoTotalNeto
    }
    setMontoTotalIncentivo(montoTotal.toFixed(2))
  }
  const handleChangeCiclo = (event) => {
    setCiclo(event.target.value)
    obtenerNombreCiclo(event.target.value)
    setIncentivo("")
    setListaTipoIncentivo([])
    setListaIncentivo([])
    obtenerIncentivoPorCiclo(event.target.value)
  }
  const obtenerNombreCiclo = (idCiclo)=>{
    for(let item of listaCiclo){
      if(item.idCiclo === idCiclo){
        setNombreCiclo(item.nombre);
        return;
      }
    }
  }
  const handleChangeIncentivo = (event) => {
    setIncentivo(event.target.value)
    obtenerNombreIncentivo(event.target.value)
    obtenerListaTipoIncentivosAPagar(ciclo,event.target.value)
  }
  const obtenerNombreIncentivo = (idTipoIncentivo)=>{
    for(let item of listaTipoIncentivo){
      if(item.idTipoIncentivo === idTipoIncentivo){
        setNombreTipoIncentivo(item.nombre);
        return;
      }
    }
  }
  const modalBotonCancelar = ()=>{
    setModalOpen(false)
  }
  const realizarPago = () => { setModalOpen(true)}
  const confirmarPago = ()=>{
    const data = {
      incentivosPagar : listaIncentivo,
      usuarioLogin : userName
    }
    requestPost('IncentivoSionPay/PagarIncentivos',data,dispatch).then((res)=>{
      if(res.code === 0){
        setListaIncentivoRespuestaPago(res.data)
        setModalOpenRespuestaPago(true)
        
      }
    })
  }
  const clickBotonAceptarModalPago = ()=>{
    setModalOpenRespuestaPago(false)
    setModalOpen(false)
    setListaIncentivo([])
    setListaTipoIncentivo([])
  }
  return (
    <>
    <Grid container spacing={3}>
      <Grid item xs={4}>
        <FormControl fullWidth variant="outlined">
          <InputLabel id="selectCicloLabel">Seleccione el ciclo</InputLabel>
          <Select
              labelId="selectCicloLabel"
              id="selectCiclo"
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
      <Grid item xs={4}>
        <FormControl fullWidth variant="outlined">
          <InputLabel id="selectIncentivoLabel">Seleccione el incentivo</InputLabel>
          <Select
              labelId="selectIncentivoLabel"
              id="selectIncentivo"
              value={incentivo}
              label="Seleccione el incentivo"
              disabled={ listaTipoIncentivo.length > 0 ? false : true}
              onChange={handleChangeIncentivo}
          >
              { listaTipoIncentivo &&
                listaTipoIncentivo.map((incentivo,index)=>(
                    <MenuItem key={index} value={incentivo.idTipoIncentivo}>{incentivo.nombre}</MenuItem>
                    )
                )
              }
          </Select>
        </FormControl>
      </Grid>
      <Grid item xs={12}>
        { listaIncentivo.length > 0 &&
          <TablePagoIncentivo
            listaIncentivo = {listaIncentivo}
          />
        }
      </Grid>
      <Grid item xs={12}>
        <Button
          variant="contained"
          color="primary"
          disabled={ listaIncentivo.length > 0 ? false : true}
          onClick={ realizarPago }>
          Realizar Pago
        </Button>
      </Grid>
    </Grid>
    <ModalTipoIncentivo
      open={ modalOpen }
      clickBotonCancelar={ modalBotonCancelar }
      clickBotonAceptar={ confirmarPago }
      montoTotalIncentivo={ montoTotalIncentivo}
      totalUsuariosBenificiados={ listaIncentivo.length}
      nombreCiclo={ nombreCiclo }
      nombreIncentivo={ nombreTipoIncentivo }
    />
    <ModalRespuestaPagoIncentivo
      open={modalOpenRespuestaPago}
      clickBotonAceptar={ clickBotonAceptarModalPago }
      listaIncentivo={ listaIncentivoRespuestaPago }
      totalUsuariosBenificiados={ listaIncentivo.length}
      nombreCiclo={ nombreCiclo }
      nombreIncentivo={ nombreTipoIncentivo }
    />
    </>
  )
}

export default PagoIncentivo;