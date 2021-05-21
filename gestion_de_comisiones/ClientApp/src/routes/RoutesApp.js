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
                <Route path='/cargar/Comisiones' component={Pages.CargarComisiones} />
                <Route path='/prorrateo' component={Pages.Prorrateo} />
                <Route path='/facturacion' component={Pages.Facturacion} />     
                <Route path='/forma/pago' component={Pages.FormaPago} />  
                <Route path='/gestion/roles' component={Pages.Roles} />            
                <Route  component={Pages.NotFoundLoad} />                
            </Switch>
         </Layout> :
          <Route  component={Pages.Login} />  
         }
      </>
    );
  
}

export default RoutesApp;
