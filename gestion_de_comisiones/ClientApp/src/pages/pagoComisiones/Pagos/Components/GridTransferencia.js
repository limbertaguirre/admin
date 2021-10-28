import React, * as GeneralReact from "react";
import * as Redux from "react-redux";
import PropTypes from 'prop-types';
import * as Core from "@material-ui/core";
import * as CoreStyles from "@material-ui/core/styles";
import * as GeneralIcons from "@material-ui/icons";

// const GeneralStyles = GeneralReact.withStyles({
//   paper: { border: "1px solid #d3d4d5" },
// })((props) => (
//   <Core.Menu
//     elevation={0}
//     getContentAnchorEl={null}
//     anchorOrigin={{
//       vertical: "bottom",
//       horizontal: "center",
//     }}
//     transformOrigin={{
//       vertical: "top",
//       horizontal: "center",
//     }}
//     {...props}
//   />
// ));

function createData(name, calories, fat, carbs, protein, price) {
  return {
    name,
    calories,
    fat,
    carbs,
    protein,
    price,
    history: [
      {
        date: "2020-01-05",
        customerId: "11091700",
        amount: 3,
      },
      {
        date: "2020-01-02",
        customerId: "Anonymous",
        amount: 1,
      },
    ],
  };
}

function Row(props) {
  const { row } = props;
  const [open, setOpen] = GeneralReact.useState(false);

  return (
    <React.Fragment>
      <Core.TableRow sx={{ "& > *": { borderBottom: "unset" } }}>
        <Core.TableCell>
          <Core.IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <GeneralIcons.KeyboardArrowUp  /> : <GeneralIcons.KeyboardArrowDown />}
          </Core.IconButton>
        </Core.TableCell>
        <Core.TableCell component="th" scope="row">
          {row.name}
        </Core.TableCell>
        <Core.TableCell align="right">{row.calories}</Core.TableCell>
        <Core.TableCell align="right">{row.fat}</Core.TableCell>
        <Core.TableCell align="right">{row.carbs}</Core.TableCell>
        <Core.TableCell align="right">{row.protein}</Core.TableCell>
      </Core.TableRow>
      <Core.TableRow>
        <Core.TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Core.Collapse in={open} timeout="auto" unmountOnExit>
            <Core.Box sx={{ margin: 1 }}>
              <Core.Typography variant="h6" gutterBottom component="div">
                History
              </Core.Typography>
              <Core.Table size="small" aria-label="purchases">
                <Core.TableHead>
                  <Core.TableRow>
                    <Core.TableCell>Date</Core.TableCell>
                    <Core.TableCell>Customer</Core.TableCell>
                    <Core.TableCell align="right">Amount</Core.TableCell>
                    <Core.TableCell align="right">Total price ($)</Core.TableCell>
                  </Core.TableRow>
                </Core.TableHead>
                <Core.TableBody>
                  {row.history.map((historyRow) => (
                    <Core.TableRow key={historyRow.date}>
                      <Core.TableCell component="th" scope="row">
                        {historyRow.date}
                      </Core.TableCell>
                      <Core.TableCell>{historyRow.customerId}</Core.TableCell>
                      <Core.TableCell align="right">{historyRow.amount}</Core.TableCell>
                      <Core.TableCell align="right">
                        {Math.round(historyRow.amount * row.price * 100) / 100}
                      </Core.TableCell>
                    </Core.TableRow>
                  ))}
                </Core.TableBody>
              </Core.Table>
            </Core.Box>
          </Core.Collapse>
        </Core.TableCell>
      </Core.TableRow>
    </React.Fragment>
  );
}

Row.propTypes = {
  row: PropTypes.shape({
    calories: PropTypes.number.isRequired,
    carbs: PropTypes.number.isRequired,
    fat: PropTypes.number.isRequired,
    history: PropTypes.arrayOf(
      PropTypes.shape({
        amount: PropTypes.number.isRequired,
        customerId: PropTypes.string.isRequired,
        date: PropTypes.string.isRequired,
      })
    ).isRequired,
    name: PropTypes.string.isRequired,
    price: PropTypes.number.isRequired,
    protein: PropTypes.number.isRequired,
  }).isRequired,
};

const rows = [
  createData("Frozen yoghurt", 159, 6.0, 24, 4.0, 3.99),
  createData("Ice cream sandwich", 237, 9.0, 37, 4.3, 4.99),
  createData("Eclair", 262, 16.0, 24, 6.0, 3.79),
  createData("Cupcake", 305, 3.7, 67, 4.3, 2.5),
  createData("Gingerbread", 356, 16.0, 49, 3.9, 1.5),
];

const GridTransferencia = () => {
  return (
    <Core.TableContainer component={Core.Paper}>
      <Core.Table aria-label="collapsible table">
        <Core.TableHead>
          <Core.TableRow>
            <Core.TableCell />
            <Core.TableCell>Dessert (100g serving)</Core.TableCell>
            <Core.TableCell align="right">Calories</Core.TableCell>
            <Core.TableCell align="right">Fat&nbsp;(g)</Core.TableCell>
            <Core.TableCell align="right">Carbs&nbsp;(g)</Core.TableCell>
            <Core.TableCell align="right">Protein&nbsp;(g)</Core.TableCell>
          </Core.TableRow>
        </Core.TableHead>
        <Core.TableBody>
          {rows.map((row) => (
            <Row key={row.name} row={row} />
          ))}
        </Core.TableBody>
      </Core.Table>
    </Core.TableContainer>
  );
};

export default GridTransferencia;
