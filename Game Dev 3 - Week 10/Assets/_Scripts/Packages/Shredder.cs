﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevWithMarco
{


    public class scp_Shredder : MonoBehaviour
    {
        /// <summary>
        /// Just destroys anything that enters in the trigger
        /// </summary>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.SetActive(false);
        }
    }
}