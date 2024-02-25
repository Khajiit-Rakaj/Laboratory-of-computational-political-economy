import {TableColumn} from "react-data-table-component/dist/src/DataTable/types";

export const buildTableColumnConfig = (fields: any[]): TableColumn<any>[] => {
    return fields.map(f => {
        return {
            name: f.name,
            sortable: true,
            reorder: true,
            selector: (o) => o[f.name]
        }
    })
}