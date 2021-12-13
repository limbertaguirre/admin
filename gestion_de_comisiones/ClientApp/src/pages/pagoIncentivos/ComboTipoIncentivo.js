import React, {useEffect, useState  } from 'react';
import { Grid, FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";




const ComboTipoIncentivoPago = (props)=> {
  const [ tipoIncentivo , setTipoIncentivo ] = useState("")

  const handleChangeCiclo = (event) =>{
    setTipoIncentivo(event.target.value);
  }
  return(
    <FormControl fullWidth variant="outlined">
      <InputLabel id="demo-simple-select-label">Elija el incentivo</InputLabel>
      <Select
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        value={tipoIncentivo}
        label="Elija el incentivo"
        onChange={handleChangeCiclo}
      >
        { props.listaIncentivo && 
          props.listaIncentivo.map((incentivo,index)=>(
            <MenuItem key={index} value={incentivo.idTipoIncentivo}>{incentivo.descripcion}</MenuItem>
            )
          )
        }
        {/* { listaCiclo &&
          listaCiclo.map((ciclo,index)=>(
            <MenuItem key={index} value={ciclo.idCiclo}>{ciclo.nombre}</MenuItem>
            )
          )
        } */}
      </Select>
    </FormControl>
  )
}



export default ComboTipoIncentivoPago