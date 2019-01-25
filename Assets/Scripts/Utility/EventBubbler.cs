
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

//PURPOSE AND USAGE:
//Allows the events to "Bubble" through multiple objects
//Attach to an object you want to be able to "click through"
//Ex. the top Game Object at the top of layered UI Buttons.

public sealed class EventBubbler : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
        private void Bubble<T>(PointerEventData eventData, ExecuteEvents.EventFunction<T> eventFunction) where T : IEventSystemHandler
        {
            var allRaycasted = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, allRaycasted);
            foreach (var raycasted in allRaycasted)
            {
                if (raycasted.gameObject == gameObject)
                {
                    continue;
                }
                ExecuteEvents.Execute(raycasted.gameObject, eventData, eventFunction);
            }
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Bubble(eventData, ExecuteEvents.pointerClickHandler);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            Bubble(eventData, ExecuteEvents.pointerDownHandler);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            Bubble(eventData, ExecuteEvents.pointerUpHandler);
        }
}

