import {useAppSelector} from "../../store/store";
import React, {useEffect} from "react";
import TableList from "../Table/TableList";
import styles from "../Table/Table.module.css";
import DataTable from "react-data-table-component";
import {buildTableColumnConfig} from "../../utillites/table/tableColumnConfigBuilder";
import {buildTableObject} from "../../utillites/table/tableObjectBuilder";
import { customStyles } from "../Table/Table";
import Uploader from "./Uploader";

const Upload = () => {
    const workTable = useAppSelector((state) => state.workTable);

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
                <tr>
                    <td>
                        <div>
                            <Uploader
                                table={workTable.workTable}
                            />
                        </div>
                    </td>
                </tr>

            </tbody>
        </table>
    </div>);
}

export default Upload