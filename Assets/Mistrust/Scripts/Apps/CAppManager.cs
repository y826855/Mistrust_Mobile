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

    [Header("------APPS------")]
    public CApp m_LockSolver = null;
    public CApp m_Chat = null;

    [Header("------FOCUS------")]
    public CApp m_FocusApp = null;
    CApp m_BeforeApp = null;


    //TODO : 앱 전환 시키는거 하자

    public void Start()
    {

    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("force on lock");
            CUtility.CLock temp = new CUtility.CLock();
            temp.m_Password = "000";
            ReciveData(CUtility.ESendToMobile.DOOR_LOCK, JsonUtility.ToJson(temp));
        }
    }
#endif


    //받은 데이터를 토대로 앱 강제 변경
    public void ReciveData(CUtility.ESendToMobile _id, string _data)
    {
        switch (_id)
        {
            case CUtility.ESendToMobile.CLOSE_APP:
                if (m_FocusApp.m_lockApp != null)
                {
                    m_FocusApp.gameObject.SetActive(false);
                    Debug.Log("좌물쇠 오프");
                }

                break;
            case CUtility.ESendToMobile.DOOR_LOCK:

                CUtility.CLock lockObj = JsonUtility.FromJson(_data,
                    typeof(CUtility.CLock)) as CUtility.CLock;
                m_LockSolver.m_lockApp.GetLockData(lockObj);
                m_BeforeApp = m_FocusApp;
                m_FocusApp = m_LockSolver;
                StartCoroutine(CoAnimOnOff(true, m_FocusApp));

                break;

            default: break;
        }
    }

    //앱 닫기
    public void CloseApp()
    {
        Debug.Log("CLOSE APP");
        if (m_FocusApp != null) StartCoroutine(CoAnimOnOff(false, m_FocusApp));
        m_FocusApp = m_BeforeApp;
        m_BeforeApp = null;
    }



    //앱 등장 모션
    [SerializeField] float duration = 0.5f;
    IEnumerator CoAnimOnOff(bool _toggle, CApp _target)
    {
        float t = 0f;
        Vector3 goal = _toggle ? Vector3.one : Vector3.zero;

        m_FocusApp.transform.SetAsLastSibling();

        if (_toggle == true) 
        {
            _target.transform.localScale = Vector3.zero;
            _target.gameObject.SetActive(_toggle); 
        }

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            Debug.Log(goal);
            _target.transform.localScale =
                Vector3.Lerp(_target.transform.localScale, goal, t);

            yield return null;
        }

        if (_toggle == false) { _target.gameObject.SetActive(_toggle); }
        else _target.NowExecute();
    }

    //public void OpenApp(EApp _app) 
    public void OpenApp(string _app) 
    {
        m_BeforeApp = m_FocusApp;

        switch (_app)
        {
            case "Chat": m_FocusApp = m_Chat;
                break;
            case "Note":
                break;
            case "Shop":
                break;
            default: break;
        }

        StartCoroutine(CoAnimOnOff(true, m_FocusApp));

    }
}
