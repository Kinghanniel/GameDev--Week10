using UnityEngine;

namespace GameDevWithMarco
{
    public class ParticleSystemAutoDistructor : MonoBehaviour
    {
        private ParticleSystem system;


        // Update is called once per frame
        void Update()
        {
            if (system == null)
            {
                system = GetComponent<ParticleSystem>();
            }

            if (system != null && !system.IsAlive(true))
            {
                Destroy(gameObject);
            }
        }
    }
}
