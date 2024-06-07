using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] Text text;
    bool isStop = false;
    int _score = 0;
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText();
        if (Input.GetKeyDown("a"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * 2.5f;
        }
        if (Input.GetKeyDown("d"))
        {
            transform.position += transform.TransformDirection(Vector3.back) * 2.5f;
        }
    }

    private void FixedUpdate()
    {
        if (!isStop)
        {
            transform.position += transform.TransformDirection(Vector3.right) * 1f;
        }
        if (isStop)
        {
            _rigidBody.AddForce(Vector3.right * 0, ForceMode.Force);
            Time.timeScale = 0.0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stop"))
        {
            isStop = true;
        }
        if (other.gameObject.CompareTag("item"))
        {
            other.gameObject.SetActive(false);
            _score++;
        }
        if (other.gameObject.CompareTag("up"))
        {
            _rigidBody.AddForce(Vector3.up * 1000, ForceMode.Force);
        }
        if (other.gameObject.CompareTag("Left"))
        {
            transform.position += transform.TransformDirection(Vector3.back) * 2.5f;
        }
        if (other.gameObject.CompareTag("Right"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * 2.5f;
        }
        if (other.gameObject.CompareTag("Back"))
        {
            transform.position = new Vector3(0,1,0);
        }
    }

    public void ScoreText()
    {
        text.text = ("Score: " + _score);
    }
}