using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeInput : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public event UnityAction SwipedLeft;
    public event UnityAction SwipedRight;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0)
            {
                SwipedRight?.Invoke();
            }
            else
            {
                SwipedLeft?.Invoke();
            }
        } 
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
