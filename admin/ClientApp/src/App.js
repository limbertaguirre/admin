import React, { Component } from 'react';
import Message from './components/message/Message';
import ProgressDialog from "./components/progressDialog/ProgressDialog";
import RoutesApp from './routes/RoutesApp';
import './custom.css'
export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <>
        <Message />
        <RoutesApp />
        <ProgressDialog />
      </>
    );
  }
}
