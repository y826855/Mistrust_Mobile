using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CMobile_InputCheck : MonoBehaviour
{
    Vector3 InputPosStart;
    Vector3 InputPosLast;

//#if UNITY_ANDROID
    public bool bIsTouch = false;
    
    public float m_DragX = 0f;
    public float m_DragY = 0f;
    public Vector3 m_Moved = Vector3.zero;
    [SerializeField] TextMeshProUGUI TMPdebugInputShow = null;

    public void Start()
    {
        CGameManager.Instance.m_Input = this;
        Debug.Log(CGameManager.Instance.m_Input.name);
    }

    CTouchable currTouch = null;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("��ġ ����");
                // ��ġ ���� ��ġ ���
                InputPosStart = touch.position;
                InputPosLast = touch.position;

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) 
                {
                    //��ġ �� ������Ʈ ����ص�
                    var obj = hit.collider.gameObject;
                    if (obj.tag == "Touchable") 
                    { 
                        currTouch = obj.GetComponent<CTouchable>();
                        currTouch.BeginInput();
                    }
                }
                // ������Ʈ ȸ�� ����
                bIsTouch = true;
            }

            //�����̰ų� ���� �����϶�
            else if (touch.phase == TouchPhase.Moved 
                || touch.phase == TouchPhase.Stationary
                && bIsTouch)
            {
                // ��ġ �̵��� ���
                m_DragX = InputPosLast.x - touch.position.x;
                m_DragY = InputPosLast.y - touch.position.y;

                InputPosLast = touch.position;

                TMPdebugInputShow.text = 
                string.Format("DRAG // x : {0}  y : {1} \n STARTPOS : {2} LASTPOS : {3} "
                    , m_DragX, m_DragY, InputPosStart, InputPosLast);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                m_Moved = InputPosLast - InputPosStart;
                // ��ġ ���� �� ȸ�� ����
                if(currTouch != null) currTouch.EndInput();
                bIsTouch = false;
            }
        }
    }
//#endif

}
