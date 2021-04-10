import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './pages/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import Message from './components/message/Message';
import ProgressDialog from "./components/progressDialog/ProgressDialog";

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <>
      <Message />
      <ProgressDialog />
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
      </>
    );
  }
}
