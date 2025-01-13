using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public enum Gender
{
    Male = 0,
    Female = 1,
    No = 2
}

[System.Serializable]
public class MothPair : IEquatable<MothPair>
{
    [Tooltip("First required moth type")] public Moth FirstMoth;
    [Tooltip("Second required moth type")] public Moth SecondMoth;
    [HideInInspector] public MothController FirstMothController;
    [HideInInspector] public MothController SecondMothController;
    [Tooltip("A weighted value of the chance to pull this moth. Higher weight = Higher chance")] public float WeigthedChanceBase;
    [Tooltip("A weighted value of the chance to pull this moth. Higher weight = Higher chance")] public float WeigthedChanceFlowered;
    [Tooltip("")] public Flower flower;

    public bool Equals(MothPair other)
    {
        if
        (
            (FirstMothController != null && other.FirstMothController != null &&
             FirstMothController == other.FirstMothController &&
             SecondMothController == other.SecondMothController) ||

            (FirstMothController != null && other.SecondMothController != null &&
             FirstMothController == other.SecondMothController &&
             SecondMothController == other.FirstMothController) ||

            (FirstMoth != null && other.FirstMoth != null &&
             FirstMoth == other.FirstMoth &&
             SecondMoth == other.SecondMoth) ||

            (FirstMoth != null && other.SecondMoth != null &&
             FirstMoth == other.SecondMoth &&
             SecondMoth == other.FirstMoth) ||

            (FirstMoth != null && other.FirstMothController != null &&
             FirstMoth == other.FirstMothController.self &&
             SecondMoth == other.SecondMothController?.self) ||

            (FirstMoth != null && other.SecondMothController != null &&
             FirstMoth == other.SecondMothController.self &&
             SecondMoth == other.FirstMothController?.self) ||

            (FirstMothController?.self != null && other.FirstMoth != null &&
             FirstMothController.self == other.FirstMoth &&
             SecondMothController?.self == other.SecondMoth) ||

            (FirstMothController?.self != null && other.SecondMoth != null &&
             FirstMothController.self == other.SecondMoth &&
             SecondMothController?.self == other.FirstMoth)
        )
            return true;
        return false;
    }

    public void SetFirstMoth(MothController m)
    {
        FirstMothController = m;
    }

    public void SetSecondMoth(MothController m)
    {
        SecondMothController = m;
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

    private float getMyWeight(MothPair mp, Flower flower)
    {
        float ret = PossibleParents.Sum(p =>
        {
            if(p.Equals(mp))
            {
                return flower == null ? p.WeigthedChanceBase : p.WeigthedChanceFlowered;
            }
            return 0;
        });

        return ret;
    }

    public static List<Moth> FindMothsWithPair(MothPair targetPair)
    {
        return GameManager.Instance.AllMoths.
            Where(moth => 
                moth.PossibleParents.Any(pair => 
                    pair.Equals(targetPair))
                ).ToList();
    }

    public static Moth SelectRandomMoth(List<Moth> potentialMoths, MothPair targetPair, Flower flower = null)
    {
        float totalWeight = potentialMoths.Sum(m => m.PossibleParents.Sum(p =>
        {
            if(p.Equals(targetPair))
            {
                return flower == null ? p.WeigthedChanceBase : p.WeigthedChanceFlowered;
            }
            return 0;
        })) + 200;

        foreach(Moth m in potentialMoths)
        {
            float rand = UnityEngine.Random.Range(0, totalWeight);
            if(rand <= m.getMyWeight(targetPair, flower))
            {
                return m;
            }
            else
            {
                totalWeight -= m.getMyWeight(targetPair, flower);
            }
        }

        if(UnityEngine.Random.Range(0, 200) <= 100)
        {
            return targetPair.FirstMoth != null ? targetPair.FirstMoth : targetPair.FirstMothController.self;
        }
        else
        {
            return targetPair.SecondMoth != null ? targetPair.SecondMoth : targetPair.SecondMothController.self;
        }

        throw new Exception("This should never happen. Couldnt find a possible child!");
    }
}
