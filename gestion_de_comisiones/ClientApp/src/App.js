import React, { Component } from 'react';
import { Route } from 'react-router';
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
      </>
    );
  }
}
