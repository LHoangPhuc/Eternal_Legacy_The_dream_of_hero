using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Lớp ScenesTransition dùng để xác định các vùng chuyển cảnh trong game.
/// </summary>
public class ScenesTransition : MonoBehaviour
{
    /// <summary> Tên của cảnh sẽ được chuyển tới./// </summary>
    public string sceneToLoad;
    /// <summary> Vị trí mà người chơi sẽ xuất hiện trong cảnh mới. </summary>
    public Vector2 playerPosition;
    /// <summary> Giá trị lưu trữ vị trí của người chơi. </summary>
    public VectorValue playerStorage;
    /// <summary> Hiệu ứng đen trắng xuất hiện khi chuyển cảnh. </summary>
    public GameObject fadeInPanel;
    /// <summary> Hiệu ứng đen trắng biến mất khi chuyển cảnh. </summary>
    public GameObject fadeOutPanel;
    /// <summary> Thời gian chờ trước khi hoàn thành chuyển cảnh. </summary>
    public float fadeWait;

    private void Awake()
    {
        // Hiển thị bảng đen trắng xuất hiện nếu có
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1); // Phá hủy bảng đen trắng sau 1 giây
        }
    }

    /// <summary>
    /// Xử lý khi người chơi khi tương tác với vùng chuyển cảnh.
    /// </summary>
    /// <param name="other">Collider của đối tượng khác</param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition; // Lưu trữ vị trí của người chơi
            StartCoroutine(FadeCo()); // Bắt đầu chuyển cảnh với hiệu ứng mờ dần
            //SceneManager.LoadScene(sceneToLoad); // Load cảnh mới đồng bộ
        }
    }

    /// <summary>
    /// Coroutine để thực hiện hiệu ứng mờ dần khi chuyển cảnh.
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity); // Hiển thị bảng đen trắng biến mất
        }
        
        yield return new WaitForSeconds(fadeWait); // Chờ một khoảng thời gian trước khi chuyển cảnh

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad); // Bắt đầu load cảnh mới không đồng bộ

        while (!asyncOperation.isDone)
        {
            yield return null; // Chờ cho đến khi quá trình load cảnh hoàn thành
        }
    }
}
