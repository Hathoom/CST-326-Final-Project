namespace Enemy
{
    public interface IEnemy
    {
        public void TakeDamage(float damage);
        public float GetHealth();
        public int GetScore();
    }
}