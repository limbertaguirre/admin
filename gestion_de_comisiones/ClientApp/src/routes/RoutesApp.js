import React, {  Suspense } from 'react';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { Layout } from '../components/Layout';
// import { Route, Switch } from 'react-router';
import Pages from './Pages';

 const RoutesApp =()=>  {
  

  
    return (
      <>       
        <Router>
            <Suspense
              fallback={<div>Loading...</div>}
            >
                <Switch>
                    <Route  exact path={process.env.PUBLIC_URL + "/"} component={Pages.Home}  />
                    <Route  exact path={process.env.PUBLIC_URL + "/counter"} component={Pages.Counter}  />
                    <Route  exact path={process.env.PUBLIC_URL + "/fetch-data"} component={Pages.FetchData}  />
                    <Route  exact path={process.env.PUBLIC_URL + "/facturacion"} component={Pages.Facturacion}  />
                    <Route  exact  component={Pages.FetchData}  />
                </Switch>
            </Suspense>

        </Router>
         {/* <Layout>
            <Switch>
                <Route exact path='/' component={Pages.Home} />
                <Route path='/counter' component={Pages.Counter} />
                <Route path='/fetch-data' component={Pages.FetchData} />
                <Route path='/facturacion' component={Pages.Facturacion} />                
                <Route  component={Pages.Facturacion} />  
            </Switch>
         </Layout> */}
      </>
    );
  
}

export default RoutesApp;
