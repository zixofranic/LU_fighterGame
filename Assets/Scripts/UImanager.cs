using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{
    [SerializeField] TMP_Text _textScore;
    [SerializeField] Sprite[] _livesSprite;
    [SerializeField] Image img_sprite;
    [SerializeField] TMP_Text _gameOverText;
    [SerializeField] TMP_Text _RestartLevelText;
    GameManager gameManager;
    void Start()
    {
        _textScore.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _RestartLevelText.gameObject.SetActive(false);
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();  
    }

    public void addPlayerScore(int _playerScore)
    {
        _textScore.text = "Score: " + _playerScore;
    }

    public void UpdateLives(int _numberOfLives)
    {
        img_sprite.sprite = _livesSprite[_numberOfLives];
        if(_numberOfLives == 0)
        {
            EndOfGameManagement();
        }
    }
    void EndOfGameManagement()
    {
        StartCoroutine(FlickerText());
        _RestartLevelText.gameObject.SetActive(true);
        gameManager.IsDead();
    }
    
    IEnumerator FlickerText()
    {
        while (true) { 
        _gameOverText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        }
    }
}
