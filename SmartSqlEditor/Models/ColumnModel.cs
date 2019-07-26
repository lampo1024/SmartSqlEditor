namespace SmartSqlEditor.Models
{
    public class ColumnModel
    {
        public string TableName { get; set; }
        public string TableDescription { get; set; }
        public int ColumnSort { get; set; }
        public string ColumnName { get; set; }
        public string IsIdentity { get; set; }
        public string IsPrimaryKey { get; set; }
        public string DataType { get; set; }
        public int Size { get; set; }
        public int Length { get; set; }
        public int DecimalPlace { get; set; }
        public string IsNullable { get; set; }
        public string DefaultValue { get; set; }
        public string ColumnDescription { get; set; }
    }
}
