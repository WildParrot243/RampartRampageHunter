using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [DefaultExecutionOrder(-1000)]
    public class WaypointManager : MonoBehaviour
    {
        private static Vector3[][] _positions;

        private void OnEnable()
        {
            if (_positions != null) return;
            _positions = new Vector3[transform.childCount][];
            for (int i = 0; i < transform.childCount; ++i)
            {
                Transform child = transform.GetChild(i);
                _positions[i] = new Vector3[child.childCount];
                for (int j = 0; j < child.childCount; ++j)
                {
                    Debug.Log(i + ", " + j, child.GetChild(j));
                    _positions[i][j] = child.GetChild(j).position;
                }
            }
            Debug.Log(_positions.Length);
        }

        public static int ChooseRandomPath()
        {
            return Random.Range(0, _positions.Length);
        }

        public static Vector3 GetNextLocation(int path, ref int current)
        {
            if (current >= _positions[path].Length) current = 0;
            return _positions[path][current++];
        }
    }
}