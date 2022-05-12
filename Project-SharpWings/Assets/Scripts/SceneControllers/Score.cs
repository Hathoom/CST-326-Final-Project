namespace SceneControllers
{
    [System.Serializable]
    public class Score
    {
        public string initials;
        public int score;

        public Score(string initials, int score)
        {
            this.initials = initials;
            this.score = score;
        }
    }
}
