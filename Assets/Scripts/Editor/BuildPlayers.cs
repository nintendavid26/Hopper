using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public struct BuildSettings
{
     string locationPathName;
     BuildTarget target;
}

public class BuildPlayers : EditorWindow
{
    private static List<string> Targets = new List<string>{ "Windows","Mac","Linux","WebGL" };
    private static List<bool> ShouldBuild= new List<bool> {false,false,false,false};
    private static List<string> Scenes;
    private static List<bool> IncludedScenes;
    private static Dictionary<BuildTarget, string> Paths=new Dictionary<BuildTarget, string>
    {
        {BuildTarget.WebGL, "Builds/WebGL" },
        {BuildTarget.StandaloneWindows,"Builds/Windows" },
        {BuildTarget.StandaloneLinuxUniversal, "Builds/Linux" },
        {BuildTarget.StandaloneOSXUniversal, "Builds/Mac" },

    };
    public static void MultiBuild()
    {
        BuildTo("Builds/Windows", UnityEditor.BuildTarget.StandaloneWindows64);

    }
    [MenuItem("BuildPlayers/Build")]
    public static void ShowWindow()
    {
        Scenes = new List<string>();
        DirSearch("Assets/_Scenes", Scenes);
        IncludedScenes=new List<bool>();
        IncludedScenes.Capacity = Scenes.Count;
        IncludedScenes.SetAll(true);
        GetWindow(typeof(BuildPlayers));
        
        
    }

    void OnGUI()
    {
        
       
        // The actual window code goes here
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        GUILayout.Label("Build To:", EditorStyles.boldLabel);
        if (ShouldBuild.AllSame(true))
        {
            if (GUILayout.Button("Deselect All"))
            {
                ShouldBuild.SetAll(false);
            }

        }
        else
        {
            if (GUILayout.Button("Select All"))
            {
                ShouldBuild.SetAll(true);
            }
        }
        for (int i = 0; i < Targets.Count; i++)
        {
            ShouldBuild[i] = EditorGUILayout.Toggle(Targets[i], ShouldBuild[i]);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        GUILayout.Label("Found Scenes:", EditorStyles.boldLabel);
        if (IncludedScenes.AllSame(true))
        {
            if (GUILayout.Button("Deselect All"))
            {
                IncludedScenes.SetAll(false);
            }

        }
        else
        {
            if (GUILayout.Button("Select All"))
            {
                IncludedScenes.SetAll(true);
            }
        }
        for (int i = 0; i <Scenes.Count; i++)
        {
            IncludedScenes[i] = EditorGUILayout.Toggle(Scenes[i], IncludedScenes[i]);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

    }

    static void DirSearch(string sDir,List<string>Scenes,string[] excluded=null)
    {
        excluded = excluded ?? new string[0];
        foreach (string d in Directory.GetDirectories(sDir))
        {
            foreach (string f in Directory.GetFiles(d))
            {
                
                if (f.Split('.').Last()=="unity")
                {
                    string name = f.Split('.').ToList().RemoveLast().ToString();
                    Debug.Log(f);
                    if (!excluded.Contains(f))
                    {
                        Scenes.Add(f);
                    }
                }
            }
            DirSearch(d, Scenes);
        }

    }

    static void BuildTo(string locationPathName, BuildTarget target)
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        List<string> Scenes = new List<string>();
        DirSearch("Assets/_Scenes", Scenes);
        /*
        buildPlayerOptions.scenes = Scenes.ToArray();
        buildPlayerOptions.locationPathName = locationPathName;
        buildPlayerOptions.target = target;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        */
    }
}
