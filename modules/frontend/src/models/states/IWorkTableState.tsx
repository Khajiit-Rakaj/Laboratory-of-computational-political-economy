import {IFetchableState} from "./IFetchableState";
import {IWorkTableModel} from "../IWorkTableModel";

export interface IWorkTableState extends IFetchableState{
    workTable?: IWorkTableModel
}

export const initialState: IWorkTableState = {
    loading: false,
}