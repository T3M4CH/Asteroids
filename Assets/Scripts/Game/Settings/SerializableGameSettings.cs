using System;
using System.Collections.Generic;
using Game.Enemies.Enums;
using Game.Utils;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableGameSettings
    {
        [field: SerializeField,Header("Ship Settings")]
        public float ShipAcceleration
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float ShipMaxSpeed
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public float ShipDeceleration
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public float ShipAngleVelocity
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int BulletsPerSecond
        {
            get;
            private set;
        }

        [field: SerializeField, Header("Asteroids Settings")]
        public int AsteroidsSplitAngle
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public int AsteroidsStartCount
        {
            get;
            private set;
        }
        
        [field: SerializeField, Header("UFO Settings")]
        public int MinTimeSpawnUfo
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public int MaxTimeSpawnUfo
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public float MinShootDelayUfo
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public float MaxShootDelayUfo
        {
            get;
            private set;
        }

        [field: SerializeField, Header("Score Settings")]
        public List<SerializablePair<ETypeEnemy, int>> ScoreList
        {
            get;
            private set;
        }
    }
}
