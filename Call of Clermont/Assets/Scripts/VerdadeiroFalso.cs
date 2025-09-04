using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class VerdadeiroFalsoStardew : MonoBehaviour
{
    [System.Serializable]
    public class Pergunta
    {
        [TextArea] public string enunciado;
        public bool respostaCorreta;
    }

    [Header("Painel da Dica")]
    public GameObject hintPanel;
    public TMP_Text hintText;
    [TextArea(3, 8)] public string narrativaInicial = "Escuta bem, viajante… Muitos acreditam que a Primeira Cruzada foi travada apenas por nobres cavaleiros. Mas eu vi com meus próprios olhos: camponeses, mulheres e até crianças marcharam rumo ao Oriente, movidos pela fé e pela promessa de salvação.\n\nAgora te desafio: julga estas afirmações como verdadeiras ou falsas, e prova que teu conhecimento é tão afiado quanto a lâmina de uma espada.";

    [Header("Painel das Perguntas")]
    public GameObject quizPanel;
    public TMP_Text perguntaText;
    public TMP_Text feedbackText;
    public Button verdadeiroButton;
    public Button falsoButton;

    [Header("Conteúdo")]
    public Pergunta[] perguntas;

    [Header("Config")]
    public float velocidadeDigitar = 0.03f;
    public float delayEntreQuestoes = 0.7f;

    int indiceAtual = 0;
    bool digitando = false;
    bool pularDigitacao = false;

    void Start()
    {
        verdadeiroButton.onClick.AddListener(() => Responder(true));
        falsoButton.onClick.AddListener(() => Responder(false));

        quizPanel.SetActive(false);
        hintPanel.SetActive(true);
        feedbackText.gameObject.SetActive(false);

        if (perguntas == null || perguntas.Length == 0)
        {
            perguntas = new Pergunta[4];
            perguntas[0] = new Pergunta { enunciado = "A Primeira Cruzada começou no final do século XI.", respostaCorreta = true };
            perguntas[1] = new Pergunta { enunciado = "Os cruzados buscavam apenas riquezas materiais.", respostaCorreta = false };
            perguntas[2] = new Pergunta { enunciado = "Jerusalém foi conquistada pelos cruzados em 1099.", respostaCorreta = true };
            perguntas[3] = new Pergunta { enunciado = "Todos que participaram eram cavaleiros ricos.", respostaCorreta = false };
        }

        StartCoroutine(FluxoInicial());
    }

    void Update()
    {
        if (digitando && Input.GetMouseButtonDown(0)) pularDigitacao = true;
    }

    IEnumerator FluxoInicial()
    {
        yield return StartCoroutine(DigitarTexto(hintText, narrativaInicial, true));
        hintPanel.SetActive(false);
        quizPanel.SetActive(true);
        MostrarPergunta();
    }

    void MostrarPergunta()
    {
        if (indiceAtual >= perguntas.Length)
        {
            StartCoroutine(Finalizar());
            return;
        }

        verdadeiroButton.interactable = false;
        falsoButton.interactable = false;
        feedbackText.gameObject.SetActive(false);
        perguntaText.text = "";
        StartCoroutine(DigitarTexto(perguntaText, perguntas[indiceAtual].enunciado, false));
    }

    IEnumerator DigitarTexto(TMP_Text alvo, string texto, bool esperarCliqueAoFinal)
    {
        digitando = true;
        pularDigitacao = false;
        alvo.text = "";

        for (int i = 0; i < texto.Length; i++)
        {
            if (pularDigitacao)
            {
                alvo.text = texto;
                break;
            }
            alvo.text += texto[i];
            yield return new WaitForSeconds(velocidadeDigitar);
        }

        digitando = false;

        if (esperarCliqueAoFinal)
        {
            yield return new WaitForSeconds(0.05f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        else
        {
            verdadeiroButton.interactable = true;
            falsoButton.interactable = true;
        }
    }

    void Responder(bool respostaJogador)
    {
        verdadeiroButton.interactable = false;
        falsoButton.interactable = false;

        bool correta = perguntas[indiceAtual].respostaCorreta;
        if (respostaJogador == correta)
        {
            if (correta)
                feedbackText.text = "Verdadeiro!";
            else
                feedbackText.text = "Falso!";

            feedbackText.color = new Color(0.2f, 0.85f, 0.3f, 1f); 
        }
        else
        {
            feedbackText.text = "Errou.";
            feedbackText.color = new Color(0.9f, 0.2f, 0.2f, 1f); 
        }

        feedbackText.gameObject.SetActive(true);
        StartCoroutine(AvancarDepoisDoFeedback());
    }


    IEnumerator AvancarDepoisDoFeedback()
    {
        yield return new WaitForSeconds(delayEntreQuestoes);
        indiceAtual++;
        MostrarPergunta();
    }

    IEnumerator Finalizar()
    {
        verdadeiroButton.gameObject.SetActive(false);
        falsoButton.gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(false);
        perguntaText.text = "";
        yield return StartCoroutine(DigitarTexto(perguntaText, "Fim do desafio, viajante. Mostraste tua sabedoria.", false));
    }
}
