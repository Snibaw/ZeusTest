using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour
{       
    public Storage storage;
    public TownBehaviour TownBehaviour;
    public GameObject[] homes;
    public float MinDistance = 5f;
    public int energyLostPerAction = 5;
    public Dictionary<DestinationType, List<Transform>> Destinations { get; private set; }

    public static Context instance;

    private IEnumerator Start()
    { 
        // Wait for the resources to be spawned
        yield return new WaitForSeconds(0.5f);
        List<Transform> restDestinations = new List<Transform>() { TownBehaviour.transform };
        List<Transform> storageDestinations = new List<Transform>() { storage.transform };
        List<Transform> resourceDestinations = GetAllResources();

        instance = this;

        Destinations = new Dictionary<DestinationType, List<Transform>>()
        {
            { DestinationType.rest, restDestinations},
            { DestinationType.storage, storageDestinations },
            { DestinationType.resource, resourceDestinations }
        };
    }

    private List<Transform> GetAllResources()
    {
        Transform[] gameObjects = FindObjectsOfType<Transform>() as Transform[];
        List<Transform> resources = new List<Transform>();
        foreach (Transform go in gameObjects)
        {
            if(go.gameObject.tag == "Resources")
            {
                resources.Add(go);
            }
        }
        return resources;
    }
    public Transform FindClosestRestPosition(Vector3 position)
    {
        float minDistance = float.MaxValue;
        Debug.Log("DestinationCount" + Destinations[DestinationType.rest].Count);
        Transform minTransform = Destinations[DestinationType.rest][0];
        foreach (Transform t in Destinations[DestinationType.rest])
        {
            float distance = Vector3.Distance(position, t.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minTransform = t;
            }
        }
        return minTransform;
    }
    public void AddDestinationTypeBuild(DestinationType type, Transform destination)
    {
        if (Destinations.ContainsKey(type))
        {
            Destinations[type].Add(destination);
        }
        // Useful for later
        // else
        // {
        //     Destinations.Add(type, new List<Transform>() { destination }); 
        // }
    }
    public void RemoveObjectFromDestination(Transform destination, DestinationType type)
    {
        if (Destinations.ContainsKey(type))
        {
            Destinations[type].Remove(destination);
            Debug.Log("Removed " + destination.name + " from " + type);
        }
    }

}
