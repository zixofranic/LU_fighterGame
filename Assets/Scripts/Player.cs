using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 5f;
    [SerializeField] float _speedMultiplier = 3f;
    [SerializeField] GameObject bulletObject;
    [SerializeField] Transform gun;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] GameObject tripleShot;
    [SerializeField] float _boosterTime;
    [SerializeField] int _playerLife = 3;
    [SerializeField] GameObject shieldVisualier;
    [SerializeField] GameObject _thrustels;
    [SerializeField] int _playerScore = 0;
    [SerializeField] GameObject _leftHurt , _rightHurt;
    [SerializeField] AudioSource audioSaurce;
    [SerializeField] AudioClip audioclip;

    float canFire = 0;
    SpawnManager _spawnManager;
    bool isTripleShot = false;
    bool isShieldActive = false;
    UImanager _uiManager;
    bool _isMoving;

    void Start()
    {
        audioSaurce = GetComponent<AudioSource>();
        if(audioSaurce == null)
        {
            Debug.Log("th player audio saurce is null");
        }
        else
        {
            audioSaurce.clip = audioclip;
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
        _spawnManager = GameObject.Find("Spawner").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.Log("Script does not exist");
        }
        if (_uiManager == null)
        {
            Debug.Log("UI manager is null!");
        }
    }

    void Update()
    {
        CheckMovement();
        if (_isMoving) { 
        PlayerInput();
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            fireLaser();
        }

    }
    void CheckMovement()
    {
        if (!Input.anyKey)
        {
            _isMoving = false;
            _thrustels.SetActive(false);
        }
        else
        {
            _isMoving = true;
            _thrustels.SetActive(true);
        }
    }
    void PlayerInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticallInput, 0);
        transform.Translate(direction * _playerSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.0f, 5.0f), 0);
        if (transform.position.x > 12f || transform.position.x <-12)
        {
            transform.position = new Vector3(-12f * horizontalInput, transform.position.y, 0);
        }
    }
    void fireLaser()
    {
        canFire = Time.time + fireRate;

        if (isTripleShot)
        {
            Instantiate(tripleShot, gun.position, Quaternion.identity);

        }else
        {
            Instantiate(bulletObject, gun.position, Quaternion.identity);
        }
        audioSaurce.Play();
        
    }
    public void PlayerDamage()
    {
         if(isShieldActive== true)
        {
            shieldVisualier.SetActive(false);
            isShieldActive = false;
            return;
        }
        else {
         _playerLife--;
         if(_playerLife ==2){
            _leftHurt.SetActive(true);
         }else if (_playerLife==1){
           _rightHurt.SetActive(true);

         }
         
        _uiManager.UpdateLives(_playerLife);
        if(_playerLife < 1)
        {
            _spawnManager.onPlayDeath();
            Destroy(gameObject);
        }
        }
    }
    public void TripleBoosted()
    {
        isTripleShot = true;
        StartCoroutine(tripleShotCooldown());
    }
       IEnumerator tripleShotCooldown()
    {
        yield return new WaitForSeconds(_boosterTime);
        isTripleShot = false;
    }

    public void speedOn()
    {
        _playerSpeed *= _speedMultiplier;
        StartCoroutine(speedBoosterCooldown());
    }

    IEnumerator speedBoosterCooldown()
    {
        yield return new WaitForSeconds(_boosterTime);
        _playerSpeed /= _speedMultiplier;
        
    }
    public void ShieldActive()
    {
        isShieldActive = true;
        shieldVisualier.SetActive(true);
    }
    public void AddToScore()
    {
        _playerScore += 10;
        _uiManager.addPlayerScore(_playerScore);
    }

}
