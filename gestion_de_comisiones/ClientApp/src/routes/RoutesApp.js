import React, {  Suspense } from 'react';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { Layout } from '../components/Layout';
// import { Route, Switch } from 'react-router';
import Pages from './Pages';

 const RoutesApp =()=>  {
  

  
    return (
      <>       
          {/* <Router>
            <Suspense
              fallback={<div>Loading...</div>}
            >
                <Switch>
                    <Route  exact path={process.env.PUBLIC_URL + "/"} component={Pages.Home}  />
                    <Route  exact path={process.env.PUBLIC_URL + "/cargar/Comisiones"} component={Pages.CargarComisiones}  />
                    <Route  exact path={process.env.PUBLIC_URL + "/prorrateo"} component={Pages.Prorrateo}  />
                    <Route  exact path={process.env.PUBLIC_URL + "/facturacion"} component={Pages.Facturacion}  />
                    <Route  exact  component={Pages.Prorrateo}  />
                </Switch>
            </Suspense>
        </Router> */}
         <Layout>
            <Switch>
                <Route exact path='/' component={Pages.Home} />
                <Route path='/cargar/Comisiones' component={Pages.CargarComisiones} />
                <Route path='/prorrateo' component={Pages.Prorrateo} />
                <Route path='/facturacion' component={Pages.Facturacion} />     
                <Route path='/forma/pago' component={Pages.FormaPago} />             
                <Route  component={Pages.Facturacion} />  
            </Switch>
         </Layout>
      </>
    );
  
}

export default RoutesApp;
