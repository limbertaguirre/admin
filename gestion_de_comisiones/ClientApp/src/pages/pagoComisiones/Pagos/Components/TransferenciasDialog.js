import { Container, Tooltip, Zoom, Chip, InputAdornment, Card, Button, Grid, TextField, Typography, FormControl, InputLabel, MenuItem, Breadcrumbs} from "@material-ui/core";
import { KeyboardDatePicker,MuiPickersUtilsProvider } from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import esLocale from "date-fns/locale/es";
import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { makeStyles } from "@material-ui/core/styles";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogTitle from "@material-ui/core/DialogTitle";
import * as Actions from "../../../../redux/actions/PagosGestorAction";
import * as ActionMensaje from "../../../../redux/actions/messageAction";
import GridTransferenciaModal from '../Components/GridTransferencia'
import PagosTransferenciaDetalleDialog from '../Components/PagosTransferenciaDetalleDialog'
import MessageConfirm from '../../../../components/mesageModal/MessageConfirm'

const useStyles = makeStyles((theme) => ({
  appBar: {
    position: "relative",
  },
  title: {
    textAlign: "center",
    flex: 1,
  },
  textoProducto: {
    fontSize: "12px",
  },
  dialgoTitle: {
    // backgroundColor: theme.palette.primary.main,
    backgroundColor: "#1872b8",
    color: "white",
    "& .MuiTypography-root": {
      color: "white",
    },
  },
  descriptionText: {
    color: "#212121",
  },
  businessSelect: {
    flex: 1,
  },
  dialogConfirmButton: {
    color: theme.palette.primary.main,
  },
  downloadButton: {
    display: "flex",
    verticalAlign: "center",
  },
  dialogContainer: {
    "& .MuiPaper-root": {
      [theme.breakpoints.down("lg")]: {
        minWidth: "740px",
      },
      [theme.breakpoints.down("md")]: {
        minWidth: "500px",
      },
      [theme.breakpoints.down("xs")]: {
        minWidth: "350px",
      },
    },
  },
  btnContainerDescargar: {
    display: "flex",
    flex: 2,
    flexDirection: "column",
    alignContent: "center",
    alignItems: "flex-end",
    justifyContent: "center",
  },
  borderContainer: {
    border: "1px solid #c6c8cb",
    borderRadius: "8px",
    paddingTop: "32px",
    paddingBottom: "16px",
    paddingRight: "24px",
    paddingLeft: "24px",
  },
  alturaGrid:{
    height:'160px'
  }
}));

