/**************************************************************************
* Copyright (C) echoAR, Inc. (dba "echo3D") 2018-2021.                    *
* echoAR, Inc. proprietary and confidential.                              *
*                                                                         *
* Use subject to the terms of the Terms of Service available at 	      *
* https://www.echo3D.co/terms, or another agreement      	              *
* between echoAR, Inc. and you, your company or other organization.       *
***************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Echo3DHologram : MonoBehaviour
{

    // Your echo3D API key
    [Tooltip("echo3d project API key.")]
    public string apiKey = "<YOUR_API_KEY>";

    [Tooltip("Project queries utilize a security key by default. Adjust this setting or view the project security key via the online console 'Security' tab.")]
    public string secKey = "<YOUR_SEC_KEY>";

    [Tooltip("Entry IDs separated by comma without spaces. (Optional)")]
    public string entries = "";
    [Tooltip("Filter by by tags separated by comma without spaces. (Optional)")]
    public string tags = "";
    [Tooltip("Holograms with this flag enabled will load in the editor via the menu Echo3D -> Load In Editor")]
    public bool editorPreview = false;

    // Specified hologram(s) data will be stored in this object
    [HideInInspector]
    public Database hologramData;

    [Tooltip("Whether to use the default parameters of entries/tags or supply a custom series of query parameters.")]
    public bool useCustomQuery = false;
    [Tooltip("A series of URL parameters (aside from the key and security key) to query the database with. Only active if Use Custom Query is checked.")]
    public string customQuery;

    [Space]
    [Tooltip("An event that gets called once for the entire set of entries. Contains the database object with the entries.")]
    public UnityEvent<Database> onQueryLoad = new UnityEvent<Database>();
    [Tooltip("An event that gets called once for each entry. Happens after the previous event. Contains the entry object and the query url.")]
    public UnityEvent<Entry, string> perEntry = new UnityEvent<Entry, string>();

    [HideInInspector]
    public string queryURL;

    void Start()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.logger.logEnabled = false;
#endif
        StartCoroutine(LoadFromEcho());
    }

    public void EditorLoad()
    {
        StartCoroutine(LoadFromEcho());
    }
    IEnumerator LoadFromEcho()
    {
        if (!useCustomQuery) queryURL = Echo3DService.GetQueryURL(apiKey, secKey, entries, tags);
        else queryURL = Echo3DService.GetCustomQueryURL(apiKey, secKey, customQuery);
        yield return StartCoroutine(Echo3DService.instance.QueryDatabase(queryURL, (responseDb) =>
        {
            hologramData = responseDb;
        }));
        if (hologramData == null)
        {
            Debug.LogError("Failed to load hologram data");
            yield break;
        }

        //Do something with all entries
        onQueryLoad.Invoke(hologramData);

        //Do something with each entry
        foreach (Entry entry in hologramData.getEntries())
        {
            perEntry.Invoke(entry, queryURL);
        }


#if UNITY_WEBGL
        Debug.Log("Building for WebGL - web socket client will not initialize");
#else
        // Start Websocket client
        StartCoroutine(Echo3DService.instance.WebsocketClient(this));
#endif

        void OnGUI()
        {
            Debug.Log("GUI");

        }
    }

}