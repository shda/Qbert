using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

[Serializable]
public class BuildProgramm 
{
    public string topDirectory = @"Build";
    public string rootDirectory = @"Builds";
    public string pathToFolderCopyFiles = @"G:\GoogleDrive\CrossowrdBuild\";

    public bool isEnable;
    public bool isRebuild = true;
    public bool zipFolder;
    public bool isCopyAfterBuild;

    public bool foldout = false;
    public BuildTarget buildPlatform;
    public string projectName = PlayerSettings.productName;

    public string[] buildTargetsNames;

    public void CreateTargetsNames()
    {
        buildTargetsNames = Enum.GetNames(typeof(BuildTarget));
    }

    public BuildProgramm()
    {
        CreateTargetsNames();
    }
}
