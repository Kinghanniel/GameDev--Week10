using GameDevWithMarco.Managers;
using UnityEngine;

namespace GameDevWithMarco
{



    public class VariablesSetter : MonoBehaviour
    {
        [SerializeField] Animator transitionAnim;

        private void Awake()
        {
            //Sets the fade variable
            MyScenemanager.Instance.transitionAnim = transitionAnim;
        }
    }
}
