using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp Signal dùng để xác định các tín hiệu giao tiếp giữa các đối tượng
/// </summary>
[CreateAssetMenu]
public class Signal : ScriptableObject
{
    /// <summary>
    /// Danh sách các listener đăng ký nhận tín hiệu
    /// </summary>
    public List<SignalListener> listeners = new List<SignalListener>();

     /// <summary>
    /// Phương thức để gửi tín hiệu tới tất cả các listener đã đăng ký
    /// </summary>
    public void Raise()
    {
        // Duyệt qua danh sách các listener và gửi tín hiệu
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    /// <summary>
    /// Phương thức để đăng ký một listener mới
    /// </summary>
    /// <param name="listener">Listener cần đăng ký</param>
    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Phương thức để hủy đăng ký một listener
    /// </summary>
    /// <param name="listener">Listener cần hủy đăng ký</param>
    public void DeRegisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }
}
