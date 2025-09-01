using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ForcaGame : MonoBehaviour
{
    public TMP_Text dicaText;
    public TMP_Text palavraText;
    public TMP_InputField inputField;
    public Button chuteLetraButton;
    public Button chutePalavraButton;
    public TMP_Text chancesText;

    private string palavraSecreta = "Papa Urbano II";
    private char[] palavraAtual;
    private int chances = 20;

    public float delayDepoisDica = 2f;
    public float velocidadeTexto = 0.05f;

    void Start()
    {
        // Esconde tudo no começo
        inputField.gameObject.SetActive(false);
        chuteLetraButton.gameObject.SetActive(false);
        chutePalavraButton.gameObject.SetActive(false);
        chancesText.gameObject.SetActive(false); // <-- aqui esconde as chances

        palavraAtual = new string('_', palavraSecreta.Length).ToCharArray();
        MostrarDica("Foi o papa que convocou a Primeira Cruzada");
    }

    void MostrarDica(string mensagem)
    {
        dicaText.text = "";
        StartCoroutine(DigitarTexto(mensagem));
    }

    IEnumerator DigitarTexto(string mensagem)
    {
        foreach (char letra in mensagem.ToCharArray())
        {
            dicaText.text += letra;
            yield return new WaitForSeconds(velocidadeTexto);
        }

        yield return new WaitForSeconds(delayDepoisDica);

        dicaText.gameObject.SetActive(false);
        palavraText.text = new string(palavraAtual);

        // Agora mostra as chances só depois da dica sumir
        chancesText.gameObject.SetActive(true);
        chancesText.text = "Chances: " + chances;

        inputField.gameObject.SetActive(true);
        chuteLetraButton.gameObject.SetActive(true);
        chutePalavraButton.gameObject.SetActive(true);
    }

    public void ChutarLetra()
    {
        if (inputField.text.Length == 0) return;

        char letra = char.ToLower(inputField.text[0]);
        bool acertou = false;

        for (int i = 0; i < palavraSecreta.Length; i++)
        {
            if (char.ToLower(palavraSecreta[i]) == letra)
            {
                palavraAtual[i] = palavraSecreta[i];
                acertou = true;
            }
        }

        if (!acertou)
        {
            chances--;
            StartCoroutine(DanoFeedback());
        }

        palavraText.text = new string(palavraAtual);
        chancesText.text = "Chances: " + chances;

        if (new string(palavraAtual) == palavraSecreta)
        {
            Debug.Log("Você ganhou!");
        }
        else if (chances <= 0)
        {
            Debug.Log("Você perdeu!");
        }

        inputField.text = "";
    }

    public void ChutarPalavra()
    {
        string chute = inputField.text;

        if (chute.ToLower() == palavraSecreta.ToLower())
        {
            palavraText.text = palavraSecreta;
            Debug.Log("Você ganhou!");
        }
        else
        {
            chances--;
            chancesText.text = "Chances: " + chances;
            StartCoroutine(DanoFeedback());
        }

        inputField.text = "";
    }

    IEnumerator DanoFeedback()
    {
        Camera.main.backgroundColor = Color.red;
        yield return new WaitForSeconds(0.2f);
        Camera.main.backgroundColor = Color.black;
    }
}
