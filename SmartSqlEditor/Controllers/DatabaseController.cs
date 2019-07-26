using Microsoft.AspNetCore.Mvc;
using SmartSqlEditor.Services;
using System.Linq;

namespace SmartSqlEditor.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        [HttpGet]
        public IActionResult FindColumns()
        {
            var list = DatabaseService.FindAllColumList();
            var colums = list.GroupBy(x => x.TableName)
                .Select(x => new {
                    TableName = x.Key,
                    Cols = x.Select(c => new { c.ColumnName })
                })
                .ToDictionary(k => k.TableName, v => v.Cols.Select(c => c.ColumnName));
            return Ok(colums);
        }
    }
}