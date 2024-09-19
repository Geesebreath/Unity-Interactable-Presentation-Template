using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

/// <summary>
/// This only applies to public or Serialized fields. When Used in a script can click an inspector
/// button to trigger a method.
/// </summary>

[System.AttributeUsage(System.AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute
{
	public static float buttonWidth = 80;
	public string methodName;

	public float ButtonWidth
	{
		get { return buttonWidth; } set { buttonWidth = value; }
	}

	public InspectorButtonAttribute(string name)
	{
		methodName = name;
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
	private MethodInfo methodInfo = null;

	public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
	{
		InspectorButtonAttribute inspectorButtonAttribute = (InspectorButtonAttribute)attribute;
		Rect buttonRect = new Rect(position.x + (position.width - inspectorButtonAttribute.ButtonWidth)* 0.5f,
									position.y,
									inspectorButtonAttribute.ButtonWidth,
									position.height);

		if (GUI.Button(buttonRect, label.text))
		{
			System.Type eventOwnerType = prop.serializedObject.targetObject.GetType();
			string eventName = inspectorButtonAttribute.methodName;

			if (methodInfo == null)
			{
				methodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}

			if (methodInfo != null)
			{
				methodInfo.Invoke(prop.serializedObject.targetObject, null);
			}
			else
				Debug.LogWarning("Inspector Button couldn't find " + eventName + " in " + eventOwnerType + ".");
		}
	}
}
#endif