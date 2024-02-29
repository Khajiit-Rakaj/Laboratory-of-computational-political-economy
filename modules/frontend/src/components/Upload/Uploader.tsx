import {IWorkTableModel} from "../../models/IWorkTableModel";
import React, {useState} from "react";
import {useAppDispatch} from "../../store/store";
import {uploadCsvData} from "../../reducers/dataUploaderReducer";

interface UploaderProps{
    table?: IWorkTableModel
}

const Uploader = (props: UploaderProps) =>{
    const {table} = props;
    const [cols, setCols] = useState<string[]>([]);
    const [data, setData] = useState<string>('');
    const [sourceDestinationPaths, setSourceDestinationPath] = useState<{[target: string]: string}>({});
    const uploaderDispatch = useAppDispatch();
    
    const setPath = (target: string, source: string) =>{
        const state = sourceDestinationPaths;
        state[target] = source;
        
        setSourceDestinationPath(state);
    } 
    
    const onInputChange = (e: any) => {
        const file = e.target.files[0];
        
        if (file && file.name.toLowerCase().endsWith('.csv')){
            const fileReader = new FileReader();
            fileReader.onload = () => {
                const fileContent = fileReader.result as string;
                setData(fileContent);
                const rows = fileContent.split('\r\n');
                const columns = rows[0].split(',').map(s => s.replaceAll(/\"/g, ''));
                setCols(columns);

                console.log(columns);
            };
            fileReader.readAsText(file);            
        }
    };
    
    const sourceDestinationRow = (fieldName: string) => {
        return(
            <tr>
                <td><b>{ fieldName }</b></td>
                <td><span> from </span></td>
                <td>{colsSelect(fieldName)}</td>
            </tr>);
    }
    
    const colsSelect = (fieldName: string) => {
        return (
            <select  name={fieldName} size={-1} onChange={e => setPath(fieldName, e.target.value)} defaultValue={cols && cols.length > 0 ? 'select source field name...' : 'upload file to parse field names...'}>
                <option disabled>
                    {cols && cols.length > 0 ? 'select source field name...' : 'upload file to parse field names...'}
                </option>
                {cols && cols.length > 0 &&
                    cols.map(item => (
                        <option key={item} value={item}>{item}</option>))
                }
            </select>
        )
    }
    
    const uploadData = () => {
        if (data.length && table){
            uploaderDispatch(uploadCsvData({data: data, model: table, sourceDestinationPath: sourceDestinationPaths}));            
        }
    }
    
    return (
        <div>
            <input type="file" onInput={onInputChange}></input>
            <table>
                <tbody>                
                    {!!table && table.fields.map(f => sourceDestinationRow(f.name))}
                </tbody>
            </table>
            <button disabled={!table || !data.length} onClick={() => uploadData()}>Upload</button>
        </div>
    );
}

export default Uploader