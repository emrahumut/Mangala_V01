using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTrigger : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.transform.parent.parent.GetComponent<PitController>().OnMouseDown();
    }
}
