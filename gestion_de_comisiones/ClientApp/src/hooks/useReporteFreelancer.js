import { useState } from "react";
import { apiComerce } from "../service/gestorproyect";
import { requestGet, requestPost } from "../service/request";
import {
  downloadExcelTable,
  downloadFreelancerPdf,
  getFreelancerExcel,
} from "../service/fileManager";
import { format } from "date-fns";
import { useDispatch } from "react-redux";
import { formatearNumero } from "../lib/utility";

const useReporteFreelancer = () => {
  const dispatch = useDispatch();
  const [query, setQuery] = useState(null);
  const [clients, setClients] = useState([]);
  const [exportAnchorEl, setExportAnchorEl] = useState(null);
  const [selectedClient, setSelectedClient] = useState(null);
  const [items, setItems] = useState([]);
  const [detailItems, setDetailItems] = useState([]);
  const [detailId, setDetailId] = useState(null);
  const [openAutocomplete, setOpenAutocomplete] = useState(false);
  const [open, setOpen] = useState(false);
  const [loadingAutcomplete, setLoadingAutocomplete] = useState(false);

  const onClickExportButton = (e) => setExportAnchorEl(e.currentTarget);

  const closeExportMenu = () => setExportAnchorEl(null);

  const openAutocompleteInput = () => setOpenAutocomplete(true);

  const closeAutocompleteInput = () => setOpenAutocomplete(false);

  const startLoadingAutocomplete = () => setLoadingAutocomplete(true);

  const stopLoadingAutocomplete = () => setLoadingAutocomplete(false);

  const getDetailReportInfo = async (idComisionDetalle) => {
    if (idComisionDetalle === detailId) {
      setOpen(true);
    } else {
      let url = "/Reporte/obtenerReporteDetalleCiclo";
      let data = { idComisionDetalle };
      try {
        let response = await requestPost(url, data, dispatch);
        if (response && response.code === 0) {
          setDetailId(idComisionDetalle);
          setDetailItems(response.data);
          setOpen(true);
        }
      } catch (error) {}
    }
  };

  const getReportInfo = async () => {
    let url = "/Reporte/obtenerReportePorFreelancer";
    let data = { idFicha: selectedClient.idFicha };
    try {
      let response = await requestPost(url, data, dispatch);
      if (response && response.code === 0) {
        setItems(response.data);
      }
    } catch (error) {}
  };

  const searchFreelancer = async (text) => {
    let url = "/Reporte/buscarFreelancerPorNombre";
    const token = localStorage.getItem("token");
    const config = {
      headers: { "Content-Type": "application/json", Authorization: token },
    };
    let data = { query: text };
    try {
      startLoadingAutocomplete();
      let res = await apiComerce.post(url, data, config);
      let response = res.data;
      if (response && response.code === 0) {
        setClients(response.data);
      }
      stopLoadingAutocomplete();
    } catch (error) {
      stopLoadingAutocomplete();
    }
  };

  const montoTotal = items.reduce((total, item) => total + item.montoNeto, 0);

  const downloadReportPdf = () =>
    downloadFreelancerPdf({
      headers: [
        "CICLO",
        "TIPO DE PAGO",
        "CUENTA SIONPAY",
        "CUENTA DE BANCO",
        "MONTO",
      ],
      data: items.map((item) => [
        item.ciclo,
        item.tipoPago,
        item.nroCuenta,
        item.cuentaBancaria,
        formatearNumero(item.montoNeto),
      ]),
      total: montoTotal,
      userModel: selectedClient,
      widths: ["*", "auto", "auto", "auto", "auto"],
      fileName: `reporte-freelancer-${format(
        new Date(),
        "yyyy-MM-dd-HH-mm-ss"
      )}.pdf`,
    });

  const downloadReportExcel = () =>
    getFreelancerExcel({
      headers: [
        "CICLO",
        "TIPO DE PAGO",
        "CUENTA SIONPAY",
        "CUENTA DE BANCO",
        "MONTO",
      ],
      data: items.map((item) => [
        item.ciclo,
        item.tipoPago,
        item.nroCuenta,
        item.cuentaBancaria,
        item.montoNeto,
      ]),
      userModel: selectedClient,
      fileName: `reporte-freelancer-${format(
        new Date(),
        "yyyy-MM-dd-HH-mm-ss"
      )}.xlsx`,
      total: montoTotal,
    });

  const closeDialog = () => setOpen(false);

  const selectedDetailItemData = detailId
    ? items.find((item) => item.idComisionDetalle === detailId)
    : null;

  return {
    clients,
    selectedClient,
    setSelectedClient,
    items,
    detailItems,
    open,
    closeDialog,
    montoTotal,
    getDetailReportInfo,
    getReportInfo,
    searchFreelancer,
    selectedDetailItemData,
    openAutocompleteInput,
    closeAutocompleteInput,
    openAutocomplete,
    loadingAutcomplete,
    downloadReportExcel,
    downloadReportPdf,
    onClickExportButton,
    closeExportMenu,
    exportAnchorEl,
  };
};

export default useReporteFreelancer;
