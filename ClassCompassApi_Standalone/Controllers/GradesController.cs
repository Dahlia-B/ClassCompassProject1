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
    public class GradesController : ControllerBase
    {
        private readonly GradesApi _gradesService;
        private readonly ILogger<GradesController> _logger;

        public GradesController(GradesApi gradesService, ILogger<GradesController> logger)
        {
            _gradesService = gradesService;
            _logger = logger;
        }

        // POST: api/grades
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AssignGrade([FromBody] Grade grade)
        {
            try
            {
                if (grade == null)
                {
                    return BadRequest("Invalid grade data.");
                }

                if (grade.StudentId <= 0)
                {
                    return BadRequest("Valid StudentId is required.");
                }

                if (string.IsNullOrEmpty(grade.Subject))
                {
                    return BadRequest("Subject is required.");
                }

                // Set default values - Fixed: Changed DateTime to Date
                grade.DateRecorded = grade.DateRecorded == default ? DateTime.Now : grade.DateRecorded;

                var assignedGrade = await _gradesService.AssignGradeAsync(grade);
                if (assignedGrade == null)
                {
                    return StatusCode(500, "Failed to assign grade.");
                }

                _logger.LogInformation("Grade assigned successfully for student {StudentId} in subject {Subject}",
                    grade.StudentId, grade.Subject);

                return Ok(new { Message = "Grade assigned successfully!", Grade = assignedGrade });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning grade for student {StudentId}", grade?.StudentId);
                return StatusCode(500, "An error occurred while assigning the grade.");
            }
        }

        // GET: api/grades/student/{studentId}
        [HttpGet("student/{studentId}")]
        [Authorize]
        public async Task<IActionResult> GetStudentGrades(int studentId)
        {
            try
            {
                if (studentId <= 0)
                {
                    return BadRequest("Valid StudentId is required.");
                }

                var grades = await _gradesService.GetGradesByStudentAsync(studentId);

                if (grades == null || !grades.Any())
                {
                    return NotFound("No grades found for this student.");
                }

                return Ok(grades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grades for student {StudentId}", studentId);
                return StatusCode(500, "An error occurred while retrieving grades.");
            }
        }

        // GET: api/grades/subject/{subject}
        [HttpGet("subject/{subject}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetGradesBySubject(string subject)
        {
            try
            {
                if (string.IsNullOrEmpty(subject))
                {
                    return BadRequest("Subject is required.");
                }

                var grades = await _gradesService.GetGradesBySubjectAsync(subject);

                if (grades == null || !grades.Any())
                {
                    return NotFound($"No grades found for subject: {subject}");
                }

                return Ok(grades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grades for subject {Subject}", subject);
                return StatusCode(500, "An error occurred while retrieving grades.");
            }
        }

        // GET: api/grades/classroom/{classroomId}
        [HttpGet("classroom/{classroomId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetClassroomGrades(int classroomId)
        {
            try
            {
                if (classroomId <= 0)
                {
                    return BadRequest("Valid ClassroomId is required.");
                }

                var grades = await _gradesService.GetGradesByClassroomAsync(classroomId);

                if (grades == null || !grades.Any())
                {
                    return NotFound("No grades found for this classroom.");
                }

                return Ok(grades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grades for classroom {ClassroomId}", classroomId);
                return StatusCode(500, "An error occurred while retrieving grades.");
            }
        }

        // GET: api/grades/{gradeId}
        [HttpGet("{gradeId}")]
        [Authorize]
        public async Task<IActionResult> GetGrade(int gradeId)
        {
            try
            {
                if (gradeId <= 0)
                {
                    return BadRequest("Valid GradeId is required.");
                }

                var grade = await _gradesService.GetGradeByIdAsync(gradeId);

                if (grade == null)
                {
                    return NotFound("Grade not found.");
                }

                return Ok(grade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grade {GradeId}", gradeId);
                return StatusCode(500, "An error occurred while retrieving the grade.");
            }
        }

        // PUT: api/grades/{gradeId}
        [HttpPut("{gradeId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateGrade(int gradeId, [FromBody] Grade grade)
        {
            try
            {
                if (gradeId <= 0)
                {
                    return BadRequest("Valid GradeId is required.");
                }

                if (grade == null)
                {
                    return BadRequest("Invalid grade data.");
                }

                // Fixed: Changed GradeId to Id
                grade.Id = gradeId;
                var result = await _gradesService.UpdateGradeAsync(grade);

                if (result)
                {
                    _logger.LogInformation("Grade {GradeId} updated successfully", gradeId);
                    return Ok(new { Message = "Grade updated successfully!" });
                }

                return NotFound("Grade not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating grade {GradeId}", gradeId);
                return StatusCode(500, "An error occurred while updating the grade.");
            }
        }

        // DELETE: api/grades/{gradeId}
        [HttpDelete("{gradeId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteGrade(int gradeId)
        {
            try
            {
                if (gradeId <= 0)
                {
                    return BadRequest("Valid GradeId is required.");
                }

                var result = await _gradesService.DeleteGradeAsync(gradeId);

                if (result)
                {
                    _logger.LogInformation("Grade {GradeId} deleted successfully", gradeId);
                    return Ok(new { Message = "Grade deleted successfully!" });
                }

                return NotFound("Grade not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting grade {GradeId}", gradeId);
                return StatusCode(500, "An error occurred while deleting the grade.");
            }
        }

        // GET: api/grades/student/{studentId}/average
        [HttpGet("student/{studentId}/average")]
        [Authorize]
        public async Task<IActionResult> GetStudentGradeAverage(int studentId)
        {
            try
            {
                if (studentId <= 0)
                {
                    return BadRequest("Valid StudentId is required.");
                }

                var average = await _gradesService.GetStudentGradeAverageAsync(studentId);

                return Ok(new { StudentId = studentId, GradeAverage = average });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating grade average for student {StudentId}", studentId);
                return StatusCode(500, "An error occurred while calculating the grade average.");
            }
        }

        // GET: api/grades/student/{studentId}/subject/{subject}/average
        [HttpGet("student/{studentId}/subject/{subject}/average")]
        [Authorize]
        public async Task<IActionResult> GetStudentSubjectAverage(int studentId, string subject)
        {
            try
            {
                if (studentId <= 0)
                {
                    return BadRequest("Valid StudentId is required.");
                }

                if (string.IsNullOrEmpty(subject))
                {
                    return BadRequest("Subject is required.");
                }

                var average = await _gradesService.GetStudentSubjectAverageAsync(studentId, subject);

                return Ok(new { StudentId = studentId, Subject = subject, GradeAverage = average });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating subject average for student {StudentId} in {Subject}", studentId, subject);
                return StatusCode(500, "An error occurred while calculating the subject average.");
            }
        }
    }
}
