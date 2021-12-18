import React from "react";
import { Route, Redirect } from "react-router-dom";

const RouteRedirect = (routeProps) => {
  return (
    <Route
      {...routeProps}
      render={(renderProps) => {
        return renderProps.location.state &&
          renderProps.location.state.namePagina ? (
          React.createElement(routeProps.element, renderProps)
        ) : (
          <Redirect to="/" />
        );
      }}
    />
  );
};

export default RouteRedirect;
