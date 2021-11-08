using DG.Tweening;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private TextAsset textJson;
    private int _j = 0;
    public float x;

    [System.Serializable]
    public class Waypoints
    {
        public float x;
        public float y;
        public float z;
    }

    [System.Serializable]
    public class Xwaypoints
    {
        public Waypoints[] ppoints;
    }

    public Xwaypoints pointList = new Xwaypoints();

    public void Start()
    {
        pointList = JsonUtility.FromJson<Xwaypoints>(textJson.text);
       Movement();

    }
    void Movement()
    {
        if (_j == pointList.ppoints.Length)
        {
            _j = 0;
            Debug.Log("work !");
        }

        gameObject.transform.DOMove(new Vector3(pointList.ppoints[_j].x, pointList.ppoints[_j].y,pointList.ppoints[_j].z),x).OnComplete(() => 
        {
            System.Threading.Thread.Sleep(100);
            _j++;
            Movement();
        });
    }
    
}


