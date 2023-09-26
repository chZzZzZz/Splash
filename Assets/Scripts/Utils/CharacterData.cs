using UnityEditor;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        public string nameString;
        public RuntimeAnimatorController animController;
        public Sprite person;
    }
}