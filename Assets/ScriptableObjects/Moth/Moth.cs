using System;
using System.ComponentModel;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MothPair
{
    public Moth FirstMoth;
    public Moth SecondMoth;
    public float Chance;

    public MothPair(Moth firstMoth, Moth secondMoth)
    {
        FirstMoth = firstMoth;
        SecondMoth = secondMoth;
    }
}

[CreateAssetMenu(fileName = "New Moth", menuName = "Moth/New Moth", order = 1)]
public class Moth : ScriptableObject
{
    [Description("The name used in game.")] public string FriendlyName;

    public MothPair[] PossibleParents;
}
