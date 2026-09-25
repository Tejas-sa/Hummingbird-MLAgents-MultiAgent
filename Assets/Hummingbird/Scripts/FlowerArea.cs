using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// it manages a collectin of flowers in a specific area, and provides a way to get a random flower from the area
public class FlowerArea : MonoBehaviour
{
    // the diameter if the area where the agent and flower can be found
    public const float AreaDiameter = 20f;

    // the list of all flower plants in this flowwer area
    private List<GameObject> flowerPlants;

    // A lookup dictinoary for looking up a flower from a nector collider
    private Dictionary<Collider, Flower> nectarFlowerDictionary;

    // list of all flowers in this area
    public List<Flower> Flowers { get; private set; }

    // reset the flowers in this area, and reset the flower area
    public void ResetFlowers()
    {
        // rotate each flowers plant around the y axis and subly around x and z
        foreach (GameObject flowerPlant in flowerPlants)
        {
            float xRotation = UnityEngine.Random.Range(-5f, 5f);
            float yRotation = UnityEngine.Random.Range(-180f, 180f);
            float zRotation = UnityEngine.Random.Range(-5f, 5f);

            flowerPlant.transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        // reset each flower in the area
        foreach (Flower flower in Flowers)
        {
            flower.ResetFlower();
        }
    }

    public Flower GetFlowerFromNectar(Collider collider)
    {
        return nectarFlowerDictionary[collider];
    }

    // called when the area wakesup
    private void Awake()
    {
        //initialize the variables
        flowerPlants = new List<GameObject>();
        nectarFlowerDictionary = new Dictionary<Collider, Flower>();
        Flowers = new List<Flower>();
    }

    // called when the area starts
    private void Start()
    {
        // find the flowers that are children of this area, and add them to the flower list
        FindChildFlowers(transform);
    }

    // 
    private void FindChildFlowers(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.CompareTag("flower_plant"))
            {
                flowerPlants.Add(child.gameObject);

                // find the flower component in the child and add it to the flower list
                FindChildFlowers(child);

            }
            else
            {
                // not a flower plant, check if it has a flower component
                Flower flower = child.GetComponent<Flower>();
                if (flower != null)
                {
                    // found a flower, add it to the flower list and add its nectar collider to the lookup dictionary
                    Flowers.Add(flower);
                    // add the flower's nectar collider to the lookup dictionary
                    nectarFlowerDictionary.Add(flower.nectarCollider, flower);
                }
                else
                {
                    FindChildFlowers(child);
                }
            }

        }
    }

}
