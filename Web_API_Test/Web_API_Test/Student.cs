namespace Web_API_Test
{
    public class Student
    {
        public string StudentId { get; set; }      // 学号
        public string Name { get; set; }          // 姓名
        public int Age { get; set; }              // 年龄
        public string Major { get; set; }         // 专业
        public DateTime EnrollmentDate { get; set; } // 入学时间
        public string Email { get; set; }         // 邮箱
    }
}