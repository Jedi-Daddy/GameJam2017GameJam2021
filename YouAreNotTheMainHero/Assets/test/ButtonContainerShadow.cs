using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonContainerShadow : MonoBehaviour
{
    public Level Level;
    public Button ButtonPrefab;
    public UnityEvent<int> OnSelectPoint;

    void Start()
    {
        for (var i = 0; i < Level.SpawnPoints.Length; i++)
        {
            var btn = Instantiate(ButtonPrefab, transform);
            btn.GetComponentInChildren<Text>().text = "SpawnPoint " + (i + 1);

            var pointIdx = i;
            btn.onClick.AddListener(() => OnSelectPoint?.Invoke(pointIdx));
        }
    }
}
