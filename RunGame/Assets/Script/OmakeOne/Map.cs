using UnityEngine;

public enum Status //�}�X�̎��
{
    Start,
    Goal,
    Wall,
    Fllor
}
public class Map : MonoBehaviour
{
    [SerializeField] int _width = 15; //��
    [SerializeField] int _height = 5; //����
    [SerializeField] GameObject _floorPrefab; //��
    [SerializeField] GameObject _wallPrefab;�@//��
    [SerializeField] GameObject _startPrefab;�@//�X�^�[�g
    [SerializeField] GameObject _goalPrefab; //�S�[��
    [SerializeField] Vector2Int _start; //�X�^�[�g�n�_
    [SerializeField] Vector2Int _goal; //�S�[���n�_

    private Status[,] map;
    private GameObject[,] mapObjects;
    private Astar pathfinder;

    void Start()
    {
        GenerateMap();
        pathfinder = new Astar();
        pathfinder.Initialize(map);
        HighlightPath();
    }

    /// <summary>
    /// �}�b�v�𐶐�����
    /// </summary>
    private void GenerateMap()
    {
        map = new Status[_width, _height];
        mapObjects = new GameObject[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector2 position = new(x, y);
                GameObject obj;

                if (new Vector2Int(x, y) == _start)
                {
                    obj = Instantiate(_startPrefab, position, Quaternion.identity);
                    map[x, y] = Status.Start;// �X�^�[�g�n�_
                }
                else if (new Vector2Int(x, y) == _goal)
                {
                    obj = Instantiate(_goalPrefab, position, Quaternion.identity);
                    map[x, y] = Status.Goal; // �S�[���n�_
                }
                else if (Random.value < 0.1) //10%�ŕǂ𐶐�
                {
                    map[x, y] = Status.Wall;//��
                    obj = Instantiate(_wallPrefab, position, Quaternion.identity);
                }
                else
                {
                    map[x, y] = Status.Fllor; // ��
                    obj = Instantiate(_floorPrefab, position, Quaternion.identity);
                }

                mapObjects[x, y] = obj;
            }
        }
    }

    private void HighlightPath()
    {
        var path = pathfinder.FindPath(_start, _goal);
        foreach (var position in path)
        {
            mapObjects[position.x, position.y].GetComponent<Renderer>().material.color = Color.green;
        }
    }
}