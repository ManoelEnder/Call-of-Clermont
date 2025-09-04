    using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("Jogo encerrado.");
    }
    public void Jogo1()
    {
        SceneManager.LoadScene("Jogo1");
    }
    public void Introducao()
    {
        SceneManager.LoadScene("Introdução");
    }
    public void VoltarJogo()
    {
        SceneManager.LoadScene("Jogo1");
    }
    public void Jogo3()
    {
        SceneManager.LoadScene("Jogo3");
    }
    public void CenaFinal()
    {
        SceneManager.LoadScene("CenaFinal");
    }
    public void Jogo4()
    {
        SceneManager.LoadScene("Jogo4");
    }
}