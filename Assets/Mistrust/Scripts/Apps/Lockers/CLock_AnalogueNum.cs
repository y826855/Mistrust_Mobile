using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CLock_AnalogueNum : MonoBehaviour
{
    [Header("--------------------------------")]
    public CLocker m_Locker = null;
    public CUtility.CLock m_Lock = null;
    

    [Header("--------------------------------")]
    public List<TextMeshProUGUI> m_ShowNums = new List<TextMeshProUGUI>();
    public List<CNumRotator> m_Nums = new List<CNumRotator>();
    //public 

    bool solved = false;

    public string m_Password = "123";

    public void GetLockData(CUtility.CLock _lock) 
    {
        m_Lock = _lock;
        m_Password = _lock.m_Password;
    }

    private void Start()
    {
        m_Locker.m_FuncTryOpen = Open;

        foreach (var it in m_ShowNums)
            it.text = "0";

        foreach (var it in m_Nums)
            it.m_FuncIsNumChanged = IsNumChanged;
    }

    public void IsNumChanged(int _num) 
    {
        m_ShowNums[_num].text = 
            RotationToNum(m_Nums[_num].transform).ToString();
    }

    public bool CheckPassword() 
    {
        string key = "";

        //foreach (var num in m_Nums) 
        //{
        //    float n = RotationToNum(num.transform);
        //    key = string.Format("{0}{1}", key, n);
        //}

        foreach (var tmp in m_ShowNums) 
        { key = string.Format("{0}{1}", key, tmp.text); }


        return key == m_Password;
    }

    public void Open() 
    {
        if (CheckPassword() == true && solved == false)
        {
            Debug.Log("OPEN!!");
            m_Locker.transform.localPosition = Vector3.up * 0.4f;

            //JsonUtility.FromJson()
            //string packit = true;
            //TODO : 전송 여기서 했음.
            string packit = string.Format("{0}+{1}",
                (int)CUtility.ESendToMobile.LOCK, true);
            CGameManager.Instance.m_Network.SendFunction(packit);

            StartCoroutine(CoWaitForDisable());
        }
        else 
        {
            //실패시 진동
            Handheld.Vibrate();
        }
    }

    IEnumerator CoWaitForDisable() 
    {
        yield return CUtility.GetSecD5To2D5(1.5f);
        CGameManager.Instance.m_AppMgr.CloseApp();
    }

    int RotationToNum(Transform _target) 
    {
        float y = _target.localRotation.eulerAngles.y;
        return Mathf.RoundToInt(y) / 36; 
    }

    private void OnEnable()
    {
        m_Locker.transform.localPosition = Vector3.zero;
        solved = false;
    }

    private void OnDisable()
    {

    }
}
