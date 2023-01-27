using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _enemySpeed = 4.0f;
    private Player player;
    Animator _ennymyAnimation;
    AnimatorClipInfo AnimatorClipInfo;
    [SerializeField] AnimationClip ennemyDead;
    BoxCollider2D _disableCol;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("Player does not exist");
        }
        _ennymyAnimation = GetComponent<Animator>();
        if (_ennymyAnimation == null)
        {
            Debug.Log("Animator does not exist");
        }
        _disableCol = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer enemyRenderer = GetComponent<SpriteRenderer>();
        if (other.tag == "Player")
        {
            
            float timeofAnim = ennemyDead.length;
            _ennymyAnimation.SetTrigger("IsEnnemyDestroyed");
            _disableCol.enabled=false;
            StartCoroutine(SlowDown());
            Destroy(gameObject, timeofAnim);
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.PlayerDamage();
            }

        }
        else if (other.tag == "Bullet")
        {
            if (player != null)
            {
                player.AddToScore();
            }
            
            Destroy(other.gameObject);
            float timeofAnim = ennemyDead.length;
            _ennymyAnimation.SetTrigger("IsEnnemyDestroyed");
            _disableCol.enabled = false;
            StartCoroutine(SlowDown());
            Destroy(gameObject, timeofAnim);
            
        }

    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        if (transform.position.y < -5.5)
        {
            float xEnemyPosition = Random.Range(-11.8f, 11.8f);
            transform.position = new Vector3(xEnemyPosition, 8.05f, 0);
        }
    }
    IEnumerator SlowDown()
    {
        while (_enemySpeed > 0)
        {
            _enemySpeed--;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
