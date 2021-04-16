import React, { Component } from 'react';
import { NavMenu } from './menu/NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu children={this.props.children} />    
      </div>

    );
  }
}
