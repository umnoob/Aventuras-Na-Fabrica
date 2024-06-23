using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Pause
    public GameObject Pause;
    // Chama codigo do jogador
    public Player Jogador;
    
    void Update()
    {  
        // Verifica se e cena de jogo
        if (Jogador.CenaSeguinte == 1)
        {
            // Chama verificacao Passou de Fase 1
            PassouFase1();
        }
        // Verifica se e cena de jogo
        if (Jogador.CenaSeguinte == 2)
        {
            // Chama verificacao Passou de Fase 2
            PassouFase2();
        }
        if (Jogador.CenaSeguinte == 3)
        {
            // Chama verificacao Passou de Fase 2
            PassouFase3();
        }
    }

    public void SaiPause()
    {
        // Desativa o modo pause
        Pause.SetActive(false);
        // O jogo esta funcionando com o tempo normal
        Time.timeScale = 1;
    }

    // Transicao de cenas
    public void Exit()
    {
        //Sair do jogo
        Application.Quit();
    }
    public void Back()
    {
        // Voltar ao menu
        SceneManager.LoadScene(1);
        // O jogo esta funcionando com o tempo normal
        Time.timeScale = 1;
        // Deleta o Score salvo
        PlayerPrefs.DeleteAll();
    }
    public void Help()
    {
        // Ir para a cena controles
        SceneManager.LoadScene(2);
    }
    public void Credits()
    {
        // Ir para a cena creditos
        SceneManager.LoadScene(3);
    }
    public void Fase1()
    {
        // Ir para a cena fase 1
        SceneManager.LoadScene(4);
        // O jogo esta funcionando com o tempo normal
        Time.timeScale = 1;
    }
    public void Fase2()
    {
        // Ir para a fase 2
        SceneManager.LoadScene(5);
        // O jogo esta funcionando com o tempo normal
        Time.timeScale = 1;
    }
    public void Fase3()
    {
        // Ir para a fase 2
        SceneManager.LoadScene(6);
        // O jogo esta funcionando com o tempo normal
        Time.timeScale = 1;
    }
    void PassouFase1()
    {
        if (Jogador.Passou == true)
            {
                // Ir para a fase 2
                SceneManager.LoadScene(5);
            }
    }
    void PassouFase2()
    {
        if (Jogador.Passou2 == true)
        {
            // Ir para a fase 3
            SceneManager.LoadScene(6);
        }
    }
    void PassouFase3()
    {
        if (Jogador.Passou3 == true)
        {
            // Ir para a fase 3
            SceneManager.LoadScene(7);
        }
    }
}
