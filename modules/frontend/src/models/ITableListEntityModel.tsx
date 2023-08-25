import {IFieldDescription} from "./IFieldDescription";

export interface ITableListEntityModel {
    tableName: string
    documentCount: number
    tableDataModel: IFieldDescription[]
}