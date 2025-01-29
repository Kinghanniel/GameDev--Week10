using GameDevWithMarco.Interfaces;
using UnityEngine;


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
