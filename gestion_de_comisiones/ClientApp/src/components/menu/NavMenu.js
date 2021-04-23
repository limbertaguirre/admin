import React from "react";
import MenuOne from "./MenuOne";
import MenuTwo from "./MenuTwo";

 const NavMenu = (props) => {
  return (
    <header>
      <div
        className="col-xl-12 col-lg-12 d-none d-lg-block"
        style={{ paddingLeft: "0px", paddingRight: "0px" }}
      >
        <MenuOne children={props.children} title={props.title} />
      </div>
      <div
        style={{ paddingLeft: "0px", paddingRight: "0px" }}
        className={`col-xl-1 d-xl-none  col-lg-1 d-lg-none  col-md-12 col-12 sticky-bar  clearfix `}
      >        
        <MenuTwo children={props.children} title={props.title}  />
      </div>
    </header>
  );
};
export default NavMenu;
