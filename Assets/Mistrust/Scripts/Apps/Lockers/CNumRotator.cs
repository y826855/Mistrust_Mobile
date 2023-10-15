using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNumRotator : MonoBehaviour
{

    Vector3 InputPosStart;
    Vector3 InputPosLast;
    [SerializeField] float m_RotationSpeed = 0.3f;
    public System.Action<int> m_FuncIsNumChanged = null;
    public int m_Idx = 0;

#if UNITY_EDITOR

    void OnMouseDown()
    {
        // ���콺 ��ư�� Ŭ���Ǹ� �巡�� �����մϴ�.
        InputPosStart = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        // ���� ���콺 ��ġ�� ���콺�� �̵����� ����մϴ�.
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDelta = currentMousePosition - InputPosStart;

        // �̵����� ����Ͽ� ������Ʈ�� ȸ���մϴ�.
        this.transform.Rotate(this.transform.up, -mouseDelta.x * m_RotationSpeed);

        // ���� ���콺 ��ġ�� ����մϴ�.
        InputPosStart = currentMousePosition;
        m_FuncIsNumChanged(m_Idx);
    }

    void OnMouseUp()
    {
        // ���콺 ��ư�� ���̸� �巡�׸� ����ϴ�.
        
    }
#endif


    //TODO : ����Ͽ� ¥����
    /*
    bool bIsTouch = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // ��ġ ���� ��ġ ���
                InputPosStart = touch.position;

                // ������Ʈ ȸ�� ����
                bIsTouch = true;
            }
            else if (touch.phase == TouchPhase.Moved && bIsTouch)
            {
                // ��ġ �̵��� ���
                InputPosLast = touch.position;
                float rotationAmount = (InputPosLast.x - InputPosStart.x) * m_RotationSpeed;

                // ������Ʈ ȸ��
                this.transform.Rotate(Vector3.up, rotationAmount);

                // ���� �������� ���� ��ġ ���� ��ġ ����
                InputPosStart = InputPosLast;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // ��ġ ���� �� ȸ�� ����
                bIsTouch = false;
            }
        }
    }*/
}


