using GameDevWithMarco.Interfaces;
using UnityEngine;


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