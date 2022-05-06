using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.MSDEV
{
    public static class DotNET_COM_Interop
    {

        [CakeMethodAlias]
        public static void GAC_INSTALL
            (
            this ICakeContext context,
            string assembly,
            bool throwExceptionOnExitCodeNotZero = false
            )
        {
            var toolname = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil.exe";
            var cmd = string.Format($@"""{toolname}"" -i ""{assembly}"" ");

            ProcessExecutor.ExecuteCommandInCakeContext(context, cmd, throwExceptionOnExitCodeNotZero);

        }

        [CakeMethodAlias]
        public static void GAC_UNINSTALL
            (
            this ICakeContext context,
            string assembly,
            bool throwExceptionOnExitCodeNotZero = false
            )
        {
            var toolname = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil.exe";
            var cmd = string.Format($@"""{toolname}"" -u ""{assembly}"" ");

            ProcessExecutor.ExecuteCommandInCakeContext(context, cmd, throwExceptionOnExitCodeNotZero);

        }
    }

}
