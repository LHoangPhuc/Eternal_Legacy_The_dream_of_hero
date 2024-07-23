using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Lớp SignalListener dùng để lắng nghe signal và gọi event tương ứng
/// </summary>
public class SignalListener : MonoBehaviour
{
    /// <summary>
    /// Signal cần lắng nghe
    /// </summary>
    public Signal signal;

    /// <summary>
    /// Event được gọi khi signal được kích hoạt
    /// </summary>
    public UnityEvent signalEvent;

    /// <summary>
    /// Phương thức được gọi khi signal được kích hoạt
    /// </summary>
    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    /// <summary>
    /// Đăng ký lắng nghe signal khi script được kích hoạt
    /// </summary>
    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    /// <summary>
    /// Hủy đăng ký lắng nghe signal khi script bị vô hiệu hóa
    /// </summary>
    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }
}
