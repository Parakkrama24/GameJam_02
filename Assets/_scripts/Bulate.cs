using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulate : MonoBehaviour
{
    [SerializeField] private GameObject explotion;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Instantiate(explotion,transform.position,Quaternion.identity);
    }
}
