using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private int nbRectangles;
    [SerializeField] private GameObject rectangle;
    [SerializeField] private InputField champ;
    [SerializeField] private Button generateBtn;
    [SerializeField] private Button deleteBtn;
    [SerializeField] private RectTransform square;
    [SerializeField] private Transform parent;

    private Vector3 _startPosition;
    private Vector3 _currentPosition;
    private Vector2 _prefabSize;
    private int _lineIndex;

    public void Start()
    {
        generateBtn.onClick.AddListener(GenerateSquares);
        deleteBtn.onClick.AddListener(DeleteObjects);
    }

    private void GenerateSquares()
    {
        var isValid = int.TryParse(champ.text, out nbRectangles);
        if (!isValid)
        {
            //use in game text
            Debug.Log("Invalid input");
            return;
        }

        _prefabSize = square.rect.size;
        _startPosition = parent.position;
        _currentPosition = _startPosition;
        _lineIndex = 0;
        SpawnSquares();
    }

    private void SpawnSquares()
    {
        if (nbRectangles <= 0)
        {
            Debug.Log("Done");
            return;
        }
        
        if (nbRectangles % 2 == 0)
        {
            _currentPosition.x -= _prefabSize.x * (nbRectangles * .5f) - _prefabSize.x * .5f;
            InstantiateSquares(nbRectangles);
            ResetParents();
            SpawnSquares();
            return;
        }

        _currentPosition.x -= _prefabSize.x * Mathf.Floor(nbRectangles * .5f);
        InstantiateSquares(nbRectangles);
        
        ResetParents();
        SpawnSquares();
    }

    private void InstantiateSquares(int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            Instantiate(square, _currentPosition, Quaternion.identity, parent);
            _currentPosition.x += _prefabSize.x;
        }
    }

    private void ResetParents()
    {
        nbRectangles--;
        _lineIndex++;
        _currentPosition.x = _startPosition.x;
        _currentPosition.y += _prefabSize.y;
    }


    //----Old
    
    private void Generate()
    {
        var isValid = int.TryParse(champ.text, out nbRectangles);
        if (!isValid)
        {
            //use in game text
            Debug.Log("Invalid input");
            return;
        }
        SpawnRectangles(nbRectangles);
    }
    
    private void SpawnRectangles(int inNbRectangles )
    {
        for (int lineIndex = 0; lineIndex < inNbRectangles; lineIndex++)
        {
            for (int cubeIndex = 0; cubeIndex < inNbRectangles - lineIndex; cubeIndex++)
            {
                float x1 = cubeIndex;
                if (lineIndex > 0)
                {
                    x1 = x1 + (lineIndex / 2f);
                }
                Vector3 pos = new Vector3(x1, lineIndex, 0f);
                Instantiate(rectangle, pos, Quaternion.identity);
            }
        }
    }

    private void DeleteObjects()
    {
        foreach (Transform child in parent) 
            Destroy (child.gameObject);
    }
}
