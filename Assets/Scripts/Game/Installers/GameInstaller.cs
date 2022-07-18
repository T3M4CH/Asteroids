using System.Collections.Generic;
using Game.Enemies.Enums;
using Game.Player;
using Game.Settings;
using UnityEngine;
using Game.Utils;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SerializableGameSettings settings;

        public override void InstallBindings()
        {
            Container
                .BindInstance(settings);

            Container
                .Bind<ShipAccelerator>()
                .AsSingle();

            Container
                .Bind<ShipRotator>()
                .AsSingle();

            Container
                .Bind<EngineSound>()
                .AsSingle();
        }
    }
}