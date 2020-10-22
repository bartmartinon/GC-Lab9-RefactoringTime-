using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RefactoringTime
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initiate data
            List<string> names = new List<string> 
            { 
                "Alfonzo Uno", "Barry Deux", "Charlotte Drei", "David Net", "Edith Cinque", "Fabio Exi", "Gunther Sju", "Hugh Acht"
            };
            List<string> hometowns = new List<string> 
            { 
                "Seville", "Paris", "Berlin", "Seoul", "Rome", "Athens", "Oslo", "Ansterdam" 
            };
            List<string> favoriteFoods = new List<string> 
            { 
                "Risotto", "Veal", "Black Forest Cake", "Guknap Bowl", "Rigatoni", "Feta Salad", "Meatballs", "Pesto Gouda"
            };
            List<string> majors = new List<string>
            { 
                "Culinary Studies", "Computer Science", "English", "French", "Game Design", "Theatre", "History", "Anthropology" 
            };
            List<string> favoriteColors = new List<string>
            {
                "Blue", "Red", "Green", "Yellow", "Pink", "Orange", "Purple", "Silver"
            };
            int studentCountOld = names.Count;

            // Pool all info from Old Lists into new List of StudentInfo instances
            List<StudentInfo> students = new List<StudentInfo>();
            for (int i = 0; i < studentCountOld; i++)
            {
                StudentInfo newStudent = new StudentInfo(names[i], hometowns[i], favoriteFoods[i], majors[i], favoriteColors[i]);
                students.Add(newStudent);
            }
            
            // Add another StudentInfo instance to showcase SortListByName method and sort student list by name values
            StudentInfo extraStudent = new StudentInfo("Adrien Guillaume", "Rilleux", "Gnocchi", "Software Developer", "Blue");
            students.Add(extraStudent);
            students = SortListByName(students);
            int studentCount = students.Count;

            // Application Loop
            bool done = false;
            while (!done)
            {
                Console.WriteLine("Welcome to the Classmates Program!");
                MakeLineSpace(1);
                Console.WriteLine("==================================");
                MakeLineSpace(1);

                // Display Classmate Data for User
                Console.WriteLine("Current Class Roster:");
                MakeLineSpace(1);
                for (int i = 0; i < studentCount; i++)
                {
                    Console.WriteLine("{0}. {1}", i, students[i].Name);
                }
                MakeLineSpace(1);

                // Prompt User to choose if they want to add a student to the system first
                if (AskToAdd())
                {
                    students = AddStudent(students);
                    studentCount = students.Count;
                    Console.Clear();
                    continue;
                }
                MakeLineSpace(1);

                // Prompt User to choose a Student to look into
                int targetStudentID = ChooseStudent(students);

                // Prompt User to look into selected Student's hometown, favroite food or major
                bool inputSubjectValid = false;
                while (!inputSubjectValid)
                {
                    Console.Write("What would you like to know about {0}? ", students[targetStudentID].Name);
                    string inputSubject = PromptForInput("(Enter either 'hometown', 'favorite food', 'major' or 'favorite color') ");
                    // Validate Subject input (Must be any of the specified options in the prompt)
                    if ((inputSubject.Trim().ToLower()).Equals("hometown"))
                    {
                        Console.WriteLine("{0}'s hometown is {1}!", students[targetStudentID].Name, students[targetStudentID].Hometown);
                        inputSubjectValid = true;
                    }
                    else if ((inputSubject.Trim().ToLower()).Equals("favorite food"))
                    {
                        Console.WriteLine("{0}'s favorite food is {1}!", students[targetStudentID].Name, students[targetStudentID].FavoriteFood);
                        inputSubjectValid = true;
                    }
                    else if ((inputSubject.Trim().ToLower()).Equals("major"))
                    {
                        Console.WriteLine("{0}'s major is {1}!", students[targetStudentID].Name, students[targetStudentID].Major);
                        inputSubjectValid = true;
                    }
                    else if ((inputSubject.Trim().ToLower()).Equals("favorite color"))
                    {
                        Console.WriteLine("{0}'s favorite color is {1}!", students[targetStudentID].Name, students[targetStudentID].FavoriteColor);
                        inputSubjectValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: Option not recognized. Please choose one of the specified terms above.");
                    }
                    MakeLineSpace(1);
                }
                // Prompt User to Continue
                done = AskToContinue();
            }
        }

        // Has the User select a student by either providing their ID number (List index) or their name.
        // Returns the list index value of a found valid student.
        //
        // Using exceptions, this is an alternative to using TryParse
        //      Providing a non-integer input that causes a FormatException will have the program use the input as a name.
        //          If no matching name is found, declare the value as invalud and prompt the user to try again.
        //      Providing an integer input that is outside the range of the student list causes an IndexOutOfRangeException that will have the program
        //          declare that the value is invalid and prompt the user to try again.
        public static int ChooseStudent(List<StudentInfo> students)
        {
            int studentCount = students.Count;
            int targetStudentID = -1;
            bool inputStuIDValid = false;
            while (!inputStuIDValid)
            {
                string inputStuIDStr = PromptForInput("Please choose a Student by ID Number or Name: ");
                // Validate Student ID/Name input (Must be an integer within the range of student ID values, or a name that matches one of the students')
                int inputStuID = -1;
                try
                {
                    // Initially assume input is an integer
                    inputStuID = int.Parse(inputStuIDStr);
                    Console.WriteLine("Integer Input detected!");
                    try
                    {
                        // Assume integer is in range
                        Console.WriteLine("You have selected Student #{0}: {1}!", inputStuID, students[inputStuID].Name);
                        targetStudentID = inputStuID;
                        inputStuIDValid = true;
                    }
                    catch (IndexOutOfRangeException) // Input integer out of range
                    {
                        Console.WriteLine("Error: No student with that ID exists. Please enter another number.");
                    }
                }
                catch (FormatException) // Input is non-integer, treat it as a string with a student's name and search for exact match
                {
                    Console.WriteLine("String Input detected!");
                    for (int i = 0; i < studentCount; i++)
                    {
                        if (inputStuIDStr.Equals(students[i].Name))
                        {
                            Console.WriteLine("You have selected Student #{0}: {1}!", i, students[i].Name);
                            targetStudentID = i;
                            inputStuIDValid = true;
                        }
                    }
                    if (!inputStuIDValid) // No match is found within the list of names
                    {
                        Console.WriteLine("Error: No Student with the given string as a name was found in the system. Please try again.");
                    }
                }
                MakeLineSpace(1);
            }
            return targetStudentID;
        }

        // Takes a StudentInfo List and sorts all elements by their name values.
        public static List<StudentInfo> SortListByName(List<StudentInfo> list)
        {
            List<StudentInfo> sortedList = list.OrderBy(x => x.Name).ToList();
            return sortedList;
        }

        // Takes a StudentInfo list and prompts the user to add individual pieces of information for a new StudentInfo instance that will be added
        // to the given list.
        // Will any String input as long as it is not a blank input via PromptForInput calls.
        public static List<StudentInfo> AddStudent(List<StudentInfo> list)
        {
            // Ask for a name string
            string studentName = PromptForInput("Please enter the Student's name: ");

            // Ask for a hometown string
            string studentHometown = PromptForInput("Please enter the Student's hometown: ");

            // Ask for a favorite food string
            string studentFavFood = PromptForInput("Please enter the Student's favorite food: ");

            // Ask for a major string
            string studentMajor = PromptForInput("Please enter the Student's major: ");

            // Ask for a favorite color string
            string studentFavColor = PromptForInput("Please enter the Student's favorite color: ");

            StudentInfo newStudent = new StudentInfo(studentName, studentHometown, studentFavFood, studentMajor, studentFavColor);
            list.Add(newStudent);
            return SortListByName(list);
        }

        // Prompts user if they want to add a student into the system. 
        public static bool AskToAdd()
        {
            while (true)
            {
                string inputStr = PromptForInput("Would you like to add a student into the system before proceeding? ('yes'/'no') ");
                if (inputStr.Equals("yes"))
                {
                    Console.WriteLine("Prepare to add information!");
                    return true;
                }
                else if (inputStr.Equals("no"))
                {
                    Console.WriteLine("Proceeding as expected.");
                    return false;
                }
                else
                {
                    Console.WriteLine($"Error: Please input 'yes' or 'no'.");
                }
            }
        }

        // ============================================================================================================================
        // Formatting Methods: Provides common functionality for most input-oriented console applications

        // Prompts user for an input, with the message parameter serving as context. Returns the string generated by the user's input.
        // Does not allow blank inputs, and will repeat until an input is given.
        public static string PromptForInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string userInput = (Console.ReadLine()).Trim();
                if (userInput.Length > 0)
                {
                    return userInput;
                }
            }
        }

        // Prompts user if they want to continue using the program. 
        // If yes, then let the loop iterate. Otherwise, stop the loop by setting done to true.
        public static bool AskToContinue()
        {
            while (true)
            {
                string inputStr = PromptForInput("Would you like to know more? ('yes'/'no') ").Trim();
                if (inputStr.Equals("yes"))
                {
                    Console.Clear();
                    return false;
                }
                else if (inputStr.Equals("no"))
                {
                    Console.WriteLine($"Thank you for using the Classmate Program! Have a nice day!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: Please input 'yes' or 'no'.");
                }
            }
        }

        // Adds empty lines in console for formatting
        public static void MakeLineSpace(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine(" ");
            }
        }

        // ============================================================================================================================
        // Legacy Code: This is just an old version of ChooseStudent that used TryParse instead of Try-Catch statements with Exceptions
        public static int LegacyChooseStudent(List<string> names)
        {
            int studentCount = names.Count;
            int targetStudentID = -1;
            bool inputStuIDValid = false;
            while (!inputStuIDValid)
            {
                string inputStuIDStr = PromptForInput("Please choose a Student by ID Number or Name: ");
                // Validate Student ID input (Must be an integer within the range of student ID values)
                int inputStuID = -1;
                if (int.TryParse(inputStuIDStr, out inputStuID))
                {
                    Console.WriteLine("Integer Input detected!");
                    if (inputStuID >= 0 && inputStuID < studentCount)
                    {
                        Console.WriteLine("You have selected Student #{0}: {1}!", inputStuID, names[inputStuID]);
                        targetStudentID = inputStuID;
                        inputStuIDValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: No student with that ID exists. Please enter another number.");
                    }
                }
                else
                {
                    Console.WriteLine("String Input detected!");
                    for (int i = 0; i < studentCount; i++)
                    {
                        if (inputStuIDStr.Equals(names[i]))
                        {
                            Console.WriteLine("You have selected Student #{0}: {1}!", i, names[i]);
                            targetStudentID = i;
                            inputStuIDValid = true;
                        }
                    }
                    if (!inputStuIDValid)
                    {
                        Console.WriteLine("Error: No Student with the given string as a name was found in the system. Please try again.");
                    }
                }
                MakeLineSpace(1);
            }
            return targetStudentID;
        }
    }
}
