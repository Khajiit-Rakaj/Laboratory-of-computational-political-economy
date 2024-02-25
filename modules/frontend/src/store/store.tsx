import {configureStore} from '@reduxjs/toolkit'
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux'

import tableReducer from '../reducers/tableReducer'
import workTableReducer from "../reducers/workTableReducer";


const store = configureStore({
    reducer: {
        tables: tableReducer,
        workTable: workTableReducer
    }
})

export default store;
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export const useAppDispatch: () => AppDispatch = useDispatch;
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
