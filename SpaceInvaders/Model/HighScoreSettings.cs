using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SpaceInvaders.Model
{
    public class HighScoreSettings
    {
      

        public static List<String[]> SortByScore()
        {
            List<String> scores = ReadFile();
            List<String[]> separatedByScorePlayerLevel = new List<string[]>();
            foreach (var current in scores)
            {
                separatedByScorePlayerLevel.Add(current.Split(","));
            }
            seperatedByScorePlayerLevel.Sort((x, y) => Int32.Parse(y[0]).CompareTo(Int32.Parse(x[0])));
            
            return seperatedByScorePlayerLevel;

        }
        public static List<string[]> SortByPlayer()
        {
            List<string[]> unsortedList = SortByScore();
            unsortedList.Sort((x,y)=>String.CompareOrdinal(x[1],y[1]));
            return unsortedList;
        }
        public static List<String[]> SortByLevel()
        {
            List<String[]> unsortedList = SortByScore();
            unsortedList.Sort((x, y) => String.CompareOrdinal(y[2], x[2]));
            return unsortedList;
        }
        public static List<String> ReadFile()
        {
            List<String> linesInFile = new List<string>();
            
            string[] lines = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TeamF" + @"\" + "highscore.txt");
            foreach (var current in lines)
            {
                linesInFile.Add(current);
            }
            return linesInFile;
        }

        internal static void SubmitScore(string name, int score, int level)
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TeamF" + @"\" + "highscore.txt",
                score + "," + name + "," + level + "\n");

        }

        internal static async void SetUpSaveFile()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TeamF");
            Debug.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TeamF"+ @"\" + "highscore.txt"))
            {
                return;
            }
            

            SubmitScore("Player", 100, 1);
            SubmitScore("Bob", 10, 1);
            SubmitScore("Steve", 400, 2);
            SubmitScore("Wizard", 1100, 3);
            SubmitScore("Arnold", 100, 1);
            SubmitScore("Dancer", 10, 1);
            SubmitScore("Gamer", 400, 2);
            SubmitScore("Kathleen Anderson", 500, 3);
            SubmitScore("Max Allred", 500, 3);
            SubmitScore("Janera Smith", 500, 3);

        }
    }
}
