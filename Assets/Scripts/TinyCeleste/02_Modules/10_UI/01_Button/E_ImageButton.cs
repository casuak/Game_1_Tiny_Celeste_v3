using TinyCeleste._01_Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TinyCeleste._02_Modules._01_UI._01_Button
{
    public class E_ImageButton : EntityComponent, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        public Sprite m_HoverSprite;
        public Sprite m_DownSprite;
        public UnityEvent m_ClickEvent;
     
        private Image m_Image;
        private Sprite m_NormalSprite;
        
        private bool m_IsMouseIn;
        private bool m_Selected;

        private void Awake()
        {
            m_Image = GetComponent<Image>();
            m_NormalSprite = m_Image.sprite;
        }

        private void OnEnable()
        {
            m_IsMouseIn = false;
            m_Selected = false;
            Update();
        }

        private void Update()
        {
            bool isMouseDown = Input.GetMouseButton(0);
            if (m_IsMouseIn)
            {
                if (isMouseDown && m_Selected) m_Image.sprite = m_DownSprite;
                else m_Image.sprite = m_HoverSprite;
            }
            else
            {
                m_Image.sprite = m_NormalSprite;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_IsMouseIn = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_ClickEvent.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_IsMouseIn = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Selected = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_Selected = false;
        }
    }
}