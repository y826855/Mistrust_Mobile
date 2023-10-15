using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLogin : MonoBehaviour
{


    public void OnClick_ConnetToPC() 
    {
        CGameManager.Instance.m_Network.ConnectToServer(IsConnetSuccess);
    }

    public void IsConnetSuccess(bool _res) 
    {
        if (_res == true)
            this.gameObject.SetActive(false);
    }

    
}
