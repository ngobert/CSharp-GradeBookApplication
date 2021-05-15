using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook: BaseGradeBook
    {
        public RankedGradeBook(string name): base(name) {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count() < 5) {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            List<double> allGrades = new List<double>();

            foreach(Student student in Students) {
                allGrades.Add(student.AverageGrade);
            }

            var sortedGrades = allGrades.OrderByDescending(d => d).ToList();
            int gradeSize = Convert.ToInt32(Students.Count() * 0.20);

            var foundPosition = false;
            int gradeEnd = gradeSize;
            int studentGradeRank = 0;

            for (int i = 0; i < 4; i++)
            {
                int lowGradeIndex = gradeEnd - 1;
                if (averageGrade >= sortedGrades[lowGradeIndex]) {
                    studentGradeRank = i;
                    foundPosition = true;
                    break;
                }
                gradeEnd += gradeSize;
            }

            if(foundPosition) {
                switch (studentGradeRank) {
                    case 0:
                        return 'A';
                    case 1:
                        return 'B';
                    case 2:
                        return 'C';
                    case 3:
                        return 'D';
                }
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count() < 5) {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            } else {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5) {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            } else {
                base.CalculateStudentStatistics(name);
            }
        }
    }
}