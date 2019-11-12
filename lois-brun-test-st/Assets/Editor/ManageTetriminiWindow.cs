using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ManageTetriminiWindow : EditorWindow
{     
    public List<TetriminoTypeWrapper> m_availableTetriminiTypes;

    private Vector2 m_windowScrollPos;

    [MenuItem("Window/Manage Tetrimini")]
    public static void ShowWindow()
    {      
        EditorWindow.GetWindow(typeof(ManageTetriminiWindow));        
    }

    [MenuItem("Tools/Reset Available Tetrimini")]
    public static void ResetTetrimini()
    {
        TweakingMenu.ResetTetrimini();
    }

    void OnGUI()
    {       
        m_windowScrollPos = EditorGUILayout.BeginScrollView(m_windowScrollPos);

        //called only once if it's first time we open window and Tweaking Menu
        // doesn't have any "available tetrimini" yet
        if (TweakingMenu.m_availableTetriminiTypes == null)
        {
            TweakingMenu.AddClassicTetrimini();
        }

        m_availableTetriminiTypes = new List<TetriminoTypeWrapper>();

        foreach (TetriminoTypeData type in TweakingMenu.m_availableTetriminiTypes)
        {
            TetriminoTypeWrapper typeWrapper = new TetriminoTypeWrapper(type);
            m_availableTetriminiTypes.Add(typeWrapper);

            typeWrapper.m_typeName = EditorGUILayout.TextField("Tetrimino Name", typeWrapper.m_typeName, GUILayout.Width(260));

            EditorGUILayout.BeginHorizontal(); //show configurations horizontally

            foreach (List<bool> bools in typeWrapper.m_typeConfigurations)
            {
                EditorGUILayout.BeginVertical(GUILayout.Width(100)); //show the 4 matrix lines vertically
                EditorGUILayout.LabelField("Configuration :", GUILayout.Width(100));
                
                EditorGUILayout.BeginHorizontal();
                bools[0] = EditorGUILayout.Toggle(bools[0], GUILayout.Width(15));
                bools[4] = EditorGUILayout.Toggle(bools[4], GUILayout.Width(15));
                bools[8] = EditorGUILayout.Toggle(bools[8], GUILayout.Width(15));
                bools[12] = EditorGUILayout.Toggle(bools[12], GUILayout.Width(15));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                bools[1] = EditorGUILayout.Toggle(bools[1], GUILayout.Width(15));
                bools[5] = EditorGUILayout.Toggle(bools[5], GUILayout.Width(15));
                bools[9] = EditorGUILayout.Toggle(bools[9], GUILayout.Width(15));
                bools[13] = EditorGUILayout.Toggle(bools[13], GUILayout.Width(15));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                bools[2] = EditorGUILayout.Toggle(bools[2], GUILayout.Width(15));
                bools[6] = EditorGUILayout.Toggle(bools[6], GUILayout.Width(15));
                bools[10] = EditorGUILayout.Toggle(bools[10], GUILayout.Width(15));
                bools[14] = EditorGUILayout.Toggle(bools[14], GUILayout.Width(15));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                bools[3] = EditorGUILayout.Toggle(bools[3], GUILayout.Width(15));
                bools[7] = EditorGUILayout.Toggle(bools[7], GUILayout.Width(15));
                bools[11] = EditorGUILayout.Toggle(bools[11], GUILayout.Width(15));
                bools[15] = EditorGUILayout.Toggle(bools[15], GUILayout.Width(15));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical(); //show the 4 matrix lines vertically
            }

            EditorGUILayout.EndHorizontal(); //show configurations horizontally
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
        }

        

        EditorGUILayout.EndScrollView();


        //GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        //testString = EditorGUILayout.TextField("Text Field", testString);

        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);        
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();
    }
};

public class TetriminoTypeWrapper
{
    public List<List<bool>> m_typeConfigurations;
    public string           m_typeName;

    public TetriminoTypeWrapper() { }

    public TetriminoTypeWrapper(TetriminoTypeData _data)
    {
        m_typeName = _data.m_name;
        m_typeConfigurations = new List<List<bool>>();

        foreach (Matrix4x4 matrix in _data.m_configurations)
        {
            List<bool> bools = new List<bool>(16);
            bools.Add(matrix.m00 == 1 ? true : false);
            bools.Add(matrix.m01 == 1 ? true : false);
            bools.Add(matrix.m02 == 1 ? true : false);
            bools.Add(matrix.m03 == 1 ? true : false);
            bools.Add(matrix.m10 == 1 ? true : false);
            bools.Add(matrix.m11 == 1 ? true : false);
            bools.Add(matrix.m12 == 1 ? true : false);
            bools.Add(matrix.m13 == 1 ? true : false);
            bools.Add(matrix.m20 == 1 ? true : false);
            bools.Add(matrix.m21 == 1 ? true : false);
            bools.Add(matrix.m22 == 1 ? true : false);
            bools.Add(matrix.m23 == 1 ? true : false);
            bools.Add(matrix.m30 == 1 ? true : false);
            bools.Add(matrix.m31 == 1 ? true : false);
            bools.Add(matrix.m32 == 1 ? true : false);
            bools.Add(matrix.m33 == 1 ? true : false);

            m_typeConfigurations.Add(bools);
        }
    }

    public TetriminoTypeData ToData()
    {
        TetriminoTypeData data = new TetriminoTypeData();
        data.m_name = m_typeName;
        data.m_configurations = new List<Matrix4x4>();

        foreach(List<bool> bools in m_typeConfigurations)
        {
            Matrix4x4 _mat = new Matrix4x4(
                new Vector4(bools[0] ? 1 : 0, bools[1] ? 1 : 0, bools[2] ? 1 : 0, bools[3] ? 1 : 0),
                new Vector4(bools[4] ? 1 : 0, bools[5] ? 1 : 0, bools[6] ? 1 : 0, bools[7] ? 1 : 0),
                new Vector4(bools[8] ? 1 : 0, bools[9] ? 1 : 0, bools[10] ? 1 : 0, bools[11] ? 1 : 0),
                new Vector4(bools[12] ? 1 : 0, bools[13] ? 1 : 0, bools[14] ? 1 : 0, bools[15] ? 1 : 0)
                );
        }

        return data;
    }    
}