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
            int gradeStart = 0;
            int gradeEnd = gradeStart + gradeSize;
            int gradeRankCounter = 0;
            int studentGradeRank = 0;

            while(!foundPosition && gradeRankCounter < 4) {
                if (averageGrade >= sortedGrades[gradeEnd]) {
                    studentGradeRank = gradeRankCounter;
                    foundPosition = true;
                }
                gradeStart += gradeSize;
                gradeRankCounter += 1;
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
                    default:
                        return 'F';
                }
            }

            return 'F';
        }
    }
}