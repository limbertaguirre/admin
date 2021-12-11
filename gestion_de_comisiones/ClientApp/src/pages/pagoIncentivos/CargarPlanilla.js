import React, {useEffect, useState} from 'react';
import { Table, Grid, TableContainer, Button , TableBody , TableCell, TableHead, TableRow }from "@material-ui/core";
import readXlsxFile from 'read-excel-file'
import { requestPost } from '../../service/request';
import {useDispatch} from 'react-redux';

const PagoIncentivo = (props)=> {
  const dispatch = useDispatch()
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
    requestPost('Incentivo/CargarPlanillaExcel',data, dispatch)
      .then((response)=>{
        console.log(response)
      })
      .catch((error)=>{
        console.log(error)
    })
  }
  const [datosExcel, setDatosExcel] = useState()
  return(
    <>
    <Grid container spacing={3}>
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
    </>
  )

}

export default PagoIncentivo;