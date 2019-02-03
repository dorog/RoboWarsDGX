using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RuneStats
{
    [SerializeField]
    private int index;
    [SerializeField]
    private string name;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int cost;
    [SerializeField]
    private GameObject prefab;

    public int Index { get => index; set => index = value; }
    public string Name { get => name; set => name = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public int Cost { get => cost; set => cost = value; }
    public GameObject Prefab { get => prefab; set => prefab = value; }
}
