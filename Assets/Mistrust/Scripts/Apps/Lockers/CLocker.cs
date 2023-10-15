using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLocker : MonoBehaviour
{
    Vector3 InputPosStart;
    Vector3 InputPosLast;
    [SerializeField] float m_RequireMove = 1.0f;
    public System.Action m_FuncTryOpen = null;

#if UNITY_EDITOR

    void OnMouseDown()
    {
        InputPosStart = Input.mousePosition;
    }

    void OnMouseUp()
    {
        Vector3 mouseDelta = Input.mousePosition - InputPosStart;
        ;
        if (m_FuncTryOpen != null 
            && mouseDelta.y > 400f) m_FuncTryOpen();
    }
#endif

    //TODO : 모바일용 짜야함
}

