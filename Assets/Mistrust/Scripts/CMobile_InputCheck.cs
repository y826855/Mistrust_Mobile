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
                Debug.Log("터치 시작");
                // 터치 시작 위치 기록
                InputPosStart = touch.position;
                InputPosLast = touch.position;

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) 
                {
                    //터치 한 오브젝트 기억해둠
                    var obj = hit.collider.gameObject;
                    if (obj.tag == "Touchable") 
                    { 
                        currTouch = obj.GetComponent<CTouchable>();
                        currTouch.BeginInput();
                    }
                }
                // 오브젝트 회전 시작
                bIsTouch = true;
            }

            //움직이거나 멈춘 상태일때
            else if (touch.phase == TouchPhase.Moved 
                || touch.phase == TouchPhase.Stationary
                && bIsTouch)
            {
                // 터치 이동량 계산
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
                // 터치 종료 시 회전 멈춤
                if(currTouch != null) currTouch.EndInput();
                bIsTouch = false;
            }
        }
    }
//#endif

}
