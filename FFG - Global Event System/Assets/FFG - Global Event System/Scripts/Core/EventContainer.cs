using System;
using UnityEngine;
using UnityEngine.Events;


namespace FFG
{
    [Serializable]
    public class EventContainer
    {
        #region Fields

        [SerializeField]
        private string _eventId;
        [SerializeField]
        private UnityEvent _noParameterEvent = null;
        [SerializeField]
        private UnityEvent<object> _singleParameterEvent = null;
        [SerializeField]
        private UnityEvent<object, GameObject> _doubleParameterEvent = null;

        #endregion Fields
        #region Constructors

        public EventContainer(string eventTag)
        {
            _eventId = eventTag;
            _noParameterEvent = new UnityEvent();
            _singleParameterEvent = new UnityEvent<object>();
            _doubleParameterEvent = new UnityEvent<object, GameObject>();
        }

        #endregion Constructors
        #region Properties

        public string EventTag => _eventId;

        #endregion Properties
        #region Public methods

        /// <summary>
        /// Method to add listener with no arguments
        /// </summary>
        public void AddListener(UnityAction listener) => _noParameterEvent.AddListener(listener);

        /// <summary>
        /// Method to add listener with 1 argument
        /// </summary>
        public void AddListener(UnityAction<object> listener) => _singleParameterEvent.AddListener(listener);

        /// <summary>
        /// Method to add listener with 2 arguments
        /// </summary>
        public void AddListener(UnityAction<object, GameObject> listener) => _doubleParameterEvent.AddListener(listener);

        /// <summary>
        /// Method to remove listener with no arguments
        /// </summary>
        public void RemoveListener(UnityAction listener) => _noParameterEvent.RemoveListener(listener);

        /// <summary>
        /// Method to remove listener with 1 argument
        /// </summary>
        public void RemoveListener(UnityAction<object> listener) => _singleParameterEvent.RemoveListener(listener);

        /// <summary>
        /// Method to remove listener with 2 argument
        /// </summary>
        public void RemoveListener(UnityAction<object, GameObject> listener) => _doubleParameterEvent.RemoveListener(listener);

        /// <summary>
        /// Method to invoke a listener with no argument
        /// </summary>
        public void Invoke() => _noParameterEvent?.Invoke();

        /// <summary>
        /// Method to invoke a listener with 1 argument
        /// </summary>
        /// <param name="data">Optional data to be passed</param>
        public void Invoke(object data) => _singleParameterEvent?.Invoke(data);

        /// <summary>
        /// Method to invoke a listener with 2 arguments
        /// </summary>
        /// <param name="data">Optional data to be passed</param>
        /// <param name="instigator">Event instigating object</param>
        public void Invoke(object data, GameObject instigator) => _doubleParameterEvent?.Invoke(data, instigator);

        #endregion Public methods
    }
}