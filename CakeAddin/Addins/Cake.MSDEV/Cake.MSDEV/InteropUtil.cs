using Cake.Core;
using Cake.Core.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.MSDEV
{
    public static class InteropUtil
    {

        [CakeMethodAlias]
        public static void TLBIMP
            (
            this ICakeContext context,
            string binaryPath,
            string outFile,
            string binaryNamespace = "",
            bool throwExceptionOnExitCodeNotZero = false
            )
        {
            var toolname = @"C:\cake\tools\tlbimp.exe";
            var cmd = string.Empty;
            if (binaryNamespace.Trim() == string.Empty)
            {
                cmd = string.Format($@"""{toolname}"" ""{binaryPath}"" /namespace:""{binaryNamespace}"" /out:""{outFile}""");
            }
            else
            {
                cmd = string.Format($@"""{toolname}"" ""{binaryPath}"" /out:""{outFile}""");
            }
            ProcessExecutor.ExecuteCommandInCakeContext(context, cmd, throwExceptionOnExitCodeNotZero);
        }

    }
}
