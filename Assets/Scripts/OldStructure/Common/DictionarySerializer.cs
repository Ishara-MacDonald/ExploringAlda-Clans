
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DictionarySerializer<TKey, TValue>
{
    [SerializeField] DictionarySerializerElement<TKey, TValue>[] elements;

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> newDict = new();

        foreach (var item in elements)
        {
            if (!newDict.ContainsKey(item.key))
                newDict.Add(item.key, item.value);
        }
        return newDict;
    }
}

[Serializable]
public class DictionarySerializerElement<TKey, TValue>
{
    [SerializeField] public TKey key;
    [SerializeField] public TValue value;
}