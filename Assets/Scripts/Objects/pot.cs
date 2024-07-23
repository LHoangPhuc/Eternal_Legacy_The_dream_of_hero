using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp pot dùng để xác định các đối tượng bình có thể bị đập vỡ
/// </summary>
public class pot : MonoBehaviour
{   
    private Animator anim;  // Đối tượng Animator để điều khiển hoạt ảnh của cái bình

    /// <summary>
    /// Phương thức để đập cái bình.
    /// </summary>
    public void Smash()
    {
        anim.SetBool("smash", true);  // Thiết lập trạng thái "smash" trong Animator thành true để kích hoạt hoạt ảnh đập vỡ
        StartCoroutine(breakCo());  // Bắt đầu coroutine để xử lý việc đập vỡ
    }

    /// <summary>
    /// Coroutine để xử lý việc đập vỡ cái bình.
    /// </summary>
    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);  // Chờ 0.3 giây trước khi thực hiện các bước tiếp theo
        this.gameObject.SetActive(false);  // Ẩn đối tượng cái bình đi
    }
}
