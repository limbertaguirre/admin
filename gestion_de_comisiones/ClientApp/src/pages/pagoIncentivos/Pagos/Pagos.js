import React, {useEffect, useState  } from 'react';
import { makeStyles, Link, Table, Grid, TableContainer, Button , TableBody , TableCell, TableHead, TableRow, FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";

import { requestPost ,requestGet } from '../../../service/request';
import {useDispatch, useSelector} from 'react-redux';
const useStyles = makeStyles((theme) => ({
  errorRow: {
    background: 'darksalmon'
  }
}))

const PagoIncentivo = () =>{

  const [ ciclo, setCiclo ] = useState("")
  const [ incentivo, setIncentivo ] = useState("")

  const handleChangeCiclo = (event) => {
    setCiclo(event.target.value)
  }
  const handleChangeIncentivo = (event) => {
    setCiclo(event.target.value)
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
              {/* { listaCiclo &&
              listaCiclo.map((ciclo,index)=>(
                  <MenuItem key={index} value={ciclo.idCiclo}>{ciclo.nombre}</MenuItem>
                  )
                )
              } */}
          </Select>
        </FormControl>
      </Grid>
      <Grid item xs={4}>
        <FormControl fullWidth variant="outlined">
          <InputLabel id="selectIncentivoLabel">Seleccione el incentivo</InputLabel>
          <Select
              labelId="selectIncentivoLabel"
              id="selectCiclo"
              value={ciclo}
              label="Seleccione el ciclo"
              disabled={true}
              onChange={handleChangeIncentivo}
          >
              {/* { listaCiclo &&
                listaCiclo.map((ciclo,index)=>(
                    <MenuItem key={index} value={ciclo.idCiclo}>{ciclo.nombre}</MenuItem>
                    )
                )
              } */}
          </Select>
        </FormControl>
      </Grid>
    </Grid>
    </>
  )
}

export default PagoIncentivo;