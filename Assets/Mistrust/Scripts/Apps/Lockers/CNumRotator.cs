using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNumRotator : CTouchable
{

    Vector3 InputPosStart;
    Vector3 InputPosLast;
    [SerializeField] float m_RotationSpeed = 0.3f;
    public System.Action<int> m_FuncIsNumChanged = null;
    public int m_Idx = 0;

#if UNITY_EDITOR

    //void OnMouseDown()
    //{
    //    // 마우스 버튼이 클릭되면 드래그 시작합니다.
    //    InputPosStart = Input.mousePosition;
    //}

    //private void OnMouseDrag()
    //{
    //    // 현재 마우스 위치와 마우스의 이동량을 계산합니다.
    //    Vector3 currentMousePosition = Input.mousePosition;
    //    Vector3 mouseDelta = currentMousePosition - InputPosStart;

    //    // 이동량을 사용하여 오브젝트를 회전합니다.
    //    this.transform.Rotate(this.transform.up, mouseDelta.x * m_RotationSpeed);

    //    // 현재 마우스 위치를 기억합니다.
    //    InputPosStart = currentMousePosition;
    //    m_FuncIsNumChanged(m_Idx);
    //}

    //void OnMouseUp()
    //{
    //    // 마우스 버튼이 놓이면 드래그를 멈춥니다.
        
    //}
#endif



    //bool bIsTouch = false;
    //void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            // 터치 시작 위치 기록
    //            InputPosStart = touch.position;

    //            // 오브젝트 회전 시작
    //            bIsTouch = true;
    //        }
    //        else if (touch.phase == TouchPhase.Moved && bIsTouch)
    //        {
    //            // 터치 이동량 계산
    //            InputPosLast = touch.position;
    //            float rotationAmount = (InputPosLast.x - InputPosStart.x) * m_RotationSpeed;

    //            // 오브젝트 회전
    //            this.transform.Rotate(Vector3.up, rotationAmount);
    //            m_FuncIsNumChanged(m_Idx);
    //            // 다음 프레임을 위해 터치 시작 위치 갱신

    //            InputPosStart = InputPosLast;
    //        }
    //        else if (touch.phase == TouchPhase.Ended)
    //        {
    //            // 터치 종료 시 회전 멈춤

    //            bIsTouch = false;
    //        }
    //    }
    //}


    Coroutine coInputToRotate = null;
    public override void BeginInput() 
    {
        coInputToRotate = StartCoroutine(CoInputToRotate());
        Debug.Log("TOUCH");
    }

    public override void EndInput() 
    {
        StopCoroutine(coInputToRotate);
        Debug.Log("TOUCH END");
    }

    IEnumerator CoInputToRotate() 
    {
        var touch = CGameManager.Instance.m_Input;

        while (true) 
        {
            float rotationAmount = touch.m_DragX * m_RotationSpeed;
            Debug.Log(rotationAmount);
            this.transform.Rotate(this.transform.up, rotationAmount);
            m_FuncIsNumChanged(m_Idx);
            yield return null;
        }
    }
}


