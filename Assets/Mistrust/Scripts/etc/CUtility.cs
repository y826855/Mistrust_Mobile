using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class CUtility
{
    //
    public enum ESendToMobile
    {
        CLOSE_APP = -1,
        LOCK = 0,
        DOOR_LOCK,

    }

    public enum ESendToPC
    {
        INTERACTION = 0,

    }


    public static string m_FolderPath = "Assets/Mistrust/";
    public static string m_TimeFormat = "yyyy-MM-dd HH:mm:ss";
    //public static IEnumerator Eof = WaitForEndOfFrame();


    //WaitForSeconds ����
    public static WaitForSeconds m_WFS_DOT1 = new WaitForSeconds(0.1f);
    public static WaitForSeconds m_WFS_DOT2 = new WaitForSeconds(0.2f);
    public static WaitForSeconds m_WFS_DOT3 = new WaitForSeconds(0.3f);
    public static WaitForSeconds m_WFS_DOT4 = new WaitForSeconds(0.4f);
    public static WaitForSeconds m_WFS_D5 = new WaitForSeconds(0.5f);
    public static WaitForSeconds m_WFS_1 = new WaitForSeconds(1f);
    public static WaitForSeconds m_WFS_1D5 = new WaitForSeconds(1.5f);
    public static WaitForSeconds m_WFS_2 = new WaitForSeconds(2f);
    public static WaitForSeconds m_WFS_2D5 = new WaitForSeconds(2.5f);
    public static WaitForSeconds m_WFS_10 = new WaitForSeconds(10f);
    public static WaitForSeconds m_WFM_1 = new WaitForSeconds(60f);


    ///<summary>
    ///WaitforSeconds 0.5������ �޾ƿ���
    ///</summary>
    public static WaitForSeconds GetSecD5To2D5(float t)
    {
        if (t <= 0.5f) return m_WFS_D5;
        else if (t <= 1) return m_WFS_1;
        else if (t <= 1.5) return m_WFS_1D5;
        else if (t <= 2) return m_WFS_2;
        else return m_WFS_2D5;
    }

    [System.Serializable]
    public class CLock
    {
        public string m_Password = "";
        public bool m_IsOpened = false;
    }

    /////////////////////////////////////////////////CSV DATA/////////////////////////////////////////////////
    [System.Serializable]
    public class CData_CSV
    {
        public uint m_ID = 0;
        public string m_Name = "";

        //������ �����
        public T Clone<T>() where T : CData_CSV
        {
            return this.MemberwiseClone() as T;
        }
    }
    [System.Serializable]
    public class CData_Iconable : CData_CSV
    {
        public string m_Description = "";
        public Sprite m_Icon = null;
    }




    [System.Serializable]
    public class CData_Item : CData_Iconable
    {
        public int m_Stack = 0;
    }
    /////////////////////////////////////////////////CSV DATA/////////////////////////////////////////////////

    /////////////////////////////////////////////////MAP DATA/////////////////////////////////////////////////
}




//serializableDictionary
[System.Serializable]
public class SerializeDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
{
    [SerializeField]
    protected List<K> keys = new List<K>();

    [SerializeField]
    protected List<V> values = new List<V>();

    public virtual void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (KeyValuePair<K, V> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public virtual void OnAfterDeserialize()
    {
        this.Clear();

        for (int i = 0, icount = keys.Count; i < icount; ++i)
        {
            this.Add(keys[i], values[i]);
            //this.Add(default(K), default(V));
        }
    }
}
//https://everyday-devup.tistory.com/88
////////////////


//����Ű��//
#if UNITY_EDITOR

[SelectionBase]
[ExecuteInEditMode]
public class CEdit_SelectParent : EditorWindow
{
    //% = ctrl
    //& = alt
    //# = shift
    [MenuItem("CustomEdit/Select parent #p")]
    static void SelectParentOfObject()
    {//�θ� �����ϱ� Shift p
        List<Object> parents = new List<Object>();

        foreach (Transform it in Selection.transforms)
        {
            parents.Add(it.parent.gameObject);
            //Debug.Log(it.parent.name);
        }
        Selection.objects = parents.ToArray();
        parents.Clear();
    }

    [MenuItem("CustomEdit/Select child #o")]
    static void SelectChildOfObjectOnly()
    {//�ڽ� �����ϱ� Shilf o

        List<Object> childs = new List<Object>();

        foreach (Transform it in Selection.transforms)
        {
            if (it.childCount == 0) continue;
            childs.Add(it.GetChild(0).gameObject);
        }

        if (childs.Count == 0) return;
        Selection.objects = childs.ToArray();
        childs.Clear();
    }
}

//read only//
public class ReadOnlyAttribute : PropertyAttribute
{

}
#endif

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

#endif