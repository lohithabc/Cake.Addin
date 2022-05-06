using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.Annotations;
using System.Diagnostics;

namespace Cake.MSDEV
{

    public enum MSDevOperation
    {
        Rebuild,
        Clean
    };
    public static class MSDEVEXE
    {
        /// <summary>
        /// Can run VC 6.0 projects.
        /// Sample args value
        /// ".\FusionLayer.dsp" /MAKE "Release" /REBUILD
        /// ".\FusionLayer.dsp" /MAKE "Release" /CLEAN
        /// ".\FusionLayer.dsp" /MAKE "Release" /CLEAN /NORECURSE
        /// /NORECURSE - don't build dependent projects
        /// </summary>
        [CakeMethodAlias]
        public static void MSDEV
            (
            this ICakeContext context,
            string args,
            string logFileWithPath = "",
            string MSDEVpath = ""
            )
        {

            CheckMSDevExists(ref MSDEVpath);
            context?.Log.Write(Verbosity.Normal, LogLevel.Information, $"MSDEV path is {MSDEVpath}.");

            CreateLogFile(ref logFileWithPath);
            context?.Log.Write(Verbosity.Normal, LogLevel.Information, $"LogFile path is {logFileWithPath}.");

            args = args + " /OUT " + logFileWithPath;
            context?.Log.Write(Verbosity.Normal, LogLevel.Information, $"args is {args}.");
            InvokeMSDEV(MSDEVpath, args);

            ReadLogFileAndShowStatus(context, logFileWithPath);
        }

        private static void ReadLogFileAndShowStatus(ICakeContext context, string logFileWithPath)
        {
            int counter = 0;
            string line;
            string previousline = "";
            System.IO.StreamReader file = new System.IO.StreamReader(logFileWithPath);
            while ((line = file.ReadLine()) != null)
            {
                context?.Log.Write(Verbosity.Normal, LogLevel.Information, $"{line}");
                counter++;
                previousline = line;
            }
            file.Close();

            if (previousline.Contains("- 0 error(s),"))
            {
                context?.Log.Write(Verbosity.Normal, LogLevel.Warning, $"Build is successful {counter}");
            }
            else
            {
                context?.Log.Write(Verbosity.Normal, LogLevel.Error, $"Build Failed {counter}");
                throw new ApplicationException("Build Failed (look for logs for more information)");
            }

            File.Delete(logFileWithPath);
        }

        private static void InvokeMSDEV(string MSDEVpath, string args)
        {
            Process process = new Process();
            process.StartInfo.FileName = MSDEVpath;
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            process.Start();
            process.WaitForExit();
        }

        private static void CreateLogFile(ref string logFileWithPath)
        {
            var logFile = string.Empty;
            if (logFileWithPath == "")
            {
                logFile = Guid.NewGuid() + ".txt";
                logFileWithPath = Path.GetTempPath() + logFile;
                //File.Create((Path.GetTempPath() + "a.txt"));
                //File.Delete((Path.GetTempPath() + "a.txt")); //check to see permision is available.
            }
        }

        private static void CheckMSDevExists(ref string MSDEVpath)
        {
            var exe = @"C:\Program Files (x86)\Microsoft Visual Studio\Common\MSDev98\Bin\msdev.exe";
            if (!File.Exists(exe))
            {
                exe = @"C:\Program Files\Microsoft Visual Studio\Common\MSDev98\Bin\msdev.exe";
            }

            if (MSDEVpath != "")
            {
                exe = MSDEVpath;
            }
            if (!File.Exists(exe))
            {
                throw new FileNotFoundException(exe + "(VC6 is not installed)");
            }

            MSDEVpath = exe;
        }
    }
}
