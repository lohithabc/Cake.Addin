using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.MSDEV
{
    
    public static class BatchFileUtil
    {
        [CakeMethodAlias]
        public static void BatchFile(
            this ICakeContext context,
            string batfile,
            bool throwExceptionOnExitCodeNotZero = false
            )
        {
            if (!File.Exists(batfile))
            {
                context?.Log.Write(Verbosity.Normal, LogLevel.Error, $"BatFile not found - {batfile}");
                throw new ApplicationException($"Batch File not found {batfile}");
            }

            ProcessExecutor.ExecuteCommandInCakeContext(context, batfile, throwExceptionOnExitCodeNotZero);
        }
    }
}
