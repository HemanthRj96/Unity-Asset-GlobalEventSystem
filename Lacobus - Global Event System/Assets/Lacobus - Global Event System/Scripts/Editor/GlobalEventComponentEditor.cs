using UnityEditor;
using Lacobus.Events;
using UnityEngine;


namespace Lacobus_Editors.Events
{

    [CustomEditor(typeof(GlobalEventComponent))]
    public class GlobalEventComponentEditor : EditorUtils<GlobalEventComponent>
    {
        SerializedProperty _ec;
        SerializedProperty _ecElem(int index) => _ec.GetArrayElementAtIndex(index);
        SerializedProperty _eventId(int index) => _ecElem(index).FindPropertyRelative("_eventId");
        SerializedProperty _noParamEvent(int index) => _ecElem(index).FindPropertyRelative("_noParameterEvent");
        SerializedProperty _singleParamEvent(int index) => _ecElem(index).FindPropertyRelative("_singleParameterEvent");
        SerializedProperty _doubleParamEvent(int index) => _ecElem(index).FindPropertyRelative("_doubleParameterEvent");

        public override void CustomOnGUI()
        {
            _ec = GetProperty("_eventContainer");

            Heading("Global Event System Settings");

            Space(20);
            BeginHorizontal();

            EditorGUILayout.LabelField("Event list", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
            _ec.arraySize = EditorGUILayout.IntField(_ec.arraySize, GUILayout.MaxWidth(20));

            EndHorizontal();

            Space(10);

            int length = _ec.arraySize;


            for (int i = 0; i < length; ++i)
            {
                if (_ecElem(i).isExpanded)
                {
                    _ecElem(i).isExpanded = EditorGUILayout.Foldout(_ecElem(i).isExpanded, $"{_eventId(i).stringValue}");

                    Space(5);
                    BeginVertical();
                    PropertyField(_eventId(i), "Event ID : ", "This is the unique identifier which will be used to call, subscribe, invoke and delete events");
                    PropertyField(_noParamEvent(i), "No parameter event : ", "");
                    PropertyField(_singleParamEvent(i), "Single parameter event : ", "");
                    PropertyField(_doubleParamEvent(i), "Double parameter event : ", "");
                    EndVertical();

                    BeginHorizontal();

                    if (Button("+", 20, 20))
                    {
                        ++length;
                        _ec.InsertArrayElementAtIndex(i + 1);
                        _eventId(i + 1).stringValue = $"Event_{length - 1}";
                    }
                    if (Button("-", 20, 20))
                    {
                        _ecElem(i).DeleteCommand();
                        --length;
                    }

                    EndHorizontal();
                }
                else
                {
                    BeginHorizontal();

                    _ecElem(i).isExpanded = EditorGUILayout.Foldout(_ecElem(i).isExpanded, $"{_eventId(i).stringValue}");
                    if (Button("+", 20, 20))
                    {
                        ++length;
                        _ec.InsertArrayElementAtIndex(i + 1);
                        _eventId(i + 1).stringValue = $"Event_{length - 1}";
                    }
                    if (Button("-", 20, 20))
                    {
                        _ecElem(i).DeleteCommand();
                        --length;
                    }

                    EndHorizontal();
                }
            }
        }
    }

    public class EditorUtils<TType> : Editor where TType : Object
    {
        public TType Root => (TType)target;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomOnGUI();
            serializedObject.ApplyModifiedProperties();
        }

        public virtual void CustomOnGUI() { }

        public SerializedProperty GetProperty(string propertyName)
            => serializedObject.FindProperty(propertyName);

        public void PropertyField(SerializedProperty property)
            => PropertyField(property, "", "");

        public void PropertyField(SerializedProperty property, string propertyName, string tooltip)
            => EditorGUILayout.PropertyField(property, new GUIContent(propertyName, tooltip));

        public void Info(string info, MessageType type = MessageType.Info)
            => EditorGUILayout.HelpBox(info, type);

        public void PropertySlider(SerializedProperty property, float min, float max, string label)
            => EditorGUILayout.Slider(property, min, max, label);

        public void Space(float val)
            => GUILayout.Space(val);

        public void Heading(string label)
        {
            var style = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold
            };
            EditorGUILayout.LabelField(label, style, GUILayout.ExpandWidth(true));
        }
        public bool Button(string content)
            => GUILayout.Button(content);

        public bool Button(string content, float height)
            => GUILayout.Button(content, GUILayout.Height(height));

        public bool Button(string content, float height, float width)
            => GUILayout.Button(content, GUILayout.Height(height), GUILayout.Width(width));

        public int DropdownList(string label, int index, string[] choices)
            => EditorGUILayout.Popup(label, index, choices);

        public void BeginVertical()
            => EditorGUILayout.BeginVertical();

        public void EndVertical()
            => EditorGUILayout.EndVertical();

        public void BeginHorizontal()
            => EditorGUILayout.BeginHorizontal();

        public void EndHorizontal()
            => EditorGUILayout.EndHorizontal();

        public void Label(string labelContent)
            => EditorGUILayout.LabelField(labelContent);
    }
}