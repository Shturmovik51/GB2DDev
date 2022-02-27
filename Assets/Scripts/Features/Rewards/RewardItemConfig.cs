using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardItemConfig", menuName = nameof(RewardItemConfig), order = 0)]
public class RewardItemConfig : ScriptableObject
{
    [SerializeField] private int _timeCooldown;
    [SerializeField] private int _timeDeadline;
    [SerializeField] private List<Reward> _rewards;

    public int TimeCooldown => _timeCooldown;
    public int TimeDeadline => _timeDeadline;
    public List<Reward> Rewards => _rewards;

}
