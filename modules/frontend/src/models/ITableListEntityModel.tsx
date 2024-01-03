import {IColumnDataModel} from "./IColumnDataModel";

export interface ITableListEntityModel {
    tableName: string
    documentCount: number
    tableDataModel: IColumnDataModel[]
}