using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.Interfaces;


namespace GameDevWithMarco
{
    public class LifePackage : MonoBehaviour, ICollidable
    {
        [SerializeField] GameEvent lifePackage;

        public void CollidedLogic()
        {
            lifePackage.Raise();
        }

    }
}