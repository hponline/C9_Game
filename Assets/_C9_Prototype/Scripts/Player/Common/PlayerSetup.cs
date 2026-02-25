using UnityEngine;

[RequireComponent(typeof(PlayerRunTimeStats))]
public class PlayerSetup : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] PlayerConfigSO playerConfig;

    [Header("References")]
    PlayerRunTimeStats runTimeStats;

    private void Awake()
    {
        runTimeStats = GetComponent<PlayerRunTimeStats>();

        runTimeStats.Init(playerConfig);
    }
}
