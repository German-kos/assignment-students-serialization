using System.Text.Json;
using System.Text.Json.Serialization;
//
string input;
string iName;
int iAge;
string age;
bool run = true;
string jsonString = File.ReadAllText("students.json");
List<Student> studentList = new List<Student>();
studentList = JsonSerializer.Deserialize<List<Student>>(jsonString);
foreach (Student student in studentList)
    student.print();

while (run)
{
    Console.WriteLine();
    Console.WriteLine(@"---Students List JSON Assignment---
Pick an option:
    a - Add a student to the list
    r - Remove a student from the list
    c - Clear the entire list
    p - Print the entire list
    s - Save changes
    q - Quit application");
    input = Console.ReadLine();
    switch (input.ToLower().Trim())
    {
        case "a":
            Console.Write("Enter the Name: ");
            iName = Console.ReadLine();
            Console.Write("Enter the Age: ");
            age = Console.ReadLine();
            if (Int32.TryParse(age.Trim(), out iAge))
                studentList.Add(new Student(iName, iAge));
            else
                Console.WriteLine("Rejected input: age input was invalid");
            break;
        case "r":
            Console.Write("Pick a student to remove from the list by name: ");
            iName = Console.ReadLine();
            bool breaker = false;
            foreach (Student student in studentList)
            {
                if (student.Name.ToLower() == iName.ToLower())
                {
                    studentList.Remove(student);
                    breaker = true;
                    Console.WriteLine($"{iName} has been removed from the list");
                    break;
                }
            }
            if (!breaker)
                Console.WriteLine($"{iName} does not exist in the list.");
            break;
        case "c":
            studentList.Clear();
            Console.WriteLine("The list has been cleared.");
            break;
        case "p":
            foreach (Student student in studentList)
                student.print();
            break;
        case "s":
            string serializedString = JsonSerializer.Serialize(studentList);
            File.WriteAllText("students.json", serializedString);
            Console.WriteLine("Changes have been saved to 'students.json'.");
            break;
        case "q":
            run = false;
            break;
        default:
            Console.WriteLine("Invalid input, please pick one of the listed options.");
            break;
    }
}
class Student
{
    private int defaultTuition = 10000;
    public string Name { get; set; }
    public int Age { get; set; }
    public int Tuition
    {
        get
        {
            if (Age > 25)
                return defaultTuition;
            else return defaultTuition - defaultTuition / 10;
        }
    }
    [JsonConstructor]
    public Student(string name, int age)
    {
        Name = name.Trim();
        Age = age;
    }
    //public Student(string name)
    //{
    //    Name = name.Trim();
    //}
    public void print()
    {
        Console.WriteLine($"{Name}, {Age}, Needs to pay {Tuition}");
    }
}