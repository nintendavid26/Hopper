﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public static void testBeeHive(this BeeHive B)
    {
        Debug.Log("hive"+B.transform.position);
    }

    public static bool AllSame<T>(this List<T> list,T val)//Can check for given value or any value
    {
        if (list == null)
        {
            return true;
        }
        if (list.Capacity < 1)//Should it return true for 0 or 1 item list?
        {
            return true;
        }
        foreach (T i in list)
        {
            if (!Equals(i,val))
            {
                return false;
            }
        }
        return true;
    }

    public static void SetAll<T>(this List<T> list, T val)
    {
        for (int i = 0; i < list.Capacity; i++)
        {
            list[i] = val;
        }
    }



    public static List<T> RemoveLast<T>(this List<T> list )
    {
        List<T> list2 = list;
        list2.RemoveAt(list2.Count-1);
        return list2;
    }

    public static void PlaySound(this MonoBehaviour g,string soundName,float volume=1,bool randomPitch=false)
    {
        SoundEffects sfx = g.GetComponent<SoundEffects>();
        if (sfx == null) { Debug.Log("PlaySound requires SoundEffects Component");
            return;
        }
        sfx.PlaySound(soundName,volume,randomPitch);
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
    public static bool FromLists<T, S>(this Dictionary<T, S> d, List<T> l1, List<S> l2)
    {
        if (l1.Count != l2.Count)
        {
            Debug.Log("Error, both lists need to be the same length to convert to dictionary.\nList1 Count=" + l1.Count + " List2 Count=" + l2.Count);
            return false;
        }
        if (l1.Count != l1.Distinct().Count())
        {
            Debug.Log("Error, Can not create dictionary with duplicate keys ");
            return false;
        }
        d.Clear();
        for (int i = 0; i < l1.Count; i++)
        {
            d.Add(l1[i], l2[i]);
        }
        return true;
    }
}
