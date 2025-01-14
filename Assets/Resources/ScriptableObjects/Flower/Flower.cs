using UnityEngine;

[CreateAssetMenu(fileName = "New Flower", menuName = "Flower/New Flower", order = 2)]
public class Flower : ScriptableObject
{
    [Tooltip("The name used in game.")] public string FriendlyName;
    [Tooltip("2D Image that is used to represent this flower.")] public Sprite FlowerRepresentation;
}