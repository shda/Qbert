using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using Debug = UnityEngine.Debug;
using Version = Assets.Qbert.Scripts.Version;

public class BuildSettings : MonoBehaviour
{
    public static string webglPath = @"G:\GoogleDrive\UnityWebBuilds\Qbert\";

    //Android
    [MenuItem("Build/Android/Build", false, 1)]
    static void BuildV1() {
        Build(BuildOptions.None, BuildTarget.Android);
    }

    [MenuItem("Build/WebGl/Increment BuidleVersion and Build", false, 1)]
    static void BuildWebGlIncrement()
    {
        IncrementBungleVersion();
        Build(BuildOptions.None, BuildTarget.WebGL);
    }

    [MenuItem("Build/Android/Increment BuidleVersion and Build", false, 1)]
    static void BuildV1Increment()
    {
        IncrementBungleVersion();
        Build(BuildOptions.None, BuildTarget.Android);
    }

    [MenuItem("Build/Android/Build and Run", false, 2)]
    static void BuildAndRunV1() {
        Build(BuildOptions.AutoRunPlayer, BuildTarget.Android);
    }

    [MenuItem("Build/Android/Increment BuidleVersion and Build and Run", false, 2)]
    static void BuildAndRunIncrement()
    {
        IncrementBungleVersion();
        Build(BuildOptions.AutoRunPlayer, BuildTarget.Android);
    }

    [MenuItem("Build/Android/Build Development", false, 1)]
    static void BuildDevelopment()
    {
        Build(BuildOptions.Development, BuildTarget.Android);
    }

    [MenuItem("Build/Android/Build Development and Run", false, 2)]
    static void BuildAndRunDevelopment()
    {
        Build(BuildOptions.Development | BuildOptions.AutoRunPlayer, BuildTarget.Android);
    }

    //IOS
    [MenuItem("Build/IOS/Build", false, 21)]
    static void BuildIOS() {
        Build(BuildOptions.None, BuildTarget.iOS);
    }

    [MenuItem("Build/IOS/Build and Run", false, 21)]
    static void BuildAndRunIOS()
    {
        Build(BuildOptions.AutoRunPlayer, BuildTarget.iOS);
    }

    private static void IncrementBungleVersion()
    {
        var buildTarget = EditorUserBuildSettings.activeBuildTarget;

        if (buildTarget == BuildTarget.Android)
        {
            PlayerSettings.Android.bundleVersionCode++;
        }
        else if (buildTarget == BuildTarget.iOS)
        {
            int version = 0;
            if (!int.TryParse(PlayerSettings.iOS.buildNumber, out version))
            {
                Debug.LogError("Error parse PlayerSettings.bundleVersion - " + PlayerSettings.bundleVersion);
            }
            else
            {
                PlayerSettings.iOS.buildNumber = (++version).ToString();
            }
        }

        
    }

    private static void Build(BuildOptions buildOptions, BuildTarget buildTarget)
    {
        Debug.Log(">> "+SceneManager.GetActiveScene().buildIndex);
        var loadedLevel = Application.loadedLevel;

        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);

        var scenes = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();

        string properVersionName = string.Format("{0} build {1}", PlayerSettings.bundleVersion, PlayerSettings.Android.bundleVersionCode);
        //PlayerSettings.iOS.buildNumber = properVersionName;

        UpdateScenes(scenes, properVersionName);

       // PreloadSigningAlias.Init();
        AssetDatabase.Refresh();

        var locationPathName = Application.dataPath + "/../";

        if (buildTarget == BuildTarget.Android)
        {
            locationPathName += GetBundleApkName(properVersionName);
            BuildPipeline.BuildPlayer(scenes, locationPathName, buildTarget, buildOptions);
        }
        else if (buildTarget == BuildTarget.iOS)
        {
            locationPathName += GetIosPath(properVersionName);
            if (!Directory.Exists(locationPathName))
            {
                Directory.CreateDirectory(locationPathName);
            }
            BuildPipeline.BuildPlayer(scenes, locationPathName, buildTarget, buildOptions);
            ModifyFilesForIOS(locationPathName, properVersionName);

           // ZipIosFolder(locationPathName, properVersionName);
        }
        else if (buildTarget == BuildTarget.WebGL)
        {
            locationPathName = webglPath;

            //locationPathName += GetBundleApkName(properVersionName);
            BuildPipeline.BuildPlayer(scenes, locationPathName, buildTarget, buildOptions);
        }


        Debug.Log("Build is done.");
        Debug.Log(locationPathName);

        Debug.Log("Loaded level -> "+loadedLevel+" from "+scenes.Length);

