import  {Home}  from '../pages/Home';
import Login from '../pages/login/Login'
import Facturacion from '../pages/pagoComisiones/Facturacion'
import  Prorrateo  from '../pages/pagoComisiones/Prorrateo';
import  CargarComisiones  from '../pages/pagoComisiones/CargarComisiones';
import FormaPago from '../pages/pagoComisiones/FormaPago';
import NotFoundLoad from '../components/notfound/NotFoundLoad';
import SinAcceso from '../components/notfound/SinAcceso';
import Roles from '../pages/usuario/Roles';
import GestionRol from '../pages/usuario/Roles/GestionRol';
import EditRol from '../pages/usuario/Roles/EditRol';
import Cliente from '../pages/fichaCliente/Cliente';
import Ficha from '../pages/fichaCliente/Ficha';

// const Layout = lazy(() => import('../components/Layout'));
// const Home = lazy( () =>  import("../pages/Home"));
// const Facturacion = lazy( () =>  import("../pages/pagoComisiones/Facturacion"));
// const Prorrateo = lazy(() =>  import("../pages/pagoComisiones/Prorrateo"));
// const CargarComisiones = lazy(() =>  import("../pages/pagoComisiones/CargarComisiones"));
// const NotFoundLoad = lazy(() =>  import("../components/notfound/NotFoundLoad"));

export default {
    Login,
    Home,
    Facturacion,
    Prorrateo,
    CargarComisiones,
    FormaPago,
    NotFoundLoad,
    SinAcceso,
    Roles,
    GestionRol,
    EditRol,
    Cliente,
    Ficha,
};