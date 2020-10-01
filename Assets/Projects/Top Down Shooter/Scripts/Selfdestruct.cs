using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct : MonoBehaviour
{
    /* Description --
     *  This script will destroy the gameObject based on a set time
     */
    public float selfDestructTime;
    public bool destroyOnAwake = false;
    public GameObject objectToDestory;

    public void Awake ()
    {
        // if object to destroy is not set in Inspector the it will autoset to itself
        if (objectToDestory == null)
        {
            objectToDestory = gameObject;
        }

        if (destroyOnAwake)
        {
            Destroy (objectToDestory, selfDestructTime);
        }
    }
    // this function will run at the start of the scene/this gameObject being in the scene

    public void destroyObject ()
    {
        Destroy (objectToDestory, selfDestructTime);
    }
    // this function will destroy the given object after the set time
}
