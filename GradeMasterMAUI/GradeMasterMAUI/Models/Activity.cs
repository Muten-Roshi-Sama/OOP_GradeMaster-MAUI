﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeMasterMAUI.Services;
using System.Diagnostics;
using Microsoft.Maui.Storage;

namespace GradeMasterMAUI.Models
{
    public class Activity
    {
        //possede un unique enseignant
        private int ects;
        private string activityName;
        private string fileName;
        private Professor professor; //activity has-a professor (composition).
        private string professorFile;
        //private static readonly object _lockObj = new object();

        public static List<Activity> ActivityList = [];

        public Activity(string activityName, string professorFile, int ects)
        {
            this.activityName = activityName;
            this.professorFile = professorFile;
            this.professor = Professor.Unpack(professorFile);
            this.ects = ects;
            fileName = $"{Path.GetRandomFileName()}.Activity.txt";
            //ActivityList.Add(this);  //not really needed since we recreate the list everytime.
        }


        //---Packing---
        public static Activity Unpack(string filename)
        {
            //Debug.WriteLine($"Unpacking activity from file: {filename}");
            var SaveFilename = Path.Combine(Config.Dir, filename); //constructs the full path to the file 
            //Debug.WriteLine($"Full path to file: {SaveFilename}");


            string content = FileAccessService.ReadFile(path:SaveFilename, errorOrigin: "Activity-Unpack"); //reads content of txt
            var tokens = content.Split(Environment.NewLine);
            //Debug.WriteLine($"Tokens extracted: {string.Join(", ", tokens)}");
            var filePathToken = tokens[1];
            //var filePathToken = tokens[1] + ".Professor.txt";

            Activity activity = new(activityName: tokens[0], professorFile: filePathToken, ects: Convert.ToInt32(tokens[2]))
            {
                GetFileName = filename
            };
           
            //Debug.WriteLine($"Activity created: {activity.DisplayName} with professor {activity.professor}");

            return activity;
        }

        public static void UnpackAll()
        {
            //lock (_lockObj)
            //{}
                try
                {
                    ActivityList = [];
                    Config.EnsureDirAndAesKey();
                    IEnumerable<Activity> Allactivities = Directory
                        .EnumerateFiles(Config.Dir, "*.Activity.txt") //get a list of file names with extension *.student.txt
                        .Select(filename => Activity.Unpack(Path.GetFileName(filename))) //deserialize each instance
                        .OrderBy(activity => activity.DisplayName);
                    foreach (var activity in Allactivities)
                    {
                        ActivityList.Add(activity);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {Debug.WriteLine($"Access denied [Activity]: {ex.Message}");}
                catch (Exception ex)
                {Debug.WriteLine($"An error occurred [Activity]: {ex.Message}");}
            
            
        }
        public void Pack()
        {
            var SaveFilename = Path.Combine(Config.Dir, GetFileName);
            string content = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, ActivityName, ProfessorFile, ECTS);
            //FileAccessService.WriteFile(SaveFilename, data, errorOrigin: "Activity-Pack");
            FileAccessService.WriteFile(SaveFilename, content, identifier: "Activity", errorOrigin: "Activity-Pack");
        }






        //----Getters----
        public static List<Activity> GetActivityList()
        {
            return ActivityList;
        }
        public int ECTS
        {
            get { return ects; }
            set { ects = value; }
        }
        
        public string ActivityName
        {
            get { return activityName; }
        }

        public string ProfessorFile
        {
            get { return professorFile; }
        }
        public string GetFileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string DisplayName
        {
            get { return $"{ActivityName}"; }
        }

        public Professor Professor { get => professor; set => professor = value; } //Encapsulate field but still use field (auto-generated).
    }
}
