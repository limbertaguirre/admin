import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { verificarAcceso } from "../lib/accesosPerfiles";
import { requestGet } from "../service/request";
import { showMessage } from "../redux/actions/messageAction";

const useFormaPagoRezagado = () => {
  let history = useHistory();
  const { location } = history;
  const dispatch = useDispatch();
  const { userName, idUsuario } = useSelector(
    (stateSelector) => stateSelector.load
  );
  const { perfiles } = useSelector((stateSelector) => stateSelector.home);
  useEffect(() => {
    try {
      verificarAcceso(
        perfiles,
        location.state.namePagina + permiso.VISUALIZAR,
        history
      );
    } catch (err) {
      verificarAcceso(perfiles, "none", history);
    }
  }, []);

  const [ciclos, setCiclos] = useState([]);
  const [idCiclo, setIdCiclo] = useState(0);
  const [idCicloSelected, setIdCicloSelected] = useState(0);
  const [nameComboSeleccionado, setNameComboSeleccionado] = useState("");
  const [listaComisionesAPagar, setListaComisionesAPagar] = useState([]);
  const [listaComisionPaginacionNueva, setListaComisionPaginacionNueva] =
    useState(false);
  const [statusBusqueda, setStatusBusqueda] = useState(false);

  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = useState("");
  const [tipoSnackbar, settipTSnackbar] = useState(true);
  const [txtBusqueda, setTxtBusqueda] = useState("");

  const [openTipoPago, setTipoPago] = useState(false);
  const [listTipoPagos, setListTipoPagos] = useState([]);
  const [idcomisionDetalleSelect, setIdcomisionDetalleSelect] = useState(0);
  const [idtipoPagoSelect, setIdtipoPagoSelect] = React.useState("0");

  const [openModalAutorizadores, setOpenModalAutorizadores] = useState(false);
  const [pendienteFormaPago, setPendienteFormaPago] = useState(false);

  const mensajeGenericoCiclo = () => {
    setOpenSnackbar(true);
    setMensajeSnackbar("¡Debe Seleccionar un ciclo!");
    settipTSnackbar("warning");
  };

  useEffect(() => {
    handleOnGetCiclos();
  }, []);

  const handleOnGetCiclos = () => {
    const headers = { usuarioLogin: userName };
    requestGet("Pagos/GetCiclos", headers, dispatch).then((res) => {
      if (res.code === 0) {
        setCiclos(res.data);
      } else {
        dispatch(showMessage({ message: res.message, variant: "info" }));
      }
    });
  };
  const recargarGetCiclos = () => {
    setCiclos([]);
    const headers = { usuarioLogin: userName };
    requestGet("Pagos/GetCiclos", headers, dispatch).then((res) => {
      if (res.code === 0) {
        setCiclos(res.data);
      }
    });
  };
  const seleccionarNombreCombo = (nombre) => {
    setNameComboSeleccionado(nombre);
  };

  const onChangeSelectCiclo = (e) => {
    const texfiel = e.target.name;
    const value = e.target.value;
    if (texfiel === "idCiclo") {
      setIdCiclo(value);
    }
    if (texfiel === "txtBusqueda") {
      setTxtBusqueda(value);
    }
  };

  const handleOnGetAplicaciones = () => {
    if (idCiclo && idCiclo !== 0) {
      setIdCicloSelected(idCiclo);
      const data = {
        usuarioLogin: userName,
        idCiclo: idCiclo,
      };
      requestPost("Pagos/ObtenerFormasPagos", data, dispatch).then((res) => {
        if (res.code === 0) {
          let data = res.data;
          setPendienteFormaPago(data.pendienteFormaPago);
          setListaComisionesAPagar(data.lista);
          setListaComisionPaginacionNueva(true);
          setStatusBusqueda(true);
          ApiVerificarAutorizador(userName, idCiclo, idUsuario, dispatch);
        } else {
          dispatch(
            showMessage({
              message: res.message,
              variant: "error",
            })
          );
        }
      });
    } else {
      setOpenSnackbar(true);
      setMensajeSnackbar("¡Debe Seleccionar un ciclo!");
      settipTSnackbar("warning");
    }
  };

  const RecargarListadoComisionesSinActualizarPagina = () => {
    if (idCiclo && idCiclo !== 0) {
      setIdCicloSelected(idCiclo);
      const data = {
        usuarioLogin: userName,
        idCiclo: idCiclo,
      };
      requestPost("Pagos/ObtenerFormasPagos", data, dispatch).then((res) => {
        if (res.code === 0) {
          let data = res.data;
          setPendienteFormaPago(data.pendienteFormaPago);
          setListaComisionesAPagar(data.lista);
          setStatusBusqueda(true);
          ApiVerificarAutorizador(userName, idCiclo, idUsuario, dispatch);
        } else {
          dispatch(
            showMessage({
              message: res.message,
              variant: "error",
            })
          );
        }
      });
    } else {
      setOpenSnackbar(true);
      setMensajeSnackbar("¡Debe Seleccionar un ciclo!");
      settipTSnackbar("warning");
    }
  };

  const closeSnackbar = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setOpenSnackbar(false);
  };

  const selecionarDetalleFrelances = (
    comisionDetalleId,
    ciSeleccionado,
    idTipoPago
  ) => {
    setIdtipoPagoSelect(String(idTipoPago));
    listarTiposPagos(ciSeleccionado);
    setIdcomisionDetalleSelect(comisionDetalleId);
  };
  const cerrarModalTipoPagoModal = () => {
    setTipoPago(false);
    setIdtipoPagoSelect("0");
  };

  async function listarTiposPagos(ciSeleccionado) {
    let respuesta = await Actions.listarFormaPagos(
      userName,
      ciSeleccionado,
      idCiclo,
      dispatch
    );
    if (respuesta && respuesta.code == 0) {
      setListTipoPagos(respuesta.data);
      setTipoPago(true);
    } else {
      dispatch(
        showMessage({
          message: respuesta.message,
          variant: "error",
        })
      );
    }
  }

  useEffect(() => {}, [
    listTipoPagos,
    idcomisionDetalleSelect,
    listaComisionesAPagar,
    listaComisionPaginacionNueva,
  ]);

  const handleChangeRadio = (event) => {
    setIdtipoPagoSelect(event.target.value);
  };
  const confirmarTipoPago = () => {
    funcionConfirmarTipoPago();
  };

  async function funcionConfirmarTipoPago() {
    if (idcomisionDetalleSelect != 0) {
      let response = await Actions.aplicarFormaPago(
        userName,
        idcomisionDetalleSelect,
        idtipoPagoSelect,
        idUsuario,
        idCiclo,
        dispatch
      );
      if (response && response.code == 0) {
        setTipoPago(false);
        setIdtipoPagoSelect("0");
        RecargarListadoComisionesSinActualizarPagina();
      } else {
        dispatch(
          showMessage({
            message: response.message,
            variant: "error",
          })
        );
      }
    }
  }

  const buscarFreelanzer = () => {
    buscarFrelancerPorCi();
  };

  async function buscarFrelancerPorCi() {
    let response = await Actions.buscarPorCarnetFormaPago(
      userName,
      idCiclo,
      txtBusqueda,
      dispatch
    );
    if (response && response.code == 0) {
      let data = response.data;
      setPendienteFormaPago(data.pendienteFormaPago);
      setListaComisionesAPagar(data.lista);
      setListaComisionPaginacionNueva(true);
    }
  }

  const seleccionarTipoFiltroBusqueda = (idTipoFormaPago) => {
    filtrarComisionPorFormaPago(idTipoFormaPago);
  };
  async function filtrarComisionPorFormaPago(idTipoFormaPago) {
    if (idCiclo && idCiclo !== 0) {
      let response = await Actions.ListarComisionFormaPagoFiltrada(
        userName,
        idCiclo,
        idTipoFormaPago,
        dispatch
      );
      if (response && response.code == 0) {
        let data = response.data;
        setPendienteFormaPago(data.pendienteFormaPago);
        setListaComisionesAPagar(data.lista);
        setListaComisionPaginacionNueva(true);
      }
    } else {
      mensajeGenericoCiclo();
    }
  }

  const [autorizadorObjeto, setAutorizadorObjeto] = useState({
    autorizador: false,
    comisionAutorizada: false,
    idciclo: 0,
    idComision: 0,
    idAutorizacionComision: 0,
    autorizadores: [],
  });
  async function ApiVerificarAutorizador(user, cicloId, idUser, dispatch) {
    let respuesta = await Actions.VerificarAutorizadorComision(
      user,
      cicloId,
      idUser,
      dispatch
    );
    if (respuesta && respuesta.code == 0) {
      setAutorizadorObjeto(respuesta.data);
      if (respuesta.data.autorizador == true) {
        setOpenModalAutorizadores(true); //abrir modal visualizaciones
      }
    } else {
      setAutorizadorObjeto({
        autorizador: false,
        comisionAutorizada: false,
        idciclo: 0,
        idComision: 0,
        idAutorizacionComision: 0,
        autorizadores: [],
      });
    }
  }

  const cerrarModalListaAutorizadosConfirm = () => {
    setOpenModalAutorizadores(false);
  };
  const confirmarModalAutorizacion = (idComision, idAutorizacionComision) => {
    ApiConfirmarAutorizacion(
      userName,
      idUsuario,
      idCiclo,
      idComision,
      idAutorizacionComision
    );
  };
  async function ApiConfirmarAutorizacion(
    userNa,
    idUser,
    idCiclo,
    idComision,
    idAutorizacionComision
  ) {
    if (idCiclo && idCiclo !== 0) {
      let response = await Actions.ConfirmarAutorizacion(
        userNa,
        idUser,
        idCiclo,
        idComision,
        idAutorizacionComision,
        dispatch
      );
      if (response && response.code == 0) {
        let data = response.data;
        setOpenModalAutorizadores(false);
        setPendienteFormaPago(data.pendienteFormaPago);
        dispatch(
          showMessage({
            message: response.message,
            variant: "success",
          })
        );
        //api recarga el estado y lista de autorizaciones
        ApiVerificarAutorizador(userName, idCiclo, idUsuario, dispatch);
      } else {
        dispatch(
          showMessage({
            message: response.message,
            variant: "error",
          })
        );
      }
    } else {
      mensajeGenericoCiclo();
    }
  }

  useEffect(() => {}, [autorizadorObjeto]);

  const [openCierrePagoModal, setOpenCierrePagoModal] = useState(false);
  const [habilitadoCierrePago, setHabilitadoCierrePago] = useState(false);
  const [listadoConfirm, setListadoConfirm] = useState([]);
  const [listadoSeleccionado, setListadoSeleccionado] = useState([]);

  const verificarConfirmarFomaPago = () => {
    ApiVerificarConfirmarFormaPago(userName, idUsuario, idCiclo);
  };
  async function ApiVerificarConfirmarFormaPago(userNa, idUser, idCICLO) {
    if (idCICLO && idCICLO !== 0) {
      let response = await Actions.VerificarCierreFormaPago(
        userNa,
        idUser,
        idCICLO,
        dispatch
      );

      if (response && response.code == 0) {
        let data = response.data;
        setOpenCierrePagoModal(true);
        setHabilitadoCierrePago(data.habilitado);
        setListadoConfirm(data.listaPorAreas);
        setListadoSeleccionado(data.listSeleccionados);
      } else {
        dispatch(
          showMessage({
            message: response.message,
            variant: "error",
          })
        );
      }
    } else {
      mensajeGenericoCiclo();
    }
  }
  const cancelarModalConfirmarCierre = () => {
    setOpenCierrePagoModal(false);
  };
  const confirmarCierrePagoModal = () => {
    ApiCerrarFormaDePago(userName, idUsuario, idCiclo);
  };

  async function ApiCerrarFormaDePago(userNa, idUser, idCICLO) {
    if (idCICLO && idCICLO !== 0) {
      let response = await Actions.CerrarFormaPago(
        userNa,
        idUser,
        idCICLO,
        dispatch
      );

      if (response && response.code == 0) {
        setOpenCierrePagoModal(false); //cierra el modal
        limpiarReiniciarValoresCerrados();
        dispatch(
          showMessage({
            message: response.message,
            variant: "success",
          })
        );
      } else {
        dispatch(
          showMessage({
            message: response.message,
            variant: "error",
          })
        );
      }
    } else {
      mensajeGenericoCiclo();
    }
  }

  const limpiarReiniciarValoresCerrados = () => {
    setListaComisionesAPagar([]);
    setStatusBusqueda(false);
    setPendienteFormaPago(false);
    setIdCiclo(0);
    setIdCicloSelected(0);
    setNameComboSeleccionado("");
    recargarGetCiclos();
  };
};

export default useFormaPagoRezagado;
