using _src.Character;
using UnityEngine;
using UnityEngine.Events;

namespace _src.SOs
{
    [CreateAssetMenu(fileName = "New Basic Event" ,menuName = "Events/Basic Event")]
    public class EventSystem : ScriptableObject
    {
        [Header("Basics")]
        [Space]
        public new string name;
        public Sprite image;
        public bool isRepeatable;
        [TextArea(5,10)]
        public string description;

        public bool isWar;
        public EventSystem[] WarEvents;
    
        public float difficulty = 5;
        public int[] chance = new int[]{10,10,10,10};
    
        [Header("Decision tree")]
        [Space]
        public EventSystem[] EventTree;
        public EventSystem[] EventTreeFalse;

        private bool isClassNeeded;
        [SerializeField] public string[] Choices = new string[4]; 
        [SerializeField] public ClassIdEnum[] ClassRequirements = new ClassIdEnum[4] {ClassIdEnum.None, ClassIdEnum.None, ClassIdEnum.None, ClassIdEnum.None};
        [SerializeField] public UnityEvent[] Events = new UnityEvent[4];
    }
}
