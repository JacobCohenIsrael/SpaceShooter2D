using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DestroyExplosion();
    }

    private void DestroyExplosion()
    {
        Destroy(this.gameObject, 2.5f);
    }
}