const TransferenciasDialog = ({
  cicloId,
  openDialog,
  closeTransferenciasDialog,
  empresas,
}) => {
    const style = useStyles();
    const dispatch = useDispatch();
    const {userName, idUsuario} =useSelector((stateSelector)=>{ return stateSelector.load});
    const [empresaId, setEmpresaId] = useState(-1);
    const [openModalFullScreen, setOpenModalFullScreen] = useState(false);
    const [list, setList] = useState([]);
  const [enabledDownloadInput, setEnabledDownloadInput] = useState(false);
  const [enabledConfirmarTodosInput, setEnabledConfirmarTodosInput] = useState(false);
  const [enabledConfirmarSeleccionInput, setEnabledConfirmarSeleccionInput] = useState(false);
  const [enabledDatePickerInput, setEnabledDatePickerInput] = useState(false);
  const [selectedDate, handleDateChange] = useState(new Date());
  const [detalleTransferencia, setDetalleTransferencia] = useState(null);
  const [openPagosTransferenciaDetalleDialog, setOpenPagosTransferenciaDetalleDialog] = useState(false);
  const [openModalCancel, setOpenModalCancel]= useState(false);
  const [isConfirmDialogType, setIsConfirmDialogType]= useState(false);
  const [selected, setSelected] = React.useState([]);
  const [transferenciaSeleccionData, setTransferenciaSeleccionData] = React.useState(null);

  const downloadExcel = (base64, fileName) => {
    // const contentType = "application/vnd.ms-excel";
    const contentType =
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    const str = base64;
    const byteCharacters = atob(str);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], {
      type: contentType,
    });
    const objectURL = window.URL.createObjectURL(blob);
    const anchor = document.createElement("a");
    anchor.href = objectURL;
    anchor.target = "_blank";
    anchor.download = fileName;
    anchor.click();

    URL.revokeObjectURL(objectURL);
  };

  const closeFullScreenModal = () => {
    setOpenModalFullScreen(false);
    if(userName && empresaId) {
      handleVerificarPagosTransferenciasTodos(userName, empresaId);
    }
  };

  const handleDownloadFileEmpresas = async (user, empresaId) => {
    if (
      cicloId &&
      cicloId !== 0 &&
      empresaId &&
      empresaId != -1 &&
      selectedDate
    ) {
      let response = await Actions.handleDownloadFileEmpresas(
        user,
        cicloId,
        empresaId,
        selectedDate,
        dispatch
      );
      console.log(
        "TransferenciasDialog.js handleDownloadFileEmpresas ",
        response
      );
      if (response && response.code == 0) {
        downloadExcel(response.data.file, response.data.fileName);
        handleVerificarPagosTransferenciasTodos(userName, empresaId);
      } else {
        dispatch(
          ActionMensaje.showMessage({
            message: response.message,
            variant: "error",
          })
        );
      }
    }
  };

  const handleEmpresasSelectChange = (event) => {
    setEmpresaId(event.target.value);
    console.log("handleEmpresasSelectChange ", event.target.value);
    handleVerificarPagosTransferenciasTodos(userName, event.target.value);
  };  

  const handleObtenerPagosTransferencias = async (user, empresaId) =>{
    if(cicloId && cicloId !== 0 && empresaId && empresaId != -1) {  
      let response = await Actions.handleObtenerPagosTransferencias(user, cicloId, empresaId, dispatch);   
      console.log('TransferenciasDialog.js handleObtenerPagosTransferencias ', response);   
      if(!response || !response.data) {
        dispatch(ActionMensaje.showMessage({
            message: "Hubo inconvenientes al recuperar la información. Intente nuevamente por favor.",
            variant: "error"}));
        return;
      }
      if(response.code == 0) { 
        setTransferenciaSeleccionData(response.data);
        setList(response.data.list);
        seleccionarTodo(response.data.list);
        setOpenModalFullScreen(true);      
      } else {
        dispatch(
          ActionMensaje.showMessage({
            message: response.message,
            variant: "error",
          })
        );
      }
    }
  };

  const handleConfirmarPagosTransferenciasTodos = async (user, empresaId) => {
    if (cicloId && cicloId !== 0 && empresaId && empresaId != -1) {
      let response = await Actions.handleConfirmarPagosTransferenciasTodos(
        user,
        cicloId,
        empresaId,
        dispatch
      );
      console.log(
        "TransferenciasDialog.js handleConfirmarPagosTransferenciasTodos response ",
        response
      );
      if (response && response.code == 0) {
        dispatch(
          ActionMensaje.showMessage({
            message: response.message,
            variant: "info",
          })
        );
        setEnabledConfirmarTodosInput(false);
        setEnabledConfirmarSeleccionInput(false);
      } else {
        dispatch(
          ActionMensaje.showMessage({
            message: response.message,
            variant: "error",
          })
        );
        setEnabledConfirmarTodosInput(true);
        setEnabledConfirmarSeleccionInput(true);
      }
    } else {
      console.log('handleConfirmarPagosTransferenciasTodos empresaId undefined ', empresaId);
    }
  };

  const handleVerificarPagosTransferenciasTodos = async (user, empresaId) => {

    const responseCodes = {
      NO_EXISTEN_PENDIENTES_NI_RECHAZADOS: 0,
      EXISTEN_PENDIENTES: 2,
      EXISTEN_RECHAZADOS: 3
    };

    if (cicloId && cicloId !== 0 && empresaId && empresaId != -1) {
      let response = await Actions.handleVerificarPagosTransferenciasTodos(
        user,
        cicloId,
        empresaId,
        dispatch
      );
      console.log(
        "TransferenciasDialog.js handleVerificarPagosTransferenciasTodos response ",
        response
      );
      let data = {
        ...response.data,
        message: response.message
      }
      handleDateChange(new Date());
      if (response && response.code == responseCodes.NO_EXISTEN_PENDIENTES_NI_RECHAZADOS) {
        dispatch(
          ActionMensaje.showMessage({
            message: response.message,
            variant: "info",
          })
        );        
        setInputs(false);
        setDetalleTransferencia(data);
        setOpenPagosTransferenciaDetalleDialog(true);
        setIsConfirmDialogType(false);
      } else if (response && response.code == responseCodes.EXISTEN_PENDIENTES) {
        if(!data.descargarExcel) {
          setInputs(false);
          setEnabledDownloadInput(true);
          setEnabledDatePickerInput(true);
        } else {
          let d = data.descargarExcel.split('/');
          let s = ''.concat(d[1],'/', d[0],'/', d[2]);
          console.log('FECHA d ', d);
          console.log('FECHA s ', s);
          handleDateChange(s);
          setInputs(true);
          setEnabledDownloadInput(false);
          setEnabledDatePickerInput(false);
        }
        setDetalleTransferencia(data);
        setIsConfirmDialogType(true);
      } else if (response && response.code == responseCodes.EXISTEN_RECHAZADOS) {
        setInputs(false);
        setDetalleTransferencia(data);
        setOpenPagosTransferenciaDetalleDialog(true);        
        setIsConfirmDialogType(false);
      } else {
        dispatch(
          ActionMensaje.showMessage({
            message: response.message,
            variant: "error",
          })
        );
        setInputs(false);
      }
    }
  };
  
  const setInputs = b => {
    setEnabledConfirmarTodosInput(b);
    setEnabledDownloadInput(b);
    setEnabledConfirmarSeleccionInput(b);
    setEnabledDatePickerInput(b);
  };

  const handleCloseTransferenciasDialog = () => {
    setInputs(false);
    setEmpresaId(-1);
    closeTransferenciasDialog();
  }

  const handleClosePagosTransferenciaDetalleDialog = () => {
    setOpenPagosTransferenciaDetalleDialog(false);
  }
  
  const seleccionarTodo = (data = null) => {
    let conteo = data ? data : list;
    const newSelecteds = conteo.map((n) => n.idComisionDetalleEmpresa);
    setSelected(newSelecteds);
  };

  return (
    <Dialog
      open={openDialog}
      fullWidth={false}
      maxWidth="md"
      className={style.dialogContainer}
      onClose={() => closeTransferenciasDialog()}
    >
      <DialogTitle className={style.dialgoTitle}>Transferencia</DialogTitle>

      <DialogContent>
        <DialogContentText>
          <Typography
            variant="body1"
            component="h2"
            className={style.descriptionText}
          >
            Selecciona la empresa a realizar la transferencia para generar el
            archivo.
          </Typography>
        </DialogContentText>
        {/* <div className="d-flex flex-row align-items-center"> */}
        <div className={style.borderContainer}>
          <Grid
            container
            alignItems="center"
            justifyContent="center"
            alignContent="center"
            spacing={1}
            flex="1"
            direction="row"
          >
            <Grid item xs={6} sm={6}  className={style.alturaGrid}>              
              <TextField
                className={style.businessSelect}
                id="outlined-select-currency"
                select
                label="Seleccione una empresa"
                value={empresaId ? empresaId : -1}
                onChange={handleEmpresasSelectChange}
                helperText="El archivo se generará a partir de la empresa seleccionada.<br/>"
                variant="outlined"
                fullWidth
              >
                <MenuItem value={-1}>Seleccione una empresa</MenuItem>
                {empresas.length > 0 &&
                  empresas.map((x, i) => {
                    return <MenuItem value={x.idEmpresa}>{x.empresa}</MenuItem>;
                  })}
              </TextField>
            </Grid>            
            <Grid item xs={6} sm={6}  className={style.alturaGrid}>              
              <MuiPickersUtilsProvider locale={esLocale} utils={DateFnsUtils}>
                <KeyboardDatePicker
                  autoOk
                  disabled={!enabledDatePickerInput}
                  variant="inline"
                  inputVariant="outlined"
                  label="Seleccione una fecha"
                  format="dd/MM/yyyy"
                  minDate={new Date()}
                  helperText="Fecha en la que el banco debe realizar el pago (Esta fecha aparecerá en el archivo excel)."
                  value={selectedDate}
                  InputAdornmentProps={{ position: "start" }}
                  onChange={(date) => handleDateChange(date)}
                />
              </MuiPickersUtilsProvider>
            </Grid>
            <Grid item xs={12} sm={12} className={style.btnContainerDescargar}>
              <Button
                disabled={!enabledDownloadInput}
                className={style.downloadButton}
                variant="outlined"
                color="primary"
                onClick={() => handleDownloadFileEmpresas(userName, empresaId)}
              >
                Descargar
              </Button>
            </Grid>
          </Grid>
          </div>
            {/* </Container> */}
        </DialogContent>
        <DialogActions>
            <Button disabled={!enabledConfirmarSeleccionInput} className={style.dialogConfirmButton} onClick={() => setOpenPagosTransferenciaDetalleDialog(true)/*setOpenModalCancel(true)/*setOpenModalCancel(true)*/}>Confimar todos</Button>
            <Button disabled={!enabledConfirmarSeleccionInput} className={style.dialogConfirmButton} onClick={()=>handleObtenerPagosTransferencias(userName, empresaId)}>Confirmar seleccion</Button>
            <Button className={style.dialogConfirmButton} onClick={()=>handleCloseTransferenciasDialog()}>Cerrar</Button>
        </DialogActions>         
        {transferenciaSeleccionData && (<GridTransferenciaModal
          idCiclo={cicloId}
          list={list}
          data={transferenciaSeleccionData}
          empresaId={empresaId}
          openModalFullScreen={openModalFullScreen}
          closeFullScreenModal={closeFullScreenModal}
          seleccionarTodo={seleccionarTodo}
          selected={selected}
          setSelected={setSelected}
        />)}
        {detalleTransferencia && empresaId && (
          <PagosTransferenciaDetalleDialog
            isConfirm={isConfirmDialogType}
            open={openPagosTransferenciaDetalleDialog}
            empresaId={empresaId}
            handleConfirm={handleConfirmarPagosTransferenciasTodos}
            close={handleClosePagosTransferenciaDetalleDialog}
            data={detalleTransferencia}
          />
        )}
      </Dialog>
    );
}

export default TransferenciasDialog;
