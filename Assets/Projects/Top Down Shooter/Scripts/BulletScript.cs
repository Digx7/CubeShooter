using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
  /* Description --
   *    This script will control any bullet
   */

  public GameObject bulletModel;
  public Rigidbody rb;
  public GameObject bulletSparksPreFab;
  [Space]
  public int rotationSpeed;
  public int bulletSpeed;

  void FixedUpdate()
  {
    bulletModel.transform.Rotate (rotationSpeed*Time.deltaTime,0,0);
    rb.velocity = transform.right * bulletSpeed;
  }
    // this function will handel anything physics related

  void OnCollisionEnter(Collision collision)
  {
    GameObject sparks = Instantiate(bulletSparksPreFab, gameObject.transform.position, Quaternion.identity); 
   
    Destroy(gameObject);
  }
    // this function is called anytime a collistion happens
}
