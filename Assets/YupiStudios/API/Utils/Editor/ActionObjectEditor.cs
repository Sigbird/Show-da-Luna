using UnityEngine;
using UnityEditor;

using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;



namespace YupiStudios.API.Utils
{

	[CustomPropertyDrawer(typeof(ActionObject))]
	public class ActionObjectEditor : PropertyDrawer {

		private float GetYSizeFromType (SerializedProperty property)
		{
			SerializedProperty propertyNormalSize = property.FindPropertyRelative ("actionType");
			float height = EditorGUI.GetPropertyHeight (propertyNormalSize);

			SerializedProperty propertyEventSize = property.FindPropertyRelative ("gameEvent");
			SerializedProperty propertySceneSize = property.FindPropertyRelative ("scene");
			SerializedProperty propertyTimeSize = property.FindPropertyRelative ("timeScaleValue");

			switch (propertyNormalSize.enumNames [propertyNormalSize.enumValueIndex])
			{
			case "CallEvent":
				height+=EditorGUI.GetPropertyHeight (propertyEventSize);
				break;
			case "LoadScene":
				height+=EditorGUI.GetPropertyHeight (propertySceneSize);
				break;
			case "SetTimeScale":
				height+=EditorGUI.GetPropertyHeight (propertyTimeSize);
				break;
			default:
				break;
			}

			return height;
		}
		
		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return GetYSizeFromType(property);
		}

		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (position, label, property);
			position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			SerializedProperty propertyType = property.FindPropertyRelative ("actionType");

			float size = EditorGUI.GetPropertyHeight (propertyType);//+GetYSizeFromType(property);
			
			// Calculate rects
			Rect typeRect = new Rect (position.x, position.y, position.width, size);
			Rect paramsRect= new Rect (position.x, position.y + size, position.width, position.height-size);

			EditorGUI.PropertyField (typeRect, property.FindPropertyRelative ("actionType"), GUIContent.none);


			switch (propertyType.enumNames [propertyType.enumValueIndex]) {
			case "SetTimeScale":
				EditorGUI.BeginChangeCheck();
				EditorGUI.PropertyField (paramsRect, property.FindPropertyRelative ("timeScaleValue"), GUIContent.none);
				if (EditorGUI.EndChangeCheck())
				{
					if ( property.FindPropertyRelative ("timeScaleValue").floatValue < 0)
						property.FindPropertyRelative ("timeScaleValue").floatValue = 0;
				}
				break;
			case "CallEvent":
				EditorGUI.PropertyField (paramsRect, property.FindPropertyRelative ("gameEvent"), GUIContent.none);
				break;
			case "LoadScene":
				EditorGUI.PropertyField (paramsRect, property.FindPropertyRelative ("scene"), GUIContent.none);
				break;
			default:
				break;
			}
			
			
			// Set indent back to what it was
			EditorGUI.indentLevel = indent;			
			EditorGUI.EndProperty ();
		}
		
	}

}