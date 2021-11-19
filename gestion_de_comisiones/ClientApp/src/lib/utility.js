export const TIPO_APLICACION_OTROS_ID= 4;

export const formatearNumero = (
    numero,
    cantidadDecimales = 2,
    separadorMiles = ".",
    separadorDecimales = ","
  ) => {
    numero = (numero + "").replace(/[^0-9+\-Ee.]/g, "");
    const n = !isFinite(+numero) ? 0 : +numero;
    const prec = !isFinite(+cantidadDecimales) ? 0 : Math.abs(cantidadDecimales);
    const sep = typeof separadorMiles === "undefined" ? "," : separadorMiles;
    const dec =
      typeof separadorDecimales === "undefined" ? "." : separadorDecimales;
    let s = "";
    const toFixedFix = function (n, prec) {
      if (("" + n).indexOf("e") === -1) {
        return +(Math.round(n + "e+" + prec) + "e-" + prec);
      } else {
        const arr = ("" + n).split("e");
        let sig = "";
        if (+arr[1] + prec > 0) {
          sig = "+";
        }
        return (+(
          Math.round(+arr[0] + "e" + sig + (+arr[1] + prec)) +
          "e-" +
          prec
        )).toFixed(prec);
      }
    };
    s = (prec ? toFixedFix(n, prec).toString() : "" + Math.round(n)).split(".");
    if (s[0].length > 3) {
      s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
    }
    if ((s[1] || "").length < prec) {
      s[1] = s[1] || "";
      s[1] += new Array(prec - s[1].length + 1).join("0");
    }
    return s.join(dec);
  };