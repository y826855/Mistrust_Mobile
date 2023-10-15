using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CNumPad : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI m_TMP_NumPad_Inputed = null;
    string _nums = "";
    public string m_Nums{
        get { return _nums; }
        set 
        {
            _nums = value;
            if (value.Length == 4) 
            { _nums = _nums.Insert(3, "-"); }
            if (value.Length == 9) 
            { _nums = _nums.Insert(8, "-"); }

            m_TMP_NumPad_Inputed.text = _nums;
        } 
    }


    public void InputBtn(string _data) 
    {
        if (m_Nums.Length > 12) return;
        m_Nums = string.Format("{0}{1}", m_Nums, _data);
    }
    public void InputDel() 
    {
        
        if (m_Nums.Length == 5)
            m_Nums = m_Nums.Substring(0, m_Nums.Length - 2);
        else if (m_Nums.Length == 10)
            m_Nums = m_Nums.Substring(0, m_Nums.Length - 2);
        else
            m_Nums = m_Nums.Substring(0, m_Nums.Length - 1);
    }

}
