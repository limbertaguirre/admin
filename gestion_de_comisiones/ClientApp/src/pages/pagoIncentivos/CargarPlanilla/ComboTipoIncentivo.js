import React, {useEffect, useState  } from 'react';
import { Grid, FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";
const ComboTipoIncentivoPago = (props)=> {
  return(
    <FormControl fullWidth variant="outlined">
      <InputLabel id="demo-simple-select-label">{ props.labelTipoIncentivoPago }</InputLabel>
      <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        value={ props.valorTipoIncentivo === undefined ? '': props.valorTipoIncentivo }
        label={ props.labelTipoIncentivoPago }
        onChange={ props.handleChangeTipoIncentivo }
      >
        { props.listaIncentivo &&
          props.listaIncentivo.map((incentivo,index)=>(
            <MenuItem key={index} value={incentivo.idTipoIncentivo}>{incentivo.descripcion}</MenuItem>
            )
          )
        }
      </Select>
    </FormControl>
  )
}
export default ComboTipoIncentivoPago