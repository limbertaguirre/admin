import { useHistory } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import { verificarAcceso } from "../lib/accesosPerfiles";
import { requestGet, requestPost } from "../service/request";
import * as permiso from "../routes/permiso";
import { useEffect, useState } from "react";
import { showMessage } from "../redux/actions/messageAction";

const usePagoRezagado = () => {
  let history = useHistory();
  const { location } = history;
  const dispatch = useDispatch();
  const { userName, idUsuario } = useSelector(
    (stateSelector) => stateSelector.load
  );
  const { perfiles } = useSelector((stateSelector) => stateSelector.home);
  const [statusBusqueda, setStatusBusqueda] = useState(false);
  const [txtBusqueda, setTxtBusqueda] = useState("");
  const [idComision, setIdComision] = useState(null);

  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [mensajeSnackbar, setMensajeSnackbar] = useState("");
  const [tipoSnackbar, settipTSnackbar] = useState(true);

  const [idCiclo, setIdCiclo] = useState(0);
  const [nameComboSeleccionado, setNameComboSeleccionado] = useState("");
  const [idCicloSelected, setIdCicloSelected] = useState(0);
  const [listCiclo, setListCiclo] = useState([]);

  const [listaComisionesAPagar, setListaComisionesAPagar] = useState([]);
  const [listaComisionPaginacionNueva, setListaComisionPaginacionNueva] =
    useState(false);
  const [empresasTransferencias, setEmpresasTransferencias] = useState([]);
  const [openTransferenciasDialog, setOpenTransferenciasDialog] =
    useState(false);

  const cargarCiclo = async (usuarioLogin) => {
    const url = "/gestionPagosRezagados/GetCiclos";
    let respuesta = await requestGet(url, { usuarioLogin }, dispatch);
    if (respuesta && respuesta.code == 0) {
      setListCiclo(respuesta.data);
    } else {
      dispatch(
        showMessage({
          message: respuesta.message,
          variant: "error",
        })
      );
    }
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

  const handleOnGetPagos = () => {
    if (idCiclo && idCiclo !== 0) {
      setIdCicloSelected(idCiclo);
      let objCicloSelected = listCiclo.find((item) => item.idCiclo === idCiclo);

      if (objCicloSelected) {
        setIdComision(objCicloSelected.idComision);
        cargarComisionesPagos(objCicloSelected.idComision, idCiclo);
      }
    } else {
      generarSnackBar(
        "¡Debe Seleccionar un ciclo para cargar las comisiones!",
        "warning"
      );
    }
  };

  async function cargarComisionesPagos(idComision, idCiclo) {
    let url = "gestionPagosRezagados/GetComisionesPagos";
    let respuesta = await requestPost(
      url,
      { idComision, idCiclo, usuarioLogin: userName },
      dispatch
    );
    if (respuesta && respuesta.code == 0) {
      setListaComisionesAPagar(respuesta.data);
      setListaComisionPaginacionNueva(true);
      setStatusBusqueda(true);
    } else {
      dispatch(
        showMessage({
          message: respuesta.message,
          variant: "error",
        })
      );
    }
  }
  //--navbar
  const closeSnackbar = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setOpenSnackbar(false);
  };
  const generarSnackBar = (mensaje, tipo) => {
    setOpenSnackbar(true);
    setMensajeSnackbar(mensaje);
    settipTSnackbar(tipo);
  };

  const selecionarDetalleFrelances = () => {};

  const seleccionarTipoFiltroBusqueda = (idTipoFormaPago) => {
    filtrarComisionPorFormaPago(idTipoFormaPago);
  };
  async function filtrarComisionPorFormaPago(idTipoFormaPago) {
    if (idCiclo && idCiclo !== 0) {
      let response = await listarFiltrada(
        userName,
        idCiclo,
        idTipoFormaPago,
        dispatch
      );

      if (response && response.code == 0) {
        let data = response.data;
        setListaComisionesAPagar(data.lista);
        setListaComisionPaginacionNueva(true);
      }
    } else {
      generarSnackBar(
        "¡Debe Seleccionar un ciclo para cargar las comisiones!",
        "warning"
      );
    }
  }
  const buscarFreelanzer = () => {
    if (txtBusqueda != "") {
      buscarFrelancerPorCi();
    } else {
      generarSnackBar("¡Introduzca carnet de identidad!", "info");
    }
  };

  async function buscarFrelancerPorCi() {
    let response = await buscarPorCarnetFormaPago(
      userName,
      idCiclo,
      txtBusqueda,
      dispatch
    );
    if (response && response.code == 0) {
      let data = response.data;
      setListaComisionesAPagar(data);
      setListaComisionPaginacionNueva(true);
    }
  }
  const [openModalConfirm, setOpenModalConfirm] = useState(false);
  const [subtitulo, setSubtitulo] = useState("");
  const abrirModal = () => {
    verificarConfirmarSionPay(userName, idCiclo);
    handleClose();
  };
  async function verificarConfirmarSionPay(userN, cicloId) {
    let response = await verificarPagoSionPayXCiclo(userN, cicloId, dispatch);
    if (response && response.code == 0) {
      var body = response.data;
      setOpenModalConfirm(true);
      setSubtitulo(
        "Se pagará a " +
          body.cantidad +
          " ACI con un monto total de " +
          body.totalPagoSionPay.toLocaleString("de-DE", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
          }) +
          " Puntos."
      );
    } else {
      dispatch(
        showMessage({
          message: response.message,
          variant: "error",
        })
      );
    }
  }

  const CloseModalConfirmacion = () => {
    setOpenModalConfirm(false);
  };
  const confirmarModal = () => {
    if (idCiclo && idCiclo !== 0) {
      prosesarPagoSionPay(userName, idUsuario, idCiclo);
    } else {
      generarSnackBar("¡Debe seleccionar un ciclo para el cierre", "info");
    }
  };
  async function prosesarPagoSionPay(userN, usuarioId, cicloId) {
    let response = await pagarComisionSionPay(
      userN,
      usuarioId,
      cicloId,
      dispatch
    );
    if (response && response.code == 0) {
      setOpenModalConfirm(false);
      dispatch(
        showMessage({
          message: response.message,
          variant: "success",
        })
      );
      handleOnGetPagos();
    } else {
      dispatch(
        showMessage({
          message: response.message,
          variant: "error",
        })
      );
    }
  }

  const handleClickOpenTransferencias = () => {
    handleTransferenciasEmpresas(userName);
    handleClose();
  };

  const handleCloseTransferencias = () => {
    setOpenTransferenciasDialog(false);
  };

  const handleTransferenciasEmpresas = async (user) => {
    if (idCiclo && idCiclo !== 0) {
      let url = "/gestionPagosRezagados/handleTransferenciasEmpresas";
      const objCiclo = listCiclo.find((item) => item.idCiclo === idCiclo);
      if (objCiclo) {
        let response = await requestPost(
          url,
          {
            usuarioLogin: user,
            idCiclo: objCiclo.idCiclo,
            idComision: objCiclo.idComision,
          },
          dispatch
        );
        if (response && response.code == 0) {
          setEmpresasTransferencias(response.data);
          setOpenTransferenciasDialog(true);
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
  };

  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);

  const handlePages = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const confirmarCierrePagos = () => {
    ApiVerificarConfirmarCierrePago(userName, idUsuario, idCiclo);
  };
  async function ApiVerificarConfirmarCierrePago(userNa, idUser, idCICLO) {
    if (idCICLO && idCICLO !== 0) {
      let response = await confirmarCierrePago(
        userNa,
        idUser,
        idCICLO,
        dispatch
      );
      if (response && response.code == 0) {
        dispatch(
          showMessage({
            message: response.message,
            variant: "success",
          })
        );
        inicializarListaPagos();
        cargarCiclo(userName);
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

  const handleTransferenciasEmpresasAction = async () => {};
  const obtenerCiclosPagos = async () => {};
  const obtenerComisionesPagos = async () => {};
  const listarFiltrada = async () => {};
  const buscarPorCarnetFormaPago = async () => {};
  const verificarPagoSionPayXCiclo = async () => {};
  const pagarComisionSionPay = async () => {};
  const confirmarCierrePago = async () => {};

  const mensajeGenericoCiclo = () => {
    setOpenSnackbar(true);
    setMensajeSnackbar("¡Debe Seleccionar un ciclo!");
    settipTSnackbar("warning");
  };

  const inicializarListaPagos = () => {
    setListaComisionesAPagar([]);
    setStatusBusqueda(false);
    setTxtBusqueda("");
    setIdCicloSelected(0);
    setListCiclo([]);
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
    cargarCiclo(userName);
  }, []);

  return {
    idCiclo,
    openTransferenciasDialog,
    handleCloseTransferencias,
    empresasTransferencias,
    handleOnGetPagos,
    statusBusqueda,
    perfiles,
    permiso,
    confirmarCierrePagos,
    txtBusqueda,
    onChangeSelectCiclo,
    buscarFreelanzer,
    listCiclo,
    handlePages,
    handleClose,
    abrirModal,
    handleClickOpenTransferencias,
    openSnackbar,
    closeSnackbar,
    tipoSnackbar,
    mensajeSnackbar,
    listaComisionesAPagar,
    listaComisionPaginacionNueva,
    setListaComisionPaginacionNueva,
    selecionarDetalleFrelances,
    seleccionarTipoFiltroBusqueda,
    openModalConfirm,
    nameComboSeleccionado,
    subtitulo,
    confirmarModal,
    CloseModalConfirmacion,
    open,
    seleccionarNombreCombo,
    anchorEl,
    idComision,
  };
};

export default usePagoRezagado;
