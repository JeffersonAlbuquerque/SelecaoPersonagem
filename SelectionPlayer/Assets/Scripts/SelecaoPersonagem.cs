using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelecaoPersonagem : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject[] playerPrefab; // Armazena os personagens disponíveis para seleção.
    public GameObject prefabCurrent; // Armazena o personagem atualmente instanciado.
    public int indexPlayer; // Índice que indica qual personagem está sendo selecionado.

    [Header("Spawns")]
    public Transform spawnPosition; // Posição onde o personagem será instanciado no cenário.

    [Header("HUD")]
    public Button leftArrow; // Botão de seta para mover a seleção à esquerda (personagem anterior).
    public Button rightArrow; // Botão de seta para mover a seleção à direita (próximo personagem).

    [Header("Information Trailer Settings")]
    public Text playerName; // Texto para exibir o nome do personagem na tela.

    private void Start()
    {
        // Adiciona as funções "Previous" e "Next" aos botões de navegação (setas).
        leftArrow.onClick.AddListener(Previous);
        rightArrow.onClick.AddListener(Next);

        // Instancia o personagem atual baseado no índice `indexPlayer`.
        prefabCurrent = Instantiate(playerPrefab[indexPlayer], spawnPosition.position, spawnPosition.rotation);

        // Atualiza as informações na HUD para o personagem atualmente selecionado.
        updateTrailerInfo();
    }

    private void Update()
    {
        // Se o índice do personagem for maior que 0, a seta esquerda (para o personagem anterior) será visível.
        // Caso contrário, a seta esquerda ficará invisível.
        if (indexPlayer > 0)
        {
            leftArrow.gameObject.SetActive(true);
        }
        else
        {
            leftArrow.gameObject.SetActive(false);
        }
    }

    public void Next()
    {
        // Incrementa o índice para selecionar o próximo personagem.
        indexPlayer++;

        // Se o índice exceder o número de personagens no array, volta ao primeiro personagem.
        if (indexPlayer >= playerPrefab.Length)
        {
            indexPlayer = 0;
        }

        // Destroi o personagem atual antes de instanciar o próximo.
        if (prefabCurrent != null)
        {
            Destroy(prefabCurrent);
        }

        // Instancia o próximo personagem de acordo com o índice atualizado.
        prefabCurrent = Instantiate(playerPrefab[indexPlayer], spawnPosition.position, spawnPosition.rotation);

        // Atualiza as informações na HUD para o novo personagem.
        updateTrailerInfo();
    }

    public void Previous()
    {
        // Decrementa o índice para selecionar o personagem anterior.
        indexPlayer--;

        // Se o índice for menor que 0, o loop volta para o último personagem no array.
        if (indexPlayer < 0)
        {
            indexPlayer = playerPrefab.Length - 1; // Corrigido para selecionar o último personagem.
        }

        // Destroi o personagem atual antes de instanciar o anterior.
        if (prefabCurrent != null)
        {
            Destroy(prefabCurrent);
        }

        // Instancia o personagem anterior de acordo com o índice atualizado.
        prefabCurrent = Instantiate(playerPrefab[indexPlayer], spawnPosition.position, spawnPosition.rotation);

        // Atualiza as informações na HUD para o personagem atual.
        updateTrailerInfo();
    }

    public void updateTrailerInfo()
    {
        // Pega o componente `CaracteristicasPersonagem` do personagem instanciado, que contém informações como nome.
        CaracteristicasPersonagem infoPlayer = prefabCurrent.GetComponent<CaracteristicasPersonagem>();

        // Se o componente existir, atualiza o nome do personagem no HUD.
        if (infoPlayer != null)
        {
            playerName.text = infoPlayer.name;
        }
    }
}
