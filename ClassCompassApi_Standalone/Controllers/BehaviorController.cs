using ClassCompassApi.Shared.Models;
using ClassCompassApi.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ClassCompassApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BehaviorController : ControllerBase
    {
        private readonly BehaviorService _behaviorService;
        private readonly ILogger<BehaviorController> _logger;

        public BehaviorController(BehaviorService behaviorService, ILogger<BehaviorController> logger)
        {
            _behaviorService = behaviorService;
            _logger = logger;
        }

        // POST: api/behavior
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> RecordBehavior([FromBody] BehaviorRemark remark)
        {
            try
            {
                if (remark == null)
                {
                    return BadRequest("Invalid behavior record data.");
                }

                if (remark.StudentId <= 0)
                {
                    return BadRequest("Valid StudentId is required.");
                }

                if (string.IsNullOrEmpty(remark.Remark))
                {
                    return BadRequest("Remark is required.");
                }

                // Set default values
                remark.Date = remark.Date == default ? DateTime.Now : remark.Date;
                remark.CreatedAt = DateTime.Now;

                var result = await _behaviorService.RecordBehaviorAsync(remark);
                if (result)
                {
                    _logger.LogInformation("Behavior recorded successfully for student {StudentId}", remark.StudentId);
                    return Ok(new { Message = "Behavior recorded successfully!", BehaviorId = remark.Id });
                }

                return StatusCode(500, "Failed to record behavior.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording behavior for student {StudentId}", remark?.StudentId);
                return StatusCode(500, "An error occurred while recording behavior.");
            }
        }

        // GET: api/behavior/student/{studentId}
        [HttpGet("student/{studentId}")]
        [Authorize]
        public async Task<IActionResult> GetBehaviorRecords(int studentId)
        {
            try
            {
                if (studentId <= 0)
                {
                    return BadRequest("Valid StudentId is required.");
                }

                var records = await _behaviorService.GetBehaviorRecordsByStudentAsync(studentId);

                if (records == null || !records.Any())
                {
                    return NotFound("No behavior records found for this student.");
                }

                return Ok(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving behavior records for student {StudentId}", studentId);
                return StatusCode(500, "An error occurred while retrieving behavior records.");
            }
        }

        // GET: api/behavior/classroom/{classroomId}
        [HttpGet("classroom/{classroomId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetClassroomBehaviorRecords(int classroomId)
        {
            try
            {
                if (classroomId <= 0)
                {
                    return BadRequest("Valid ClassroomId is required.");
                }

                var records = await _behaviorService.GetBehaviorRecordsByClassroomAsync(classroomId);

                if (records == null || !records.Any())
                {
                    return NotFound("No behavior records found for this classroom.");
                }

                return Ok(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving behavior records for classroom {ClassroomId}", classroomId);
                return StatusCode(500, "An error occurred while retrieving behavior records.");
            }
        }

        // GET: api/behavior/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBehaviorRecord(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Valid Id is required.");
                }

                var record = await _behaviorService.GetBehaviorRecordByIdAsync(id);

                if (record == null)
                {
                    return NotFound("Behavior record not found.");
                }

                return Ok(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving behavior record {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the behavior record.");
            }
        }

        // PUT: api/behavior/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateBehaviorRecord(int id, [FromBody] BehaviorRemark remark)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Valid Id is required.");
                }

                if (remark == null)
                {
                    return BadRequest("Invalid behavior record data.");
                }

                remark.Id = id;
                var result = await _behaviorService.UpdateBehaviorRecordAsync(remark);

                if (result)
                {
                    _logger.LogInformation("Behavior record {Id} updated successfully", id);
                    return Ok(new { Message = "Behavior record updated successfully!" });
                }

                return NotFound("Behavior record not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating behavior record {Id}", id);
                return StatusCode(500, "An error occurred while updating the behavior record.");
            }
        }

        // DELETE: api/behavior/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteBehaviorRecord(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Valid Id is required.");
                }

                var result = await _behaviorService.DeleteBehaviorRecordAsync(id);

                if (result)
                {
                    _logger.LogInformation("Behavior record {Id} deleted successfully", id);
                    return Ok(new { Message = "Behavior record deleted successfully!" });
                }

                return NotFound("Behavior record not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting behavior record {Id}", id);
                return StatusCode(500, "An error occurred while deleting the behavior record.");
            }
        }

        // GET: api/behavior/types
        [HttpGet("types")]
        [Authorize]
        public IActionResult GetRemarkTypes()
        {
            try
            {
                var remarkTypes = new[]
                {
                    "Positive",
                    "Negative",
                    "Neutral",
                    "Excellent",
                    "Good",
                    "Needs Improvement",
                    "Concerning",
                    "Outstanding",
                    "Disciplinary"
                };

                return Ok(remarkTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving remark types");
                return StatusCode(500, "An error occurred while retrieving remark types.");
            }
        }
    }
}
