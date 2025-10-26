using System.Runtime.CompilerServices;

namespace C__basics__classes__exceptions
{
    class Student
    {
        private string firstName;

        public string Firstname
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        private string surname;

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }

        private int studentNumber;

        public int StudentNumber
        {
            get
            {
                return studentNumber;
            }
            set
            {

            }
        }

        private List<int> grades;

        public List<int> Grades
        {
            get
            {
                return grades;
            }
            set
            {
                grades = value;
            }
        }

        public Student()
        {
            Firstname = "Karina";
            Surname = "Zinovieva";
            grades = new List<int>();
        }

        public Student(string firstName, string surname, int studentNumber)
        {
            Firstname = firstName;
            Surname = surname;
            grades = new List<int>();
        }

        public bool Passed()
        {
            if (grades.Count == 0)
            {
                return false;
            }

            return grades.All(grade => grade > 7);
        }

        public void AddGrade(int grade)
        {
            grades.Add(grade);
        }

        public double GetAverageGrade()
        {
            if (grades.Count == 0)
                return 0;
            return grades.Average();
        }

        public override string ToString()
        {
            return $"{Surname} {Firstname}";
        }
    }

    class Group
    {
        private List<Student> students;
        private string groupName;

        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                groupName = value;
            }
        }

        private string spec; //specialization

        public string Spec
        {
            get
            {
                return spec;
            }
            set
            {
                spec = value;
            }
        }

        private int courseNumber;

        public int CourseNumber
        {
            get
            {
                return courseNumber;
            }
            set
            {
                courseNumber = value;
            }
        }

        public Group()
        {
            students = new List<Student>();
            students.Add(new Student());
            groupName = "P45";
            spec = "C#";
            courseNumber = 1;
        }

        public Group(List<Student> students, string groupName, string spec, int courseNumber)
        {
            this.students = new List<Student>(students);
            GroupName = groupName;
            Spec = spec;
            CourseNumber = courseNumber;
        }

        public Group(Group other)
        {
            students = new List<Student>(other.students);
            GroupName = other.groupName;
            Spec = other.spec;
            CourseNumber = other.courseNumber;
        }

        public void Print()
        {
            Console.WriteLine($"Group name: {GroupName}");
            Console.WriteLine($"Specialization: {Spec}");
            Console.WriteLine($"Course number: {CourseNumber}");
            Console.WriteLine($"Student:");
            var sortedStudents = students.OrderBy( x => x.Surname ).ToList();
            for ( int i = 0; i < sortedStudents.Count; i++ )
            {
                Console.WriteLine($"{i+1}. {sortedStudents[i].Surname} {sortedStudents[i].Firstname}");
            }
            Console.WriteLine();
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
            Console.WriteLine("Student " + student + " was added to group " + groupName);
        }

        public void TransferStudent(Student student, Group fromGroup, Group toGroup)
        {
            if (fromGroup.students.Contains(student))
            {
                fromGroup.students.Remove(student);
                toGroup.students.Add(student);
                Console.WriteLine($"Student {student} was transfered from {fromGroup.GroupName} to {toGroup.GroupName}");
            }
        }

        public void FailedStudents()
        {
            var failedStudents = students.Where(s => !s.Passed()).ToList();

            if (failedStudents.Count == 0)
            {
                Console.WriteLine("No failed students!");
            }

            foreach (var student in failedStudents)
            {
                Console.WriteLine($"Student {student} failed");
            }

            int removedCount = students.RemoveAll(s => !s.Passed());
            Console.WriteLine($"{removedCount} student\\s were removed from the group {groupName}");
            Console.WriteLine();
        }

        public void WorstStudent()
        {
            if (students.Count == 0)
            {
                return;
            }

            var worstStudent = students.OrderBy(s => s.GetAverageGrade()).First();

            Console.WriteLine("Worst student:");
            Console.WriteLine($"Name: {worstStudent.Surname} {worstStudent.Firstname}");
            Console.WriteLine($"Average grade: {worstStudent.GetAverageGrade():F2}");
            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student("John", "Doe", 1);
            student1.AddGrade(12);
            student1.AddGrade(12);
            student1.AddGrade(12);

            Student student2 = new Student("Jane", "Doe", 2);
            student2.AddGrade(2);
            student2.AddGrade(2);
            student2.AddGrade(2);

            List<Student> students = new List<Student> { student1, student2 };

            Group group1 = new Group(students, "P45", "C#", 1);
            group1.Print();

            Group group2 = new Group(group1);
            group2.Print();

            Student student3 = new Student("Bob", "Bobovich", 3);
            student3.AddGrade(3);
            student3.AddGrade(3);
            student3.AddGrade(3);

            Student student4 = new Student("Bob", "Smith", 5);
            student4.AddGrade(5);
            student4.AddGrade(5);
            student4.AddGrade(5);
            group1.AddStudent(student4);

            group1.AddStudent(student3);
            group1.Print();

            group1.TransferStudent(student3, group1, group2);
            group1.TransferStudent(student4, group1, group2);
            group2.Print();

            group1.WorstStudent();
            group1.FailedStudents();

            group2.WorstStudent();
            group2.FailedStudents();
        }
    }
}
