using ApprovementBack_end.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public class UpdateStatusDto
        {
            public int Status { get; set; }
            public string Reason { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int page = 1,int pageSize = 10) {
            var total = await _context.ApproveRequest.CountAsync();
            var data = await _context.ApproveRequest.Skip((page - 1) * pageSize).Take((pageSize)).ToListAsync();

            return Ok(new { total, data });
        }


        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id , [FromBody] UpdateStatusDto dto) {
            var item = await _context.ApproveRequest.FindAsync(id);
            if (item == null) return NotFound();
            if (item.STATUS == 1) return BadRequest("อนุมัติแล้วไม่สามารถเปลี่ยนได้");

            item.STATUS = dto.Status;
            item.REASON = dto.Reason;

            await _context.SaveChangesAsync();

            return Ok(item);
        }

    }
}
