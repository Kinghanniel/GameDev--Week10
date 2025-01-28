using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.Managers;
using GameDevWithMarco.DesignPattern;

namespace GameDevWithMarco
{


    public class FallenObjectsSpawner : MonoBehaviour
    {

        [Header("Packages Spawn Position")]
        //Referencing Gameobjects
        public GameObject[] spawners;

        [Header("Packages Delay Variables")]
        [SerializeField] float intitialDelay = 2.0f;
        [SerializeField] float minDelay = 0.5f;
        [SerializeField] float delayIncreaseData = 0.1f;
        float currentDelay;

        [Header("Packages Drop Chance Percentage")]
        [SerializeField] float goodPackageDropPercentage = 70f;
        [SerializeField] float badPackageDropPercentage = 25f;
        [SerializeField] float lifePackageDropPercentage = 5f;
        [SerializeField] float minimum_GoodPackageDropPercentage;
        [SerializeField] float maximum_badPackageDropPercentage;
        [SerializeField] float percentageChangeRatio = 0.1f;





        void Start()
        {
            StartCoroutine(SpawningLoop());
        }

   

        private void SapwnPackageAtRaondomLocation(ObjectPoolingPattern.TypeOfPool poolType)
        {
            //gets one item from the pool
            GameObject spawnedPackage = ObjectPoolingPattern.Instance.GetPoolItem(poolType);
            //random integer to use for the position
            int randomInteger = Random.Range(0, spawners.Length - 1);
            //get random range
            Vector2 spawnPosition = spawners[randomInteger].transform.position;
            //movess it to the right place
            spawnedPackage.transform.position = spawnPosition;
        }


        IEnumerator SpawningLoop()
        {
            // Adjust spawn rate based on GameManager values
            goodPackageDropPercentage = GameManager.Instance.goodPackageSpawnRate * 100;
            badPackageDropPercentage = GameManager.Instance.badPackageSpawnRate * 100;
            lifePackageDropPercentage = GameManager.Instance.lifePackageSpawnRate * 100;

            // Determine the package type based on updated spawn rates
            ObjectPoolingPattern.TypeOfPool packageType = GetPackageTypeBaseOnPercentage();

            // Spawn the package
            SapwnPackageAtRaondomLocation(packageType);

            // Wait for the delay before spawning again
            yield return new WaitForSeconds(currentDelay);

            // Gradually reduce the delay to increase difficulty
            currentDelay -= delayIncreaseData;
            if (currentDelay < minDelay)
            {
                currentDelay = minDelay; // Cap the minimum delay
            }

            // Continue the loop
            StartCoroutine(SpawningLoop());
        }

        private ObjectPoolingPattern.TypeOfPool GetPackageTypeBaseOnPercentage()
        {
            float randomValue = Random.Range(0f, 100.1f);

            if (randomValue <= goodPackageDropPercentage)
            {
                return ObjectPoolingPattern.TypeOfPool.Good;
            }
            else if (randomValue > goodPackageDropPercentage && randomValue <= (goodPackageDropPercentage + badPackageDropPercentage))
            {
                return ObjectPoolingPattern.TypeOfPool.Bad;
            }
            else
            {
                return ObjectPoolingPattern.TypeOfPool.Life;
            }
        }


        private void CapThePercentages()
        {
            if (goodPackageDropPercentage <= minimum_GoodPackageDropPercentage &&
                badPackageDropPercentage >= maximum_badPackageDropPercentage)
            {
                goodPackageDropPercentage = minimum_GoodPackageDropPercentage;
                badPackageDropPercentage = maximum_badPackageDropPercentage;
            
            }
        }

        public void GrowBadPercentage()
        {
            //starts to shift the percentage towards the bad packages
            goodPackageDropPercentage -= percentageChangeRatio;
            badPackageDropPercentage += percentageChangeRatio;
            CapThePercentages();
        }

        public void GrowGoodPerncentage()
        {
            //starts to shift the percentage towards the bad packages
            goodPackageDropPercentage += percentageChangeRatio;
            badPackageDropPercentage -= percentageChangeRatio;
            CapThePercentages();
        }
    }
}

