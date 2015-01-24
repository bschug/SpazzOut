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
}
