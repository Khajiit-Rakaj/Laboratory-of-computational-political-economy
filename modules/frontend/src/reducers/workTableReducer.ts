import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";

import * as routes from "../constants/routeConstants";

import {initialState} from "../models/states/IWorkTableState";
import {IWorkTableModel} from "../models/IWorkTableModel";

export const fetchTableData = createAsyncThunk('get/workTable', async (): Promise<IWorkTableModel> => {
    const responce = await fetch(`http://127.0.0.1:8080${routes.tablesRoute}`)
        .then(res => res.json())
        .then(res => {
            return res.tables as unknown as IWorkTableModel
        });
    return responce;
});

export const workTableSlice = createSlice({
    name: "workTable slice",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchTableData.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchTableData.fulfilled, (state, action) => {
            state.workTable = action.payload;
            state.loading = false;
        });
        builder.addCase(fetchTableData.rejected, (state) => {
            state.loading = false;
        });
    }
});

export default workTableSlice.reducer;