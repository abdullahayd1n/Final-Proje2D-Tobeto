using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
   [System.Serializable]
    public class Sound 
    {
        public string name;
        public AudioClip clip;
    }
}
