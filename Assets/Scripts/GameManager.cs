using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject[] enemies;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject EndScreen;
    [SerializeField] GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        killEnemies();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");

    }

    private void killEnemies() {
        int enemigos = enemies.Length;
        if (enemigos == 0)
        {
            EndScreen.SetActive(true);
            Destroy(player);
            text.text = "";

        }
        else {
            text.text = "Enemigos restantes: " + enemigos;
        }

    }

}
