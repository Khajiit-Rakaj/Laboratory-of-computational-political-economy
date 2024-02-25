import React, {useEffect} from "react";
import {fetchFetchableTables, setActiveTable} from "../../reducers/tableReducer";
import {useAppDispatch, useAppSelector} from "../../store/store";
import {fetchTableData} from "../../reducers/workTableReducer";

function TableList(): JSX.Element {
    const {tables, loading} = useAppSelector((state) => state.tables);
    const tableDispatch = useAppDispatch();

    useEffect(() => {
        tableDispatch(fetchFetchableTables());
    }, [tableDispatch])

    const setWorkTableState = (e: string) => {
        setActiveTable(e);
        tableDispatch(fetchTableData(e));
    }

    const emptyOption = [<option key={-1}>{'Select work table'}</option>];

    return (
        <div>
            <select name="Tables" size={-1} onChange={e => setWorkTableState(e.target.value)}>
                {!loading && tables
                    ? emptyOption.concat(tables?.map(item => (
                        <option key={item.tableName} value={item.tableName}>
                            {item.tableName}({item.documentCount})
                        </option>)))
                    : <option>
                        {'pending data...'}
                    </option>
                }
            </select>
        </div>
    )
}

export default TableList