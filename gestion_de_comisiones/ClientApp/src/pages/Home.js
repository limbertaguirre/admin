import React from 'react';
import {Breadcrumbs,Chip,emphasize, withStyles} from '@material-ui/core';
import HomeIcon from '@material-ui/icons/Home';
import {useSelector, useDispatch} from "react-redux";
import * as Action from '../redux/actions/homeAction';

const StyledBreadcrumb = withStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.grey[100],
    height: theme.spacing(3),
    color: theme.palette.grey[800],
    fontWeight: theme.typography.fontWeightRegular,
    '&:hover, &:focus': {
      backgroundColor: theme.palette.grey[300],
    },
    '&:active': {
      boxShadow: theme.shadows[1],
      backgroundColor: emphasize(theme.palette.grey[300], 0.12),
    },
  },
}))(Chip); 



export const Home =()=> {

    const dispatch = useDispatch();

     dispatch(Action.loadHome());
     
    return (
      <>
        <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}>
          <Breadcrumbs aria-label="breadcrumb">
                <StyledBreadcrumb key={1} component="a" label="Home"icon={<HomeIcon fontSize="small" />}  />                
          </Breadcrumbs>
        </div>
        <br />
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
