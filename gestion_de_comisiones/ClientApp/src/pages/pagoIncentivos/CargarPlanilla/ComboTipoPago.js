import React, {useEffect, useState  } from 'react';
import { Grid, FormControl, InputLabel, Select, MenuItem, TextField } from "@material-ui/core";
const ComboTipoPago = (props)=> {
  return(
    <FormControl fullWidth variant="outlined">
      <TextField
        id="simple-select"
        select
        variant="outlined"
        value={ props.valorTipoPago === undefined ? '': props.valorTipoPago }
        label={props.labelTipoPago}
        onChange={ props.handleChangeTipoPago }
        size="small"
      >
        { props.listaTipoPago &&
          props.listaTipoPago.map((tipoPago,index)=>(
            <MenuItem key={index} value={tipoPago.idTipoPago}>{tipoPago.nombre}</MenuItem>
            )
          )
        }
      </TextField>
    </FormControl>
  )
}
export default ComboTipoPago