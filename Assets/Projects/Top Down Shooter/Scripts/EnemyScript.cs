using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour
{
    /* Description --
     *  This Script will control the basic enemy
     */
    /* Notes -- 
     *  May be able to repurpose this code as a starting point for later enemys
     */

    public Vector3 movementDirection;
    public GameObject player;
    public float movenmentSpeed;
    public GameObject enemyDeathPreFab;
    public Rigidbody rb;
    public UnityEvent spawn;
    public UnityEvent death;

    public void Awake ()
    {
        spawn.Invoke();

        // if player is not set, will auto set
        if(player == null)
        {
            GameObject[] _player = GameObject.FindGameObjectsWithTag("Player");

            int i = 1;
            if (i == _player.Length) player = _player[0];
            else StartCoroutine("waitForPlayerToSpawn"); 
           
        }
    }
    // This function will be called at the start of the scene

    public IEnumerator waitForPlayerToSpawn ()
    {
        int i = 1;
        GameObject[] _player;
        do
        {
            _player = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        } while (i != _player.Length);

        player = _player[0];

        yield return null;
    }

    public void FixedUpdate ()
    {   if (player != null)
        {
            movementDirection = player.transform.position - gameObject.transform.position; //finds direction of player
            movementDirection = movementDirection.normalized;// changes vector to scale of 0-1
            rb.velocity = movementDirection * movenmentSpeed * Time.deltaTime;// applys movement
        }
    }
    // This function should be used for any physics related stuff

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            startDeath();
        }
    }
    // This function will be called everytime a collision happens

    public void startDeath ()
    {
        GameObject sparks = Instantiate(enemyDeathPreFab, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
    // this function will start the death animation
}
