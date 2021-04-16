import React from 'react';
import {useSelector, useDispatch} from "react-redux";
import * as Action from '../redux/actions/homeAction';

export const Home =()=> {

    const dispatch = useDispatch();

     dispatch(Action.loadHome());
     
    return (
      <>
        <h1>Hello, que hace!</h1>
        <p>Welcome to your new single-page application, built with:</p>
        <ul>
          <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>         
        </ul>
        <br/>
        <h1>SI FUNCIONA NO LO TOQUES!!!!!!</h1>
        <ul>
          <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>         
        </ul>
        <h1>TODO SE PUEDE CRISTO QUE ME FORTALECE!!!!!!</h1>
      </>
    );

}
