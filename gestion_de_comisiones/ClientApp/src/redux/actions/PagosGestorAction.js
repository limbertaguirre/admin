import { requestGet, requestPost } from "../../service/request";
import * as Action from "./messageAction";

export async function ObtenerCiclosPagos(userName, dispatch) {
  return new Promise((resolve) => {
    const data = {
      usuarioLogin: userName,
    };
    requestGet("gestionPagos/GetCiclos", data, dispatch).then((response) => {
      // console.log('ger list :', response);
      resolve(response);
    });
  });
}

export async function ObtenerComisionesPagos(userName, cicloId, dispatch) {
  return new Promise((resolve) => {
    const data = {
      usuarioLogin: userName,
      idCiclo: cicloId,
    };
    requestPost("gestionPagos/GetComisionesPagos", data, dispatch).then(
      (response) => {
        resolve(response);
      }
    );
  });
}

export async function getFiltroFormaPagoDisponibles(
  userName,
  idCiclo,
  dispatch
) {
  return new Promise((resolve) => {
    const data = {
      usuarioLogin: userName,
      idCiclo: idCiclo,
    };
    requestPost(
      "gestionPagos/GetFiltroFormaPagosDisponibles",
      data,
      dispatch
    ).then((response) => {
      // console.log('respuesta api :', response);
      resolve(response);
    });
  });
}
export async function ListarFiltrada(userName, idCiclo, idTipoPago, dispatch) {
  return new Promise((resolve) => {
    const data = {
      usuarioLogin: userName,
      idCiclo: idCiclo,
      idTipoPago: parseInt(idTipoPago),
    };
    // console.log('data : ', data);
    requestPost("gestionPagos/FiltrarComisionPagoPorTipoPago", data, dispatch).then(
      (response) => {
        resolve(response);
      }
    );
  });
}

export async function buscarPorCarnetFormaPago(
  userName,
  idCiclo,
  criterio,
  dispatch
) {
  return new Promise((resolve) => {
    const data = {
      usuarioLogin: userName,
      idCiclo: parseInt(idCiclo),
      nombreCriterio: criterio,
    };
    //console.log('llego a', data);
    requestPost(
      "gestionPagos/BuscarComisionCarnetFormaPago",
      data,
      dispatch
    ).then((response) => {
      resolve(response);
    });
  });
}
export async function pagarComisionSionPay(
  userName,
  usuarioId,
  idCiclo,
  dispatch
) {
  return new Promise((resolve) => {
    const data = {
      usuarioLogin: userName,
      idUsuario: parseInt(usuarioId),
      idCiclo: parseInt(idCiclo),
    };
    //console.log('llego a', data);
    requestPost("gestionPagos/PagarComisionSionPay", data, dispatch).then(
      (response) => {
        resolve(response);
      }
    );
  });
}

export async function verificarPagoSionPayXCiclo(userName, idCiclo, dispatch) {
  return new Promise((resolve) => {
    const data = {
      usuarioLogin: userName,
      idCiclo: parseInt(idCiclo),
    };
    //console.log('llego a', data);
    requestPost(
      "gestionPagos/VerificarPagosSionPayFormaPagoCiclo",
      data,
      dispatch
    ).then((response) => {
      resolve(response);
    });
  });
}

export async function handleTransferenciasEmpresas(user, cicloId, dispatch) {
  return new Promise((resolve) => {
    const data = {
      usuarioLogin: user,
      idCiclo: cicloId,
      tipoPagoId: 1
    };
    // console.log('handleTransferenciasEmpresas data: ', data);
    requestPost(
      "gestionPagos/handleTransferenciasEmpresas",
      data,
      dispatch
    ).then((response) => {
      resolve(response);
    });
  });
}

export async function handleDownloadFileEmpresas(
  user,
  cicloId,
  empresaId,
  date,
  dispatch
) {
  return new Promise((resolve) => {
    const data = {
      user,
      cicloId,
      empresaId,
      date
    };
    
    requestPost("gestionPagos/handleDownloadFileEmpresas", data, dispatch).then(
      (response) => {
        resolve(response);
      }
    );
  });
}

export async function handleObtenerPagosTransferencias(
  user,
  cicloId,
  empresaId,
  dispatch
) {
  return new Promise((resolve) => {
    const data = {
      user,
      cicloId,
      empresaId
    };
    
    requestPost("gestionPagos/handleObtenerPagosTransferencias", data, dispatch).then(
      (response) => {
        resolve(response);
      }
    );
  });
}

export async function handleConfirmarPagosTransferenciasTodos(
  user,
  cicloId,
  empresaId,
  dispatch
) {
  return new Promise((resolve) => {
    const data = {
      user: user,
      cicloId: cicloId,
      empresaId: empresaId
    };
    
    requestPost(
      "gestionPagos/handleConfirmarPagosTransferenciasTodos",
      data,
      dispatch
    ).then((response) => {
      resolve(response);
    });
  });
}

export async function handleConfirmarPagosTransferencias(
  userName,
  usuarioId,
  idCiclo,
  lista,
  empresaId,
  dispatch
) {
  return new Promise((resolve) => {
    const data = {
      user: userName,
      idUsuario: parseInt(usuarioId),
      cicloId: parseInt(idCiclo),
      confirmados:lista,
      empresaId:empresaId
    };    
    requestPost("gestionPagos/handleConfirmarPagosTransferencias", data, dispatch).then(
      (response) => {
        resolve(response);
      });
  });
}

export async function handleVerificarPagosTransferenciasTodos(
  user,
  cicloId,
  empresaId,
  dispatch
) {
  return new Promise((resolve) => {
    const data = {
      user: user,
      cicloId: cicloId,
      empresaId: empresaId
    };
    
    requestPost(
      "gestionPagos/handleVerificarPagosTransferenciasTodos",
      data,
      dispatch
    ).then((response) => {
      resolve(response);
    });
  });
}
