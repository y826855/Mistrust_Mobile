using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Text.RegularExpressions;

public class CCSVLoader : CScriptableSingletone<CCSVLoader>
{
    //---------------------------------------------------//
    public static bool m_isReaded = false;
    //---------------------------------------------------//
    public CGameManager m_GameMgr = null;
    

    //������ �а� ����
#if UNITY_EDITOR
    


    static public void ReadCSV()
    {
        //EditorUtility.SetDirty(Instance);
        //Instance.m_AllWeapon.Clear();
        //����
        AssetDatabase.Refresh();
        //if (m_isReaded == true) return;
        m_isReaded = true;
        //�˾Ƽ� �ʱ�ȭ ����


        //�ʼ�
        EditorUtility.SetDirty(CGameManager.Instance.m_Dictionary);
        //EditorUtility.SetDirty(CGameManager.Instance.m_ItemRows);
        //CGameManager.Instance.m_ItemRows.m_Datas.Clear();
        CGameManager.Instance.m_Dictionary.m_AllScriptables.Clear();


      
        List<Dictionary<string, object>> data;
    
        //������ csv
        
        data = CSVReader.Read("CSVData/ItemData");
        SaveData<CUtility.CData_Item>(data);
        data.Clear();

    }

    //������ �а� Scriptable�� ������.
    static public void SaveData<T>(List<Dictionary<string, object>> _data) where T : CUtility.CData_CSV
    {
 

        var dic = CGameManager.Instance.m_Dictionary;

        string IconFolderPath = "Assets/SnakeGame/Textures/UI/Item/";

        var gameInst = CGameManager.Instance;
        //var itemRows = gameInst.m_ItemRows;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////s
        //������
        if (typeof(T) == typeof(CUtility.CData_Item))
        {
            foreach (var it in _data)
            {
                CUtility.CData_Item temp = new CUtility.CData_Item()
                {
                    m_ID = CSVParseUINT(it["ID"].ToString()),
                    m_Name = it["Name"].ToString(),
                    m_Description = it["Description"].ToString(),
                };

                CUtility.CData_Item item = new CUtility.CData_Item();
                item = temp;
                item.m_Icon = //������ ��������
                    CScriptable_CSVData<ScriptableObject>
                    .LoadTexture(IconFolderPath, temp.m_Name);

                if (item.m_Icon == null)
                    item.m_Icon = //������ ���ٸ� ����Ʈ ���� ��
                    CScriptable_CSVData<ScriptableObject>
                    .LoadTexture(IconFolderPath, "Default");

                //gameInst.m_ItemRows.m_Datas.Add(temp.m_ID, item);
                Debug.Log(item.m_Name);
            }
        }

    }
    static string SPLIT_RE = @"\[(.*?)\]";
    //static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    //static string SPLIT_RE = @",(?=(?:^\[))";
    //static string SPLIT_RE = @"\[((?:^|,)]+)\]";
    //static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    //https://regexper.com/#%5C%5B%28%5B%5E%2C%5D%5D%29%5C%5D

    //������ �Ľ�
    static float CSVParseFloat(string _data) => float.Parse(_data);
    static uint CSVParseUINT(string _data) => uint.Parse(_data);




#endif
    public override void BeforeLoadInstance() { }
}
