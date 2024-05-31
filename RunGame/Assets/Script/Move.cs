using System;
using System.Collections;
using System.Collections.Generic;
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
        if (!isStop)
        {
            _rigidBody.AddForce(Vector3.right * 1000 * Time.deltaTime, ForceMode.Force);
        }
        if (isStop)
        {
            _rigidBody.AddForce(Vector3.right * 0, ForceMode.Force);
            Time.timeScale = 0.0f;
        }
        if (Input.GetKeyDown("a"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * 1.7f;
        }
        if (Input.GetKeyDown("d"))
        {
            transform.position += transform.TransformDirection(Vector3.back) * 1.7f;
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
    }

    public void ScoreText()
    {
        text.text = ("Score: " + _score);
    }
}