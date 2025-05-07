using System;
using System.Collections.Generic;



namespace Task_Tracker_project
{
    class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duedate { get; set; }

        public string Priority { get; set; }
        public string Status { get; set; }

        //constructor
        public Task(int id, string title, string description, DateTime duedate, string priority, string status)
        {
            Id = id;
            Title = title;
            Description = description;
            Duedate = duedate;
            Priority = priority;
            Status = status;
        }


        public void DisplayTaskDetails()
        {
            Console.WriteLine($"----------------------------");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Due Date: {Duedate.ToShortDateString()}");
            Console.WriteLine($"Priority: {Priority}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"----------------------------\n");
        }

    }

    internal class Program
    {
        static List<Task> tasks = new List<Task>();
        static void Main(string[] args)
        {
            // task option from user
            bool running = true;

            while (running)
            {
                Console.WriteLine("==== Task Tracker Menu ====");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Update Task Status");
                Console.WriteLine("3. View All Tasks");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Choose an option: ");


                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        HandleAddTask();
                        break;
                    case "2":
                        HandleUpdateTaskStatus();
                        break;
                    case "3":
                        DisplayTasks();
                        break;
                    case "4":
                        HandleDeleteTask();
                        break;
                    case "5":
                        running = HandleExit();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }
        
        
        // handle add task
        static void HandleAddTask()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Due Date (yyyy-mm-dd): ");
            string dueDateInput = Console.ReadLine();

            DateTime dueDate;
            while( !DateTime.TryParse(dueDateInput, out dueDate))
            {
                Console.Write("invalid date. PLease enter again (yyyy-mm-dd) ");
                dueDateInput = Console.ReadLine() ;
            }

            Console.Write("Enter Priority (High / Medium / Low): ");
            string priority = Console.ReadLine();

            string status = "Pending";
            int newId = tasks.Count + 1;

            Task newTask = new Task(newId, title, description, dueDate, priority, status);
            tasks.Add(newTask);

            Console.WriteLine("Task added successfully! ");
        }

        //Handle update task status
        static void HandleUpdateTaskStatus()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            Console.WriteLine("Current Tasks: ");
            DisplayTasks();

            Console.Write("Enter the task ID to Update: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task taskToUpdate = tasks.Find(t => t.Id == taskId);

                if (taskToUpdate != null)
                {
                    Console.Write("Enter new status (Pending / In Progress / Completed): ");
                    string newStatus = Console.ReadLine();

                    if (newStatus == "Pending" || newStatus == "In Progress" || newStatus == "Completed")
                    {
                        taskToUpdate.Status = newStatus;
                        Console.WriteLine("Task status updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine(" Invalid status.");
                    }
                }
                else
                {
                    Console.WriteLine("Task not found");
                }
            }
            else
            {
                Console.WriteLine("invalid input. Please enter a valid task id.");
            }
        }

        //display task
        static void DisplayTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks availabe.");
                return;
            }

            Console.WriteLine("\n Active Tasks: ");
            foreach (var task in tasks)
            {
                if((task.Status == "Pending" || task.Status == "In Progress") && task.Duedate >= DateTime.Now)
                {
                    task.DisplayTaskDetails();
                }
            }
            
            Console.WriteLine("\n Overdue Tasks: ");
            foreach (var task in tasks)
            {
                if((task.Status == "Pending" || task.Status == "In Progress") && task.Duedate < DateTime.Now)
                {
                    task.DisplayTaskDetails();
                }
            }
            
            Console.WriteLine("\n Completed Tasks: ");
            foreach (var task in tasks)
            {
                if(task.Status == "Completed" )
                {
                    task.DisplayTaskDetails();
                }
            }

      
        }

        // Handle Delete Task
        static void HandleDeleteTask()
        {
            if(tasks.Count == 0)
            {
                Console.WriteLine("No tasks to delete.");
                return;
            }

            Console.WriteLine("Current Tasks: ");
            DisplayTasks();

            Console.WriteLine("Enter the Task ID to delete: ");
            if(int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task taskToDelete = tasks.Find(t => t.Id==taskId);
                if(taskToDelete != null)
                {
                    tasks.Remove(taskToDelete);
                    Console.WriteLine("Task deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            else
            {
                Console.WriteLine("invalid input . PLease enter a vaid task id.");
            }
        }

        // exit
        static bool HandleExit()
        {
            Console.Write("Are you sure want to exit? (y/n): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "y")
            {
                Console.WriteLine("Exiting the application.");
                return false;
            }
            else
            {
                Console.WriteLine("Exit cancelled.");
                return true;
            }

        }
    }
}
