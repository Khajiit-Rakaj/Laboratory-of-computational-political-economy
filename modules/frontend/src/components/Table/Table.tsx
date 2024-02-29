import styles from "./Table.module.css"
import DataTable, {TableColumn} from "react-data-table-component"
import DATA from "../../data/corp_finance.json"
import React, {useEffect} from "react";
import TableList from "./TableList";
import {useAppSelector} from "../../store/store";
import { buildTableColumnConfig } from "../../utillites/table/tableColumnConfigBuilder";
import {buildTableObject} from "../../utillites/table/tableObjectBuilder";

export const customStyles = {
    headCells: {
        style: {
            paddingLeft: "8px", // override the cell padding for head cells
            paddingRight: "8px",
        },
    },
    cells: {
        style: {
            paddingLeft: "8px", // override the cell padding for data cells
            paddingRight: "8px",
        },
    },
}
const data: any[] = DATA

function Table(): JSX.Element {
    const workTable = useAppSelector((state) => state.workTable);

    console.log(workTable);

    useEffect(() => {
    }, [workTable])

    return (
        <div>
            <table width={"100%"}>
                <tbody>
                <tr>
                    <td>
                        <div>
                            <TableList/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div className={styles.table}>
                            <DataTable
                                title={workTable.loading ? 'Fetching' : (workTable.workTable?.name ?? 'Select table')}
                                columns={workTable.workTable && buildTableColumnConfig(workTable.workTable.fields) || []}
                                data={workTable.workTable && buildTableObject(workTable.workTable.values) || []}
                                pagination
                                highlightOnHover
                                customStyles={customStyles}
                            />
                        </div>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    )
}

export default Table
