using System.Collections;
using UnityEngine;
using Aya.DataBinding;

namespace Aya.Sample
{
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
            UBind.BindSource("PlayerData", Player);
            StartCoroutine(_test());
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
