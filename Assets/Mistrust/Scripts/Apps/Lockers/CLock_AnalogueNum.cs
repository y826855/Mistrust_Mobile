using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CLock_AnalogueNum : MonoBehaviour
{
    public RectTransform m_ShowUI = null;
    
    [Header("--------------------------------")]
    public CLocker m_Locker = null;
    public CUtility.CLock m_Lock = null;
    

    [Header("--------------------------------")]
    public List<TextMeshProUGUI> m_ShowNums = new List<TextMeshProUGUI>();
    public List<CNumRotator> m_Nums = new List<CNumRotator>();
    //public 

    public string m_Password = "123";

    public void GetLockData(CUtility.CLock _lock) 
    {
        m_Lock = _lock;
        m_Password = _lock.m_Password;
        this.gameObject.SetActive(true);
    }

    private void Start()
    {
        m_ShowUI.gameObject.SetActive(true);
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
        if (CheckPassword() == true) 
        {
            Debug.Log("OPEN!!");
            m_Locker.transform.localPosition = Vector3.up * 0.4f;

            

            //JsonUtility.FromJson()
        }
    }

    int RotationToNum(Transform _target) 
    {
        float y = _target.localRotation.eulerAngles.y;
        return Mathf.RoundToInt(y) / 36; 
    }

    private void OnDisable()
    {
        m_ShowUI.gameObject.SetActive(false);
    }
}
