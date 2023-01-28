using System;
using _src.Character;
using _src.Managers;
using UnityEngine;

namespace _src.MaterialSystems
{
    public class MaterialBase : MonoBehaviour
    {
        [SerializeField] private float maxValue = 100;
        [SerializeField] public ClassIdEnum ModifierClass;
        [SerializeField] private CharacterManager _characterManager;

        public event EventHandler<ValueChangedEventArgs> OnValueChanged;

        public bool doesHaveLimit = true;

        private float _value;
        public float Value
        {
            get => _value;
            private set
            {
                if (doesHaveLimit)
                {
                    this._value = Mathf.Clamp(value, 0, maxValue);
                }
                else
                {
                    this._value = Mathf.Clamp(value, 0, float.MaxValue);
                }

                OnValueChanged?.Invoke(this, new ValueChangedEventArgs
                {
                    Value = this._value,
                    MaxValue = maxValue
                });
            }
        }

        private float _modifier = 1.0f;
        public float Modifier
        {
            get => _modifier;
            set => _modifier = value;
        }

        private void Start() => Value = maxValue;

        public void Add(float aValue)
        {
            foreach (var character in _characterManager.Crew)
            {
                if (character.ClassId == ModifierClass)
                    Modifier = character.BuffModifier;
                else
                    Modifier = 1f;
            }
            Value += aValue * Modifier;
        }

        public void ChangeValue(float aValue) => Value = aValue;

        public void ModifierAdd(float aValue)
        {
            _modifier += aValue;
        }

        public void SetAll(ClassIdEnum modifierClass, float value, float modifier, bool doesHaveLimit)
        {
            ChangeValue(value);
            
            Modifier = modifier;
            ModifierClass = modifierClass;
            this.doesHaveLimit = doesHaveLimit;
        }
    }

    public class ValueChangedEventArgs : EventArgs
    {
        public float Value { get; set; }
        public float MaxValue { get; set; }
    }
}