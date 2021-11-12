using UnityEngine;
using UnityEngine.UI;

public class ButtonContainerEnemies: MonoBehaviour
{
    public Level Level;
    public Button ButtonPrefab;

    void Start()
    {
        for (var i = 0; i < Level.Enemies.Length; i++)
        {
            var enemy = Level.Enemies[i];
            var btn = Instantiate(ButtonPrefab, transform);
            btn.GetComponentInChildren<Text>().text = enemy.name;
            btn.onClick.AddListener(() => Level.Spawn(enemy));
        }
    }
}
