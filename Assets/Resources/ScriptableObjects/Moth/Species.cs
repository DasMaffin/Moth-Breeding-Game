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
public class SpeciesPair : IEquatable<SpeciesPair>
{
    [Tooltip("First required moth type")] public Species FirstMoth;
    [Tooltip("Second required moth type")] public Species SecondMoth;
    [HideInInspector] public MothController FirstMothController;
    [HideInInspector] public MothController SecondMothController;
    [Tooltip("A weighted value of the chance to pull this moth. Higher weight = Higher chance")] public float WeigthedChanceBase;
    [Tooltip("A weighted value of the chance to pull this moth. Higher weight = Higher chance")] public float WeigthedChanceFlowered;
    [Tooltip("")] public Flower flower;

    public bool Equals(SpeciesPair other)
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
             FirstMoth == other.FirstMothController.Moth.species &&
             SecondMoth == other.SecondMothController?.Moth.species) ||

            (FirstMoth != null && other.SecondMothController != null &&
             FirstMoth == other.SecondMothController.Moth.species &&
             SecondMoth == other.FirstMothController?.Moth.species) ||

            (FirstMothController?.Moth.species != null && other.FirstMoth != null &&
             FirstMothController.Moth.species == other.FirstMoth &&
             SecondMothController?.Moth.species == other.SecondMoth) ||

            (FirstMothController?.Moth.species != null && other.SecondMoth != null &&
             FirstMothController.Moth.species == other.SecondMoth &&
             SecondMothController?.Moth.species == other.FirstMoth)
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
public class Species : ScriptableObject
{
    [Tooltip("The name used in game.")] public string FriendlyName;
    [Tooltip("The prefab that will be spawned into the game.")] public GameObject Prefab;
    [Tooltip("2D Image that is used to represent this moth.")] public Sprite MothRepresentation;
    [Tooltip("")] public Material[] DefaultWing;
    [Tooltip("")] public Material[] DefaultBody;

    [Tooltip("Every parent combination that can result in this moth.")] public SpeciesPair[] PossibleParents;

    [Tooltip("Wether the moth should spawn in a new game or not.")] public bool isStandardSpecies = false;

    private float getMyWeight(SpeciesPair mp, Flower flower)
    {
        float ret = PossibleParents.Sum(p =>
        {
            if(p.Equals(mp))
            {
                return (flower == null || flower != p.flower) ? p.WeigthedChanceBase : p.WeigthedChanceFlowered;
            }
            return 0;
        });

        return ret;
    }

    public static List<Species> FindMothsWithPair(SpeciesPair targetPair)
    {
        return GameManager.Instance.AllMoths.
            Where(moth => 
                moth.PossibleParents.Any(pair => 
                    pair.Equals(targetPair))
                ).ToList();
    }

    public static Species SelectRandomMoth(List<Species> potentialMoths, SpeciesPair targetPair, Flower flower = null)
    {
        float totalWeight = potentialMoths.Sum(m => m.PossibleParents.Sum(p =>
        {
            if(p.Equals(targetPair))
            {
                return (flower == null || flower != p.flower) ? p.WeigthedChanceBase : p.WeigthedChanceFlowered;
            }
            return 0;
        })) + 200;

        foreach(Species m in potentialMoths)
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
            return targetPair.FirstMoth != null ? targetPair.FirstMoth : targetPair.FirstMothController.Moth.species;
        }
        else
        {
            return targetPair.SecondMoth != null ? targetPair.SecondMoth : targetPair.SecondMothController.Moth.species;
        }

        throw new Exception("This should never happen. Couldnt find a possible child!");
    }
}


public class Moth
{
    public Species species;
    public Material[] Wings;
    public Material[] Body;
    public Gender Gender;
    public float growSpeed = 0.001f;
    public float fullSize = 0.1f;

    public float BreedingCooldown = 30f;
    public float BreedingCooldownLeft { get => BreedingCooldown - currentBreedingCooldown; }

    private float currentBreedingCooldown = 0f;
    public bool BreedingCooldownActive = false;

    public Moth(Species _species, Gender _gender)
    {
        species = _species;
        Gender = _gender;
        Wings = _species.DefaultWing;
        Body = _species.DefaultBody;
    }

    public void ElapseBreedingCooldown(float timeElapse)
    {
        if(BreedingCooldownActive)
        {
            currentBreedingCooldown += Time.deltaTime;
            if(currentBreedingCooldown > BreedingCooldown)
            {
                currentBreedingCooldown = 0f;
                BreedingCooldownActive = false;
            }
        }
    }

    public bool CanBreed()
    {
        return !BreedingCooldownActive;
    }
}