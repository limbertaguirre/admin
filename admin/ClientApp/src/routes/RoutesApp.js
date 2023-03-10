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
         <Layout title={'OPERACIONES 2.0'}>
            <Switch>
                <Route exact path='/' component={Pages.Home} />
                <RouteRedirect exact path='/gestion/roles' element={Pages.GestionRol}  />
                <Route exact path='/gestion/nuevo/roles' component={Pages.Roles}  />
                <Route path='/gestion/edit/rol' component={Pages.EditRol} exact />
                <RouteRedirect path='/page/sin-acceso' element={Pages.SinAcceso} exact />
                <RouteRedirect exact path='/usuario/asignar-roles' element={Pages.SetRol}  />
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
