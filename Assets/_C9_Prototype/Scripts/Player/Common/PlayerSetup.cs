using UnityEngine;

[RequireComponent(typeof(PlayerRunTimeStats),typeof(PlayerHealth),typeof(PlayerSkillController))]
public class PlayerSetup : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] PlayerConfigSO playerConfig;
    [SerializeField] int level = 1;

    [Header("References")]
    PlayerRunTimeStats runTimeStats;
    PlayerHealth health;
    PlayerSkillController playerSkillController;

    private void Awake()
    {
        runTimeStats = GetComponent<PlayerRunTimeStats>();
        health = GetComponent<PlayerHealth>();

        runTimeStats.Init(playerConfig);

        Debug.Log("Enemy scriptine bakýlacak, eventlere bakýlacak, " +
            "PlayerHealth health " +
            "PlayerSkillController playerSkillController " +
            "bunlara bakýlacak");
    }

}
