using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class CScriptable_CSVData<T> : ScriptableObject where T : ScriptableObject
{
    //public Sprite m_Icon = null;

#if UNITY_EDITOR
    //
    static public T CreateInst(string path, string name)
    {
        //���� �����ϴ��� ���� �ҷ�����
        //T get = Resources.Load<T>(string.Format("{0}/{1}", path, name));
        T get = AssetDatabase.LoadAssetAtPath<T>(string.Format("{0}{1}.asset", path, name));


        if (get == null)
        {
            //���� ��� Ȯ��. ���ٸ� ���� ����
            if (AssetDatabase.IsValidFolder(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }

            //��ο� �ٽ� ����
            get = AssetDatabase.LoadAssetAtPath<T>(string.Format("{0}{1}.asset", path, name));

            //�ٽ� �����ص� ���ٸ�
            if (get == null)
            {//��ü ����
                get = CreateInstance<T>();
                Debug.Log(string.Format("Create Asset : {0}{1}.asset", path, name));
                AssetDatabase.CreateAsset(get, string.Format("{0}{1}.asset", path, name));
            }
        }

        return get;
    }

    //�ؽ��� �ҷ�����
    static public Sprite LoadTexture(string path, string name)
    {
        //png
        var texture = AssetDatabase.LoadAssetAtPath<Sprite>
                (string.Format("{0}SP_{1}.png", path, name));
        //jpg
        if (texture == null)
            texture = AssetDatabase.LoadAssetAtPath<Sprite>
            (string.Format("{0}SP_{1}.jpg", path, name));

        else return texture;

        


        /*TODO : �ٸ� Ȯ���ڵ� �߰��غ���*/


        return texture;
    }

    //������ �ҷ�����
    static public GameObject LoadPref(string _path, string _name)
    {
        var pref = AssetDatabase.LoadAssetAtPath<GameObject>
            (string.Format("{0}Pref_{1}.prefab", _path, _name));
        return pref;
    }
#endif
}
