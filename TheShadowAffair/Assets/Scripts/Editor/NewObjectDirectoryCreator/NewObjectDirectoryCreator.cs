using System;
using UnityEngine;
using UnityEditor;

public class NewObjectDirectoryCreator : EditorWindow
{
    private const string PathToObjects = "Assets/Graphics/_Objects";
    private string _finalLocation;

    //Object Name//
    private string _newObjectName;

    //Subdirectory//
    private bool _createInSubdirectory;
    private string _subdirectoryAdditionalPath;

    //Directories To Create//
    private bool _createDirectoryMaterials;
    private bool _createDirectoryTextures;
    private bool _createDirectorySprites;
    private bool _createDirectoryAnimations;
    private bool _createDirectoryModels;
    private bool _createDirectoryVisualFXs;
    private bool _createDirectoryShaders;


    [MenuItem("ExperimentalFox/Create/New Object Creator")]
    private static void Init()
    {
        // Get existing open window or if none, make a new one:
        NewObjectDirectoryCreator window = (NewObjectDirectoryCreator)EditorWindow.GetWindow(typeof(NewObjectDirectoryCreator));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("New Object Name", EditorStyles.boldLabel);
        _newObjectName = EditorGUILayout.TextField("Object Name", _newObjectName);

        GUILayout.Space(10);

        GUILayout.Label("Create In Subdirectory? (Deeper in `DefaultPath/ObjectName`)", EditorStyles.boldLabel);
        _createInSubdirectory = EditorGUILayout.BeginToggleGroup("Create In Subdirectory", _createInSubdirectory);
        _subdirectoryAdditionalPath = EditorGUILayout.TextField("Path to subdirectory parent", _subdirectoryAdditionalPath);
        EditorGUILayout.EndToggleGroup();

        GUILayout.Space(10);

        GUILayout.Label("Subdirectories To Create", EditorStyles.boldLabel);
        _createDirectoryMaterials = EditorGUILayout.Toggle("Materials", _createDirectoryMaterials);
        _createDirectoryTextures = EditorGUILayout.Toggle("Textures", _createDirectoryTextures);
        _createDirectorySprites = EditorGUILayout.Toggle("Sprites", _createDirectorySprites);
        _createDirectoryAnimations = EditorGUILayout.Toggle("Animations", _createDirectoryAnimations);
        _createDirectoryModels = EditorGUILayout.Toggle("Models", _createDirectoryModels);
        _createDirectoryVisualFXs = EditorGUILayout.Toggle("VFXs", _createDirectoryVisualFXs);
        _createDirectoryShaders = EditorGUILayout.Toggle("Shaders", _createDirectoryShaders);

        GUILayout.Space(10);

        if (GUILayout.Button("Create Directories"))
        {
            ExecuteCreations();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ExecuteCreations()
    {
        #region Set path

        _finalLocation = PathToObjects;

        if (AssetDatabase.IsValidFolder(_finalLocation + "/" + _newObjectName))
        {
            Debug.LogError("Directory for new object already exists!");
            return;
        }

        if (_createInSubdirectory)
        {
            if (AssetDatabase.IsValidFolder(_finalLocation + "/" + _subdirectoryAdditionalPath + "/" + _newObjectName))
            {
                Debug.LogError("Subdirectory already exists!");
                return;
            }

            //Update location//
            _finalLocation += "/" + _subdirectoryAdditionalPath;
        }

        #endregion Set path


        //Create main object dir//
        AssetDatabase.CreateFolder(_finalLocation, _newObjectName);
        
        //Update Path for dir creation//
        _finalLocation += "/" + _newObjectName;

        #region Create Subdirectories

        //Create all subdirectories//
        if (_createDirectoryMaterials)
        {
            CreateNewFolder("Materials");
        }

        if (_createDirectoryTextures)
        {
            CreateNewFolder("Textures");
        }

        if (_createDirectorySprites)
        {
            CreateNewFolder("Sprites");
        }

        if (_createDirectoryAnimations)
        {
            CreateNewFolder("Animations");
        }

        if (_createDirectoryModels)
        {
            CreateNewFolder("Models");
        }

        if (_createDirectoryVisualFXs)
        {
            CreateNewFolder("VFXs");
        }

        if (_createDirectoryShaders)
        {
            CreateNewFolder("Shaders");
        }

        #endregion
        
        //Fix for Unity sometimes not letting me do more than one creation at the time//
        _finalLocation = PathToObjects;
    }

    private void CreateNewFolder(string newDirectoryName)
    {
        AssetDatabase.CreateFolder(_finalLocation, newDirectoryName);
    }
}