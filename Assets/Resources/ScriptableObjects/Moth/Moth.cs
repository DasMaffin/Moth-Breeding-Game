using UnityEngine;

[System.Serializable]
public class MothPair
{
    [Tooltip("First required moth type")] public Moth FirstMoth;
    [Tooltip("Second required moth type")] public Moth SecondMoth;
    [Tooltip("A weighted value of the chance to pull this moth. Higher weight = Higher chance")] public float Chance;

    public MothPair(Moth firstMoth, Moth secondMoth)
    {
        FirstMoth = firstMoth;
        SecondMoth = secondMoth;
    }
}

[CreateAssetMenu(fileName = "New Moth", menuName = "Moth/New Moth", order = 1)]
public class Moth : ScriptableObject
{
    [Tooltip("The name used in game.")] public string FriendlyName;
    [Tooltip("The prefab that will be spawned into the game.")] public GameObject Prefab;
    [Tooltip("2D Image that is used to represent this moth.")] public Sprite MothRepresentation;

    [Tooltip("Every parent combination that can result in this moth.")] public MothPair[] PossibleParents;

    [Tooltip("Wether the moth should spawn in a new game or not.")] public bool isStandardMoth = false;
}
