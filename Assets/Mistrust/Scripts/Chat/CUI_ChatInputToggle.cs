using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUI_ChatInputToggle : MonoBehaviour
{
    public RectTransform m_ChatField;
    public RectTransform m_InputField;

    public bool toggle = false;
    float chatBottom = 0f;
    float InputFieldTop = 0f;

    private void Awake()
    {
        chatBottom = m_ChatField.offsetMin.y;
        InputFieldTop = m_InputField.offsetMax.y;
    }

    public void ToggleInputField() 
    {
        toggle = !toggle;
        m_InputField.gameObject.SetActive(toggle);

        if (toggle == true) EnableInput();
        else DisableInput();
    }

    public void DisableInput() 
    {
        m_ChatField.offsetMin = new Vector2(m_ChatField.offsetMin.x, chatBottom);
    }
    public void EnableInput() 
    {
        m_ChatField.offsetMin = new Vector2(m_ChatField.offsetMin.x, InputFieldTop);
    }


}
