using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class CScriptableSingletone<T> : ScriptableObject where T : ScriptableObject
{
    //�θ� ���� ��ġ
    static public string m_FolderPath = "Assets/Resources/";
    //static public string m_FolderPath = CUtility.m_FolderPath;

    //������ ����
    static public string m_AssetFolder = "Singletone";
    //static public string m_AssetFolder = "Scriptable";

    static private T _instance = null;

    static public System.Action func;
    abstract public void BeforeLoadInstance();

    static public T Instance
    {
        get
        {
            if (_instance != null) return _instance;


            //_instance = AssetDatabase.LoadAssetAtPath<T>(string.Format("[0][1][2]", m_FolderPath, m_AssetFolder, m_AssetFileName));

            //���� �����ϴ��� ���� �ҷ�����
            _instance = Resources.Load<T>(string.Format("{0}/{1}", m_AssetFolder, typeof(T).Name));
            //Debug.Log(string.Format("{0}/{1}", m_AssetFolder, typeof(T).Name));

#if UNITY_EDITOR
            string directory = string.Format("{0}{1}/", m_FolderPath, m_AssetFolder);

            //��ο� ���ٸ�
            if (_instance == null)
            {
                //���� ��� Ȯ��. ���ٸ� ���� ����
                if (AssetDatabase.IsValidFolder(directory) == false)
                {
                    //AssetDatabase.CreateFolder(m_FolderPath, m_AssetFolder);
                    System.IO.Directory.CreateDirectory(directory);
                }

                //��ο� �ٽ� ����
                //_instance = AssetDatabase.LoadAssetAtPath<T>(directory);
                _instance = AssetDatabase.LoadAssetAtPath<T>(string.Format("{0}{1}.asset", directory, typeof(T).Name));

                //�ٽ� �����ص� ���ٸ�
                if (_instance == null)
                {//��ü ����
                    _instance = CreateInstance<T>();
                    AssetDatabase.CreateAsset(_instance, string.Format("{0}{1}.asset", directory, typeof(T).Name));
                    Debug.Log(directory + typeof(T).Name);
                    Debug.Log("obj created");
                }
            }
#endif
            //�ν��Ͻ� ó�� ȣ���Ҷ� AWAKE ����
            (_instance as CScriptableSingletone<T>).BeforeLoadInstance();
            return _instance;
        }
    }
}
