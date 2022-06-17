using UnityEditor;
using FFG;
using UnityEngine;


namespace FFG_Editors
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
}