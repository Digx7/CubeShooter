using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
  /* Description --
    This script should manage everything suronding the player controller
  */

  /* Notes --
    This should be flexable enough to have any controls hooked up to it
    player or ai
  */

  public int playerHealth = 10;
  [Space]
  public Rigidbody rb;
  public GameObject playerModel;
  [Space]
  public Vector2 moveDirection;
  public Vector2 lookDirection;
  public int speed;
  public int rotationSpeed;
  [Space]
  public GameObject bulletprefab;
  public GameObject firePoint;
  public float fireRate;
  private float t = 0;
  [Space]
  public GameObject playerDeathSparks;
  public GameObject scriptHolder;

  [Space]
  public UnityEvent playerSpawned;
  public UnityEvent shootEvent;
  public UnityEvent playerStartMoving;
  public UnityEvent playerStopMoving;
  public UnityEvent playerDied;
  private bool isMoving = false;

    public void Awake()
    {
        playerSpawned.Invoke();

        // if script holder is not set in inspector then will auto set
        if (scriptHolder == null)
        {
            GameObject[] _player = GameObject.FindGameObjectsWithTag("Script");

            scriptHolder = _player[0];
        }

        // adds the following listener to the playerDied Unity Event
        playerDied.AddListener(scriptHolder.GetComponent<PlayerDeathAndRespawn>().startDeath);
    }
    // This function will run at the start of the scene

    public void move (Vector2 move)
    {
        rb.velocity = new Vector3(move.x,move.y,0) * speed * Time.deltaTime;

        if (isMoving == false)
        {
          if (move.x != 0 || move.y != 0)
          {
            playerStartMoving.Invoke();
            isMoving = true;
          }
        }
        else
        {
          if (move.x == 0 && move.y == 0)
          {
            playerStopMoving.Invoke();
            isMoving = false;
          }
        }
    }
    // This function will handle moving the player based on input

    public void shoot (Vector2 direction)
  {
    Debug.Log ("The player is shooting in the " + direction + " direction");

    // rotating player
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    // shooting projectile
    if (t >= fireRate)
    {
      //shoot
      GameObject projectile = Instantiate(bulletprefab, firePoint.transform.position, transform.rotation);

      shootEvent.Invoke();

      t = 0;
    }
    else t += Time.deltaTime;
  }
    // This function will handel the player shooting based on the selected weapon

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            playerDied.Invoke();
        }
    }
    // This function will run when a collision happens

    public void FixedUpdate ()
    {
    move(moveDirection);
    if (lookDirection.x != 0 || lookDirection.y != 0) shoot(lookDirection);
    playerModel.transform.Rotate (rotationSpeed*Time.deltaTime,0,0);
    }
    // This funcion will happen on a fixed clock to keep the physics accurate
}