       // EditorApplication.OpenScene(scenes[loadedLevel]);
    }

    private static void ModifyFilesForIOS(string locationPathName, string properVersionName) {
        Debug.Log("AA modify for ios");
        var pbxName = locationPathName + "/Unity-iPhone.xcodeproj/project.pbxproj";
        var pbxLines = File.ReadAllLines(pbxName);
        for (var i = 0; i < pbxLines.Length; i++) {
            var pbxLine = pbxLines[i];
            pbxLines[i] = pbxLine.Replace("$(SRCROOT)/Libraries\\\\Plugins/iOS","$(SRCROOT)/Libraries/Plugins/iOS");
            if (pbxLine.Contains("libiconv.2.dylib")) pbxLines[i] = "";
        }
        File.WriteAllLines(pbxName, pbxLines);

        var plistName = locationPathName + "/Info.plist";
        var plistLines = File.ReadAllLines(plistName);
//        for (var i = 0; i < plistLines.Length; i++) {
//            var plistLine = plistLines[i];
//            if (plistLine.Contains("<key>CFBundleVersion</key>")) {
//                plistLines[i + 1] = string.Format("    <string>{0}</string>", properVersionName);
//                break;
//            }
//        }

        plistLines[plistLines.Length - 2] = "    <key>NSAppTransportSecurity</key>\r\n    <dict>\r\n        <key>NSAllowsArbitraryLoads</key>\r\n        <true/>\r\n    </dict>\r\n" + plistLines[plistLines.Length - 2];

        File.WriteAllLines(plistName, plistLines);
    }

    private static string GetIosPath(string properVersionName) {
        return string.Format("../qarcade_v{0}_xcode", properVersionName);
    }

    private static string GetBundleApkName(string properVersionName) {
#if OCULUS
        return string.Format("qarcade_oculus_v{0}.apk", properVersionName);
#else
        return string.Format("qarcade_v{0}.apk", properVersionName);
#endif

    }

    private static void UpdateScenes(string[] scenes, string properVersionName)
    {
        string activeScene = EditorSceneManager.GetActiveScene().path;
        Debug.Log(activeScene);

        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        
        foreach (var scene in scenes)
        {
            EditorApplication.OpenScene(scene);

            var findObjectOfType = FindObjectOfType<Version>();
            
            if (findObjectOfType) {

                EditorUtility.SetDirty(findObjectOfType);
                findObjectOfType.version = properVersionName;
                findObjectOfType.buildCounter++;
                findObjectOfType.date =
                    string.Format("{0:yyMMdd} {1:D2}:{2:D2}", 
                    DateTime.Now, DateTime.Now.Hour, DateTime.Now.Minute);
            }


            /*
            var findGoogleAnalytics = FindObjectOfType<GoogleAnalyticsV4>();
            if (findGoogleAnalytics)
            {
                EditorUtility.SetDirty(findGoogleAnalytics);
                findGoogleAnalytics.productName = Application.productName;
                findGoogleAnalytics.bundleIdentifier = Application.bundleIdentifier;
                findGoogleAnalytics.bundleVersion = PlayerSettings.bundleVersion;
            }
            */
            EditorApplication.SaveScene(scene);
        }
        

        EditorSceneManager.OpenScene(activeScene);
    }

    [PostProcessBuild]
	public static void OnPostprocessBuild (BuildTarget buildTarget, string path)
	{
        if (buildTarget == BuildTarget.iOS)
        {
            //tvOS support was introduced in 5.3.1, causing the target file to be renamed 
#if UNITY_5_3_OR_NEWER && !UNITY_5_3_0
            string viewControllerFile = "UnityViewControllerBaseiOS.mm";
#else
			string viewControllerFile = "UnityViewControllerBase.mm";
#endif

            //include the leading tab character in the target string so we don't re-re-comment on each Build -> Append
            string targetString = "\tNSAssert(UnityShouldAutorotate()";
            string filePath = Path.Combine(path, "Classes");
            filePath = Path.Combine(filePath, "UI");
            filePath = Path.Combine(filePath, viewControllerFile);
            if (File.Exists(filePath))
            {
                string classFile = File.ReadAllText(filePath);
                string newClassFile = classFile.Replace(targetString, "\t//NSAssert(UnityShouldAutorotate()");
                if (classFile.Length != newClassFile.Length)
                {
                    File.WriteAllText(filePath, newClassFile);
                    Debug.Log("Disable iOS Autorotate Assertion succeeded for file: " + filePath);
                }
                else {
                    Debug.LogWarning("Disable iOS Autorotate-Assertion FAILED -- Target string not found: \"" + targetString + "\"");
                }
            }
            else {
                Debug.LogWarning("Disable iOS Autorotate-Assertion FAILED -- File not found: " + filePath);
            }
        }
    }
}
