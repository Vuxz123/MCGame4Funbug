using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.ethnicthv.Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public GameObject gameOverPanel;
        public GameObject gameWinPanel;
        
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI timer;
        
        public int unlockLevel = 1;
        
        public int maxTime = 60;
        public int maxScore = 10;

        private int _eaten = 0;
        private int _time = 0;

        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            scoreText.text = "0";
            timer.text = "0s";
            
            _time = maxTime;
            
            StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                _time--;
                timer.text = _time + "s";
                if (_time <= 0 && _eaten < maxScore)
                {
                    GameOver();
                }
            }
        }

        public void Eat()
        {
            _eaten++;
            scoreText.text = _eaten.ToString() + "/" + maxScore;
            if (_eaten >= maxScore && _time >= 0)
            {
                GameWin();
            }
        }
        
        public void GameOver()
        {
            Debug.Log("Game Over");
            StopAllCoroutines();
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }
        
        public void GameWin()
        {
            Debug.Log("Game Win");
            StopAllCoroutines();
            Time.timeScale = 0f;
            gameWinPanel.SetActive(true);
            PlayerPrefs.SetInt("Level" + unlockLevel, 1);
        }

        public void Back()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }
}
