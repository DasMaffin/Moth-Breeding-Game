using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager _instance;

    public GameManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if(_instance != null)
            {
                Destroy(value.gameObject);
                return;
            }
            _instance = value;
        }
    }

    public List<Moth> AllMoths = new List<Moth>();

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        AllMoths = Resources.LoadAll<Moth>("ScriptableObjects/Moth").ToList();
        FirstStart();
    }


    public void FirstStart()
    {
        float xAxis = 0;
        foreach(Moth moth in AllMoths)
        {
            if(moth.isStandardMoth)
            {
                GameObject obj = Instantiate(moth.Prefab, new Vector3(xAxis, 0, 0), Quaternion.identity, null);
                obj.GetComponent<MothController>().self = moth;
                xAxis += 1;
            }
        }
    }
}
