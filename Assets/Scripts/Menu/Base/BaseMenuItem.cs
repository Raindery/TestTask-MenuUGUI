using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMenuItem : MonoBehaviour
{
    public abstract TMP_Text Text { get; }
    public abstract Button Button { get; }
}