using UnityEngine;

namespace ExperimentalFox.GameController
{
    public class GameControllersRoot : MonoBehaviour
    {
        private static GameControllersRoot _instance;
        
        #region Instance

        void InstanceCheck()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion
        
        private void Awake()
        {
            InstanceCheck();
        }
    }
}