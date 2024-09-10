using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 4f);
    }
}
