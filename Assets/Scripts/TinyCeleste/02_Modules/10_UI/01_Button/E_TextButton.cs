using TinyCeleste._01_Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TinyCeleste._02_Modules._10_UI._01_Button
{
    public class E_TextButton : Entity, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        public Color hoverColor;
        public Color downColor;
        public UnityEvent clickEvent;

        private Text text;
        private Color normalColor;

        private bool isMouseIn;
        private bool selected;
        
        private void Awake()
        {
            text = GetComponent<Text>();
            normalColor = text.color;
        }
        
        private void OnEnable()
        {
            isMouseIn = false;
            selected = false;
            Update();
        }

        private void Update()
        {
            bool isMouseDown = Input.GetMouseButton(0);
            if (isMouseIn)
            {
                if (isMouseDown && selected) text.color = downColor;
                else text.color = hoverColor;
            }
            else
            {
                text.color = normalColor;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseIn = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            clickEvent.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseIn = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            selected = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            selected = false;
        }
    }
}