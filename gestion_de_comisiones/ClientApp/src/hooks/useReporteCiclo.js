import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { requestGet, requestPost } from "../service/request";
import { downloadExcelTable, downloadPdfTable } from "../service/fileManager";
import { format } from "date-fns";

const useReporteCiclo = () => {
  const dispatch = useDispatch();
  const { userName: usuarioLogin } = useSelector((state) => state.load);
  // DATA STATE
  const [ciclo, setCiclo] = useState(null);
  const [detailId, setDetailId] = useState(null);
  const [items, setItems] = useState([]);
  const [ciclos, setCiclos] = useState([]);
  const [detailItems, setDetailItems] = useState([]);
  //UI SATE
  const [open, setOpen] = useState(false);

  const downloadPdfReport = () => {
    if (items.length > 0) {
      downloadPdfTable({
        headers: [
          "NOMBRE",
          "DOC. IDENTIDAD",
          "CUENTA DE BANCO",
          "CUENTA SIONPAY",
          "MONTO",
          "TIPO DE PAGO",
        ],
        data: items.map((item) => [
          item.nombres + " " + item.apellidos,
          item.ci,
          item.cuentaBancaria,
          item.nroCuenta,
          item.montoNeto,
          item.tipoPago,
        ]),
        widths: ["*", "auto", "auto", "auto", "auto", "auto"],
        fileName: `reporte-ciclos-${format(
          new Date(),
          "yyyy-MM-dd-HH-mm-ss"
        )}.pdf`,
      });
    }
  };

  const downloadExcelReport = () => {
    if (items.length > 0) {
      downloadExcelTable({
        headers: [
          "NOMBRE",
          "DOC. IDENTIDAD",
          "CUENTA DE BANCO",
          "CUENTA SIONPAY",
          "MONTO",
          "TIPO DE PAGO",
        ],
        data: items.map((item) => [
          item.nombres + " " + item.apellidos,
          item.ci,
          item.cuentaBancaria,
          item.nroCuenta,
          item.montoNeto,
          item.tipoPago,
        ]),
        title: ciclo.nombre,
        fileName: `reporte-ciclo-${format(
          new Date(),
          "yyyy-MM-dd-HH-mm-ss"
        )}.xlsx`,
      });
    }
  };

  const getCiclos = async () => {
    let url = "/GestionPagos/getCiclos";
    try {
      let response = await requestGet(url, { usuarioLogin }, dispatch);
      if (response && response.code === 0) {
        setCiclos(response.data);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const getReportInfo = async () => {
    if (ciclo) {
      let url = "/Reporte/obtenerReporteCiclo";
      let data = { idCiclo: ciclo };
      try {
        let response = await requestPost(url, data, dispatch);
        console.log(response);
        if (response && response.code === 0) {
          setItems(response.data);
        }
      } catch (error) {}
    }
  };

  const closeDialog = () => setOpen(false);

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

  const getSumAttr = (paymentType = null) => {
    if (paymentType) {
      return items
        .filter((item) => item.idTipoPago === paymentType)
        .reduce((total, item) => total + item.montoNeto, 0);
    } else {
      return items.reduce((total, item) => total + item.montoNeto, 0);
    }
  };

  const totalSionPay = getSumAttr(1);
  const totalTransferencia = getSumAttr(2);

  const selectedDetailItemData = detailId
    ? items.find((item) => item.idComisionDetalle === detailId)
    : null;

  useEffect(() => {
    getCiclos();
  }, []);

  return {
    ciclo,
    setCiclo,
    items,
    ciclos,
    detailItems,
    getDetailReportInfo,
    getReportInfo,
    totalSionPay,
    totalTransferencia,
    closeDialog,
    open,
    selectedDetailItemData,
    downloadPdfReport,
    downloadExcelReport,
  };
};

export default useReporteCiclo;
