import React, {useEffect, useState  } from 'react';
import { Grid, FormControl, InputLabel, Select, MenuItem, makeStyles, TextField } from "@material-ui/core";

const useStyles = makeStyles(()=>({
  comboStyle:{
    padding: '4px 0px'
  }
}))

const ComboTipoIncentivoPago = (props)=> {
  const style = useStyles()
  return(
    <FormControl fullWidth variant="outlined">
      <TextField
        variant="outlined"
        size='small'
        id="simple-select-incentivo"
        select
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
      </TextField>
    </FormControl>
  )
}
export default ComboTipoIncentivoPago