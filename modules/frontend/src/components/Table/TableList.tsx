import React, {useEffect} from "react";
import {fetchTablesData} from "../../reducers/tableReducer";
import {useAppDispatch, useAppSelector} from "../../store/store";

function TableList(): JSX.Element {
    const {tables, loading} = useAppSelector((state) => state.tables);
    const tableDispatch = useAppDispatch();

    useEffect(() => {
        tableDispatch(fetchTablesData());
    }, [tableDispatch])

    return (
        <div>
            <select name="Tables" size={-1}>
                {!loading && tables
                    ? tables?.map(item => (
                        <option key={item.tableName} value={item.tableName}>
                            {item.tableName}({item.documentCount})
                        </option>))
                    : ['pending data']
                }
            </select>
        </div>
    )
}

export default TableList