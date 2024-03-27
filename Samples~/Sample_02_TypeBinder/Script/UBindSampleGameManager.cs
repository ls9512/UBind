using System;
using System.Collections;
using UnityEngine;
using Aya.DataBinding;

namespace Aya.Sample
{
    [Serializable]
    public class UBindSamplePlayerData
    {
        public string Name;
        public int Exp;
        public float Stat;
        public float PlayTime;
    }

    public class UBindSampleGameManager : MonoBehaviour
    {
        public UBindSamplePlayerData Player;

        public void Awake()
        {
            Player = new UBindSamplePlayerData() {Name = "Player",};

            // Source
            UBind.BindSource("PlayerData", Player);

            // Manual Both
            // UBind.BindBoth(nameof(UBindSamplePlayerData) + "." + nameof(Player.Name) + ".PlayerData", Player, nameof(Player.Name));
            // UBind.BindBoth(nameof(UBindSamplePlayerData) + "." + nameof(Player.Exp) + ".PlayerData", Player, nameof(Player.Exp));
            // UBind.BindBoth(nameof(UBindSamplePlayerData) + "." + nameof(Player.Stat) + ".PlayerData", Player, nameof(Player.Stat));
            // UBind.BindBoth(nameof(UBindSamplePlayerData) + "." + nameof(Player.PlayTime) + ".PlayerData", Player, nameof(Player.PlayTime));

            // StartCoroutine(_test());
        }

        IEnumerator _test()
        {
            while (true)
            {
                Player.PlayTime += Time.deltaTime;
                Player.Stat = Player.PlayTime % 1f;
                Player.Exp = (int) (Player.PlayTime / 5);
                yield return null;
            }
        }
    }

    /*

    public class GameManager : BindableMonoBehaviour
    {
        [BindTypeSource("Player")]
        public PlayerData Player;

        public void Awake()
        {
            Player = new PlayerData()
            {
                Name = "Player",
                Exp = 0,
                Stat = 0,
                PlayTime = 0,
            };
        }
    }

    */
}
