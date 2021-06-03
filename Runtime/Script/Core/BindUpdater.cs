using System.Collections.Generic;
using UnityEngine;

namespace Aya.DataBinding
{
    public class BindUpdater : MonoBehaviour
    {
        #region Instance

        protected static BindUpdater Instance;

        internal static BindUpdater Ins
        {
            get
            {
                if (Instance != null) return Instance;
                Instance = (BindUpdater)FindObjectOfType(typeof(BindUpdater));
                if (Instance != null) return Instance;
                var obj = new GameObject
                {
                    hideFlags = HideFlags.HideAndDontSave,
                    name = "BindUpdater"
                };
                DontDestroyOnLoad(obj);
                Instance = obj.AddComponent<BindUpdater>();
                return Instance;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        protected static void Init()
        {
            var ins = Ins;
        }

        #endregion

        internal HashSet<DataBinder> AddList = new HashSet<DataBinder>();
        internal HashSet<DataBinder> RemoveList = new HashSet<DataBinder>();
        public HashSet<DataBinder> UpdateSourceList = new HashSet<DataBinder>();

        public void Add(DataBinder dataBinder)
        {
            AddList.Add(dataBinder);
        }

        public void Remove(DataBinder dataBinder)
        {
            RemoveList.Add(dataBinder);
        }

        public void SyncUpdateList()
        {
            if (AddList.Count > 0)
            {
                foreach (var dataBinder in AddList)
                {
                    UpdateSourceList.Add(dataBinder);
                }

                AddList.Clear();
            }

            if (RemoveList.Count > 0)
            {
                foreach (var dataBinder in RemoveList)
                {
                    UpdateSourceList.Remove(dataBinder);
                }

                RemoveList.Clear();
            }
        }

        public void Update()
        {
            SyncUpdateList();
            foreach (var dataBinder in UpdateSourceList)
            {
                if (dataBinder.UpdateType != UpdateType.Update) continue;
                dataBinder.UpdateSource();
            }
        }

        public void LateUpdate()
        {
            foreach (var dataBinder in UpdateSourceList)
            {
                if (dataBinder.UpdateType != UpdateType.LateUpdate) continue;
                dataBinder.UpdateSource();
            }
        }

        public void FixedUpdate()
        {
            foreach (var dataBinder in UpdateSourceList)
            {
                if (dataBinder.UpdateType != UpdateType.FixedUpdate) continue;
                dataBinder.UpdateSource();
            }
        }
    }
}
