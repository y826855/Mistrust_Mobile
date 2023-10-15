using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNumRotator : MonoBehaviour
{

    Vector3 lastMousePosition;
    [SerializeField] float rotationSpeed = 1.0f;
    public System.Action<int> m_FuncIsNumChanged = null;
    public int m_Idx = 0;
    void OnMouseDown()
    {
        // 마우스 버튼이 클릭되면 드래그 시작합니다.
        lastMousePosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        // 현재 마우스 위치와 마우스의 이동량을 계산합니다.
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDelta = currentMousePosition - lastMousePosition;

        // 이동량을 사용하여 오브젝트를 회전합니다.
        this.transform.Rotate(this.transform.up, -mouseDelta.x * rotationSpeed);

        // 현재 마우스 위치를 기억합니다.
        lastMousePosition = currentMousePosition;

    }

    void OnMouseUp()
    {
        // 마우스 버튼이 놓이면 드래그를 멈춥니다.
        m_FuncIsNumChanged(m_Idx);
    }

}
