using UnityEngine;

public class ItemScale : MonoBehaviour
{
    [SerializeField] int _count;
    [SerializeField] float _index = -45.5f;
    [SerializeField] float _z = 0;
    [SerializeField] float _y = 0;
    // Start is called before the first frame update
    void Start()
    {
        _z = this.transform.position.z;
        _y = this.transform.position.y;
        for (var i = 0; i < _count; i++)
        {
            var obj = (GameObject)Resources.Load("ItemScale");
            var collider = obj.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            Instantiate(obj, new Vector3(_index, _y, _z), Quaternion.identity);
            obj.tag = "item";
            _index += 50;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}