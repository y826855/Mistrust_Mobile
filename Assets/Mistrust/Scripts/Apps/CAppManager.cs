using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAppManager : MonoBehaviour
{
    private void Awake()
    {
        CGameManager.Instance.m_AppMgr = this;
    }

    public GameObject m_Main = null;
    public GameObject m_Dialogue = null;
    public GameObject m_ChatApp = null;

    public GameObject m_CurrFocus = null;

    public CLock_AnalogueNum m_AnalogueLockSolver = null;



    //TODO : 앱 전환 시키는거 하자

    public void Start()
    {
        m_CurrFocus = m_Main;
    }



    //받은 데이터를 토대로 앱 강제 변경
    public void ReciveData(CUtility.ESendToMobile _id, string _data)    
    {
        m_CurrFocus.gameObject.SetActive(false);

        switch (_id) 
        {
            case CUtility.ESendToMobile.CLOSE_APP:
                m_Main.SetActive(true);
                break;
            case CUtility.ESendToMobile.DOOR_LOCK:
                CUtility.CLock lockObj = JsonUtility.FromJson(_data, 
                    typeof(CUtility.CLock)) as CUtility.CLock;

                m_AnalogueLockSolver.gameObject.SetActive(true);
                m_AnalogueLockSolver.GetLockData(lockObj);

                m_CurrFocus = m_AnalogueLockSolver.gameObject;
                break;

            default: break;
        }
    }
}
