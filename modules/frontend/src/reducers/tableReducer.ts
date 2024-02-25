import {createAction, createAsyncThunk, createSlice, PayloadAction} from '@reduxjs/toolkit'

import * as routes from '../constants/routeConstants';

import {initialState} from '../models/states/ITablesListState';
import {ITableListEntityModel} from "../models/ITableListEntityModel";
import exp from "constants";

export const fetchFetchableTables = createAsyncThunk('get/tableList', async (): Promise<ITableListEntityModel[]> => {
    return await fetch(`http://127.0.0.1:8080${routes.tablesRoute}`)
        .then(res => res.json())
        .then(res => {
            return res as unknown as ITableListEntityModel[]
        });
});

export const tableListSlice = createSlice({
    name: "tables slice",
    initialState,
    reducers: {
        setActiveTable: (state, action: PayloadAction<string>) => {
            state.activeTable = action.payload
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchFetchableTables.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(fetchFetchableTables.fulfilled, (state, action) => {
            state.tables = action.payload;
            state.loading = false;
        });
        builder.addCase(fetchFetchableTables.rejected, (state) => {
            state.loading = false;
        });
    }
});

export default tableListSlice.reducer;

export const {setActiveTable} = tableListSlice.actions;