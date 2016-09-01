using System;
using UnityEngine;
using System.Collections;
using System.IO;
using Ionic.Zip;
using UnityEditor;

public class BuildScript 
{
    virtual public void Build(BuildProgramm programm)
    {
        string pathToBuildDirectory = string.Format(@"{0}/{1}/{2}", programm.rootDirectory,
            programm.buildPlatform.ToString(), programm.topDirectory);

        if (programm.isRebuild)
        {
            DeleteFolder(pathToBuildDirectory);
            CreateFolder(pathToBuildDirectory);

            string path = CreateEndBuildPath(pathToBuildDirectory, programm);
            BuildPipeline.BuildPlayer(GetScenes(), path, programm.buildPlatform, BuildOptions.None);
        }
        
        After(programm, pathToBuildDirectory);
    }

    protected virtual string CreateEndBuildPath(string pathToBuild, BuildProgramm programm)
    {
        return pathToBuild;
    }

    protected void After(BuildProgramm programm, string pathToBuild)
    {
        string pathToZipFile = programm.rootDirectory + "/" + GetNameBuildFille(programm) + ".zip";

        if (programm.zipFolder)
        {
            ZipFolder(pathToBuild, pathToZipFile);
            if (programm.isCopyAfterBuild)
            {
                CopyFile(pathToZipFile, programm.pathToFolderCopyFiles);
            }
        }
        else
        {
            if (programm.isCopyAfterBuild)
            {
                CopyDirectory(pathToBuild, programm.pathToFolderCopyFiles);
            }
        }
        
    }

    protected static void DeleteFolder(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    protected void CreateFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    protected static string[] GetScenes()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }

    protected string GetNameBuildFille(BuildProgramm programm)
    {
        return programm.projectName + "_" + EditorUserBuildSettings.activeBuildTarget.ToString() +
               "_" + DateTime.Now.ToString("yyyy_MM_d_HH_mm");
    }

    protected void ZipFolder(string path, string pathToSave)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AddDirectory(path, "");
            zip.Save(pathToSave);
        }
    }

    protected void CopyFile(string pathToZipFile, string pathToCopyZip)
    {
        File.Copy(pathToZipFile,
                Path.Combine(pathToCopyZip, Path.GetFileName(pathToZipFile)), true);
    }

    protected void CopyDirectory(string pathToSource, string pathToDest)
    {
        DirectoryCopy(pathToSource, pathToDest);
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, true);
        }

        foreach (DirectoryInfo subdir in dirs)
        {
            string temppath = Path.Combine(destDirName, subdir.Name);
            DirectoryCopy(subdir.FullName, temppath);
        }
    }
}
