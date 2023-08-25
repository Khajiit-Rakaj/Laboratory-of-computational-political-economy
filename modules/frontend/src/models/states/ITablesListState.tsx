import {ITableListEntityModel} from "../ITableListEntityModel";
import {IFetchableState} from "./IFetchableState";

export interface ITablesListState extends IFetchableState{
    tables: ITableListEntityModel[]
}

export const initialState : ITablesListState = {
    loading: false,
    tables: [],
}