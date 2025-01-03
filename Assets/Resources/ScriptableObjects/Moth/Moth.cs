using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class MothPair : IEquatable<MothPair>
{
    [Tooltip("First required moth type")] public Moth FirstMoth;
    [Tooltip("Second required moth type")] public Moth SecondMoth;
    [Tooltip("A weighted value of the chance to pull this moth. Higher weight = Higher chance")] public float Chance;

    public MothPair() { }

    public bool Equals(MothPair other)
    {
        if(FirstMoth == other.FirstMoth && SecondMoth == other.SecondMoth ||
            FirstMoth == other.SecondMoth && SecondMoth == other.FirstMoth)
            return true;
        return false;
    }

    public void SetFirstMoth(Moth m)
    {
        FirstMoth = m;
    }

    public void SetSecondMoth(Moth m)
    {
        SecondMoth = m;
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

    public static List<Moth> FindMothsWithPair(MothPair targetPair)
    {
        return GameManager.Instance.AllMoths.Where(moth => moth.PossibleParents.Any(pair =>
            (pair.FirstMoth == targetPair.FirstMoth && pair.SecondMoth == targetPair.SecondMoth) ||
            (pair.FirstMoth == targetPair.SecondMoth && pair.SecondMoth == targetPair.FirstMoth)
        )).ToList();
    }

    public static Moth SelectRandomMoth(List<Moth> potentialMoths, MothPair targetPair)
    {
        float totalWeight = potentialMoths.Sum(m => m.PossibleParents.Sum(p => 
        {
            if(p.Equals(targetPair))
            {
                return p.Chance;
            }
            return 0;
        }));

        foreach(Moth m in potentialMoths)
        {
            foreach(MothPair mp in m.PossibleParents.Where(p => p.Equals(targetPair)))
            {
                float rand = UnityEngine.Random.Range(0, totalWeight);
                if(rand <= mp.Chance)
                {
                    return m;
                }
                else
                {
                    totalWeight -= mp.Chance;
                }
            }
        }

        throw new Exception("This should never happen. Couldnt find a possible child!");
    }
}
