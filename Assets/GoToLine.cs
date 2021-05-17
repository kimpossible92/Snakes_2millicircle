using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLine : MonoBehaviour
{
    [SerializeField] private Transform[] rts;
    private int rtsGo;
    private float tParam; public float tpar;
    private Vector2 cPosition;
    [SerializeField] LayerMask MoveOnSightLayer, MoveOnSightObstaclesLayer, DeadLayer, ButtonLayer;
    private float speedMod;
    private bool IenumeratorAllowed;
    bool mousedown = false;
    bool goToCam = false;
    protected int score;
    //public void Nil() { score = 0; }
    public int loadScore() { return score; }
    public float RetTPar() { return tpar;}//
    public void GetRTS(Transform[] rt)
    {
        rts = rt;
    }
    // Start is called before the first frame update
    void Start()
    {
        rtsGo = 0;
        tParam = 0;
        speedMod = 0.5f;
        IenumeratorAllowed = true;
    }
    public void snachala()
    {
        score = 25;
        goToCam = true;
        transform.position = rts[0].GetChild(0).transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(MMDebug.Raycast3DBoolean(transform.position, Vector3.right, 0.2f, DeadLayer, Color.blue, true)
            || MMDebug.Raycast3DBoolean(transform.position, Vector3.left, 0.2f, DeadLayer, Color.blue, true)
            || MMDebug.Raycast3DBoolean(transform.position, new Vector3(1,1,0), 0.2f, DeadLayer, Color.blue, true)
            || MMDebug.Raycast3DBoolean(transform.position, new Vector3(1,-1,0), 0.2f, DeadLayer, Color.blue, true)
            || MMDebug.Raycast3DBoolean(transform.position, new Vector3(-1,1,0), 0.2f, DeadLayer, Color.blue, true)
            || MMDebug.Raycast3DBoolean(transform.position, new Vector3(-1,-1,0), 0.2f, DeadLayer, Color.blue, true)
            || MMDebug.Raycast3DBoolean(transform.position, Vector3.down, 0.2f, DeadLayer, Color.blue, true)
            )
        {
            tParam = 0; tpar = 0; score = 25;
            rtsGo = 0;
            transform.position = rts[0].GetChild(0).transform.position;print("dead");
            cPosition = rts[0].GetChild(0).transform.position;
            //StopCoroutine(GoIEnumerator(rtsGo));
           
        }
        if (goToCam) { Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10); }
        if (Input.GetMouseButtonDown(0)&& 
            IenumeratorAllowed)
        {
            mousedown = true;
            StartCoroutine(GoIEnumerator(rtsGo));
        }
        if (Input.GetMouseButtonUp(0)) { mousedown = false; }
    }
    private IEnumerator GoIEnumerator(int rnum)
    {
        IenumeratorAllowed = false;
        Vector2 p1 = rts[rnum].GetChild(0).position;
        Vector2 p2 = rts[rnum].GetChild(1).position;
        Vector2 p3 = rts[rnum].GetChild(2).position;
        Vector2 p4 = rts[rnum].GetChild(3).position;
        while (tParam < 1&&mousedown)
        {
            tParam += Time.deltaTime * speedMod;
            tpar += Time.deltaTime * speedMod;
            cPosition = MMathf.Powed(1 - tParam, 3) * p1 + 3
                * MMathf.Powed(1 - tParam, 2) * tParam * p2 + 3
               * (1 - tParam) * MMathf.Powed(tParam, 2) * p3 +
               MMathf.Powed(tParam, 3) * p4;
            //print(cPosition);
            transform.position = cPosition;
            yield return new WaitForEndOfFrame();
        }

        if (tParam < 1) { }
        else
        {
            score += 25;
            tParam = 0;
            rtsGo += 1;
        }
        if (rtsGo > rts.Length-1)
        {
            tpar = 0;
            goToCam = false;
            Camera.main.transform.position = new Vector3(31.44f, -4.450001f, -10); rtsGo = 0;
            OpenAppLevel.THIS.loadNext();
        }
        IenumeratorAllowed = true;
    }
}
