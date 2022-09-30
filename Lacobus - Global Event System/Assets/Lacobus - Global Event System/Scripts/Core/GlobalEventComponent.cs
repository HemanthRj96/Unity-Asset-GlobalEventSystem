using System.Collections.Generic;
using UnityEngine;


namespace Lacobus.Events
{
    public sealed class GlobalEventComponent : MonoBehaviour
    {
        // Fields

        [SerializeField] private EventContainer[] _eventContainer = null;

        private List<EventInvokeHelpers> _invokeHelpers = new List<EventInvokeHelpers>();


        // Private methods

        private void init()
        {
            EventInvokeHelpers helper = null;

            foreach (var ec in _eventContainer)
            {
                helper = new EventInvokeHelpers(ec.EventTag, ec);

                GlobalEvents.CreateEvent(helper.EventTag());
                GlobalEvents.AddEventListener(helper.EventTag(), helper.NoParamEvent);
                GlobalEvents.AddEventListener(helper.EventTag(), helper.SingleParamEvent);
                GlobalEvents.AddEventListener(helper.EventTag(), helper.DoubleParamEvent);

                _invokeHelpers.Add(helper);
            }
        }


        // Lifecycle methods

        private void Awake()
        {
            init();
        }

        private void OnDestroy()
        {
            foreach (var helper in _invokeHelpers)
            {
                GlobalEvents.RemoveEventListener(helper.EventTag(), helper.NoParamEvent);
                GlobalEvents.RemoveEventListener(helper.EventTag(), helper.SingleParamEvent);
                GlobalEvents.RemoveEventListener(helper.EventTag(), helper.DoubleParamEvent);
            }
        }


        // Nested types

        internal class EventInvokeHelpers
        {
            // Constructor
            public EventInvokeHelpers(string eventTag, EventContainer eventContainer)
            {
                _eventTag = eventTag;
                _eventContainer = eventContainer;
            }


            // Fields & properties
            private string _eventTag = null;
            private EventContainer _eventContainer = null;


            // Public methods
            public string EventTag() => _eventTag;
            public void NoParamEvent() => _eventContainer.Invoke();
            public void SingleParamEvent(object eventData) => _eventContainer.Invoke(eventData);
            public void DoubleParamEvent(object eventData, GameObject eventInstigator) => _eventContainer.Invoke(eventData, eventInstigator);
        }
    }
}