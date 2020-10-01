using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSparks : MonoBehaviour
{
   public void Awake()
    {
        Destroy(gameObject, 1.0f);
    }
}
