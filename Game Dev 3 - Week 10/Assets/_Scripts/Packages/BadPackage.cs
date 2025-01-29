using GameDevWithMarco.Interfaces;
using UnityEngine;


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
