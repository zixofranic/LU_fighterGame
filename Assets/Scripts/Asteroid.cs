using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 3.0f;
    [SerializeField] GameObject _asteroidExplosion;
    SpawnManager _astroidSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        _astroidSpawner = GameObject.Find("Spawner").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime*_rotationSpeed) ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Instantiate(_asteroidExplosion, transform.position,Quaternion.identity);
            _astroidSpawner.StartSpawning();
            Destroy(gameObject);
            
        }
    }
}
