using System;

class Program
{
    static void Main()
    {

        int[,] grades = {
            { 85, 90, 78 },  // Student 1
            { 88, 76, 92 },  // Student 2
            { 90, 85, 85 },  // Student 3
            { 75, 80, 79 },  // Student 4
            { 95, 89, 94 }   // Student 5
        };

        int students = grades.GetLength(0);
        int subjects = grades.GetLength(1);

        Console.WriteLine("Student Grades Matrix:\r\n");
        for (int i = 0; i < students; i++)
        {
            Console.Write("Student " + (i + 1) + ": ");
            for (int j = 0; j < subjects; j++)
            {
                Console.Write(grades[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nAverage Grades Per Student:");
        for (int i = 0; i < students; i++)
        {
            int sum = 0;
            for (int j = 0; j < subjects; j++)
            {
                sum += grades[i, j];
            }
            double average = (double)sum / subjects;
            Console.WriteLine("Student " + (i + 1) + ": " + average.ToString("F2"));
        }

        Console.WriteLine("\nHighest Grade Per Subject:");
        for (int j = 0; j < subjects; j++)
        {
            int highest = grades[0, j];
            for (int i = 1; i < students; i++)
            {
                if (grades[i, j] > highest)
                {
                    highest = grades[i, j];
                }
            }
            Console.WriteLine("Subject " + (j + 1) + ": " + highest);
        }

        Console.WriteLine("\nLowest Grade Per Subject:");
        for (int j = 0; j < subjects; j++)
        {
            int lowest = grades[0, j];
            for (int i = 1; i < students; i++)
            {
                if (grades[i, j] < lowest)
                {
                    lowest = grades[i, j];
                }
            }
            Console.WriteLine("Subject " + (j + 1) + ": " + lowest);
        }

        Console.WriteLine("\nMedian Grade Per Subject:");
        for (int j = 0; j < subjects; j++)
        {
            int[] subjectGrades = new int[students];

            for (int i = 0; i < students; i++)
            {
                subjectGrades[i] = grades[i, j];
            }

            Array.Sort(subjectGrades);
            double median = subjectGrades[students / 2];

            Console.WriteLine("Subject " + (j + 1) + ": " + median);
        }

    }
}