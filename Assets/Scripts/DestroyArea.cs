using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Tall")
        {
            return;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
