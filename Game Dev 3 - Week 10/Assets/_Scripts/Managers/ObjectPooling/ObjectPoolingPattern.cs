using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.Managers;

namespace GameDevWithMarco.DesignPattern
{
    public class ObjectPoolingPattern : Singleton<ObjectPoolingPattern>
    {
        [SerializeField] PoolData goodPackagePoolData;
        [SerializeField] PoolData badPackagePoolData;
        [SerializeField] PoolData lifePackagePoolData;

        public List<GameObject> goodPool = new List<GameObject>();
        public List<GameObject> badPool = new List<GameObject>();
        public List<GameObject> lifePool = new List<GameObject>();

        public enum TypeOfPool
        {
            Good,
            Bad,
            Life
        }

        protected override void Awake()
        {
            FillThePool(goodPackagePoolData, goodPool);
            FillThePool(badPackagePoolData, badPool);
            FillThePool(lifePackagePoolData, lifePool);
        }

            
     

        private void FillThePool(PoolData poolData, List<GameObject> targetPoolContainer)
        {
            //Clears the pool
            GameObject container = CreateAContainerForThePool(poolData);
            //Goes as many time as we want the pool amount to be
            for (int i = 0; i < poolData.poolAmount; i++)
            {
                //Instantiates on item in the pool
                GameObject thingToAddToThePool = Instantiate(poolData.poolItem, container.transform);
                //Deactivates it 
                thingToAddToThePool.SetActive(false);
                //Adds it to the pool container list
                targetPoolContainer.Add(thingToAddToThePool);
            }
        }

        public GameObject CreateAContainerForThePool(PoolData poolData)
        {
            //creates a container for the pool items in ghe hierearchy
            GameObject container = new GameObject();

            //sets it as child of this
            container.transform.SetParent(this.transform);

            //sets its name
            container.name = poolData.name;

            return container;
        }

        public GameObject GetPoolItem(TypeOfPool typeOfPoolToUse)
        {
            List<GameObject> poolToUse = new List<GameObject>();

            Debug.Log("Current Spawn Rates - Bad: " + GameManager.Instance.badPackageSpawnRate +
             ", Good: " + GameManager.Instance.goodPackageSpawnRate +
             ", Life: " + GameManager.Instance.lifePackageSpawnRate);

            // Adjust spawn probability based on the difficulty level from GameManager
            if (typeOfPoolToUse == TypeOfPool.Bad)
            {
                if (Random.value < GameManager.Instance.badPackageSpawnRate)
                {
                    poolToUse = badPool;
                }
                else
                {
                    poolToUse = goodPool; // Default to good package if not bad
                }
            }
            else if (typeOfPoolToUse == TypeOfPool.Good)
            {
                if (Random.value < GameManager.Instance.goodPackageSpawnRate)
                {
                    poolToUse = goodPool;
                }
                else
                {
                    poolToUse = lifePool; // Default to life package if not good
                }
            }
            else if (typeOfPoolToUse == TypeOfPool.Life)
            {
                if (Random.value < GameManager.Instance.lifePackageSpawnRate)
                {
                    poolToUse = lifePool;
                }
                else
                {
                    poolToUse = goodPool; // Default to good package if not life
                }
            }

            // Return the first inactive object from the selected pool
            foreach (GameObject item in poolToUse)
            {
                if (!item.activeSelf)
                {
                    item.SetActive(true);
                    return item;
                }
            }

            Debug.LogWarning("No Available Items Found, Pool Too Small!");
            return null;
        }


    }
}
