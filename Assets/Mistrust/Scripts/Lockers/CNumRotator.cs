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
        // ���콺 ��ư�� Ŭ���Ǹ� �巡�� �����մϴ�.
        lastMousePosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        // ���� ���콺 ��ġ�� ���콺�� �̵����� ����մϴ�.
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDelta = currentMousePosition - lastMousePosition;

        // �̵����� ����Ͽ� ������Ʈ�� ȸ���մϴ�.
        this.transform.Rotate(this.transform.up, -mouseDelta.x * rotationSpeed);

        // ���� ���콺 ��ġ�� ����մϴ�.
        lastMousePosition = currentMousePosition;

    }

    void OnMouseUp()
    {
        // ���콺 ��ư�� ���̸� �巡�׸� ����ϴ�.
        m_FuncIsNumChanged(m_Idx);
    }

}
