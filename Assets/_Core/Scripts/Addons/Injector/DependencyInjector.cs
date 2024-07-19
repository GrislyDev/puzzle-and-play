using System;
using System.Collections.Generic;
using UnityEngine;

public static class DependencyInjector
{
	private static readonly Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

	public static void Register<T>(T implementation)
	{
		Type interfaceType = typeof(T);

		_dependencies[interfaceType] = implementation;
	}

	public static T Resolve<T>()
	{
		Type interfaceType = typeof(T);
		if (!_dependencies.ContainsKey(interfaceType))
		{
			Debug.LogError($"Dependency of type {interfaceType} is not registered.");
			return default;
		}

		return (T)_dependencies[interfaceType];
	}
}
