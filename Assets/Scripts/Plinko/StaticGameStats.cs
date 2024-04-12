using UnityEngine;

namespace RobbieWagnerGames.Plinko
{
    public static class StaticGameStats 
    {
        public delegate void StaticValueChangedDelegate(int newValue); 

        private static int roundScore = 0;
        public static int RoundScore
        {
            get => roundScore; 
            set 
            { 
                if(value == roundScore)
                    return;
                roundScore = value; 
                OnScoreChanged?.Invoke(roundScore);
                Debug.Log($"Score: {roundScore}");
            }
        }
        public static event StaticValueChangedDelegate OnScoreChanged;

        private static int highScore = 0;
        public static int HighScore
        {
            get => highScore; 
            set 
            { 
                if(value == highScore)
                    return;
                highScore = value; 
                OnHighScoreChanged?.Invoke(highScore);
            }
        }
        public static event StaticValueChangedDelegate OnHighScoreChanged;
        

        private static int leaves = 0;
        public static int Leaves
        {
            get => leaves;
            set
            {
                if(value == leaves)
                    return;
                leaves = value;
                OnLeavesValueChanged?.Invoke(leaves);
            }
        }
        public static event StaticValueChangedDelegate OnLeavesValueChanged;

        private static int flowers = 0;
         public static int Flowers
        {
            get => flowers;
            set
            {
                if(value == flowers)
                    return;
                flowers = value;
                OnFlowersValueChanged?.Invoke(flowers);
            }
        }
        public static event StaticValueChangedDelegate OnFlowersValueChanged;
    }
}