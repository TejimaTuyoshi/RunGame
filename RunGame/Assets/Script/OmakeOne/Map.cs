using UnityEngine;

public enum Status //マスの種類
{
    Start,
    Goal,
    Wall,
    Fllor
}
public class Map : MonoBehaviour
{
    [SerializeField] int _width = 15; //幅
    [SerializeField] int _height = 5; //高さ
    [SerializeField] GameObject _floorPrefab; //床
    [SerializeField] GameObject _wallPrefab;　//壁
    [SerializeField] GameObject _startPrefab;　//スタート
    [SerializeField] GameObject _goalPrefab; //ゴール
    [SerializeField] Vector2Int _start; //スタート地点
    [SerializeField] Vector2Int _goal; //ゴール地点

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
    /// マップを生成する
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
                    map[x, y] = Status.Start;// スタート地点
                }
                else if (new Vector2Int(x, y) == _goal)
                {
                    obj = Instantiate(_goalPrefab, position, Quaternion.identity);
                    map[x, y] = Status.Goal; // ゴール地点
                }
                else if (Random.value < 0.1) //10%で壁を生成
                {
                    map[x, y] = Status.Wall;//壁
                    obj = Instantiate(_wallPrefab, position, Quaternion.identity);
                }
                else
                {
                    map[x, y] = Status.Fllor; // 床
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