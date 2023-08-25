import {createAsyncThunk, createSlice} from '@reduxjs/toolkit'

import * as routes from '../constants/routeConstants';

import {initialState} from '../models/states/ITablesListState';
import {ITableListEntityModel} from "../models/ITableListEntityModel";

export const fetchTablesData = createAsyncThunk('get/tableList', async (): Promise<ITableListEntityModel[]> => {
    const responce = await fetch(`http://127.0.0.1:8080${routes.tablesRoute}`)
        .then(res => res.json())
        .then(res => {
            return res as unknown as ITableListEntityModel[]
        });
    return responce;
});

export const tableListSlice = createSlice({
    name: "tables slice",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchTablesData.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchTablesData.fulfilled, (state, action) => {
            state.tables = action.payload;
            state.loading = false;
        });
        builder.addCase(fetchTablesData.rejected, (state) => {
            state.loading = false;
        });
    }
});

export default tableListSlice.reducer;