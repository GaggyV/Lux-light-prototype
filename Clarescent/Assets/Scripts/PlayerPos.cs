using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private ReSpawner rs;
    public GameObject ting;

    void Start()
    {
        rs = GameObject.FindGameObjectWithTag("ReSpawner").GetComponent<ReSpawner>();
        transform.position = rs.lastCheckpointPos;
        if (rs.hasTing)
        {
            ting.SetActive(true);
            ting.transform.position = transform.position;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
