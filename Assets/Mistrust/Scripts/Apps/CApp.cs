using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CApp : MonoBehaviour
{
    public RectTransform m_LoadingImg = null;
    public RectTransform m_MoveingObj = null;

    Vector3 beforPos = Vector3.zero;
    [SerializeField] float duration = 0.5f;

    [Header("-----Contents-----")]
    public CLock_AnalogueNum m_lockApp = null;
    public CApp_Chat m_AppChat = null;
    
    private void OnEnable()
    {
        if (m_LoadingImg != null) m_LoadingImg.gameObject.SetActive(true);
        if (m_MoveingObj != null) beforPos = m_MoveingObj.localPosition;

    }
    private void OnDisable()
    {
        if (m_LoadingImg != null) m_LoadingImg.gameObject.SetActive(true);
        if (m_MoveingObj != null) m_MoveingObj.localPosition = beforPos;
        StopAllCoroutines();
    }

    IEnumerator CoLoading() 
    {
        yield return CUtility.GetSecD5To2D5(0.5f);
        LoadDoen();
    }

    public void NowExecute() 
    {
        StartCoroutine(CoLoading());
    }

    public void LoadDoen() 
    {
        if (m_LoadingImg != null) m_LoadingImg.gameObject.SetActive(false);
        if(m_MoveingObj != null) m_MoveingObj.localPosition = Vector3.zero;
    }
}
