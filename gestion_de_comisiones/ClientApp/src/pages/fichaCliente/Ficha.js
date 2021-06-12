import React, {useEffect, useState}  from 'react';
import BorderWrapper from 'react-border-wrapper'
import { emphasize, withStyles } from '@material-ui/core/styles';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import HomeIcon from '@material-ui/icons/Home';

import * as permiso from '../../routes/permiso'; 
import { verificarAcceso, validarPermiso} from '../../lib/accesosPerfiles';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import * as ActionCliente from '../../redux/actions/clienteAction';
import * as moment from "moment";
import "moment/locale/es";

const StyledBreadcrumb = withStyles((theme) => ({
    root: {
      backgroundColor: theme.palette.grey[100],
      height: theme.spacing(3),
      color: theme.palette.grey[800],
      fontWeight: theme.typography.fontWeightRegular,
      '&:hover, &:focus': {
        backgroundColor: theme.palette.grey[300],
      },
      '&:active': {
        boxShadow: theme.shadows[1],
        backgroundColor: emphasize(theme.palette.grey[300], 0.12),
      },
    },
  }))(Chip); 


 const Ficha = (props)=> {    
     
  let history = useHistory();
  const {perfiles} = useSelector((stateSelector) =>{ return stateSelector.home});   
/*   useEffect(()=>{  try{  
     verificarAcceso(perfiles, props.location.state.namePagina + permiso.VISUALIZAR, history);
     }catch (err) {  verificarAcceso(perfiles, 'none', history); }
  },[]) */
  const dispatch = useDispatch();
  
  //const[paraIdCliente, setParaIdCliente]= useState(0);
  //setParaIdCliente(props.location.state.idCliente);

  useEffect(()=>{ 
    console.log('paramet : ', props.location.state.idCliente);
    dispatch(ActionCliente.listaPaises());
    dispatch(ActionCliente.obtenerCiudadesPorPais(2));
    dispatch(ActionCliente.obtenerClienteXId(parseInt(props.location.state.idCliente)));
  },[])

    const regresarPage=()=>{        
        history.goBack();        
    }
    const [codigo, setCodigo]= useState("");
    const [fechaRegistro, setFechaRegistro]= useState(moment().format("YYYY/MM/DD"));
    const [nombre, setNombre]= useState("");
    const [apellido, setApellido]= useState("");
    const [ci, setCi]= useState("");
    const [telOficina, setTelOficina]=useState(0);
    const [telMovil, setTelMovil] = useState(0);
    const [telFijo, setTelFijo] = useState(0);
    const [direccion, setDireccion]= useState("");
    const [idCiudad,setIdCiudad]= useState(0);
    const [idPais, setIdPais]= useState(0);
    const [correoElectronico, setCorreoElectronico]= useState("");
    const [fechaNacimiento, setFechaNacimiento] = useState(moment().format("YYYY/MM/DD"))
    const [codigoPatrocinador, setCodigoPatrocinador] = useState("");
    const [nombrePatrocinador, setNombrePatrocinador]= useState("");
    const [nivel, setNivel]= useState("");
    const [comentario, setComentario]=useState("");

    const [idBanco, setIdBanco]=useState(0);
    const [cuentaBancaria, setCuentaBancaria]= useState("");
    const [codigoBanco, setCodigoBanco]= useState(0);

    const [razonSocial, setRazonSocial]= useState("");
    const [nit, setNit]= useState("");

    const [fechaBaja, setFechaBaja]= useState(moment().format("YYYY/MM/DD"));
    const [idTipoBaja, setIdBaja]= useState(0);
    const [motivoBaja, setMotivoBaja]= useState("");
    


  


    const _onChangeregistro= (e) => {
      const texfiel = e.target.name;
      const value = e.target.value;
      if (texfiel === "codigo") {
           setCodigo(value);
      }


      
   };
     
    return (
      <>
           <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}> 
              <Breadcrumbs aria-label="breadcrumb">
                      <div  onClick={regresarPage} >   <StyledBreadcrumb key={2}  component="a" label="Cliente" icon={<HomeIcon fontSize="small" />} /> </div>  
                        <StyledBreadcrumb key={2} component="a" label="Ficha cliente"  />
                       
              </Breadcrumbs>
           </div>
           <br/>
           <br/>
         
          <h1> ficha Cliente </h1>
                        
  
      </>
    );

}
export default Ficha;