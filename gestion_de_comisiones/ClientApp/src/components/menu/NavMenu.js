import React from "react";
import MenuOne from "./MenuOne";
import MenuTwo from "./MenuTwo";

export const NavMenu = (props) => {
  return (
    <header>
      <div
        className="col-xl-12 col-lg-12 d-none d-lg-block"
        style={{ paddingLeft: "0px", paddingRight: "0px" }}
      >
        <MenuOne children={props.children} />
      </div>
      <div
        style={{ paddingLeft: "0px", paddingRight: "0px" }}
        className={`col-xl-1 d-xl-none  col-lg-1 d-lg-none  col-md-12 col-12 sticky-bar  clearfix `}
      >
        {/* <Acordion children={props.children}/> */}
        <MenuTwo children={props.children} />
      </div>
    </header>
  );
};
