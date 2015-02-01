using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Utils
{
	public static bool IsAttachedTo (Transform parent, Transform obj)
	{
		if (obj.parent == null) return false;
		if (obj.parent == parent) return true;
		return IsAttachedTo (parent, obj.parent);
	}

	public static T FindInParents<T> (Transform obj) where T:Component
	{
		if (obj.GetComponent<T> () != null) return obj.GetComponent<T>();
		if (obj.parent == null) return null;
		return FindInParents<T> (obj.parent);
	}

	public static Transform FindChild(string name, Transform obj) {
		for (int i=0; i < obj.childCount; i++) {
			var child = obj.GetChild (i);
			if (child.name == name) {
				return child;
			}

			var found = FindChild (name, child);
			if (found != null) {
				return found;
			}
		}

		return null;
	}
}
