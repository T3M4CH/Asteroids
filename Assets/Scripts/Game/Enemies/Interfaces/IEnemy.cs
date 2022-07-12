using Game.Player.Interfaces;

namespace Game.Enemies.Interfaces
{
    public interface IEnemy : IDamagable
    {
        void Despawn();
    }
}
