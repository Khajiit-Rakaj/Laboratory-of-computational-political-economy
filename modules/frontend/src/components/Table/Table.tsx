// import React, { useState } from "react"
// import DataTable, { TableColumn } from "react-data-table-component"
// import { CorporationFinancials } from "../../types/types"

// type Data = {
//   id: number
//   name: string
//   age: number
//   title: string
//   director: string
//   year: string
// }

// type DataRow = {
//   age: number
//   title: string
//   director: string
//   year: string
// }

// const columns: TableColumn<DataRow>[] = [
//   { id: "name", name: "Age", selector: (row) => row.title, sortable: true },
//   {
//     name: "Title",
//     selector: (row) => row.title,
//   },
//   {
//     name: "Director",
//     selector: (row) => row.director,
//   },
//   { id: "year", name: "Year", selector: (row) => row.year, sortable: true },
// ]

// const data: Data[] = [
//   { id: 1, name: "John Smith", age: 32, title: "string", director: "ivan", year: "2001" },
//   { id: 2, name: "Jane Doe", age: 25, title: "string", director: "ivan", year: "2002" },
//   {
//     id: 3,
//     name: "Bob Johnson",
//     age: 45,
//     title: "string",
//     director: "ivan",
//     year: "2003",
//   },
// ]

// function Table(): JSX.Element {
//   const [selectedRows, setSelectedRows] = useState<Data[]>([])

//   const handleRowSelected = (row: Data) => {
//     setSelectedRows(selectedRows.concat(row))
//   }

//   const handleRowDeselected = (row: Data) => {
//     setSelectedRows(selectedRows.filter((r) => r.id !== row.id))
//   }

//   return (
//     <DataTable
//       title="Corporation Financials"
//       columns={columns}
//       data={data}
//       selectableRows
//       selectableRowsVisibleOnly
//       selectableRowsHighlight
//       fixedHeader
//       pagination
//     />
//   )
// }

// export default Table

import DataTable, { TableColumn } from "react-data-table-component"
import DATA from "../../corp_finance.json"
import { CorporationFinancials } from "../../types/types"

const columns: TableColumn<CorporationFinancials>[] = [
  {
    name: "Title",
    selector: (row) => row.corporation_name,
  },
  {
    name: "Year",
    selector: (row) => row.year,
    sortable: true,
  },
  {
    name: "Currency",
    selector: (row) => row.currency,
    sortable: true,
  },
  {
    name: "REER",
    selector: (row) => row.real_effective_exchange_rate,
    sortable: true,
  },
  {
    name: "NR",
    selector: (row) => row.net_revenue,
    sortable: true,
  },
  {
    name: "CPS",
    selector: (row) => row.cost_of_products_sold,
    sortable: true,
  },
  {
    name: "GP",
    selector: (row) => row.gross_profit,
    sortable: true,
  },
  {
    name: "GM",
    selector: (row) => row.gross_margin,
    sortable: true,
  },
  {
    name: "Ebitda",
    selector: (row) => row.ebitda,
    sortable: true,
  },
  {
    name: "Ebitda Margin",
    selector: (row) => row.ebitda_margin,
    sortable: true,
  },
  {
    name: "NP",
    selector: (row) => row.net_profit,
    sortable: true,
  },
  {
    name: "NM",
    selector: (row) => row.net_margin,
    sortable: true,
  },
  {
    name: "ITE",
    selector: (row) => row.income_tax_expenses,
    sortable: true,
  },
  {
    name: "FC",
    selector: (row) => row.finance_costs,
    sortable: true,
  },
  {
    name: "DAA",
    selector: (row) => row.depreciation_and_amortization_of_assets,
    sortable: true,
  },
]
const data: any = DATA

function Table(): JSX.Element {
  return (
    <DataTable title="Corporation Financials" columns={columns} data={data} pagination />
  )
}

export default Table
