using GradeMasterMAUI.Services;
using System.Diagnostics;

namespace GradeMasterMAUI.Models
{
    public class Eval
    {
        private string studentFile;
        private string activityFile;
        private string fileName;
        public int eval;
        private Student student;
        private Activity activity;

        public static List<Eval> EvalList;

        public Eval(int eval, string studentFile, string activityFile)
        {
            this.eval = eval;
            this.studentFile = studentFile;
            this.activityFile = activityFile;   
            this.student = Student.Unpack(studentFile);

            this.activity = Activity.Unpack(activityFile);
            fileName = $"{Path.GetRandomFileName()}.Eval.txt";
        }

        //public void Note(string appr)
        //{//type 2 : N(=4), C(=8), B(=12), TB(=16), X(=20)
        //    this.eval = appr switch
        //    {
        //        "X" => 20,
        //        "TB" => 16,
        //        "B" => 12,
        //        "C" => 8,
        //        "N" => 4,
        //        _ => 0,
        //    };
        //}
        

       
        //---Packing---
        public static Eval Unpack(string filename)
        {
            var SaveFilename = Path.Combine(Config.Dir, filename); //constructs the full path to the file 
            string content = FileAccessService.ReadFile(SaveFilename, errorOrigin: "Eval-Unpack"); //reads content of txt
            var tokens = content.Split(Environment.NewLine);

            Eval eval = new(eval: Convert.ToInt32(tokens[0]), studentFile: tokens[1], activityFile: tokens[2])
            {
                GetFileName = filename
            };

            //Debug.WriteLine($"Eval created: {student.DisplayName} with appreciation {activity.professor}");

            return eval;
        }

        public static void UnpackAll()
        {
            //lock (_lockObj)
            //{}
            try
            {
                EvalList = new List<Eval>();
                Config.EnsureDirectory();
                IEnumerable<Eval> AllEvals = Directory
                    .EnumerateFiles(Config.Dir, "*.Eval.txt") //get a list of file names with extension *.student.txt
                    .Select(filename => Eval.Unpack(Path.GetFileName(filename))) //deserialize each instance
                    .OrderBy(eval => eval.GetEvalActivity);
                foreach (var eval in AllEvals)
                {
                    EvalList.Add(eval);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle the case when access to a file or directory is denied
                Debug.WriteLine($"Access denied [Activity]: {ex.Message}");
                // Additional logging or error handling logic can go here
            }
            catch (Exception ex)
            {
                // Handle other types of exceptions
                Debug.WriteLine($"An error occurred [Activity]: {ex.Message}");
                // Additional logging or error handling logic can go here
            }


        }
        public void Pack()
        {
            var SaveFilename = Path.Combine(Config.Dir, GetFileName);
            string content = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, eval, StudentFile, ActivityFile);
            //FileAccessService.WriteFile(SaveFilename, data, errorOrigin: "Eval-Pack");
            FileAccessService.WriteFile(SaveFilename, content, identifier: "Eval", errorOrigin: "Eval-Pack");
        }








        //---GETTERS-----
        public static List<Eval> GetEvalList()
        {
            return EvalList;
        }
        public string GetFileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public string GetEval
        {
            get { return eval.ToString(); }
            //set { eval = value; }
        }
        public string GetEvalActivity
        {
            get { return $"{GetEval}/20  in  {Activity.DisplayName}"; }
            
        }

        public string StudentFile { get => studentFile; set => studentFile = value; } //Encapsulate field but still use field (auto-generated).
        public string ActivityFile { get => activityFile; set => activityFile = value; } //Encapsulate field but still use field (auto-generated).
        public Activity Activity { get => activity; set => activity = value; }
        public Student Student { get => student; set => student = value; }
    }
}
