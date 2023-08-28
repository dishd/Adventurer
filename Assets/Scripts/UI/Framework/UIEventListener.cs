using UnityEngine;
using UnityEngine.EventSystems;


namespace NGUI.Framework
{
    /// <summary>
    /// UI事件监听器 ：管理所以UGUI事件，提供事件参数类         
    ///                        附加到需要交互的UI元素上，用于监听用户的操作
    ///                        类似于 EventTrigger
    /// </summary>

    // 2.定义委托数据类型
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
        // 3.声明事件
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

        // 1.继承接口
        public void OnPointerClick(PointerEventData eventData)
        {
            // 抽象类 接口(多个抽象行为) 委托(一类抽象行为)
            // 4.引发事件
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
        /// 通过变换组件获取事件监听器
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
