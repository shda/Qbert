using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public class BuidingWindow : EditorWindow
{
    private const string pathToSave = "Assets/InfoBuildWindow.txt";
    private const string pathToBuildVersion = "Assets/version.txt";

    private List<BuildProgramm> buildProgramms = new List<BuildProgramm>();
    private Vector2 scroll = new Vector2(0,0);
    private float width = 0;

    void OnEnable()
    {
        Load();
    }

    void OnDisable()
    {
        Save();
    }

    void OnGUI()
    {
        if (GUI.changed)
        {
            EditorUtility.SetDirty(this);
        }

        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Width(this.position.width),
            GUILayout.Height(this.position.height));
        {
            DrawGUIProgramms();
            DrawGUIButtons();
        }
        EditorGUILayout.EndScrollView();
    }

    private void DrawGUIProgramms()
    {
        foreach (var buildProgramm in buildProgramms)
        {
            if (DrawProgramm(buildProgramm))
            {
                return;
            }
        }
    }

    private bool DrawProgramm(BuildProgramm programm)
    {
        EditorGUILayout.BeginHorizontal();
        programm.foldout = EditorGUILayout.Foldout(programm.foldout, programm.buildPlatform.ToString());
        programm.isEnable = EditorGUILayout.Toggle("", programm.isEnable);
        if (ControlButtons(programm))
        {
            return true;
        }
        EditorGUILayout.Space();

        EditorGUILayout.EndHorizontal();
        {
            if (programm.foldout)
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(15);

                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.Space();
                            ShowInfo(programm);
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
        }
        return false;
    }

    private bool ControlButtons(BuildProgramm programm)
    {
        int index = buildProgramms.IndexOf(programm);

        if (GUILayout.Button("Up"))
        {
            if (index > 0)
            {
                index--;
            }

            buildProgramms.Remove(programm);
            buildProgramms.Insert(index, programm);
            return true;
        }

        if (GUILayout.Button("Down"))
        {
            if (index < buildProgramms.Count - 1)
            {
                index++;
            }

            buildProgramms.Remove(programm);
            buildProgramms.Insert(index, programm);
            return true;
        }

        GUILayout.Space(30);
        if (GUILayout.Button("Delete"))
        {
            buildProgramms.Remove(programm);
            return true;
        }

        return false;
    }

    private void ShowInfo(BuildProgramm programm)
    {
        programm.topDirectory = EditorGUILayout.TextField("Имя конечной папки", programm.topDirectory);
        programm.rootDirectory = EditorGUILayout.TextField("Имя в папке Assets", programm.rootDirectory);
        EditorGUILayout.Space();
        programm.isRebuild = EditorGUILayout.Toggle("Пересобрать?", programm.isRebuild);
        
        EditorGUILayout.Space();
        programm.zipFolder = EditorGUILayout.Toggle("Zip?", programm.zipFolder);
        programm.isCopyAfterBuild = EditorGUILayout.Toggle("Копировать после сборки?", programm.isCopyAfterBuild);
        EditorGUILayout.LabelField("Папка куда будут копироваться файлы");
        programm.pathToFolderCopyFiles = EditorGUILayout.TextField("", programm.pathToFolderCopyFiles);

        ShowPlatform(programm);
    }

    private void ShowPlatform(BuildProgramm programm)
    {
        if (programm.buildTargetsNames == null)
        {
            programm.CreateTargetsNames();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Платформа:");
        int select = Array.IndexOf(programm.buildTargetsNames, programm.buildPlatform.ToString());
        select = EditorGUILayout.Popup(select, programm.buildTargetsNames);
        programm.buildPlatform = (BuildTarget)Enum.Parse(typeof(BuildTarget), programm.buildTargetsNames[select]);
    
    }

    private void DrawGUIButtons()
    {
        EditorGUILayout.Space();
        if (GUILayout.Button("Add programm"))
        {
            AddProgramm();
        }
        /*
        EditorGUILayout.Space();
        if (GUILayout.Button("DeleteSelect"))
        {
            DeleleteSelect();
        }
         */
        GUILayout.Space(25);
        if (GUILayout.Button("Start"))
        {
            StartBuild();
        }
    }

    private void StartBuild()
    {
        SetBundleVersion();
        SaveVersionToFile();

        foreach (var buildProgramm in buildProgramms)
        {
            if (buildProgramm.isEnable)
            {
                Build(buildProgramm);
            }
        }
    }
    
    
    void Build(BuildProgramm programm)
    {
        BuildScript buildScript = null;

        if (programm.buildPlatform == BuildTarget.Android)
        {
            buildScript = new BuildScriptAndroid();
        }
        else
        {
            buildScript = new BuildScript();
        }

        buildScript.Build(programm);
    }
   

    private static void SaveVersionToFile()
    {
        try
        {
            SetBundleVersion();
            File.WriteAllText(pathToBuildVersion, PlayerSettings.bundleVersion);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
        catch (Exception)
        {
            Debug.Log("Error save version.");
        }
    }

    private static void SetBundleVersion()
    {
        string format = "yyyy_MM_d_HH_mm";
        PlayerSettings.bundleVersion = DateTime.Now.ToString(format);
    }

    private void AddProgramm()
    {
        BuildProgramm buildProgramm = new BuildProgramm();
        buildProgramm.buildPlatform = BuildTarget.WebGL;
        buildProgramms.Add(buildProgramm);
    }

    [MenuItem("Build/Window build")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BuidingWindow));
    }


    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(pathToSave);
        bf.Serialize(file, buildProgramms);
        file.Close();
    }

    private void Load()
    {
        if (File.Exists(pathToSave))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.Open(pathToSave, FileMode.Open))
                {
                    buildProgramms = new List<BuildProgramm>();
                    buildProgramms = bf.Deserialize(file) as List<BuildProgramm>;

                    file.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Error deserialization " + ex.Message);
            }
        }
    }



	void Start ()
	{
        scroll = new Vector2(0, 0);
	    Load();
	}

	void Update () 
    {
	
	}
}
