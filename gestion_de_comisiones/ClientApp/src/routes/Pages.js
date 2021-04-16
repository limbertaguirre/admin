import React, { lazy } from "react";

import  {Layout}  from '../components/Layout';
import  {Home}  from '../pages/Home';
import {Facturacion} from '../pages/pagoComisiones/Facturacion'
import { FetchData } from '../components/FetchData';
import { Counter } from '../components/Counter';
import NotFoundLoad from '../components/notfound/NotFoundLoad';

// const Layout = lazy(() => import('../components/Layout'));
// const Home = lazy( () =>  import("../pages/Home"));
// const Facturacion = lazy( () =>  import("../pages/pagoComisiones/Facturacion"));
// const FetchData = lazy(() =>  import("../components/FetchData"));
// const Counter = lazy(() =>  import("../components/Counter"));
// const NotFoundLoad = lazy(() =>  import("../components/notfound/NotFoundLoad"));

export default {
    // Layout,
    Home,
    Facturacion,
    FetchData,
    Counter,
    NotFoundLoad,
};