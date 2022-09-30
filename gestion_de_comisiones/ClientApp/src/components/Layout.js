import React from 'react';
import  NavMenu  from './menu/NavMenu';

const  Layout=(props)=> {
   
    return (
            <div>
              <NavMenu children={props.children} title={props.title} />    
            </div>
         );
  
};
export default Layout;
