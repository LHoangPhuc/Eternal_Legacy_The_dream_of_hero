using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Lớp RoomMovement dùng để xác định các phòng và thay đổi vị trí của camera và người chơi khi vào phòng mới.
/// </summary>
public class RoomMovement : MonoBehaviour
{
    /// <summary>
    /// Thay đổi vị trí của camera khi vào phòng mới.
    /// </summary>
    public Vector2 cameraChange;

    /// <summary>
    /// Thay đổi vị trí của người chơi khi vào phòng mới.
    /// </summary>
    public Vector3 playerChange;

    /// <summary>
    /// Tham chiếu đến lớp CameraMovement.
    /// </summary>
    private CameraMovement cam; 

    /// <summary>
    /// Biến đánh dấu xem cần hiển thị tên địa điểm hay không.
    /// </summary>
    public bool needText; 

    /// <summary>
    /// Tên địa điểm cần hiển thị.
    /// </summary>
    public string placeName; 

    /// <summary>
    /// Đối tượng văn bản hiển thị tên địa điểm.
    /// </summary>
    public GameObject text; 

    /// <summary>
    /// Component văn bản để hiển thị tên địa điểm.
    /// </summary>
    public TextMeshProUGUI placeText; 

    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>(); // Lấy tham chiếu đến CameraMovement từ Camera chính
    }

    /// <summary>
    /// Phương thức xử lý khi người chơi va chạm với cửa phòng.
    /// </summary>
    /// <param name="other">Collider của đối tượng khác</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(placeNameCo()); // Bắt đầu coroutine để hiển thị tên địa điểm
            }
        }
    }

    /// <summary>
    /// Coroutine để hiển thị tên địa điểm trong một khoảng thời gian ngắn.
    /// </summary>
    private IEnumerator placeNameCo()
    {
        text.SetActive(true); // Kích hoạt đối tượng văn bản hiển thị
        placeText.text = placeName; // Đặt nội dung văn bản là tên địa điểm

        yield return new WaitForSeconds(3f); // Chờ 3 giây

        text.SetActive(false); // Tắt đối tượng văn bản hiển thị
    }
}
