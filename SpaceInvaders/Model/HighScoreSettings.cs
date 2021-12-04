using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    public class HighScoreSettings
    {
        private static int leaderBoardSize;

        public static List<String[]> SortByScore()
        {
            List<String> scores = ReadFile();
            List<String[]> seperatedByScorePlayerLevel = new List<string[]>();
            foreach (var current in scores)
            {
                seperatedByScorePlayerLevel.Add(current.Split(","));
            }
            seperatedByScorePlayerLevel.Sort((x, y) => Int32.Parse(y[0]).CompareTo(Int32.Parse(x[0])));
            List<String[]> finalList = new List<string[]>();
            leaderBoardSize = 10;
            for(int i = 0; i < leaderBoardSize; i++)
            {
                finalList.Add(seperatedByScorePlayerLevel[i]);
            }
            return seperatedByScorePlayerLevel;

        }
        public static List<String[]> SortByPlayer()
        {
            List<String[]> unsortedList = SortByScore();
            unsortedList.Sort((x,y)=>String.Compare(x[1],y[1]));
            return unsortedList;
        }
        public static List<String[]> SortByLevel()
        {
            List<String[]> unsortedList = SortByScore();
            unsortedList.Sort((x, y) => String.Compare(y[2], x[2]));
            return unsortedList;
        }
        public static List<String> ReadFile()
        {
            List<String> linesInFile = new List<string>();
            
            string[] lines = System.IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TeamF" + @"\" + "highscore.txt");
            foreach (var current in lines)
            {
                linesInFile.Add(current);
            }
            return linesInFile;
        }

        internal static async Task SubmitScoreAsync(string name, int score, int level)
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TeamF" + @"\" + "highscore.txt",
                score+","+name+","+level + "\n");
            // try
            // {
            //     string output = score + "," + name + "," + level;
            //     //create file in public folder
            //     StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //     StorageFile sampleFile = await storageFolder.CreateFileAsync("highscore.txt", CreationCollisionOption.ReplaceExisting);
            //     File.Copy("highscore.txt", sampleFile.Path);
            //
            //     //write sring to created file
            //     await FileIO.AppendTextAsync(sampleFile, output);
            //
            //     //get asets folder
            //     StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            //     StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync(AppDomain.CurrentDomain.BaseDirectory);
            //
            //     //move file from public folder to assets
            //     await sampleFile.MoveAsync(assetsFolder, "highscore.txt", NameCollisionOption.ReplaceExisting);
            //
            // }
            // catch (Exception ex)
            // {
            //     Debug.WriteLine("error: " + ex);
            //
            // }

        }

        internal static async void SetUpSaveFile()
        {
            System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TeamF");
            Debug.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TeamF"+ @"\" + "highscore.txt"))
            {
                return;
            }
            

            await SubmitScoreAsync("Player", 100, 1);
            await SubmitScoreAsync("Bob", 10, 1);
            await SubmitScoreAsync("Steve", 400, 2);
            await SubmitScoreAsync("Wizard", 1100, 3);
            await SubmitScoreAsync("Arnold", 100, 1);
            await SubmitScoreAsync("Dancer", 10, 1);
            await SubmitScoreAsync("Gamer", 400, 2);
            await SubmitScoreAsync("Kathleen Anderson", 500, 3);
            await SubmitScoreAsync("Max Allred", 500, 3);
            await SubmitScoreAsync("Janera Smith", 500, 3);

        }
    }
}
