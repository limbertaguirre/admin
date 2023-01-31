import React from 'react';
import {Breadcrumbs,Chip,emphasize, withStyles} from '@material-ui/core';
import HomeIcon from '@material-ui/icons/Home';
import { useDispatch} from "react-redux";
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

  
     
    return (
      <>
        <div className="col-xl-12 col-lg-12 d-none d-lg-block" style={{ paddingLeft: "0px", paddingRight: "0px" }}>
          <Breadcrumbs aria-label="breadcrumb">
                <StyledBreadcrumb key={1} component="a" label="Home"icon={<HomeIcon fontSize="small" />}  />                
          </Breadcrumbs>
        </div>
        <br />
        <h1>HOME!</h1>
       
      </>
    );

}
