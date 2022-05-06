using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.MSDEV;

namespace Cake.MSDEV.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ICakeContext context = new MYContext();


            context.BatchFile(@"C:/Build/test.bat");
            context.GAC_INSTALL(@"C:\SourceCode\OutputUnicode\My.Net.Assembly.dll");
            context.GAC_UNINSTALL(@"My.Net.Assembly");
            context.TLBIMP( @"C:\SourceCode\OutputUnicode\dlltoimport.dll",
                            @"C:\SourceCode\OutputUnicode\Interop.dlltoimport.dll",
                            "DLLNamespace", 
                            true);

            Console.ReadLine();
        }
    }


    public class MYContext : ICakeContext
    {
        public IFileSystem FileSystem => throw new NotImplementedException();

        public ICakeEnvironment Environment => throw new NotImplementedException();

        public IGlobber Globber => throw new NotImplementedException();

        public ICakeLog Log => new MyLogger();

        public ICakeArguments Arguments => throw new NotImplementedException();

        public IProcessRunner ProcessRunner => throw new NotImplementedException();

        public IRegistry Registry => throw new NotImplementedException();

        public IToolLocator Tools => throw new NotImplementedException();

        public ICakeDataResolver Data => throw new NotImplementedException();

        public ICakeConfiguration Configuration => throw new NotImplementedException();
    }

    class MyLogger : ICakeLog
    {
        public Verbosity Verbosity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Write(Verbosity verbosity, LogLevel level, string format, params object[] args)
        {
            Console.WriteLine(format);
        }
    }
}
