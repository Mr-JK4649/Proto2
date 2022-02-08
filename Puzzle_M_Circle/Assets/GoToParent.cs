using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToParent : MonoBehaviour
{


    private void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.5f);
        }
    }
}
