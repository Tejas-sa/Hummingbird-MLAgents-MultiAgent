using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flower : MonoBehaviour
{
    [Tooltip("The color when the flower is pollinated")]
    public Color pollinatedColor = new Color(1f, 0.5f, 0.5f);

    [Tooltip("The color when the flower is not pollinated")]
    public Color notPollinatedColor = new Color(0.5f, 1f, 0.5f);

    // The trigger collider representing the nectar area of the flower
    [HideInInspector]
    public Collider nectarCollider;

    //the solid collider representing the folwer petals
    private Collider flowerCollider;

    // the folwer material
    private Material flowerMaterial;

    // the flower's up vector
    public Vector3 FlowerUpVector
    {
        get
        {
            return nectarCollider.transform.up;
        }
    }

    // the center postion of nector collider
    public Vector3 NectarCenterPosition
    {
        get
        {
            return nectarCollider.transform.position;
        }
    }

    // The amount of nector remaning in the flower
    public float NectorAmount { get; private set; }

    // when the flower has any nector left
    public bool HasNector
    {
        get
        {
            return NectorAmount > 0f;
        }
    }

    // Attempts to remove Nector from flower, returns the amount actually removed
    public float Feed(float amount)
    {
        // tracks how much nector was actually removed from the flower
        float nectorTaken = Mathf.Clamp(amount, 0f, NectorAmount);
        NectorAmount -= nectorTaken;

        if (NectorAmount <= 0f)
        {
            NectorAmount = 0f;
            flowerCollider.gameObject.SetActive(false);
            nectarCollider.gameObject.SetActive(false);

            flowerMaterial.SetColor("_BaseColor", pollinatedColor);
        }
        return nectorTaken;
    }

    // resets the flower to its initial state
    public void ResetFlower()
    {
        // refil the nector and re-enable the colliders
        NectorAmount = 1f;
        flowerCollider.gameObject.SetActive(true);
        nectarCollider.gameObject.SetActive(true);
        // set the flower color to the not pollinated color
        flowerMaterial.SetColor("_BaseColor", notPollinatedColor);
    }

    // called when the flower wakes up, used to initialize the flower
    private void Awake()
    {
        // Finds the flower's mesh render and gets the material 
        MeshRenderer flowerMeshRenderer = GetComponentInChildren<MeshRenderer>();
        flowerMaterial = flowerMeshRenderer.material;

        // Finds the flower's nectar collider and gets the collider component
        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("FlowerNectarCollider").GetComponent<Collider>();

    }
}
