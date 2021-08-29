using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace game_ideas
{

    [Serializable]
    public class PoolingListData
    {
        public string poolingName; // reference for finding object pooled list
        public GameObject poolObject; // reference, just in case if don't have any available objects to be pooled
        public List<GameObject> objectPooled; // prefab object pooled list
    }

    public class PoolingManager : MonoBehaviour
    {

        #region initialize
        private static PoolingManager instance;

        public static PoolingManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = null;
            }
        }
        #endregion

        [Header("Player Armament")]
        public List<ObjectPooledData> objectPooledPlayerArm;
        private List<PoolingListData> poolingListPlayerArm = new List<PoolingListData>();

        [Header("Enemy Armament")]
        public List<ObjectPooledData> objectPooledEnemyArm;
        private List<PoolingListData> poolingListEnemyArm = new List<PoolingListData>();

        [Header("Muzzle Flash")]
        public List<ObjectPooledData> objectPooledMuzzleFlash;
        private List<PoolingListData> poolingListMuzzleFlash = new List<PoolingListData>();

        [Header("Explosion")]
        public List<ObjectPooledData> objectPooledExplosion;
        private List<PoolingListData> poolingListExplosion = new List<PoolingListData>();

        [Header("Effects")]
        public List<ObjectPooledData> objectPooledEffects;
        private List<PoolingListData> poolingListEffects = new List<PoolingListData>();

        [Header("Popup Text")]
        public List<ObjectPooledData> objectPooledPopupText;
        private List<PoolingListData> poolingListPopupText = new List<PoolingListData>();

        [Header("Environment")]
        public List<ObjectPooledData> objectPooledEnvironment;
        private List<PoolingListData> poolingListEnvironment = new List<PoolingListData>();

        private void Start()
        {
            InstantiateObjectPools(objectPooledPlayerArm, poolingListPlayerArm);
            InstantiateObjectPools(objectPooledEnemyArm, poolingListEnemyArm);
            InstantiateObjectPools(objectPooledMuzzleFlash, poolingListMuzzleFlash);
            InstantiateObjectPools(objectPooledExplosion, poolingListExplosion);
            InstantiateObjectPools(objectPooledEffects, poolingListEffects);
            InstantiateObjectPools(objectPooledPopupText, poolingListPopupText);
            InstantiateObjectPools(objectPooledEnvironment, poolingListEnvironment);
        }
        
        private void InstantiateObjectPools(List<ObjectPooledData> objectPoolData, List<PoolingListData> poolingListData)
        {
            foreach (ObjectPooledData opd in objectPoolData)
            {
                List<GameObject> objectPooledList = new List<GameObject>();

                for (int x = 0; x < opd.objectStartingSize; x++)
                {
                    GameObject newObj = Instantiate(opd.objectPrefab) as GameObject;
                    newObj.SetActive(opd.objectOnEnabled);
                    objectPooledList.Add(newObj);
                }

                PoolingListData listPooled = new PoolingListData
                {
                    poolingName = opd.objectPooledName,
                    poolObject = opd.objectPrefab,
                    objectPooled = objectPooledList
                };

                poolingListData.Add(listPooled);
            }
        }

        private GameObject GetPooledObjects(List<PoolingListData> poolingListData, string poolName)
        {
            foreach (PoolingListData pld in poolingListData)
            {
                // find object by pool name
                if (pld.poolingName == poolName)
                {
                    for (int x = 0; x < pld.objectPooled.Count; x++)
                    {
                        // check if object is available
                        if (!pld.objectPooled[x].activeInHierarchy)
                        {
                            // if object is available, it will use
                            return pld.objectPooled[x];
                        }
                    }

                    // if there's no current usable object to be pool, then
                    // create new object to be pool and return the crated one
                    GameObject newObj = Instantiate(pld.poolObject) as GameObject;
                    pld.objectPooled.Add(newObj);
                    return newObj;
                }
            }

#if UNITY_EDITOR
            Debug.LogError("Pooled object doesn't exist");
#endif 
            return null;
        }


        // get pooled objects 
        public GameObject GetPooledObjectsPlayerArm(string poolName)
        {
            return GetPooledObjects(poolingListPlayerArm, poolName);
        }

        public GameObject GetPooledObjectEnemyArm(string poolName)
        {
            return GetPooledObjects(poolingListEnemyArm, poolName);
        }

        public GameObject GetPooledObjectMuzzleFlash(string poolName)
        {
            return GetPooledObjects(poolingListMuzzleFlash, poolName);
        }

        public GameObject GetPooledObjectExplosion(string poolName)
        {
            return GetPooledObjects(poolingListExplosion, poolName);
        }

        public GameObject GetPooledObjectEffects(string poolName)
        {
            return GetPooledObjects(poolingListEffects, poolName);
        }

        public GameObject GetPooledObjectPopupText(string poolName)
        {
            return GetPooledObjects(poolingListPopupText, poolName);
        }

        public GameObject GetPooledObjectEnvironment(string poolName)
        {
            return GetPooledObjects(poolingListEnvironment, poolName);
        }


    }
}