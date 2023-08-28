using UnityEngine;
using UnityEngine.EventSystems;


namespace NGUI.Framework
{
    /// <summary>
    /// UI�¼������� ����������UGUI�¼����ṩ�¼�������         
    ///                        ���ӵ���Ҫ������UIԪ���ϣ����ڼ����û��Ĳ���
    ///                        ������ EventTrigger
    /// </summary>

    // 2.����ί����������
    public delegate void PointerEventHandler(PointerEventData eventData);
    public delegate void BaseEventHandler(BaseEventData eventData);
    public delegate void AxiseEventHandler(AxisEventData eventDate);

    public class UIEventListener : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler,
        IInitializePotentialDragHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IScrollHandler,
        IUpdateSelectedHandler,
        ISelectHandler,
        IDeselectHandler,
        IMoveHandler,
        ISubmitHandler,
        ICancelHandler
    {
        // 3.�����¼�
        public event PointerEventHandler PointerEnter;
        public event PointerEventHandler PointerExit;
        public event PointerEventHandler PointerClick;
        public event PointerEventHandler PointerDown;
        public event PointerEventHandler PointerUp;
        public event PointerEventHandler InitializePotentialDrag;
        public event PointerEventHandler BeginDrag;
        public event PointerEventHandler Drag;
        public event PointerEventHandler EndDrag;
        public event PointerEventHandler Drop;
        public event PointerEventHandler Scroll;
        public event BaseEventHandler UpdateSelected;
        public event BaseEventHandler Select;
        public event BaseEventHandler Deselect;
        public event AxiseEventHandler Move;
        public event BaseEventHandler Submit;
        public event BaseEventHandler Cancel;

        // 1.�̳нӿ�
        public void OnPointerClick(PointerEventData eventData)
        {
            // ������ �ӿ�(���������Ϊ) ί��(һ�������Ϊ)
            // 4.�����¼�
            if (PointerClick != null) { PointerClick(eventData); }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (PointerDown != null) { PointerDown(eventData); }
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (PointerUp != null) { PointerUp(eventData); }
        }

        /// <summary>
        /// ͨ���任�����ȡ�¼�������
        /// </summary>
        /// <param name="tf"></param>
        /// <returns></returns>
        public static UIEventListener GetListener(Transform tf)
        {
            UIEventListener uiEvent = tf.GetComponent<UIEventListener>();
            if (uiEvent == null) uiEvent = tf.gameObject.AddComponent<UIEventListener>();
            return uiEvent;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           if (PointerEnter != null) { PointerEnter(eventData); }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (PointerExit != null) { PointerExit(eventData); }
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (InitializePotentialDrag != null) {  InitializePotentialDrag(eventData); }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
           if (BeginDrag != null) { BeginDrag(eventData); }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Drag != null) { Drag(eventData); }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (EndDrag != null) { EndDrag(eventData); }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (Drop != null) { Drop(eventData); }
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (Scroll != null) { Scroll(eventData); }
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (UpdateSelected != null) { UpdateSelected(eventData); }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (Select != null) { Select(eventData); }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (Deselect != null) { Deselect(eventData); }
        }

        public void OnMove(AxisEventData eventData)
        {
            if (Move != null) { Move(eventData); }
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (Submit != null) { Submit(eventData);}
        }

        public void OnCancel(BaseEventData eventData)
        {
            if (Cancel != null) { Cancel(eventData); }
        }
    }

}
