using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlag : MonoBehaviour
{
    [SerializeField]private Transform[] Positions;
    public int level;
    //public MPows GetPositions()
    //{
    //    return Positions[0];
    //}
    public Transform[] GetM()
    {
        return Positions;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
