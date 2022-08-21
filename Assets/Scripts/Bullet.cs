using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        if (this.transform.position.y < -10 || this.transform.position.y > 500)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        this.gameObject.SetActive(false);
    }
}
