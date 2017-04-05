using UnityEngine;

namespace TAMKShooter
{
    public class SpawnPoints : MonoBehaviour
    {
        public Vector3 GetSpawnPoint(int playerIndex)
        {
            if (transform.childCount == 0) throw new UnityException("SpawnPoints has no spawn points set up!");
            if (playerIndex < 0) throw new UnityException("SpawnPoints::GetSpawnPoint: parameter playerIndex must"+
                "be in range [0, int.maxValue]. Given value is " + playerIndex);
            var spawnIndexToFind = Mathf.Min(playerIndex, transform.childCount);

#if UNITY_EDITOR
            if (spawnIndexToFind != playerIndex)
                Debug.LogWarning("Not enough unique spawn points set for the numbe of players in the scene."+
                    "falling back to last spawn point for overflow...");
#endif

            return transform.GetChild(spawnIndexToFind).position;
        }
    }
}
