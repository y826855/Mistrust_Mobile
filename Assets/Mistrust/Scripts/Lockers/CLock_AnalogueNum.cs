using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLock_AnalogueNum : MonoBehaviour
{
    [Header("--------------------------------")]
    public Transform m_Locker = null;

    [Header("--------------------------------")]
    public CNumRotator m_Num01 = null;
    public CNumRotator m_Num02 = null;
    public CNumRotator m_Num03 = null;


    private void Start()
    {
        m_Num01.m_FuncIsNumChanged = IsNumChanged;
        m_Num02.m_FuncIsNumChanged = IsNumChanged;
        m_Num03.m_FuncIsNumChanged = IsNumChanged;
    }

    public void IsNumChanged(int _num) 
    {
        switch (_num) 
        {
            case 0: Debug.Log("Change1"); break;
            case 1: Debug.Log("Change1"); break;
            case 2: Debug.Log("Change1"); break;
        }


        RotationToNum(m_Num01.transform);
    }

    int RotationToNum(Transform _target) 
    {
        float y = _target.localRotation.eulerAngles.y;
        Debug.Log(Mathf.RoundToInt(y) / 36);
        return Mathf.RoundToInt(y) / 36; 
    }
}
