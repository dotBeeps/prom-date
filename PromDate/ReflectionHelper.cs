using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public static class ReflectionHelper
{
    public static T GetPrivateField<T>(Type instanceType, object instance, string fieldName)
    {
        return (T) instanceType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
    }

    public static void SetPrivateField<T>(Type instanceType, object instance, string fieldName, T value)
    {
        instanceType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, value);
    }

    public static MethodInfo GetPrivateMethod(Type instanceType, string methodName)
    {
        return instanceType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public static T CallPrivateMethod<T>(Type instanceType, object instance, string methodName, object[] args)
    {
        return (T)GetPrivateMethod(instanceType, methodName).Invoke(instance, args);
    }

    public static void CallPrivateMethod(Type instanceType, object instance, string methodName, object[] args)
    {
        GetPrivateMethod(instanceType, methodName).Invoke(instance, args);
    }
}

