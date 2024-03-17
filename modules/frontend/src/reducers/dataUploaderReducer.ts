import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";

import * as routes from "../constants/routeConstants";
import {initialState} from "../models/states/IUploadDataState";
import {IWorkTableModel} from "../models/IWorkTableModel";

interface UploadCsvDataProps{
    model: IWorkTableModel; 
    sourceDestinationPath: {[target: string]: string}; 
    data: string;
    metadata: string;
}

interface Body{
    model: string,
    sourceDestinationPath: string,
    data:string,
}

export const uploadCsvData = createAsyncThunk('post/uploadData', async (props: UploadCsvDataProps): Promise<string> => {
    const paths = props.model.fields.map(c => { return {key: c.name, value: props.sourceDestinationPath[c.name]}})
    // const body: Body= {model: 'modell', data: 'dataa', sourceDestinationPath: 'sourceDestinationPathh'};
    const requestHeaders: HeadersInit = new Headers();
    requestHeaders.set('Content-Type', 'application/json');
    const request = new Request(
        `http://127.0.0.1:8080${routes.apiRoute}${routes.csvDataUpload}`,
        {
            method: 'POST',
            headers: requestHeaders,
            body: `{
                "model": "${props.model.sourceTables[0]}",
                "sourceDestinationPath": ${JSON.stringify(paths)},
                "data": "${props.data.replaceAll('"', '\\"').replaceAll('\r\n', '\\n')}",
                "metadataSource": "${props.metadata}"
            }`
        });
    return await fetch(request)
        .then(res => {
            return res.text() as unknown as string;
        });
});

export const uploadDataSlice = createSlice({
    name: 'upload slice',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(uploadCsvData.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(uploadCsvData.fulfilled, (state, action) => {
            state.uploadResult = action.payload;
            state.loading = false;
        });
        builder.addCase(uploadCsvData.rejected, (state) => {
            state.loading = false;
        });
    }    
});

export default uploadDataSlice.reducer;