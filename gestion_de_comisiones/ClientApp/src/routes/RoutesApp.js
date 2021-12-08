import React, { useEffect, useRef } from 'react';
import { Switch, Route, BrowserRouter } from "react-router-dom";
import  Layout  from '../components/Layout';
import Pages from './Pages';
import * as Action from '../redux/actions/homeAction';
import {useSelector,useDispatch} from "react-redux";
import useSocket from '../hooks/useSocket';
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
 const RoutesApp =()=>  {
      const routerRef = useRef();
      const {socket} = useSocket({routerRef})
     const dispatch = useDispatch();
     dispatch(Action.cargarMenu());
     const {load} =useSelector((stateSelector)=>{ return stateSelector.load});
    
    return (
      <BrowserRouter basename={baseUrl} ref={routerRef}>      
        {load?
       
         <Layout title={'GESTOR DE COMISIONES'}>
            <Switch>
                <Route exact path='/' component={Pages.Home} />
                <Route exact path='/cargar/comisiones' component={Pages.CargarComisiones} />
                <Route exact path='/cargar-aplicaciones' component={Pages.CargarAplicaciones} />
                <Route exact path='/prorrateo' component={Pages.Prorrateo}  />
                <Route path='/facturacion' component={Pages.Facturacion} exact />     
                <Route path='/forma/pago' component={Pages.FormaPago} exact />  
                <Route exact path='/pagos-gestor' component={Pages.Pagos}  />                
                <Route exact path='/gestion/roles' component={Pages.GestionRol}  />     
                <Route path='/gestion/nuevo/roles' component={Pages.Roles} exact /> 
                <Route path='/gestion/edit/rol' component={Pages.EditRol} exact />        
                <Route path='/page/sin-acceso' component={Pages.SinAcceso} exact />  
                <Route path='/clientes' component={Pages.Cliente} exact />  
                <Route  path='/cliente/ficha' component={Pages.Ficha} exact />  
                <Route  path='/facturacion/detalle/adjunto' component={Pages.DetalleAdjunto} exact />  
                <Route exact path='/usuario/asignar-roles' component={Pages.SetRol}  />  
                <Route exact path='/reporte/ciclos' component={Pages.ReporteCiclo}  />  
                <Route exact path='/reporte/freelancer' component={Pages.ReporteFreelancer}  />  
                <Route  component={Pages.NotFoundLoad} />                
            </Switch>  
          </Layout> 
         :
          <Route  component={Pages.Login} />  
         }
      </BrowserRouter>
    );
  
}

export default RoutesApp;
