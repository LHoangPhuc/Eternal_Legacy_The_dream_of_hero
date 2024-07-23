using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp ContextClue dùng để xác định các gợi ý dể tương tác với người chơi
/// </summary>
public class ContextClue : MonoBehaviour
{
    /// <summary> Đối tượng gợi ý người chơi </summary>
    public GameObject contextClue;
    /// <summary>
    /// Biến đánh dấu xem gợi ý có hoạt động hay không
    /// </summary>
    public bool contextActive = false;

    /// <summary>
    /// Phương thức để thay đổi trạng thái của gợi ý 
    /// </summary>
    public void ChangeContext()
    {
        contextActive = !contextActive;
        if(contextActive)
        {
            contextClue.SetActive(true);
        }else
        {
            contextClue.SetActive(false);
        }
    }
}
