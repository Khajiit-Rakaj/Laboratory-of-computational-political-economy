export interface IWorkTableModel {
    id?: string // UID to store views
    name: string
    sourceTables: string[]
    joins: string[] // TODO: change to interface BaseTable.Field => DependentTable.Field (description of table joins)
    fields: string[] // TODO: change to interface Table.Field.DataType (description of represented fields)
    values: string[] // table data in JSON strings (one string per row)
}
