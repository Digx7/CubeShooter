using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Security.Policy;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    /* Description --
     *  this script will handel reading a Spawn Class and spawning that item
     */

    public Spawn spawn;

    public void Awake ()
    {

        // starts corouting spawning
       if (spawn.startOnAwake)
        {
            StartCoroutine("spawning");
        }

    }
    // this function will run on the start of the scene

    public void startSpawning ()
    {
        StartCoroutine("spawning");
    }
    // this function will start the Coroutine spawning

    public void stopSpawning ()
    {
        StopCoroutine("spawning");
    }
    // this function will stop the Coroutine spawning

    IEnumerator spawning ()
    {
        int x = 0;
        float rate;
        Vector3 location;

        // will run until has hit max rate
        while(x < spawn.maxSpawn)
        {
            //find spawn rate
            rate = UnityEngine.Random.Range(spawn.spawnRateRange.x, spawn.spawnRateRange.y);
            yield return new WaitForSeconds(rate);

            //increment spawned
            x++;

            //find spawn location
            location = new Vector3(
                UnityEngine.Random.Range(spawn.spawnLocationXRange.x, spawn.spawnLocationXRange.y),
                UnityEngine.Random.Range(spawn.spawnLocationYRange.x, spawn.spawnLocationYRange.y),
                UnityEngine.Random.Range(spawn.spawnLocationZRange.x, spawn.spawnLocationZRange.y)
                );
            if (spawn.spawnRelitiveToThisGameObject)
            {
                location += gameObject.transform.position;
            }

            //spawn object
            GameObject g = Instantiate(spawn.spawnableObject, location, Quaternion.identity);
        }
    }
    // this function will handel reading the spawn class and spawning the item


}
