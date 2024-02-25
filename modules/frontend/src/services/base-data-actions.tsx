import {ITableListEntityModel} from "../models/ITableListEntityModel";

export class BaseDataActions {
    public async getDataList(): Promise<ITableListEntityModel[]>{
        return  fetch('http://127.0.0.1:8080/api/tables')
            .then(res => res.json())
            .then(res => {
                return res.tables as unknown as ITableListEntityModel[]
            })
    }

    public async testConnection(): Promise<string>{
        let test = fetch('http://127.0.0.1:8080/api/tables').then(response => response.json())
            .then(data => console.log(data))
            .catch(error => {
                console.log('here comes trouble' + error)
            });

        return  fetch('http://127.0.0.1:8080/api/tables')
            .then(res => {
                return res as unknown as string
            })
    }
}