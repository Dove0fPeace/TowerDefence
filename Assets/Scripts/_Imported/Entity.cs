using UnityEngine;

namespace _Imported
{

    /// <summary>
    /// base interact class.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private string m_Nickname;
        public string Nickname => m_Nickname;
    }
}