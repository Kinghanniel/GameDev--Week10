using UnityEngine;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Player
{

    public class Player_Collision : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            ExecuteLogicBaseOnWhatWeHaveCollidedWith(collision);
            collision.gameObject.SetActive(false);
        }

        private void ExecuteLogicBaseOnWhatWeHaveCollidedWith(Collider2D collision)
        {
            ICollidable collidable = collision.GetComponent<ICollidable>();
            if (collidable != null)
            {
                collidable.CollidedLogic();
            }
        }
    }
}
