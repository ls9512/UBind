using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.Sample
{
    [Serializable]
    public class PropertyBinderTestData : MonoBehaviour
    {
        public Text Text { get; set; }
        public string StrValue;
        public int IntValue;
        [Range(0, 1)]
        public float FloatValue;

        public void Awake()
        {
            Text = GetComponentInChildren<Text>();
        }

        public void Update()
        {
            Text.text = StrValue + "\t" + IntValue + "\t" + FloatValue;
        }
    }
}
