using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*  TESTING CUSTOM PROPERTY EDITOR
[CustomPropertyDrawer(typeof(NewTetriTypeAttribute))]
public class TetriminoPropertyDrawer : PropertyDrawer
{
    public Texture m_testTexture;

    public TetriminoPropertyDrawer()
    {
        m_testTexture = Texture2D.blackTexture;
        //new Texture2D()
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 300.0f;
    }
    
    public void FunctionOnItemSelected()
    {
        Debug.Log("coucou");
    }

    public override void OnGUI(Rect _rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(_rect, label, property);

        NewTetriTypeAttribute tetriEnum = (NewTetriTypeAttribute)attribute;

        EditorGUI.LabelField(_rect, label.text); //name of class ?
       
        //EditorGUI.DrawTextureAlpha(new Rect(_rect.xMin, _rect.yMin + 20, 100, 100), m_testTexture);
        EditorGUI.DrawPreviewTexture(new Rect(_rect.xMin, _rect.yMin + 20, 50, 50), m_testTexture);

       
        //Rect posDropdownMenu = new Rect(_rect.xMin, _rect.yMin + 80, 50, 50);

        //GUIContent contentDropdown = new GUIContent("texxxt");

        //GenericMenu genericMenu = new GenericMenu();
        //genericMenu.AddItem(new GUIContent("guiContent"), false, FunctionOnItemSelected);
        //genericMenu.DropDown(posDropdownMenu);

        //EditorGUI.DropdownButton(posDropdownMenu, contentDropdown, FocusType.Passive);


        //switch (tetriEnum.m_type)
        //{
        //    case NewTetriTypeAttribute.eNewTetriminoType.BLANDK:
        //        EditorGUI.LabelField(position, label.text);
        //        break;

        //    case NewTetriTypeAttribute.eNewTetriminoType.TYPE_I:
        //        EditorGUI.LabelField(position, label.text, "avec un label en plus");
        //        break;

        //    case NewTetriTypeAttribute.eNewTetriminoType.TYPE_J:
        //        EditorGUI.DrawPreviewTexture(position, TEST);
        //        break;

        //    default:
        //        break;
        //}

        EditorGUI.EndProperty();
    }
}
*/