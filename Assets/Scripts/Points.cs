using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Points : MonoBehaviour
{
    [System.Serializable]
    private class WayPoints
    {
        public Vector3[] points;
    }
    
    [SerializeField] private float moveDuration;
    [SerializeField] private TextAsset textJson;
    [SerializeField] private WayPoints pointList = new WayPoints();
    private int _counter;
    private WaitForSeconds _delay;

    private void Start()
    {
        pointList = JsonUtility.FromJson<WayPoints>(textJson.text);
        StartCoroutine(Movement());
        _delay = new WaitForSeconds(2);
    }
    private IEnumerator Movement()
    {
        if (_counter == pointList.points.Length)
        {
            _counter = 0;
            Debug.Log("work !");
        }

        gameObject.transform.DOMove(pointList.points[_counter], moveDuration);

        yield return _delay;
        
        _counter++;
        StartCoroutine(Movement());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var startPoint = pointList.points[pointList.points.Length - 1];

        foreach (var point in pointList.points)
        {
            Gizmos.DrawSphere(point,.25f);
            Gizmos.DrawLine(startPoint, point);
            startPoint = point;
        }
    }
}