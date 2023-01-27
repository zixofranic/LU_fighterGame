using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] float _tripleShootSpeed = 3.0f;
    [SerializeField] int powerUpId; 
    [SerializeField] GameObject[] _boosterId;

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _tripleShootSpeed);
        if(transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if(player != null)
            {
                switch (powerUpId)
                {
                    case 0: player.TripleBoosted();
                        break;
                    case 1: player.speedOn();
                        break;
                    case 3: player.ShieldActive();
                        break;
                    default: Debug.Log("Default");
                        break;
                }

            }
            Destroy(this.gameObject);
        }
    }
}
