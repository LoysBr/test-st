using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweakingMenu : MonoBehaviour
{
    public static TweakingMenu Instance = null;

    public void Start()
    {
        Instance = this;
    }

    [HideInInspector]
    public List<TetriminoTweak> m_availableTetrimini = new List<TetriminoTweak>();

    [System.Serializable]
    public class TetriminoTweak
    {
        public string                   m_name;
        public Tetrimino.eTetriminoType m_type;
        public Color                    m_color;
    };
       
    
    //NEW
    [HideInInspector]
    public static List<TetriminoTypeData> m_availableTetriminiTypes;

    public static void ResetTetrimini()
    {
        m_availableTetriminiTypes = new List<TetriminoTypeData>();
        AddClassicTetrimini();
    }
    public static void AddClassicTetrimini()
    {
        if(m_availableTetriminiTypes == null)
            m_availableTetriminiTypes = new List<TetriminoTypeData>();

        TetriminoTypeData type_I = new TetriminoTypeData();
        type_I.m_name = "type_I";
        m_availableTetriminiTypes.Add(type_I);

        type_I.m_configurations = new List<Matrix4x4>();
        type_I.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 0, 0),        
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 0, 0)));
        type_I.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 0, 0, 0),
            new Vector4(1, 1, 1, 1),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0)));

        TetriminoTypeData type_O = new TetriminoTypeData();
        type_O.m_name = "type_O";
        m_availableTetriminiTypes.Add(type_O);

        type_O.m_configurations = new List<Matrix4x4>();
        type_O.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 1, 1, 0),
            new Vector4(0, 1, 1, 0),
            new Vector4(0, 0, 0, 0)));

        TetriminoTypeData type_S = new TetriminoTypeData();
        type_S.m_name = "type_S";
        m_availableTetriminiTypes.Add(type_S);

        type_S.m_configurations = new List<Matrix4x4>();
        type_S.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 1, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 0)));
        type_S.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 1, 0),
            new Vector4(1, 1, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0)));

        TetriminoTypeData type_Z = new TetriminoTypeData();
        type_Z.m_name = "type_Z";
        m_availableTetriminiTypes.Add(type_Z);

        type_Z.m_configurations = new List<Matrix4x4>();
        type_Z.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 1, 1, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0)));
        type_Z.m_configurations.Add(new Matrix4x4(
            new Vector4(1, 1, 0, 0),
            new Vector4(0, 1, 1, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0)));

        TetriminoTypeData type_L = new TetriminoTypeData();
        type_L.m_name = "type_L";
        m_availableTetriminiTypes.Add(type_L);

        type_L.m_configurations = new List<Matrix4x4>();
        type_L.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 1, 0),
            new Vector4(0, 0, 0, 0)));
        type_L.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 0, 1, 0),
            new Vector4(1, 1, 1, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0)));
        type_L.m_configurations.Add(new Matrix4x4(
            new Vector4(1, 1, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 0, 0)));
        type_L.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 0, 0, 0),
            new Vector4(1, 1, 1, 0),
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 0, 0, 0)));

        TetriminoTypeData type_J = new TetriminoTypeData();
        type_J.m_name = "type_J";
        m_availableTetriminiTypes.Add(type_J);

        type_J.m_configurations = new List<Matrix4x4>();
        type_J.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 1, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0)));
        type_J.m_configurations.Add(new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(1, 1, 1, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0)));
        type_J.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(1, 1, 0, 0),
            new Vector4(0, 0, 0, 0)));
        type_J.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 0, 0, 0),
            new Vector4(1, 1, 1, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 0)));

        TetriminoTypeData type_T = new TetriminoTypeData();
        type_T.m_name = "type_T";
        m_availableTetriminiTypes.Add(type_T);

        type_T.m_configurations = new List<Matrix4x4>();
        type_T.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 1, 1, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0)));
        type_T.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 0, 0),
            new Vector4(1, 1, 1, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0)));
        type_T.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 1, 0, 0),
            new Vector4(1, 1, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0)));
        type_T.m_configurations.Add(new Matrix4x4(
            new Vector4(0, 0, 0, 0),
            new Vector4(1, 1, 1, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 0, 0)));
    }
};

//NEW
public class TetriminoTypeData
{
    public string m_name;
    public Color m_color;

    public List<Matrix4x4> m_configurations;
};