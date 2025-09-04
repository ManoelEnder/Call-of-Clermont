using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MultiplaEscolhaStardew : MonoBehaviour
{
    [Header("Painel da narrativa")]
    public GameObject narrativaPanel;
    public TextMeshProUGUI narrativaText;
    public string narrativaMensagem = "Estrangeiro, se desejas seguir comigo at� Jerusal�m, dever�s provar que conheces bem nossa jornada. Escolhe com sabedoria a resposta correta, ou ficar�s perdido no deserto da ignor�ncia�";

    [Header("Painel do quiz")]
    public GameObject quizPanel;
    public TextMeshProUGUI perguntaText;
    public TextMeshProUGUI feedbackText;
    public Button alternativaA;
    public Button alternativaB;
    public Button alternativaC;
    public Button alternativaD;

    private bool narrativaTerminou = false;

    void Start()
    {
        narrativaPanel.SetActive(true);
        quizPanel.SetActive(false);
        feedbackText.text = "";

        StartCoroutine(EscreverNarrativa());
    }

    void Update()
    {
        if (narrativaTerminou && Input.GetMouseButtonDown(0))
        {
            narrativaPanel.SetActive(false);
            quizPanel.SetActive(true);
            MostrarPergunta();
        }
    }

    IEnumerator EscreverNarrativa()
    {
        narrativaText.text = "";
        foreach (char letra in narrativaMensagem)
        {
            narrativaText.text += letra;
            yield return new WaitForSeconds(0.03f);
        }
        narrativaTerminou = true;
    }

    void MostrarPergunta()
    {
        perguntaText.text = "Qual foi o principal objetivo dos cruzados?";
        alternativaA.onClick.AddListener(() => Responder(false));
        alternativaB.onClick.AddListener(() => Responder(true));
        alternativaC.onClick.AddListener(() => Responder(false));
        alternativaD.onClick.AddListener(() => Responder(false));
    }

    void Responder(bool correta)
    {
        if (correta)
        {
            feedbackText.text = "<color=green>Acertou! O objetivo era libertar Jerusal�m do dom�nio mu�ulmano.</color>";
        }
        else
        {
            feedbackText.text = "<color=red>Errou! A resposta certa era: Libertar Jerusal�m do dom�nio mu�ulmano.</color>";
        }

        alternativaA.interactable = false;
        alternativaB.interactable = false;
        alternativaC.interactable = false;
        alternativaD.interactable = false;
    }
}
