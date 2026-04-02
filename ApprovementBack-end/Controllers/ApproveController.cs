using ApprovementBack_end.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApprovementBack_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApproveController : ControllerBase
    {
        private readonly ConnectDB _context;

        public ApproveController(ConnectDB context) {
            _context = context;
        }

        public class ApproveDto
        {
            public int id { get; set; }
            public string reason { get; set; }
            public int status { get; set; }
            public DateTime create_at { get; set; }
            public DateTime update_at { get; set; }
        }

        public class UpdateStatusDto
        {
            public List<int> id { get; set; }
            public int status { get; set; }
            public string reason { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int page = 1,int pageSize = 10) {
            var total = await _context.ApprovelistTable.CountAsync();
            var data = await _context.ApprovelistTable.Skip((page - 1) * pageSize).Take((pageSize))
                .Select(x => new ApproveDto { 
                    id = x.ID,
                    reason = x.REASON ,
                    status = x.STATUS ,
                    create_at = x.CREATE_AT,
                    update_at = x.UPDATE_AT
                })
                .ToListAsync();

            return Ok(new { total, data });
        }


        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto dto) {
            var items = await _context.ApprovelistTable.
                Where(x => dto.id.Contains(x.ID) && x.STATUS != 1).
                ToListAsync();
            if (items == null) return NotFound();

            foreach (var item in items)
            {
                item.STATUS = dto.status;
                item.REASON = dto.reason;
                item.UPDATE_AT = DateTime.Now ;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
