using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 8.0f;

    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed *Time.deltaTime);
        if (transform.position.y > 7)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
