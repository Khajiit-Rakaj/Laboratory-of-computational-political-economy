import {IFetchableState} from "./IFetchableState";

export interface IUploadDataState extends IFetchableState{
    uploadResult?: string;
}

export const initialState: IUploadDataState = {
    loading: false,
}