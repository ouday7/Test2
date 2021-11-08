using System;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]  int nbRectangles;
    [SerializeField]  GameObject rectangle;
    [SerializeField]  InputField champ;
    [SerializeField] Button generateBtn;
    [SerializeField] Button deleteBtn;
    
    void SpawningRectangles(int nbRectangles )
    {
        for (int y = 0; y < nbRectangles; y++)
        {
            for (int x = 0; x < nbRectangles - y; x++)
            {
                float x1 = x;
                if (y > 0)
                {
                    x1 = x1 + (y / 2f);
                }
                Vector3 pos = new Vector3(x1, y, 0f);
                Instantiate(rectangle, pos, Quaternion.identity);
            }
        }
    }
    public void Start()
    {
        generateBtn.onClick.AddListener(Generate);
        deleteBtn.onClick.AddListener(DeeleteObject);
    }
    void Generate()
    {
        nbRectangles= Int32.Parse(champ.text);
        SpawningRectangles(nbRectangles);
        
    }
    void DeeleteObject()
     {
         var destroyObject = GameObject.FindGameObjectsWithTag("cube");
         foreach (GameObject oneObject in destroyObject) 
            Destroy (oneObject);
     }
}
