using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    /* Description --
     *  This script will handel the player inputs
     */
    /* Notes --
     *  This script may need to be adjusted as I learn more about unitys input system
     */

    public PlayerScript playerScript;//references the player script
    //inputactions
    public Player Player; // is the input action map
    private GameObject _Player;

    public void Awake()
    {
        Player = new Player();

        Rebind();
    }
    // this function will run at the start of the scene

    public void Rebind()
    {
        // if player is not set in inspector then it will auto set
        if (_Player == null)
        {
            GameObject[] _player = GameObject.FindGameObjectsWithTag("Player");

            int i = 1;
            if (i == _player.Length)
            {
                _Player = _player[0];

                BindInputs();
            }
            else StartCoroutine("waitForPlayerToSpawn");
        }
    }
    // this function will bind the needed variables to the player prefab

    public void BindInputs ()
    {
        if (playerScript == null)
        {
            playerScript = _Player.GetComponent<PlayerScript>();
        }

        
        Player.Shooter.Move.performed += ctx => playerScript.moveDirection = ctx.ReadValue<Vector2>();//ties inputs to given values
        Player.Shooter.Shoot.performed += ctx => playerScript.lookDirection = ctx.ReadValue<Vector2>();// test
    }

    public IEnumerator waitForPlayerToSpawn()
    {
        int i = 1;
        GameObject[] _player;
        do
        {
            _player = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        } while (i != _player.Length);

        _Player = _player[0];

        Rebind();
        BindInputs();

        yield return null;
    }

    private void OnEnable()
    {
      Player.Enable();
    }
    // this function will enabled the inputsystem when this gameObject is enabled

    private void OnDisable()
    {
      Player.Disable();
    }
    // this function will disabled the inputsystem when this gameObject is disabled
    
}
