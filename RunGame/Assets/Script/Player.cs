using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject FinishPanel;
    [SerializeField] GameObject SecondManager;
    [SerializeField] GameObject ThirdManager;
    [SerializeField] GameObject FourthManager;
    [SerializeField] GameObject FifthManager;
    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] AudioClip _jump;
    [SerializeField] AudioClip _item;
    [SerializeField] AudioClip _move;
    AudioSource audioSource;
    [SerializeField] Text text;
    bool isStop = true;
    int _score = 0;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText();
        if (Input.GetKeyDown("a") && !isStop)
        {
            audioSource.PlayOneShot(_move);
            transform.position += transform.TransformDirection(Vector3.forward) * 2.5f;
        }
        if (Input.GetKeyDown("d")&& !isStop)
        {
            audioSource.PlayOneShot(_move);
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
            FinishPanel.SetActive(true);
            text.transform.position = new Vector3(900, 500, 0);
            text.fontSize = 100;
        }
        if (other.gameObject.CompareTag("item"))
        {
            audioSource.PlayOneShot(_item);
            other.gameObject.SetActive(false);
            _score++;
        }
        if (other.gameObject.CompareTag("up"))
        {
            audioSource.PlayOneShot(_jump);
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
            other.gameObject.SetActive(false);
        }
    }

    public void ScoreText()
    {
        text.text = ("Score: " + _score);
    }

    public void NotMove()
    {
        isStop = false;
    }
}