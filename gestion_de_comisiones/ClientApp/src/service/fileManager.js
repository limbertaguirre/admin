import ExcelJS from "exceljs";
import FileSaver from "file-saver";
import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";
import { formatearNumero } from "../lib/utility";

pdfMake.vfs = pdfFonts.pdfMake.vfs;

export const downloadPdfTable = ({ headers, data, widths, fileName }) => {
  let docDefinition = {
    pageSize: "LETTER",
    pageOrientation: "landscape",
    content: [
      {
        table: {
          styles: "tableExample",
          headerRows: 1,
          widths,
          body: [headers, ...data],
        },
      },
    ],
    styles: {
      header: {
        fontSize: 18,
        bold: true,
        margin: [0, 0, 0, 10],
      },
      subheader: {
        fontSize: 16,
        bold: true,
        margin: [0, 10, 0, 5],
      },
      tableExample: {
        margin: [0, 5, 0, 15],
      },
      tableHeader: {
        bold: true,
        fontSize: 13,
        color: "black",
      },
    },
  };
  pdfMake.createPdf(docDefinition).download(fileName);
};

export const downloadExcelTable = ({ headers, data, title, fileName }) => {
  const workBook = new ExcelJS.Workbook();
  const sheet = workBook.addWorksheet(title);
  data.unshift(headers);
  const newRows = sheet.addRows(data);
  const widths = getColumWidths(data).map((item) => ({ width: item }));
  sheet.columns = widths;
  sheet.eachRow((row) => {
    row.eachCell((cell) => {
      cell.border = {
        top: { style: "thin" },
        left: { style: "thin" },
        bottom: { style: "thin" },
        right: { style: "thin" },
      };
    });
  });
  workBook.xlsx.writeBuffer().then(function (data) {
    var blob = new Blob([data], {
      type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
    });
    FileSaver.saveAs(blob, fileName);
  });
};

const getColumWidths = (data) => {
  let objectMaxLength = [];
  for (let i = 0; i < data.length; i++) {
    let value = data[i];
    for (let j = 0; j < value.length; j++) {
      if (typeof value[j] === "number") {
        objectMaxLength[j] = 10;
      } else if (typeof value[j] === "string") {
        objectMaxLength[j] =
          objectMaxLength[j] >= value[j].length
            ? objectMaxLength[j]
            : value[j].length;
      } else {
        objectMaxLength[j] = objectMaxLength[j] >= 10 ? objectMaxLength[j] : 10;
      }
    }
  }
  return objectMaxLength;
};

export const getFreelancerExcel = ({
  headers,
  data,
  title,
  fileName,
  userModel,
  total,
}) => {
  const workBook = new ExcelJS.Workbook();
  const sheet = workBook.addWorksheet(title);
  data.unshift(headers);
  let headingRowsConfig = [
    [
      {
        richText: [
          { text: "Nombres: ", font: { bold: true } },
          { text: userModel.nombres },
        ],
      },
    ],
    [
      {
        richText: [
          { text: "Apellidos: ", font: { bold: true } },
          { text: userModel.apellidos },
        ],
      },
    ],
    [
      {
        richText: [
          { text: "CI: ", font: { bold: true } },
          { text: userModel.ci },
        ],
      },
    ],
    [
      {
        richText: [
          { text: "Email: ", font: { bold: true } },
          { text: userModel.correoElectronico },
        ],
      },
    ],
  ];
  let infoRows = sheet.addRows(headingRowsConfig);
  let dataRows = sheet.addRows(data);
  const widths = getColumWidths(data).map((item) => ({ width: item }));
  sheet.columns = widths;
  dataRows.forEach((row) => {
    row.eachCell((cell) => {
      cell.border = {
        top: { style: "thin" },
        left: { style: "thin" },
        bottom: { style: "thin" },
        right: { style: "thin" },
      };
    });
  });
  let totalRow = sheet.addRow(["", "", "", "TOTAL", total]);
  let startCellName = totalRow.getCell(1).address;
  let endCellName = totalRow.getCell(4).address;
  sheet.mergeCells(startCellName, endCellName);
  totalRow.getCell(1).value = "TOTAL";
  totalRow.getCell(1).style = {
    alignment: {
      horizontal: "right",
    },
    font: {
      bold: true,
    },
  };
  totalRow.eachCell((cell) => {
    cell.border = {
      top: { style: "thin" },
      left: { style: "thin" },
      bottom: { style: "thin" },
      right: { style: "thin" },
    };
  });
  workBook.xlsx.writeBuffer().then(function (data) {
    var blob = new Blob([data], {
      type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
    });
    FileSaver.saveAs(blob, fileName);
  });
};

export const downloadFreelancerPdf = ({
  headers,
  data,
  widths,
  fileName,
  userModel,
  total,
}) => {
  let docDefinition = {
    pageSize: "LETTER",
    pageOrientation: "landscape",
    content: [
      {
        text: [{ text: "Nombres: ", bold: true }, { text: userModel.nombres }],
      },
      {
        text: [
          { text: "Apellidos: ", bold: true },
          { text: userModel.apellidos },
        ],
      },
      {
        text: [{ text: "CI: ", bold: true }, { text: userModel.ci }],
      },
      {
        text: [
          { text: "Email: ", bold: true },
          { text: userModel.correoElectronico },
        ],
      },
      {
        table: {
          styles: "tableExample",
          headerRows: 1,
          widths,
          body: [
            headers,
            ...data,
            [
              {
                colSpan: 4,
                border: [true, true, true, true],
                text: "TOTAL",
                alignment: "right",
              },
              "",
              "",
              "",
              formatearNumero(total),
            ],
          ],
        },
      },
    ],
    styles: {
      header: {
        fontSize: 18,
        bold: true,
        margin: [0, 0, 0, 10],
      },
      subheader: {
        fontSize: 16,
        bold: true,
        margin: [0, 10, 0, 5],
      },
      tableExample: {
        margin: [0, 5, 0, 15],
      },
      tableHeader: {
        bold: true,
        fontSize: 13,
        color: "black",
      },
    },
  };
  pdfMake.createPdf(docDefinition).download(fileName);
};
