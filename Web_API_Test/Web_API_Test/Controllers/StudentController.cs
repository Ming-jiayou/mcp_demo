using Microsoft.AspNetCore.Mvc;

namespace Web_API_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        // 模拟学生数据
        private static readonly List<Student> Students = new()
        {
            new Student
            {
                StudentId = "2023001",
                Name = "张三",
                Age = 20,
                Major = "计算机科学",
                EnrollmentDate = new DateTime(2023, 9, 1),
                Email = "zhangsan@example.com"
            },
            new Student
            {
                StudentId = "2023002",
                Name = "李四",
                Age = 21,
                Major = "软件工程",
                EnrollmentDate = new DateTime(2023, 9, 1),
                Email = "lisi@example.com"
            },
            new Student
            {
                StudentId = "2023003",
                Name = "王五",
                Age = 19,
                Major = "人工智能",
                EnrollmentDate = new DateTime(2023, 9, 1),
                Email = "wangwu@example.com"
            }
        };

        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 根据学号获取学生信息
        /// </summary>
        /// <param name="studentId">学生学号</param>
        /// <returns>学生信息</returns>
        [HttpGet("{studentId}")]
        public ActionResult<Student> GetStudentById(string studentId)
        {
            _logger.LogInformation($"正在查找学号为 {studentId} 的学生信息");
            
            var student = Students.FirstOrDefault(s => s.StudentId == studentId);
            
            if (student == null)
            {
                _logger.LogWarning($"未找到学号为 {studentId} 的学生");
                return NotFound(new { message = $"未找到学号为 {studentId} 的学生" });
            }
            
            _logger.LogInformation($"成功找到学号为 {studentId} 的学生: {student.Name}");
            return Ok(student);
        }

        /// <summary>
        /// 获取所有学生列表
        /// </summary>
        /// <returns>学生列表</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok(Students);
        }
    }
}