import React,{useState}  from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap'; // al habilitar esta linea quitar la de abajo
import { Link } from 'react-router-dom';

import './NavMenu.css';

const  Acordion =(props)=> {

    const [collapsed, setCollapsed] = useState(true)    
    const toggleNavbar= ()=> {
      setCollapsed(!collapsed);    
    }    
  

return(
     <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">gestion de comisiones</NavbarBrand> 
            <NavbarToggler onClick={toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Cargar/Comisiones">Cargar comisiones</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
                </NavItem>
              </ul>
            </Collapse>
            {props.children}
          </Container>          
        </Navbar> 
)
}
export default  Acordion;