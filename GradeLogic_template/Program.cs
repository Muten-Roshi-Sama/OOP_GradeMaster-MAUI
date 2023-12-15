public class Person {
    private string firstname;
    private string lastname;
    
    public Person(string firstname, string lastname) {
        this.firstname = firstname;
        this.lastname = lastname;
    }

    public string Firstname
        {
            get { return firstname; }
        }

    public string Lastname
        {
            get { return lastname; }
        }
}

public class Student : Person
{
    //possède plusieurs eval
    //obtenir la moyenne des evals
    private List<Eval> evaluations = new List<Eval>();
    private static List<Student> studentList = new List<Student>(); //static: single list for all instances.

    // Dictionary<Eval> evalDict;
    public Student(string firstName, string lastName, List<Eval> evaluations)
        : base(firstName, lastName)    
    {
        this.evaluations = evaluations;
        studentList.Add(this);
    }
    public static List<Student> GetStudentList() {
        return studentList;
    }
    public void Add(Eval eval)
    {
        evaluations.Add(eval);
    }

    public double GetAverage()
    {
        int ECTSsum = 0;
        int pointsTotal = 0;
        int EvalCount = 0;
        foreach(Eval eval in evaluations){
            EvalCount += 1;
            ECTSsum += eval.activity.ECTS;
            pointsTotal += eval.Note()*eval.activity.ECTS;
        }
        // Use double for the division to get a decimal result
        double activityAverage = (double)pointsTotal / EvalCount;
        return activityAverage / ECTSsum;
    }

    public string Bulletin()
    {
        return "hello";
    }

    
}

public class Professor : Person
{
    private double salary; //good practice to use private, use a get function for reading the value. Now its protected from involuntary changes to salary.

    public Professor(string firstName, string lastName, double salary)
        : base(firstName, lastName)
    {
        this.salary = salary;
    }

    public string GetSalary()
    {
        return salary.ToString();
    }
}

public class Activity
{
    //possede un unique enseignant
    public int ECTS;
    public string activityName;
    public Professor professor; //activity has-a professor (composition).

    public Activity(string activityName, Professor professor, int ECTS)
    {
        this.activityName = activityName;
        this.professor = professor;
        this.ECTS = ECTS;
    }
}

public abstract class Eval
{ //implémentation au cas-par-cas donc on la met dans les sous-classes.
    //Par ex. on ne calcule pas l'aire d'un cercle comme l'aire d'un rectangle, implémentation générale impossible
    //toujours liée à une activité
    //type 1 : note chifrée de 0 à 20
    //type 2 : N(=4), C(=8), B(=12), TB(=16), X(=20)

    // constructors in subclasses since Evalabstract

    public Activity activity;
    public abstract int Note();

    // public Note note;
    public Eval(Activity activity)
    {
        this.activity = activity;
    }
    
}
class Cote : Eval
{
    public int note;

    //Constructor
    public Cote(int note, Activity activity)
        : base(activity)
    {
        this.note = note;
    }
    public override int Note(){
        return note;
    }
}

class Appreciation : Eval
{
    private string appreciation {get;}
    private static List<Appreciation> ApprList {get; set;} = new List<Appreciation>();


    public Appreciation(string appreciation, Activity activity) :base(activity)
    {
        this.appreciation = appreciation;
        ApprList.Add(this);
    }

    public override int Note()
    {
        if (appreciation == "X"){return 20;}
        else if (appreciation == "TB"){return 16;}
        else if (appreciation == "B")
        {
            return 12;
        }
        else if (appreciation == "C")
        {
            return 8;
        }
        else if (appreciation == "N")
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Run();
    }

    static void Run()
    {
        Person Jeanne = new Person("Jeanne", "Delafleur");
        Professor Trelawney  = new Professor("Sybille ", "Trelawney ", 4000);
        Professor Snape = new Professor("Severus", "Snape", 8000);
        Professor Chourave = new Professor("Pomona", "Chourave", 8000);

        Activity Divination = new Activity("Divination", Trelawney , 30);
        Activity Potions = new Activity("Potions", Snape, 20);
        Activity Botanique = new Activity("Botanique", Chourave,10);


        List<Eval> eval_Sophie = new List<Eval>
        {
            new Cote(15, Divination),
            new Cote(12, Potions),
            new Appreciation("X", Botanique)
        };
        Student Sophie = new Student("Sophie", "Marcourt", eval_Sophie);
        List<Eval> eval_andy = new List<Eval>
        {
            new Cote(17, Divination),
            new Cote(9, Potions),
            new Appreciation("B", Botanique)
        };
        Student Andy = new Student("Andy", "Myers", eval_andy);

        Console.WriteLine($"{Jeanne.Firstname} is a Person.");
        // Console.WriteLine("Sophie is a student, her evaluations are : " Sophie.Bulletin());
        Console.WriteLine("Trelawney salary is : " + Trelawney.GetSalary());
        Console.WriteLine("Potions professor is : " + Potions.professor.Lastname);
        Console.WriteLine("Sophie's average is : " + Sophie.GetAverage());
        Console.WriteLine("Andy's average is : " + Andy.GetAverage());
        // List<Student> allStudents = Student.GetStudentList();
        foreach(Student student in Student.GetStudentList() ){
            Console.WriteLine($"{student.Firstname} {student.Lastname}");
        }

        // Console.WriteLine();
        // Console.WriteLine();
    }
}

