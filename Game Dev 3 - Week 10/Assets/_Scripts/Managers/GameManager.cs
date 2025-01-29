using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevWithMarco.Managers
{

    public class GameManager : Singleton<GameManager>
    {

        public int score;
        public float timeLeft = 60f;
        public int[] packageValues = new int[] { 12345, -5434 };
        public int successRate;
        public int lives = 5;
        public float playTime = 0;
        public float badPackageSpawnRate = 0.2f; // Default spawn rate for bad packages, 0.2  20% chance
        public float goodPackageSpawnRate = 0.6f; // Default spawn rate for good packages
        public float lifePackageSpawnRate = 0.05f; // Default spawn rate for life packages



        [SerializeField] GameEvent restartGame;
        [SerializeField] GameEvent gameOver;




        protected override void Awake()
        {
            base.Awake();
            Initialisation();
        }

        // Update is called once per frame
        void Update()
        {
            ValuesClamping();
            TimeGoingDown();
            if (SceneManager.GetActiveScene().name == ("scn_Level1"))
            {
                StartCounting();
            }
            else if (SceneManager.GetActiveScene().name == ("scn_GameOver") && Input.anyKeyDown)
            {
                restartGame.Raise();
            }
        }



        private void Initialisation()
        {
            playTime = 0f;
            score = 0;
            successRate = 0;
        }

        private void TimeGoingDown()
        {
            if (SceneManager.GetActiveScene().name == ("scn_MainMenu"))
            {
                return;
            }
            else if (SceneManager.GetActiveScene().name == ("scn_GameOver"))
            {
                return;
            }
            else
            {
                timeLeft -= Time.deltaTime;
            }

        }

        private void ValuesClamping()
        {
            score = Mathf.Clamp(score, 0, 100000000);
            successRate = Mathf.Clamp(successRate, 0, 100);
            timeLeft = Mathf.Clamp(timeLeft, 0, 120);
            lives = Mathf.Clamp(lives, 0, 10);
        }

        private void StartCounting()
        {
            playTime += Time.deltaTime;
        }

        public void GreenPackLogic()
        {
            score += packageValues[0];
            successRate++;
            AdjustDifficulty(); // Update difficulty when good package is picked up

        }
        public void RedPackLogic()
        {
            score += Mathf.RoundToInt(packageValues[1]);
            successRate -= 3;
            lives--;
            AdjustDifficulty(); // Update difficulty when bad package is picked up

            if (lives <= 0)
            {
                gameOver.Raise();
            }
        }

        public void AdjustDifficulty()
        {
            if (score >= 10000 && successRate > 15 && lives > 3)
            {
                // Increase bad packages, decrease good and life packages
                badPackageSpawnRate = Mathf.Min(0.9f, badPackageSpawnRate + 0.05f); // Cap at 90%
                goodPackageSpawnRate = Mathf.Max(0.01f, goodPackageSpawnRate - 0.05f); // Reduce good packages, make sure it doesnt go below 1% and decrease it by 0.05(5%) each time this happens
                lifePackageSpawnRate = Mathf.Max(0.02f, lifePackageSpawnRate - 0.05f); // Keep life package spawn rate low

                Debug.Log("Increased bad packages, decreased good and life packages");
            }
            else
            {
                // Reset spawn rates to default
                badPackageSpawnRate = 0.2f;
                goodPackageSpawnRate = 0.6f;
                lifePackageSpawnRate = 0.05f;

                Debug.Log("Reset spawn rates to defaults");
            }
        }




        public void LifePackLogic()
        {
            lives++;
            AdjustDifficulty(); // Update difficulty when life package is picked up

        }



        public void RestartGame()
        {
            score = 0;
            successRate = 0;
            lives = 5;
            playTime = 0f;
        }
    }
}
