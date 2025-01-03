using System.ComponentModel;
using UnityEngine;
[CreateAssetMenu(fileName = "New Moth", menuName = "Moth/New Moth", order = 1)]
public class Moth : ScriptableObject
{
    [Description("The name used in game.")] public string FriendlyName;
}
