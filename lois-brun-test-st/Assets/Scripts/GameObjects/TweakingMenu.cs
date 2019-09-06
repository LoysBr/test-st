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
    }
};


