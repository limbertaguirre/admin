import React, {useEffect, useState  } from 'react';
import { Grid, FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";
const ComboTipoPago = (props)=> {
  return(
    <FormControl fullWidth variant="outlined">
      <InputLabel id="demo-simple-select-label">{props.labelTipoPago}</InputLabel>
      <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        value={ props.valorTipoPago === undefined ? '': props.valorTipoPago }
        label={props.labelTipoPago}
        onChange={ props.handleChangeTipoPago }
      >
        { props.listaTipoPago &&
          props.listaTipoPago.map((tipoPago,index)=>(
            <MenuItem key={index} value={tipoPago.idTipoPago}>{tipoPago.nombre}</MenuItem>
            )
          )
        }
      </Select>
    </FormControl>
  )
}
export default ComboTipoPago