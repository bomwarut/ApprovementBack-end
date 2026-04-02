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
            public int Status { get; set; }
            public string Reason { get; set; }
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


        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id , [FromBody] UpdateStatusDto dto) {
            var item = await _context.ApprovelistTable.FindAsync(id);
            if (item == null) return NotFound();
            if (item.STATUS == 1) return BadRequest("อนุมัติแล้วไม่สามารถเปลี่ยนได้");

            item.STATUS = dto.Status;
            item.REASON = dto.Reason;

            await _context.SaveChangesAsync();

            return Ok(item);
        }

    }
}
