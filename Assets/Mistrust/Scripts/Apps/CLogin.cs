using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CLogin : MonoBehaviour
{
    public TMP_InputField m_IP;
    public TMP_InputField m_Port;

    public void OnClick_ConnetToPC() 
    {
        CGameManager.Instance.m_Network.ConnectToServer(IsConnetSuccess,
            m_IP.text, m_Port.text);
    }

    public void IsConnetSuccess(bool _res) 
    {
        if (_res == true)
            this.gameObject.SetActive(false);
    }

    
}
