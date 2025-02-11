using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalculadoraGenerator : MonoBehaviour
{
    public GameObject buttonPrefab; // Prefab do botão
    public Transform numerosParent; // Parent dos números
    public Transform operacoesParent; // Parent das operações
    public TextMeshProUGUI display; // Área de exibição dos números

    private string input = "";

    void Start()
    {
        // Verificações antes de executar a criação dos botões
        if (buttonPrefab == null)
        {
            Debug.LogError("ERRO: buttonPrefab não foi atribuído no Inspector.");
            return;
        }

        if (numerosParent == null || operacoesParent == null)
        {
            Debug.LogError("ERRO: numerosParent ou operacoesParent não foram atribuídos.");
            return;
        }

        CriarBotoesNumericos();
        CriarBotoesOperacoes();
    }

    void CriarBotoesNumericos()
    {
        for (int i = 0; i <= 9; i++)
        {
            GameObject btn = Instantiate(buttonPrefab, numerosParent);

            Text btnText = btn.GetComponentInChildren<Text>();
            if (btnText == null)
            {
                Debug.LogError("ERRO: O prefab do botão não contém um componente Text.");
                return;
            }

            btnText.text = i.ToString();
            int numero = i;
            btn.GetComponent<Button>().onClick.AddListener(() => AdicionarValor(numero.ToString()));
        }
    }

    void CriarBotoesOperacoes()
    {
        string[] operacoes = { "+", "-", "×", "÷" };

        foreach (string operacao in operacoes)
        {
            GameObject btn = Instantiate(buttonPrefab, operacoesParent);

            Text btnText = btn.GetComponentInChildren<Text>();
            if (btnText == null)
            {
                Debug.LogError("ERRO: O prefab do botão não contém um componente Text.");
                return;
            }

            btnText.text = operacao;
            btn.GetComponent<Button>().onClick.AddListener(() => AdicionarValor(operacao));
        }
    }

    public void AdicionarValor(string valor)
    {
        input += valor;
        display.text = input;
    }
}
