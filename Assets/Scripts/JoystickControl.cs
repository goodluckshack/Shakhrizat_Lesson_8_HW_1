using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickControl : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image Joystick;
    [SerializeField] private Image Stick;
    private Vector2 inputVector;

    // Start is called before the first frame update
    void Start()
    {
        Joystick = GetComponent<Image>();
        Stick = transform.GetChild(0).GetComponent<Image>();        
    }


    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }


    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        Stick.rectTransform.anchoredPosition = Vector2.zero;
    }


    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Joystick.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / Joystick.rectTransform.sizeDelta.x);
            pos.y = (pos.y / Joystick.rectTransform.sizeDelta.x);

            inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            Stick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (Joystick.rectTransform.sizeDelta.x / 2), inputVector.y * (Joystick.rectTransform.sizeDelta.y / 2));
        }
    }


    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }


    public float Vertical()
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return Input.GetAxis("Vertical");
    }
}
