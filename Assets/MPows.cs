using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPows : MonoBehaviour
{
    [SerializeField] private Transform[] cpoints;
    private Vector2 gposition;
    [SerializeField] Mesh newmesh;
    public Transform GetCpoints(int i)
    {
        return cpoints[i];
    }
    private void OnDrawGizmos()
    {
        for(float t=0; t <= 1; t += 0.05f)
        {
            gposition = MMathf.Powed(1 - t, 3) * cpoints[0].position + 3 * MMathf.Powed(1 - t, 2) * t * cpoints[1].position +
                3 * (1 - t) * MMathf.Powed(t, 2) * cpoints[2].position + MMathf.Powed(t, 3) * cpoints[3].position;
            Gizmos.DrawCube(gposition, new Vector3(0.25f,0.25f,0.25f));
            //Gizmos.
            Gizmos.color = Color.green;
        }
        Gizmos.DrawLine(new Vector2(cpoints[0].position.x, cpoints[0].position.y), new Vector2(cpoints[1].position.x, cpoints[1].position.y));
        Gizmos.DrawLine(new Vector2(cpoints[2].position.x, cpoints[2].position.y), new Vector2(cpoints[3].position.x, cpoints[3].position.y));
    }
}
