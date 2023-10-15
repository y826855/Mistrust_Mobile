using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class CUtility
{
    public static string m_FolderPath = "Assets/TextSim/";
    public static string m_TimeFormat = "yyyy-MM-dd HH:mm:ss";
    //public static IEnumerator Eof = WaitForEndOfFrame();


    //WaitForSeconds ����
    public static WaitForSeconds m_WFS_DOT1 = new WaitForSeconds(0.1f);
    public static WaitForSeconds m_WFS_DOT2 = new WaitForSeconds(0.2f);
    public static WaitForSeconds m_WFS_DOT3 = new WaitForSeconds(0.3f);
    public static WaitForSeconds m_WFS_D5 = new WaitForSeconds(0.5f);
    public static WaitForSeconds m_WFS_1 = new WaitForSeconds(1f);
    public static WaitForSeconds m_WFS_1D5 = new WaitForSeconds(1.5f);
    public static WaitForSeconds m_WFS_2 = new WaitForSeconds(2f);
    public static WaitForSeconds m_WFS_2D5 = new WaitForSeconds(2.5f);
    public static WaitForSeconds m_WFS_10 = new WaitForSeconds(10f);
    public static WaitForSeconds m_WFM_1 = new WaitForSeconds(60f);


    //
    public static Color32 m_Color32_AlphaNone = new Color32(1, 1, 1, 0);

    //WaitforSeconds 0.5������ �޾ƿ���
    public static WaitForSeconds GetSecD5To2D5(float t)
    {
        if (t <= 0.5f) return m_WFS_D5;
        else if (t <= 1) return m_WFS_1;
        else if (t <= 1.5) return m_WFS_1D5;
        else if (t <= 2) return m_WFS_2;
        else return m_WFS_2D5;
    }

    //������ Ȯ�� -> ��� // Ÿ�ݰ��־����. ����
    //���۰Ÿ� // ��Ʋ�Ÿ��� ���� �־����.
    //�Ϲ� ���� ���� // �׳� ����
    //���Ʒ� �ﷷ�Ÿ� // ������ ����
    //������ ������ �ؽ�Ʈ // ������ ����
    public enum ETextAnimation { NONE, WIGGLE, DRUNKEN };
    public enum ETextSpawnEffect { NONE, DECREASE };

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

    public enum EDialogType { TALK, MONO }

    [System.Serializable]
    //old
    public class CWordEffect 
    {
        public Vector2Int m_Range = Vector2Int.zero;
        public ETextSpawnEffect m_SpawnEff = ETextSpawnEffect.NONE;
        public ETextAnimation m_Anim = ETextAnimation.NONE;
    }

    public class CIcon : CData_CSV 
    {
        public string m_Description = "����";
        public Sprite m_Icon = null;
    }
    [System.Serializable]
    public class CSkill : CIcon
    {
        //public string m_Duration = "0000/00/01"; // 1��
        public uint m_Duration = 0;
        public uint m_Cost_Cash = 0;
        public uint m_Cost_Coin = 0;
        public Vector2 m_Range = Vector2.zero;
        public float m_Chance = 0;

        //�ʿ� ������. 0�̸� �ϰ͵� �ʿ� ����
        public uint m_Require = 0;

    }

    public enum EItemType { ITEM, SKILL,INFO, };
    //������
    [System.Serializable]
    public class CItem : CIcon
    {
        public uint m_Count = 0;
        public bool m_Consiumable = false;
        public EItemType m_ItemType = EItemType.ITEM;

        //���� ������ �Ǹ� ���ϴ� ������
        public uint m_Cost_Cash = 0;
        public uint m_Cost_Coin = 0;

        //max�� 0 �̸� �������� �Ⱦƿ� ������� ����
        public uint m_MaxCount = 0;
        public uint m_Require = 0;
    }


    [System.Serializable]
    public class CScriptEffect
    {
        public string m_words = "";
        public ETextSpawnEffect m_SpawnEff = ETextSpawnEffect.NONE;
        public ETextAnimation m_Anim = ETextAnimation.NONE;
    }

    [System.Serializable]
    public class CScriptQueue 
    {
        public string m_script = "";
        public CScriptEffect m_scriptEff = null;

        public bool m_IsBold = false;
        public Color m_Color = Color.black;
    }

    public enum ECharactor { HT }
    public enum ERewordType { COIN, CASH, NAME_VAL, ROOT, CAFE, BAR }



    [System.Serializable]
    //���
    public class CData_NPC_Script : CData_CSV 
    {
        public string m_Dialog = "";

        //��ȭ Ÿ��
        public EDialogType m_DialogType = EDialogType.TALK;
        public List<CWordEffect> m_WordEffs = null;

        //��ȭ ��������
        public bool m_bIsTalkDone = false;
    }

    //��� ����
    [System.Serializable]
    public class CData_NPC_Paragraph : CData_CSV
    {
        public List<CData_NPC_Script> m_Dialogs = new List<CData_NPC_Script>();
    }


    //[CreateAssetMenu(fileName = "Stage_", menuName = "CUSTOM MAPS/CStage")]
    //[System.Serializable]
    //public class CStage : ScriptableObject
    //{ public List<EStageType> m_Maps = new List<EStageType>(); }

    //[CreateAssetMenu(fileName = "StageMapList_", menuName = "CUSTOM MAPS/Stage_MapList")]
    //[System.Serializable]
    //public class CStage_MapList : ScriptableObject
    //{
    //    //public SerializeDictionary<EStageType, List<CMap>> m_Maps = new SerializeDictionary<EStageType, List<CMap>>(); 
    //    [System.Serializable]
    //    public class CMapData 
    //    {
    //        public EStageType m_Type = EStageType.ROAD;
    //        public List<CMap> m_Maps = new List<CMap>();
    //    }
    //    public List<CMapData> m_Maps = new List<CMapData>();
    //}

    //[CreateAssetMenu(fileName = "StageGroup_", menuName = "CUSTOM MAPS/Stage_Group")]
    //public class CStageGroup : ScriptableObject
    //{
    //    public CStage_MapList m_Map = null;
    //    public List<CStage> m_Stages = new List<CStage>(); 
    //}

    /////////////////////////////////////////////////MAP DATA/////////////////////////////////////////////////
    ///

    //-----------------------------Funcs-----------------------------//

    static public System.DateTime ParseDate(string _date)
    {
        return System.DateTime.ParseExact(_date, "yyyy-MM-dd",
            System.Globalization.CultureInfo.InvariantCulture);
    }

    static public string ParseDate(System.DateTime _date) 
    {
        return _date.ToString("yyyy/MM/dd");
    }
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