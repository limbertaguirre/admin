import React, { useEffect, Suspense, lazy } from "react";


// import  {Layout}  from '../components/Layout';
// import  {Home}  from '../pages/Home';
// import {Facturacion} from '../pages/pagoComisiones/Facturacion'
// import { FetchData } from '../components/FetchData';
// import { Counter } from '../components/Counter';
// import NotFoundLoad from '../components/notfound/NotFoundLoad';

// const Layout = lazy(() => import('../components/Layout'));
const Home = lazy( async() => await import('../pages/Home'));
const Facturacion = lazy(async () => await import('../pages/pagoComisiones/Facturacion'));
const FetchData = lazy(async() => await import('../components/FetchData'));
const Counter = lazy(async() => await import('../components/Counter'));
const NotFoundLoad = lazy(async() => await import('../components/notfound/NotFoundLoad'));


export default {
    // Layout,
    Home,
    Facturacion,
    FetchData,
    Counter,
    NotFoundLoad,
};