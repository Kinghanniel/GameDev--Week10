using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.Managers;
using GameDevWithMarco.Interfaces;


namespace GameDevWithMarco
{
    public class GoodPackage : MonoBehaviour, ICollidable
    {
        [SerializeField] GameEvent goodPackageCollected;

        public void CollidedLogic()
        {
            goodPackageCollected.Raise();
        }
    }
}
