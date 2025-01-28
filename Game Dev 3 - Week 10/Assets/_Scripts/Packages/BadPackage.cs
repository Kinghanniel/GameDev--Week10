using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.Interfaces;


namespace GameDevWithMarco
{
    public class BadPackage : MonoBehaviour, ICollidable
    {
        [SerializeField] GameEvent badPackageCollted;

        public void CollidedLogic()
        {
            badPackageCollted.Raise();
        }
    }
}
