import {INameValuePair} from "../../models/INameValuePair";

export const buildTableObject = (values: INameValuePair[][]) => {
    return values.map(row => {
        return row.reduce((p, c) => ({...p, [c.name]: c.value}), {})
    });
}