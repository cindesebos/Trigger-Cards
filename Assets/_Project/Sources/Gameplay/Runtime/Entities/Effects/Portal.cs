using UnityEngine;
using Zenject;
using System;
using System.Linq;
using System.Collections.Generic;
using Sources.Gameplay.Runtime.Buffs;

namespace Sources.Gameplay.Runtime.Entities
{
    public class Portal : Effect
    {
        private const float LifeTime = 3f;

        private IEntitiesObserver _entitiesObserver;
        private ImmortalityBuff _buff;

        public void Init(IEntitiesObserver entitiesObserver, Vector3 mousePosition, ImmortalityBuff buff)
        {
            _entitiesObserver = entitiesObserver;
            _buff = buff;

            Vector2 characterMovedPosition = new Vector2(mousePosition.x, mousePosition.y);
            entitiesObserver.GetCharacter().Transform.position = characterMovedPosition;

            entitiesObserver.GetCharacter().AddBuff(_buff);

            Visualize(mousePosition);

            Invoke(nameof(Hide), LifeTime);
        }

        private void Visualize(Vector3 mousePosition)
        {
            mousePosition.z = 0f;

            GameObject instance = Instantiate(gameObject, mousePosition, Quaternion.identity);

            Destroy(instance, LifeTime);
        }

        private void Hide() => gameObject.SetActive(false);
    }
}
