using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(CCSVLoader))]
public class CEdit_CSVLoader : Editor
{


    [MenuItem("Assets/Load CSV Data")]
    public static void OpenInspector()
    {
        //Selection.activeObject = test.Instance;
        //Selection.activeObject = CCSVLoader.Instance;
        CCSVLoader.ReadCSV();
    }

    //�ٲ�°� �����ϱ��
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUI.changed == true)
        {
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }

}

#endif