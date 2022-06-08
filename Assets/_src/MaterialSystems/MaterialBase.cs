using System;
using PCC;
using Unity.VisualScripting;
using UnityEngine;

    public class MaterialBase : MonoBehaviour
    {
        [SerializeField] private float maxValue = 100;
        [SerializeField] public ClassIdEnum ModifierClass;
        [SerializeField] private CharacterManager _characterManager;

        public event EventHandler<ValueChangedEventArgs> OnValueChanged;

        public float MaxValue => maxValue;
        public bool isMax = true;

        private float value;
        public float Value
        {
            get => value;
            private set
            {
                if (isMax)
                {
                    this.value = Mathf.Clamp(value, 0, maxValue);
                }
                else
                {
                    this.value = Mathf.Clamp(value, 0, float.MaxValue);
                }

                OnValueChanged?.Invoke(this, new ValueChangedEventArgs
                {
                    Value = this.value,
                    MaxValue = maxValue
                });
            }
        }

        public float Modifier = 1.0f;
        public float _modifier = 1.0f;

        private void Start() => Value = maxValue;

        public void Add(float aValue)
        {
            foreach (var character in _characterManager.Crew)
            {
                if (character.ClassId == ModifierClass)
                    Modifier = character.BuffModifier;
                else
                    Modifier = _modifier;
            }
            Value += aValue * Modifier;
        }

        public void ChangeValue(float aValue) => Value = aValue;

        public void ModifierAdd(float aValue)
        {
            _modifier += aValue;
        }

        public void SetAll(ClassIdEnum classid, float value, float Modifier, float _modifier, bool max)
        {
            ChangeValue(value);
            this.Modifier = Modifier;
            this._modifier = _modifier;
            ModifierClass = classid;
            isMax = max;
        }
    }

    public class ValueChangedEventArgs : EventArgs
    {
        public float Value { get; set; }
        public float MaxValue { get; set; }
    }