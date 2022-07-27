using UnityEngine;

public class HologramLoader : MonoBehaviour
{
    public void LoadHologram(Entry entry, string queryURL)
    {
        Echo3DService.instance.DownloadAndInstantiate(entry, queryURL, this.gameObject);
    }

    public void EntryCount(Database database)
    {
        Debug.Log("Found " + database.getEntries().Count + " entries");
    }
}
