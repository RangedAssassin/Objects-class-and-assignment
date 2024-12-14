using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInSpace : MonoBehaviour
{
    [SerializeField] private float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        //Draw all the time 
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnDrawGizmosSelected()
    {
        //draw only when selected

    }
}
