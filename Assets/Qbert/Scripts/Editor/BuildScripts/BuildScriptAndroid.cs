using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class BuildScriptAndroid : BuildScript 
{
    protected override string CreateEndBuildPath(string pathToBuild, BuildProgramm programm)
    {
        return Path.Combine(pathToBuild , GetNameBuildFille(programm) + ".apk");
    }
}
