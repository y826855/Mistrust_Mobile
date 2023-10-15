using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUI_ChatList : MonoBehaviour
{
    public CUI_Chat mPref_ChatRight = null;
    public CUI_Chat mPref_ChatRight_Emo = null;
    public CUI_Chat mPref_ChatLeft = null;
    public CUI_Chat mPref_ChatLeft_Emo = null;

    CUI_Chat m_CurrChat = null;

    public List<CUI_Chat> m_ChatList = new List<CUI_Chat>();

    [Header("-----------TEST-------------")]
    public bool m_IsTest = false;

    Coroutine coSpawnDelay = null;

    public void Start()
    {
        if (m_IsTest == true) 
        { StartCoroutine(Co_TestTextInput()); }
    }

    IEnumerator Co_TestTextInput()
    {
        List<string> m_TestWords = new List<string>() 
        { 
            "안녕하세요", 
            "테스트 입니다." ,
            "이미지도 시험 해봐야 하는데",
            "아직은 생각 없습니다",
            "동해물과 백 두 산이 마르고 닳도록 대 한 사람 대 한 으 로 길이 보전하 세."
        };
        
        foreach (var it in m_TestWords) 
        {
            ReceiveMessage(it);
            yield return coSpawnDelay;
        }

        //coSpawnDelay = Co_SpawnDelay();
        yield return null;
    }


    //TODO : 대답한 대사 넣자
    public void SendPlayerMessage(string _text) 
    {
        m_CurrChat = Instantiate(mPref_ChatRight, this.transform);
        m_CurrChat.m_TMP.text = "...";
        m_ChatList.Add(m_CurrChat);
    }
    public void SendPlayerMessage(Image _img)
    {
        m_CurrChat = Instantiate(mPref_ChatRight_Emo, this.transform);
        m_CurrChat.m_Emoticon = _img;
        m_ChatList.Add(m_CurrChat);
    }
    
    public void ReceiveMessage(string _text) 
    {
        m_CurrChat = Instantiate(mPref_ChatLeft, this.transform);
        m_CurrChat.m_TMP.text = "...";
        m_ChatList.Add(m_CurrChat);
        coSpawnDelay = StartCoroutine(Co_SpawnDelay(_text));
    }

    public void ReceiveMessage(Image _img)
    {
        m_CurrChat = Instantiate(mPref_ChatLeft_Emo, this.transform);
        m_CurrChat.m_Emoticon = _img;
        m_ChatList.Add(m_CurrChat);
    }

    IEnumerator Co_SpawnDelay(string _text) 
    {
        float t = 0;
        float e = 0f;

        if (_text.Length <= 4) e = 0f;
        else if (_text.Length <= 15) e = 1.2f;
        else if (_text.Length <= 30) e = 1.8f;
        else  e = 2.4f;

        bool _anim = false;

        //0.3f 초 마다 .. -> ... 으로 바뀜
        while (t < e) 
        {
            t += 0.3f;
            m_CurrChat.m_TMP.text = _anim ? "..." : "..";
            _anim = !_anim;
            yield return CUtility.m_WFS_DOT3;
        }

        m_CurrChat.m_TMP.text = _text;
        coSpawnDelay = null;
    }
}
