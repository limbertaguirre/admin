import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './pages/Home';
import {Facturacion} from './pages/pagoComisiones/Facturacion'

import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import Message from './components/message/Message';
import ProgressDialog from "./components/progressDialog/ProgressDialog";
import RoutesApp from './routes/RoutesApp';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <>
      <Message />
      <ProgressDialog />
      <RoutesApp />
      
      {/* <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/facturacion' component={Facturacion} />
      </Layout> */}
      </>
    );
  }
}
