using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleList : MonoBehaviour
{
    public ListExample listExample = new ListExample();

}

[Serializable]
public class ListExample
{
    public List<ExampleGuide> id;

    public ListExample()
    {
        id = new List<ExampleGuide>();
    }
}

[Serializable]
public class ExampleGuide
{
    public int id;
    public string name;
    public float value;
    public List<int> list;

    public ExampleGuide()
    {
        list = new List<int>();
    }
}

