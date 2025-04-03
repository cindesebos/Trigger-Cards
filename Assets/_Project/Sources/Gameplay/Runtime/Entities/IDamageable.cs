namespace Sources.Gameplay.Runtime.Entities
{
    public interface IDamageable
    {
        int Health { get; }

        void ApplyDamage(int damage);
    }
}
