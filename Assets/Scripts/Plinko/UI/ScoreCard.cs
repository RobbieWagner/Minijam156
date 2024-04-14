using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace RobbieWagnerGames.Plinko
{
    public enum ScoreType
    {
        SCORE,
        LEAVES,
        FLOWERS
    } 

    public class ScoreCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private int currentDisplayedScore = 0;
        private Coroutine currentScoreRaiseCo = null;

        [SerializeField] private TextMeshProUGUI leavesText;
        private int currentDisplayedLeaves = 0;
        private Coroutine currentLeavesRaiseCo = null;

        [SerializeField] private TextMeshProUGUI flowersText;
        private int currentDisplayedFlowers = 0;
        private Coroutine currentFlowersRaiseCo = null;

        [SerializeField] private int characters = 9;

        private void Awake()
        {
            StaticGameStats.OnScoreChanged += UpdateScore;
            StaticGameStats.OnLeavesValueChanged += UpdateLeaves;
            StaticGameStats.OnFlowersValueChanged += UpdateFlowers;
        }   

        private void UpdateScore(int score)
        {
            if(currentScoreRaiseCo == null)
                currentScoreRaiseCo = StartCoroutine(UpdateTextCo(score, scoreText, ScoreType.SCORE));
        }   

        private void UpdateLeaves(int leaves)
        {
            if(currentLeavesRaiseCo == null)
                currentLeavesRaiseCo = StartCoroutine(UpdateTextCo(leaves, leavesText, ScoreType.LEAVES));
        }

        private void UpdateFlowers(int flowers)
        {
            if(currentFlowersRaiseCo == null)
                currentFlowersRaiseCo = StartCoroutine(UpdateTextCo(flowers, flowersText, ScoreType.FLOWERS));
        }

        private IEnumerator UpdateTextCo(int newValue, TextMeshProUGUI text, ScoreType scoreType)
        {
            if(scoreType == ScoreType.SCORE)
            {
                while(currentDisplayedScore < newValue)
                {
                    currentDisplayedScore++;
                    UpdateText(currentDisplayedScore.ToString(), text);
                    yield return null;
                }
                currentDisplayedScore = newValue;
                UpdateText(currentDisplayedScore.ToString(), text);
                currentScoreRaiseCo = null;
            }
            else if(scoreType == ScoreType.LEAVES)
            {
                while(currentDisplayedLeaves < newValue)
                {
                    currentDisplayedLeaves++;
                    UpdateText(currentDisplayedLeaves.ToString(), text);
                    yield return null;
                }
                currentDisplayedLeaves = newValue;
                UpdateText(currentDisplayedLeaves.ToString(), text);
                currentLeavesRaiseCo = null;
            }
            else if(scoreType == ScoreType.FLOWERS)
            {
                while(currentDisplayedFlowers < newValue)
                {
                    currentDisplayedFlowers++;
                    UpdateText(currentDisplayedFlowers.ToString(), text);
                    yield return null;
                }
                currentDisplayedFlowers = newValue;
                UpdateText(currentDisplayedFlowers.ToString(), text);
                currentFlowersRaiseCo = null;
            }
        }

        private void UpdateText(string score, TextMeshProUGUI text)
        {
            text.text = score;
            while(text.text.Length < characters && characters > 0)
               text.text = "0" + text.text;
        }
    }
}