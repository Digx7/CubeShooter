using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathAndRespawn : MonoBehaviour
{
    /* Description --
     *  This script will handel the death and respawning of the player
     */
    /* Notes --
     *  This script still needs the respawn component
     */

    public int lives;
    [Space]
    public float freezeFrameLength = 1.0f; // how long game freezes upon death
    public float enemyClearDelay = 0.5f;
    public float respawnDelay = 0.5f;
    public float spawnerRestartDelay = 0.5f;
    private bool canRunSpawnCode = true;
    [Space]
    public GameObject Player;
    public GameObject PlayerDeathSparks;
    [Space]
    public GameObject[] Enemies;
    [Space]
    public SpawnerScript[] EnemySpawners;
    public SpawnerScript PlayerSpawner;
    [Space]
    public PlayerInputScript playerInputScript;
    [Space]
    public UnityEvent PlayerDeathStart, PlayerDeathAfterFreeze, PlayerRespawn, EnemiesRestartRespawning;

    public void Awake ()
    {
        Rebind();
    }
    // this function will run at the start of the scene

    public void Rebind ()
    {
        // if player is not set in inspector then it will auto set
        if (Player == null)
        {
            GameObject[] _player = GameObject.FindGameObjectsWithTag("Player");

            int i = 1;
            if (i == _player.Length)
            {
                Player = _player[0];
                bindPlayerDeathSparks();
            }
            else StartCoroutine("waitForPlayerToSpawn");
        }
    }
    // this function will handel rehooking up the needed variables 

    public void bindPlayerDeathSparks ()
    {
        // if PlayerDeathSparks is not set then it will auto set based on player
        if (PlayerDeathSparks == null)
        {
            PlayerDeathSparks = Player.GetComponent<PlayerScript>().playerDeathSparks;
        }
    }
    // this function will handel rehooking up the needed variables

    public IEnumerator waitForPlayerToSpawn()
    {
        int i = 1;
        GameObject[] _player;
        do
        {
            _player = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        } while (i != _player.Length);

        Player = _player[0];

        Rebind();
        bindPlayerDeathSparks();

        yield return null;
    }
    // this will passivly wait for the player to rehook it up to the code

    public void startDeath ()
    {
        UnityEngine.Debug.Log("Starting Death Function");
        StartCoroutine("Death");
    }
    // this function will start the coroutine Death

    public IEnumerator Death ()
    {
        if (!canRunSpawnCode) yield break;
        else canRunSpawnCode = false;

        UnityEngine.Debug.Log("Starting Death IEnumerator");
        PlayerDeathStart.Invoke();
        Time.timeScale = 0.0f; // will appear to freeze the game
        yield return new WaitForSecondsRealtime(freezeFrameLength); // how long to freeze the game

        // death animation
        PlayerDeathAfterFreeze.Invoke();
        GameObject sparks = Instantiate(PlayerDeathSparks, Player.transform.position, Quaternion.identity);
        Destroy(Player);

        // clearing all enemies from the map
        yield return new WaitForSecondsRealtime(enemyClearDelay);
        stopAllSpawners();
        Time.timeScale = 1.0f;
        findEnemies();
        destroyEnemies();

        // respawn player
        if (lives > 0)
        {
            yield return new WaitForSeconds(respawnDelay);
            respawnPlayer();
            Rebind();
            playerInputScript.Rebind();
            PlayerRespawn.Invoke();

            yield return new WaitForSeconds(spawnerRestartDelay);
            startAllSpawners();
            EnemiesRestartRespawning.Invoke();
        }

        canRunSpawnCode = true;
    }
    // this coroutine will handel the death animation of the player

    public void findEnemies ()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    // this function will all enemies 

    public void destroyEnemies ()
    {
        foreach (GameObject Enemy in Enemies)
        {
            Enemy.GetComponent<EnemyScript>().startDeath();
        }
    }
    // this function will destroy all enemies

    public void stopAllSpawners ()
    {
        foreach (SpawnerScript spawner in EnemySpawners)
        {
            spawner.stopSpawning();
        }
    }
    // this function will stop all the enemy spawners

    public void startAllSpawners ()
    {
        foreach (SpawnerScript spawner in EnemySpawners)
        {
            spawner.startSpawning();
        }
    }
    // this function will start all the enemy spawners

    public void respawnPlayer ()
    {
        PlayerSpawner.startSpawning();
    }
    // this function will respawn the player
}
