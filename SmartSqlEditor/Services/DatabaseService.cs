using Dapper;
using SmartSqlEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSqlEditor.Services
{
    public class DatabaseService
    {
        public static List<ColumnModel> FindAllColumList()
        {
            var sql = @"SELECT  d.name AS TableName -- 表名
	,TableDescription= CASE WHEN a.colorder=1 THEN ISNULL(f.value,'') ELSE '' END --表说明
	,ColumnSort = a.colorder
	,ColumnName = a.name --字段序号
    ,IsIdentity = CASE WHEN COLUMNPROPERTY(a.id,a.name,'IsIdentity')=1 THEN 'Y' ELSE 'N' END ----标识
    ,IsPrimaryKey = CASE WHEN EXISTS ( SELECT  1 
                                FROM    sysobjects
                                WHERE   xtype='PK'
                                        AND name IN (
                                        SELECT  name
                                        FROM    sysindexes
                                        WHERE   indid IN (
                                                SELECT  indid
                                                FROM    sysindexkeys
                                                WHERE   id=a.id
                                                        AND colid=a.colid)) )
                  THEN 'Y'
                  ELSE 'N'
             END --主键
		,DataType = b.name -- 类型
		,Size = a.length -- 占用字节数
        ,Length = COLUMNPROPERTY(a.id,a.name,'PRECISION') --长度
        ,DecimalPlace = ISNULL(COLUMNPROPERTY(a.id,a.name,'Scale'),0) --小数位数
        ,IsNullable = CASE WHEN a.isnullable=1 THEN 'Y' ELSE 'N' END --允许空 THEN '√' ELSE '×' END
		,DefaultValue = ISNULL(e.text,'') --默认值
		,ColumnDescription = ISNULL(g.[value],'') --字段说明
FROM    syscolumns a
LEFT   JOIN systypes b
        ON a.xusertype=b.xusertype
INNER   JOIN sysobjects d
        ON a.id=d.id
           AND d.xtype='U'
           AND d.name<>'dtproperties'
LEFT   JOIN syscomments e
        ON a.cdefault=e.id
LEFT   JOIN sys.extended_properties g
        ON a.id=g.major_id
           AND a.colid=g.minor_id
LEFT   JOIN sys.extended_properties f
        ON d.id=f.major_id
           AND f.minor_id=0 --where   d.name='V_test'         --如果只查询指定表,加上此条件
ORDER   BY a.id,a.colorder;";
            using (var conn = DbFactory.GetDbConnection)
            {
                var list = conn.Query<ColumnModel>(sql).ToList();
                return list;
            }
        }
    }
}
