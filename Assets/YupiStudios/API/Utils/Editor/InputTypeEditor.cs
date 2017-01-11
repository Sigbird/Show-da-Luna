using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

using YupiStudios.API.Utils;

namespace YupiStudios.API.Utils
{
	[CustomPropertyDrawer(typeof(InputType))]
	public class InputTypeEditor : PropertyDrawer {

		private int selectedType;
		private int selectedParam;

		private GUIContent []typesContent;

		public void GetInputType ()
		{
			string []types = Enum.GetNames (typeof(EInputType));

			typesContent = new GUIContent[types.Length];

			for (int i = 0; i < types.Length; ++i) {
				typesContent[i] = new GUIContent(types[i]);
			}
		}

		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (position, label, property);
			position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;
			
			// Calculate rects
			Rect typeRect = new Rect (position.x, position.y, position.width / 3, position.height);
			Rect paramsRect= new Rect (position.x + position.width / 3 + 5, position.y, position.width / 3, position.height);


			SerializedProperty propertyType = property.FindPropertyRelative ("inputType");
			EditorGUI.PropertyField (typeRect, property.FindPropertyRelative ("inputType"), GUIContent.none);

			switch (propertyType.enumNames [propertyType.enumValueIndex]) {	
			case "KeyCode":
				EditorGUI.PropertyField (paramsRect, property.FindPropertyRelative ("keyCode"), GUIContent.none);
				break;
			case "Click":
				EditorGUI.PropertyField (paramsRect, property.FindPropertyRelative ("mouseButton"), GUIContent.none);
				break;
			default:
				break;
			}
		
			EditorGUI.indentLevel = indent;			
			EditorGUI.EndProperty ();

		}

	}

}