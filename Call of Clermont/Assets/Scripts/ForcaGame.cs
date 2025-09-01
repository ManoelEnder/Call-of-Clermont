using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ForcaGame : MonoBehaviour
{
    public TMP_Text dicaText;
    public TMP_Text palavraText;
    public TMP_Text chancesText;
    public TMP_InputField inputField;
    public AudioSource somErro;
    public Image telaVermelha;
    public Button botaoIniciar; // Novo: Botão pra começar o jogo

    private string palavraSecreta = "PAPA URBANO II";
    private string palavraOculta;
    private int chances = 20;
    private bool jogoAtivo = false;
    private bool mostrandoDica = true;
    private string dica = "Essa pessoa convocou a Primeira Cruzada.";

    void Start()
    {
        palavraSecreta = palavraSecreta.ToUpper();
        palavraOculta = "";
        foreach (char c in palavraSecreta)
        {
            if (c == ' ') palavraOculta += " ";
            else palavraOculta += "_";
        }

        dicaText.text = "";
        palavraText.text = "";
        chancesText.text = "";
        inputField.gameObject.SetActive(false);
        botaoIniciar.gameObject.SetActive(true); // Botão visível no começo
    }

    // Função chamada pelo botão de iniciar
    public void IniciarComBotao()
    {
        botaoIniciar.interactable = false; // Desativa o botão
        botaoIniciar.gameObject.SetActive(false); // Esconde o botão (opcional, tu escolhe)
        StartCoroutine(MostrarDica());
    }

    IEnumerator MostrarDica()
    {
        foreach (char letra in dica)
        {
            dicaText.text += letra;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        dicaText.text = "";
        mostrandoDica = false;
        IniciarJogo();
    }

    void IniciarJogo()
    {
        jogoAtivo = true;
        palavraText.text = palavraOculta;
        chancesText.text = "Chances: " + chances;
        inputField.gameObject.SetActive(true);
        inputField.ActivateInputField(); // Foca no campo de input
    }

    public void VerificarInput()
    {
        if (!jogoAtivo) return;

        string tentativa = inputField.text.ToUpper();
        inputField.text = "";

        if (tentativa.Length > 1)
        {
            if (tentativa == palavraSecreta)
            {
                Venceu();
            }
            else
            {
                Errou();
            }
        }
        else if (tentativa.Length == 1)
        {
            char letra = tentativa[0];
            bool acertou = false;
            char[] palavraArray = palavraOculta.ToCharArray();

            for (int i = 0; i < palavraSecreta.Length; i++)
            {
                if (palavraSecreta[i] == letra && palavraArray[i] == '_')
                {
                    palavraArray[i] = letra;
                    acertou = true;
                }
            }

            palavraOculta = new string(palavraArray);
            palavraText.text = palavraOculta;

            if (!acertou) Errou();
            else if (palavraOculta == palavraSecreta) Venceu();
        }
    }

    void Errou()
    {
        chances--;
        chancesText.text = "Chances: " + chances;
        somErro.Play();
        StartCoroutine(EfeitoVermelho());

        if (chances <= 0)
        {
            jogoAtivo = false;
            palavraText.text = "Você perdeu! A palavra era: " + palavraSecreta;
            inputField.gameObject.SetActive(false); // Desativa input no fim
        }
    }

    IEnumerator EfeitoVermelho()
    {
        telaVermelha.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.3f);
        telaVermelha.color = new Color(1, 0, 0, 0);
    }

    void Venceu()
    {
        jogoAtivo = false;
        palavraText.text = "Você ganhou! A palavra era: " + palavraSecreta;
        inputField.gameObject.SetActive(false); // Desativa input no fim
    }
}