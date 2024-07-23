using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp FloatValue dùng để lưu trữ giá trị số thực
/// </summary>
[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    /// <summary>
    /// Giá trị số thực ban đầu
    /// </summary>
    public float initialValue;

    /// <summary>
    /// Giá trị số thực tại thời điểm chạy
    /// </summary>
    [HideInInspector]
    public float RuntimeValue;
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
    public void OnBeforeSerialize()
    {

    }
}
