using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameAnimation : MonoBehaviour
{
    public float moveDuration = 1f; // Thời gian di chuyển của mỗi sprite
    public float distanceBetweenSprites = 1f; // Khoảng cách giữa các sprite
    public GameObject[] sprites; // Danh sách các sprite
    [SerializeField] private GameObject logoGame;
    [SerializeField] private GameObject logoNPH;

    void Start()
    {
        MoveSprites();
    }

    void MoveSprites()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            distanceBetweenSprites = distanceBetweenSprites - 0.4f;
            Vector3 targetPosition = new Vector3(sprites[i].transform.position.x, sprites[i].transform.position.y + distanceBetweenSprites, sprites[i].transform.position.z);
            LeanTween.move(sprites[i], targetPosition, moveDuration).setEase(LeanTweenType.easeOutQuad).setDelay(moveDuration * i);
        }
    }
    private void Update()
    {
        if(sprites[4].transform.position.y == -2.479999f)
        {
            logoGame.SetActive(true);
            logoNPH.SetActive(true);
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
    }
}
