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
        SceneManager.LoadScene("MenuPrincipal");
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
}