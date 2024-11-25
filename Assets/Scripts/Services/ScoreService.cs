using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreService : BaseService
{
    private int _score;

    public UnityEvent<string> OnScoreModified;

    public void AddScore(int amount)
    {
        _score += amount;
        OnScoreModified?.Invoke(_score.ToString());
    }
}
