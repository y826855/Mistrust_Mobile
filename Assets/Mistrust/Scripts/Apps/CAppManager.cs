using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAppManager : MonoBehaviour
{
    private void Awake()
    {
        CGameManager.Instance.m_AppMgr = this;
    }

    public GameObject m_Dialogue = null;
    public GameObject m_ChatApp = null;
    public CLock_AnalogueNum m_AnalogueLockSolver = null;

}
