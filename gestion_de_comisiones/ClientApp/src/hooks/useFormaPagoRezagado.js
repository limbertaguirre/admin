import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { verificarAcceso } from "../lib/accesosPerfiles";
import { requestGet, requestPost } from "../service/request";
import { showMessage } from "../redux/actions/messageAction";
import * as permiso from "../routes/permiso";

const useFormaPagoRezagado = () => {
  let history = useHistory();
  const { location } = history;
  const dispatch = useDispatch();
  const { userName, idUsuario } = useSelector(
    (stateSelector) => stateSelector.load
  );
  const { perfiles } = useSelector((stateSelector) => stateSelector.home);
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
  const [idtipoPagoSelect, setIdtipoPagoSelect] = useState("0");
  const [idListaFormasPagoSelect, setIdListaFormasPagoSelect] = useState(0);
  const [idDetalleEstadoFormaPagoSelect, setIdDetalleEstadoFormaPagoSelect] = useState(0);

  const [openModalAutorizadores, setOpenModalAutorizadores] = useState(false);
  const [pendienteFormaPago, setPendienteFormaPago] = useState(false);

  const mensajeGenericoCiclo = () => {
    setOpenSnackbar(true);
    setMensajeSnackbar("¡Debe Seleccionar un ciclo!");
    settipTSnackbar("warning");
  };

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
    handleOnGetCiclos();
  }, []);

  const handleOnGetCiclos = () => {
    const headers = { usuarioLogin: userName };
    requestGet("formasPagosRezagados/GetCiclos", headers, dispatch).then(
      (res) => {
        if (res.code === 0) {
          setCiclos(res.data);
        } else {
          dispatch(showMessage({ message: res.message, variant: "info" }));
        }
      }
    );
  };
  const recargarGetCiclos = () => {
    setCiclos([]);
    const headers = { usuarioLogin: userName };
    requestGet("formasPagosRezagados/GetCiclos", headers, dispatch).then(
      (res) => {
        if (res.code === 0) {
          setCiclos(res.data);
        }
      }
    );
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
      const cicloObjectSelected = ciclos.find(
        (item) => item.idCiclo === idCiclo
      );
      setIdCicloSelected(idCiclo);
      const data = {
        usuarioLogin: userName,
        idCiclo: idCiclo,
        idComision: cicloObjectSelected.idComision,
      };
      requestPost(
        "formasPagosRezagados/GetComisionesRezagados",
        data,
        dispatch
      ).then((res) => {
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
      let cicloObjectSelected = ciclos.find((item) => item.idCiclo === idCiclo);
      const data = {
        usuarioLogin: userName,
        idCiclo: idCiclo,
        idComision: cicloObjectSelected.idComision,
      };
      requestPost(
        "formasPagosRezagados/GetComisionesRezagados",
        data,
        dispatch
      ).then((res) => {
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
    idTipoPago,
    idListaFormasPago,
    idDetalleEstadoFormaPago
  ) => {
    setIdtipoPagoSelect(String(idTipoPago));
    listarTiposPagos(ciSeleccionado);
    setIdcomisionDetalleSelect(comisionDetalleId);
    setIdListaFormasPagoSelect(idListaFormasPago);
    setIdDetalleEstadoFormaPagoSelect(idDetalleEstadoFormaPago);
  };
  const cerrarModalTipoPagoModal = () => {
    setTipoPago(false);
    setIdtipoPagoSelect("0");
  };

  async function listarTiposPagos(ciSeleccionado) {
    const cicloObjectSelected = ciclos.find((item) => item.idCiclo === idCiclo);
    let respuesta = await requestPost(
      "/formasPagosRezagados/GetListarFormaPagos",
      {
        usuarioLogin: userName,
        carnet: ciSeleccionado,
        idCiclo: idCiclo,
        comisionId: cicloObjectSelected.idComision,
      },
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

  const handleChangeRadio = (event) => {
    setIdtipoPagoSelect(event.target.value);
  };
  const confirmarTipoPago = () => {
    funcionConfirmarTipoPago();
  };

  async function funcionConfirmarTipoPago() {
    if (idcomisionDetalleSelect != 0) {
      const cicloObjectSelected = ciclos.find(
        (item) => item.idCiclo === idCiclo
      );
      let body = {
        usuarioLogin: userName,
        idComisionDetalle: parseInt(idcomisionDetalleSelect),
        idtipoPago: parseInt(idtipoPagoSelect),
        idUsuario: idUsuario,
        idCiclo: idCiclo,
        comisionId: cicloObjectSelected.idComision,
        idListaFormasPago: parseInt(idListaFormasPagoSelect),
        idDetalleEstadoFormaPago: parseInt(idDetalleEstadoFormaPagoSelect)
      };

      let response = await requestPost(
        "/formasPagosRezagados/aplicarMetodoPagoComision",
        body,
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
    let url = "/formasPagosRezagados/BuscarComisionCarnetFormaPago";
    let body = {
      usuarioLogin: userName,
      idCiclo: parseInt(idCiclo),
      nombreCriterio: txtBusqueda,
    };
    let response = await requestPost(url, body, dispatch);
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
      const cicloObjectSelected = ciclos.find(
        (item) => item.idCiclo === idCiclo
      );
      let url = "/formasPagosRezagados/FiltrarComisionPagoPorTipoPago";
      let body = {
        usuarioLogin: userName,
        idCiclo,
        idTipoPago: parseInt(idTipoFormaPago),
        comisionId: cicloObjectSelected.idComision,
      };
      let response = await requestPost(url, body, dispatch);
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
    const cicloObjectSelected = ciclos.find((item) => item.idCiclo === cicloId);
    let respuesta = await requestPost(
      "/formasPagosRezagados/VerificarAutorizadorPorComision",
      {
        usuarioLogin: user,
        idCiclo: cicloId,
        comisionId: cicloObjectSelected.idComision,
        idUsuario: idUser,
      },
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
      let url = "/formasPagosRezagados/ConfirmarAutorizacion";
      const cicloObjectSelected = ciclos.find(
        (item) => item.idCiclo === idCiclo
      );
      let body = {
        usuarioLogin: userNa,
        idUsuario: idUser,
        idCiclo,
        idComision: cicloObjectSelected.idComision,
        idAutorizacionComision,
      };
      let response = await requestPost(url, body, dispatch);
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

  const [openCierrePagoModal, setOpenCierrePagoModal] = useState(false);
  const [habilitadoCierrePago, setHabilitadoCierrePago] = useState(false);
  const [listadoConfirm, setListadoConfirm] = useState([]);
  const [listadoSeleccionado, setListadoSeleccionado] = useState([]);

  const verificarConfirmarFomaPago = () => {
    ApiVerificarConfirmarFormaPago(userName, idUsuario, idCiclo);
  };
  async function ApiVerificarConfirmarFormaPago(userNa, idUser, idCICLO) {
    if (idCICLO && idCICLO !== 0) {
      let url = "/formasPagosRezagados/VerificarCierreFormaPago";
      const cicloObjectSelected = ciclos.find(
        (item) => item.idCiclo === idCiclo
      );
      let body = {
        usuarioLogin: userName,
        idUsuario: idUsuario,
        idCiclo: parseInt(idCiclo),
        comisionId: cicloObjectSelected.idComision,
      };
      let response = await requestPost(url, body, dispatch);

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
      const cicloObjectSelected = ciclos.find(
        (item) => item.idCiclo === idCiclo
      );
      let url = "/formasPagosRezagados/CerrarFormaDePago";
      let body = {
        usuarioLogin: userName,
        idUsuario: idUsuario,
        idCiclo: parseInt(idCiclo),
        comisionId: cicloObjectSelected.idComision,
      };
      let response = await requestPost(url, body, dispatch);

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

  const cicloSeleccionado = idCiclo
    ? ciclos.find((item) => item.idCiclo === idCiclo)
    : null;

  return {
    pendienteFormaPago,
    autorizadorObjeto,
    statusBusqueda,
    buscarFreelanzer,
    ciclos,
    seleccionarNombreCombo,
    handleOnGetAplicaciones,
    openSnackbar,
    closeSnackbar,
    tipoSnackbar,
    mensajeSnackbar,
    listaComisionesAPagar,
    listaComisionPaginacionNueva,
    setListaComisionPaginacionNueva,
    selecionarDetalleFrelances,
    seleccionarTipoFiltroBusqueda,
    idCiclo,
    openTipoPago,
    cerrarModalTipoPagoModal,
    confirmarTipoPago,
    listTipoPagos,
    idtipoPagoSelect,
    handleChangeRadio,
    openModalAutorizadores,
    nameComboSeleccionado,
    cerrarModalListaAutorizadosConfirm,
    confirmarModalAutorizacion,
    openCierrePagoModal,
    cancelarModalConfirmarCierre,
    confirmarCierrePagoModal,
    listadoConfirm,
    habilitadoCierrePago,
    listadoSeleccionado,
    idCicloSelected,
    onChangeSelectCiclo,
    perfiles,
    verificarConfirmarFomaPago,
    txtBusqueda,
    cicloSeleccionado,
  };
};

export default useFormaPagoRezagado;
