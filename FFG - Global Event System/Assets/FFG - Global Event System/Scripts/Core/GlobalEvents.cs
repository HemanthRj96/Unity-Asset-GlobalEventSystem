using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace FFG
{
    public static class GlobalEvents
    {
        #region Fields

        private static Dictionary<string, EventContainer> s_eventContainerLookup = new Dictionary<string, EventContainer>();

        #endregion Fields
        #region Private methods

        private static bool containsKey(string key) => s_eventContainerLookup.ContainsKey(key);

        private static bool isValidString(string str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);

        private static bool eventDoesNotExist(string eventId) => isValidString(eventId) && !containsKey(eventId);

        private static bool eventDoesExist(string eventId) => isValidString(eventId) && containsKey(eventId);

        private static void createEvent(string eventId, UnityAction noParamEvent = null, UnityAction<object> singleParamEvent = null, UnityAction<object, GameObject> doubleParamEvent = null)
        {
            EventContainer container = new EventContainer(eventId);

            if (noParamEvent != null)
                container.AddListener(noParamEvent);
            else if (singleParamEvent != null)
                container.AddListener(singleParamEvent);
            else if (doubleParamEvent != null)
                container.AddListener(doubleParamEvent);
            s_eventContainerLookup.Add(eventId, container);
        }

        #endregion Private methods
        #region Public methods

        /// <summary>
        /// Method to create a new event
        /// </summary>
        /// <param name="eventId">Event tag</param>
        public static void CreateEvent(string eventId)
        {
            if (eventDoesNotExist(eventId))
                createEvent(eventId);
        }

        /// <summary>
        /// Method to create a new event with a default listener
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Target listener with no parameters</param>
        public static void CreateEvent(string eventId, UnityAction targetEvent)
        {
            if (eventDoesExist(eventId))
                AddEventListener(eventId, targetEvent);
            else if (isValidString(eventId))
                createEvent(eventId, noParamEvent: targetEvent);
        }

        /// <summary>
        /// Method to create a new event with a default listener
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Target listener with single parameter</param>
        public static void CreateEvent(string eventId, UnityAction<object> targetEvent)
        {
            if (eventDoesExist(eventId))
                AddEventListener(eventId, targetEvent);
            else if (isValidString(eventId))
                createEvent(eventId, singleParamEvent: targetEvent);
        }

        /// <summary>
        /// Method to create a new event with a default listener
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Target listener with two parameters</param>
        public static void CreateEvent(string eventId, UnityAction<object, GameObject> targetEvent)
        {
            if (eventDoesExist(eventId))
                AddEventListener(eventId, targetEvent);
            else if (isValidString(eventId))
                createEvent(eventId, doubleParamEvent: targetEvent);
        }

        /// <summary>
        /// Method to add a listener to an event
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Target event with no parameters</param>
        public static void AddEventListener(string eventId, UnityAction targetEvent)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].AddListener(targetEvent);
        }

        /// <summary>
        /// Method to add a listener to an event
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Target event with one parameter</param>
        public static void AddEventListener(string eventId, UnityAction<object> targetEvent)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].AddListener(targetEvent);
        }

        /// <summary>
        /// Method to add a listener to an event
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Target event with two parameter</param>
        public static void AddEventListener(string eventId, UnityAction<object, GameObject> targetEvent)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].AddListener(targetEvent);
        }

        /// <summary>
        /// Method to invoke an event
        /// </summary>
        /// <param name="eventId">Event tag</param>
        public static void InvokeEvent(string eventId)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].Invoke();
        }

        /// <summary>
        /// Method to invoke an event with data
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="eventData">Event data</param>
        public static void InvokeEvent(string eventId, object eventData)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].Invoke(eventData);
        }

        /// <summary>
        /// Method to invoke an event with data and instigator
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="eventData">Event data</param>
        /// <param name="eventInstigator"></param>
        public static void InvokeEvent(string eventId, object eventData, GameObject eventInstigator)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].Invoke(eventData, eventInstigator);
        }

        /// <summary>
        /// Method to remove event listener
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Listener to be removed</param>
        public static void RemoveEventListener(string eventId, UnityAction targetEvent)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].RemoveListener(targetEvent);
        }

        /// <summary>
        /// Method to remove event listener
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Listener to be removed</param>
        public static void RemoveEventListener(string eventId, UnityAction<object> targetEvent)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].RemoveListener(targetEvent);
        }

        /// <summary>
        /// Method to remove event listener
        /// </summary>
        /// <param name="eventId">Event tag</param>
        /// <param name="targetEvent">Listener to be removed</param>
        public static void RemoveEventListener(string eventId, UnityAction<object, GameObject> targetEvent)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup[eventId].RemoveListener(targetEvent);
        }

        /// <summary>
        /// Method to destroy an event
        /// </summary>
        /// <param name="eventId">Event tag</param>
        public static void DeleteEvent(string eventId)
        {
            if (eventDoesExist(eventId))
                s_eventContainerLookup.Remove(eventId);
        }

        #endregion Public methods
    }
}
