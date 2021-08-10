import React from 'react';
import { Switch, Route } from "react-router-dom";
import  Layout  from '../components/Layout';
import Pages from './Pages';
import * as Action from '../redux/actions/homeAction';
import {useSelector,useDispatch} from "react-redux";

 const RoutesApp =()=>  {
  
     const dispatch = useDispatch();
     dispatch(Action.cargarMenu());
     const {load} =useSelector((stateSelector)=>{ return stateSelector.load});
    
    return (
      <>       
        {load?
         <Layout title={'GESTOR DE COMISONES'}>
            <Switch>
                <Route exact path='/' component={Pages.Home} />
                <Route path='/cargar/comisiones' component={Pages.CargarComisiones} />
                <Route path='/cargar-aplicaciones' component={Pages.CargarAplicaciones} />
                <Route path='/prorrateo' component={Pages.Prorrateo} exact />
                <Route path='/facturacion' component={Pages.Facturacion} exact />     
                <Route path='/forma/pago' component={Pages.FormaPago} exact />                  
                <Route path='/gestion/roles' component={Pages.GestionRol} exact />     
                <Route path='/gestion/nuevo/roles' component={Pages.Roles} exact /> 
                <Route path='/gestion/edit/rol' component={Pages.EditRol} exact />        
                <Route path='/page/sin-acceso' component={Pages.SinAcceso} exact />  
                <Route path='/clientes' component={Pages.Cliente} exact />  
                <Route  path='/cliente/ficha' component={Pages.Ficha} exact />  
                <Route  path='/facturacion/detalle/adjunto' component={Pages.DetalleAdjunto} exact />  
                <Route  path='/usuario/asignar-roles' component={Pages.SetRol} exact />  
                <Route  component={Pages.NotFoundLoad} />                
            </Switch>
         </Layout> :
          <Route  component={Pages.Login} />  
         }
      </>
    );
  
}

export default RoutesApp;
