Need to include Cake.MSDev.dll to use it functionality at the top.

#r "./Cake.MSDev.dll"

-----------------------------------------------------------------
Execute Batch File
-----------------------------------------------------------------
BatchFile("C:/SourceCode/Build/test.bat");


Task("TestBatch")
    .IsDependentOn("XYZ")
    .ContinueOnError()
    .Does(() =>
{
    BatchFile("C:/SourceCode/Build/test.bat");
});

-----------------------------------------------------------------
Execute Batch File
-----------------------------------------------------------------
TLBIMP(System.IO.Path.GetFullPath(inputBinary, namespace, outputBinary);
                    

Task("TLBImport")
    //.IsDependentOn("Build")
    //.ContinueOnError()
        .IsDependentOn("SetConfig")
    .Does(() =>
                {
                    TLBIMP(System.IO.Path.GetFullPath(buildDir + "/<FileName>.dll"),    "<fileNamespace>",    System.IO.Path.GetFullPath(buildDir +"/Interop.<FileName>.dll"));
                }
        )
.OnError(exception =>
{
    // Handle the error here.
    
})
;