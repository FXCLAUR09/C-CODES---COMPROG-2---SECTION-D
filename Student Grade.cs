using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[,] grades = {
            {85,   90,   78},
            {92,   88,   95},
            {76,   95,   89},
            {95,   85,   92},
            {89,   92,   86}
        };

        Console.WriteLine("Grades Matrix: \r\n");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(grades[i, j] + " \t");
            }
            
            Console.WriteLine();
            
        }
        Console.WriteLine("\r\n");

        for (int i = 0; i < 5; i++)
        {
            double sum = 0.0;
            for (int j = 0; j < 3; j++)
            {
                sum += grades[i, j];
            }
            Console.WriteLine($"Student {i + 1} Average: {sum / 3.0:F2}");
        }

        for (int j = 0; j < 3; j++)
        {
            int highest = grades[0, j];
            int lowest = grades[0, j];
            int[] subjectGrades = new int[5];

            for (int i = 0; i < 5; i++)
            {
                if (grades[i, j] > highest)
                    highest = grades[i, j];
                if (grades[i, j] < lowest)
                    lowest = grades[i, j];

                subjectGrades[i] = grades[i, j];
            }

            Array.Sort(subjectGrades);
            double median = subjectGrades.Length % 2 == 0
                ? (subjectGrades[2] + subjectGrades[3]) / 2.0
                : subjectGrades[2];

            Console.WriteLine($"\r\nSubject {j + 1} Highest: {highest}");
            Console.WriteLine($"Subject {j + 1} Lowest: {lowest}");
            Console.WriteLine($"Subject {j + 1} Median: {median:F2}");
        }
    }
}