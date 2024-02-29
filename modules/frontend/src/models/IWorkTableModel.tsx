import {INameValuePair} from "./INameValuePair";
import {IColumnDataModel} from "./IColumnDataModel";

export interface IWorkTableModel {
    id?: string // UID to store views
    name: string
    sourceTables: string[]
    joins?: string[] // TODO: change to interface BaseTable.Field => DependentTable.Field (description of table joins)
    fields: IColumnDataModel[] // TODO: change to interface Table.Field.DataType (description of represented fields)
    values: INameValuePair[][] // table data in JSON strings (one string per row)
}
