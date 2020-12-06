using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace itsours
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        // To hide the window
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        Regex TXTregex;
        Regex PS1regex;
        Regex BATregex;
        Regex EXEregex;
        Regex PHPregex;
        Regex ASPregex;
        Regex ASPXregex;
        Regex itsoursregex;

        FileSystemWatcher watcher = new FileSystemWatcher();
        List<FileSystemWatcher> watches = new List<FileSystemWatcher>();

        public Program()
        {
            //this.hide();
            this.TXTregex = new Regex(@"^.*\.txt$", RegexOptions.IgnoreCase);
            this.PS1regex = new Regex(@"^.*\.ps1$", RegexOptions.IgnoreCase);
            this.BATregex = new Regex(@"^.*\.bat$", RegexOptions.IgnoreCase);
            this.EXEregex = new Regex(@"^.*\.exe$", RegexOptions.IgnoreCase);
            this.PHPregex = new Regex(@"^.*\.php$", RegexOptions.IgnoreCase);
            this.ASPregex = new Regex(@"^.*\.asp$", RegexOptions.IgnoreCase);
            this.ASPXregex = new Regex(@"^.*\.aspx$", RegexOptions.IgnoreCase);
            this.itsoursregex = new Regex(@"^.*itsoursnow.*", RegexOptions.IgnoreCase);
            this.start_me();
        }


        public void start_me()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            FileSystemWatcher watch;

            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Fixed)
                {
                    watch = new FileSystemWatcher();
                    watch.InternalBufferSize = 1024 * 1024;
                    watch.Path = d.RootDirectory.ToString();
                    watch.IncludeSubdirectories = true;
                    watch.NotifyFilter = NotifyFilters.FileName;
                    watch.Created += new FileSystemEventHandler(OnCreation);
                    watch.EnableRaisingEvents = true;
                    watches.Add(watch);
                }
            }
            return;
        }

        public bool TryToCopy(FileSystemEventArgs e) {
            try
            {
                if (!File.Exists(e.FullPath))
                    return false;
                string fileName = Path.GetFileName(e.FullPath);
                string destFile = Path.Combine("C:\\itsoursnow", fileName);
                File.Copy(e.FullPath, destFile, true);
                Console.WriteLine(e.FullPath);

                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void OnCreation(object source, FileSystemEventArgs e)
        {
            if(itsoursregex.Match(e.FullPath).Success) return;

            if (TXTregex.Match(e.FullPath).Success |
                PS1regex.Match(e.FullPath).Success |
                EXEregex.Match(e.FullPath).Success |
                ASPregex.Match(e.FullPath).Success |
                PHPregex.Match(e.FullPath).Success |
                ASPXregex.Match(e.FullPath).Success |
                BATregex.Match(e.FullPath).Success)
            {

                while (true)
                {
                    if (TryToCopy(e) == true)
                    {
                        break;
                    }
                }

                return;
            }
        }

        private void hide()
        {
            var handle = GetConsoleWindow();
            // Hide
            ShowWindow(handle, SW_HIDE);
            return;
        }

        static void Main(string[] args)
        {
            try
            {
                string itsoursnowPath = @"C:\itsoursnow";
                Directory.CreateDirectory(itsoursnowPath);
                Program p = new Program();
                var name = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Main(args);
            }
        }
    }
}
