using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using BitStrap;

public static class ExtensionMethods {

    public static Vector3 setX(this Vector3 v,float X)
    {

        return new Vector3(X, v.y,v.z);
        
    }
    public static T RandomItemConditional<T>(this List<T> list,Predicate<T> condition)
    {
        List<T> temp = list.FindAll(condition);
        if (temp.Count == 0) { return default(T); }
        return temp[UnityEngine.Random.Range(0, list.Count-1)];

    }
    public static Vector3 setY(this Vector3 v, float Y)
    {
        
        return new Vector3(v.x, Y,v.z);
    }
    public static void Next(this Enum e){
       // Enum.GetValues(e);
    }

    public static void ToLists<T, S>(this Dictionary<T, S> d , List<T> l1, List<S> l2)
    {
        l1 = new List<T>();
        l2 = new List<S>();
        foreach (KeyValuePair<T, S> pair in d)
        {
            l1.Add(pair.Key);
            l2.Add(pair.Value);
        }
    }
    public static void FromLists<T, S>(this Dictionary<T, S> d, List<T> l1, List<S> l2)
    {
        if (l1.Count != l2.Count)
        {
            Debug.Log("Error, both lists need to be the same length to convert to dictionary.\nList1=" + l1.Count + " List2 Count=" + l2.Count);
            return;
        }
        if (l1.Count != l1.Distinct().Count())
        {
            Debug.Log("Error, Can not create dictionary with duplicate keys ");
            return;
        }
        d.Clear();
        for (int i = 0; i < l1.Count; i++)
        {
            d.Add(l1[i], l2[i]);
        }
       
    }
}
