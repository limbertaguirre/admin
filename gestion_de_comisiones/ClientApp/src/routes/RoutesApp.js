import React, { useEffect, useRef } from 'react';
import { Switch, Route, BrowserRouter } from "react-router-dom";
import  Layout  from '../components/Layout';
import Pages from './Pages';
import RouteRedirect from '../components/RouteRedirect';
import * as Action from '../redux/actions/homeAction';
import {useSelector,useDispatch} from "react-redux";
//import useSocket from '../hooks/useSocket';
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
 const RoutesApp =()=>  {
      const routerRef = useRef();
      //const {socket} = useSocket({routerRef})
     const dispatch = useDispatch();
     dispatch(Action.cargarMenu());
     const {load} =useSelector((stateSelector)=>{ return stateSelector.load});
    
    return (
      <BrowserRouter basename={baseUrl} ref={routerRef}>      
        {load?
       
         <Layout title={'GESTOR DE COMISIONES'}>
            <Switch>
                <Route exact path='/' component={Pages.Home} />
                <RouteRedirect exact path='/cargar/comisiones' element={Pages.CargarComisiones} />
                <RouteRedirect exact path='/cargar-aplicaciones' element={Pages.CargarAplicaciones} />
                <RouteRedirect exact path='/prorrateo' element={Pages.Prorrateo}  />
                <RouteRedirect path='/facturacion' element={Pages.Facturacion} exact />     
                <RouteRedirect path='/forma/pago' element={Pages.FormaPago} exact />  
                <RouteRedirect exact path='/pagos-gestor' element={Pages.Pagos}  />                
                <RouteRedirect exact path='/gestion/roles' element={Pages.GestionRol}  />     
                <RouteRedirect exact path='/gestion/nuevo/roles' element={Pages.Roles}  /> 
                <Route path='/gestion/edit/rol' component={Pages.EditRol} exact />        
                <RouteRedirect path='/page/sin-acceso' element={Pages.SinAcceso} exact />  
                <RouteRedirect path='/clientes' element={Pages.Cliente} exact />  
                <RouteRedirect  path='/cliente/ficha' element={Pages.Ficha} exact />  
                <RouteRedirect  path='/facturacion/detalle/adjunto' element={Pages.DetalleAdjunto} exact />  
                <RouteRedirect exact path='/usuario/asignar-roles' element={Pages.SetRol}  />  
                <RouteRedirect exact path='/pago/rezagados' element={Pages.PagoRezagado}  />    
                <RouteRedirect exact path='/forma-pago/rezagados' element={Pages.FormaPagoRezagado}  />    
                <RouteRedirect exact path='/reporte/ciclos' element={Pages.ReporteCiclo}  />  
                <RouteRedirect exact path='/reporte/freelancer' element={Pages.ReporteFreelancer}  />  
                <RouteRedirect exact path='/pagos/incentivos/cargar-planilla' element={Pages.CargarPlanillaSionPay}  /> 
                <RouteRedirect exact path='/pagos/incentivos/pagar' element={Pages.PagoIncentivo}  />
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
