using Cake.Core;
using Cake.Core.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.MSDEV
{
    public static class ProcessExecutor
    {

        public static void ExecuteCommandInCakeContext(ICakeContext context, string command, bool throwExceptionOnExitCodeNotZero)
        {
            try
            {
                context?.Log.Write(Verbosity.Normal, LogLevel.Information,"Executing:"+ command);
                
                var exitCode = ProcessExecutor.ExecuteCommand(command);

                if (exitCode != 0)
                {
                    context?.Log.Write(Verbosity.Normal, LogLevel.Error, "Error: Executing command");
                    context?.Log.Write(Verbosity.Normal, LogLevel.Error, command);

                    if (throwExceptionOnExitCodeNotZero)
                    {
                        throw new ApplicationException("Error:" + command);
                    }
                }
                else
                {
                    context?.Log.Write(Verbosity.Normal, LogLevel.Information, "Success: Executing command");
                    context?.Log.Write(Verbosity.Normal, LogLevel.Information, command);
                }
            }
            catch(Exception ex)
            {
                if (ex is ApplicationException)
                {
                    //do nothing
                }
                else
                {
                    context?.Log.Write(Verbosity.Normal, LogLevel.Error, (ex.Message + ex.StackTrace));
                }
                throw;
            }
        }

        public static int ExecuteCommand(string command)
        {
            var truearg = @" /c """ + command + @"""";

            var processInfo = new ProcessStartInfo("cmd.exe", truearg);
            var exitcode = 0;
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            exitcode = process.ExitCode;
            process.Close();
            return exitcode;
        }
    }
}
