using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///  Lớp VectorValue dùng để lưu trữ giá trị vector
/// </summary>
[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver {

    /// <summary>  Giá trị vector ban đầu  </summary>
    public Vector2 initialValue;

    /// <summary> Giá trị vector tại thời điểm chạy </summary>
    public Vector2 defaultValue;

    /// <summary> Phương thức để thiết lập giá trị vector </summary>
    public void OnAfterDeserialize() { initialValue = defaultValue; }

    public void OnBeforeSerialize() { }
}


