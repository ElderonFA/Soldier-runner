using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PullController : MonoBehaviour
{
    [SerializeField] 
    private List<PulledObjectConfig> pulledPrefabConfigs;

    private List<IPulled> allPulledObjects;
    
    public static Action<IPulled> respawnPulled;
    
    void Awake()
    {
        StartSpawnPulledObject();
        respawnPulled += RespawnPulled;
    }

    private void StartSpawnPulledObject()
    {
        foreach (var config in pulledPrefabConfigs)
        {
            for (var i = 0; i < config.counts; i++)
            {
                var obj = Instantiate(config.pulledPrefab);
                
                var iPulledComponent = obj.GetComponentInChildren<IPulled>();
                iPulledComponent.Spawn(GeneratePositionWithType(iPulledComponent.PulledType));
                
                //allPulledObjects.Add(iPulledComponent);
            }
        }
    }
    
    private void RespawnPulled(IPulled pulled)
    {
        pulled.Spawn(GeneratePositionWithType(pulled.PulledType));
    }

    private Vector3 GeneratePositionWithType(PulledType pulledType)
    {
        return pulledType switch
        {
            PulledType.Enemy => new Vector3(Random.Range(-6f, 6f), 0f, Random.Range(20f, 40f)),
            PulledType.Tree => new Vector3(Random.Range(0, 2) == 0 ? 9f : -9f, 0f, Random.Range(20f, 60f)),
        };
    }
}

[Serializable]
public class PulledObjectConfig
{
    public GameObject pulledPrefab;
    public int counts;
}
