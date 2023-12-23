using UnityEngine;

namespace HELPER
{
    public class DontDestroyOnPlay : MonoBehaviour
    {
        #region Attributes

        private static DontDestroyOnPlay instance;

        #endregion

        private void Awake()
        {
            if (instance != null)
            {
                GameObject.DestroyImmediate(gameObject);
                return;
            }

            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}