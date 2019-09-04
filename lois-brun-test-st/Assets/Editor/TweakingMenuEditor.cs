using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(TweakingMenu))]
public class TweakingMenuEditor : Editor
{
    private ReorderableList m_availableTetriminiList;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        this.serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Available Tetrimini", EditorStyles.boldLabel);
        ReorderableListUtility.DoLayoutListWithFoldout(this.m_availableTetriminiList);
       
        this.serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        var propertyAvailableTetrimini = this.serializedObject.FindProperty("m_availableTetrimini");

        this.m_availableTetriminiList = ReorderableListUtility.CreateAutoLayout(propertyAvailableTetrimini, new string[] { "Name", "Geometry", "Color" });
    }
}