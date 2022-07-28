# Unity-SDK
Thanks for downloading the echo3D Unity SDK package!

## Installing the echo3D SDK

### Embedded Vs Local Packages
A local unity package exists somewhere on your computer outside of your project folder. An embedded package exists within your project `Packages` folder. We recommend installing our package as an embedded package for each project unless you are confident you need a local package installation.

### Installing as an embedded package
1. Within your unity project folder, create a `Packages` folder if one does not exist already
2. Download the echo3D SDK from echo3D here. Unzip the file into your `Packages` directory so that it looks like `YourProject/Packages/co.echo3d.unity/`
3. Open your project in Unity (or switch focus to Unity if it is already open). Unity will automatically import the package and its dependencies

## Configuring your scene and gameobjects
Go to `Packages/echo3D Unity SDK/Prefabs` to see the two primary prefabs used by the SDK.

`Echo3DService` is a singleton prefab which handles streaming and instantiation for any scene holograms. Place this prefab in any scene where echo3D holograms will be loaded. This prefab is set to `DoNotDestroyOnLoad` by default to persist through scene loading.

`Echo3DHologram` is a plain old gameobject with the `Echo3DHologram.cs` script attached. This represents the most basic way to load holograms from the platform:
1. Set the `apiKey` and `secKey` values (All echo3D accounts have a security key enabled by default, view the setting here)
2. By default, the `Echo3DHologram.cs` script will query the `apiKey` and load all holograms that belong to that project. To load one or multiple specific holograms from a project, input the hologram entryID found in the echo3D web console into `Entries`. Separate multiple ids with `,`. 

## Default Behavior
Any gameobjects with properly configured `Echo3DHologram.cs` scripts will automatically load their holograms when their gameobject is created via the script `Start()`

##EXPERIMENTAL - Editor Preview
The echo3D SDK allows for holograms to be streamed at design time (ie, before pressing Play) to aid with composing your scene. 

1. Check the `Editor Preview` box in the inspector to designate the object for editor preview
2. From the top menu, go to Echo3D -> Load In Editor. All objects within your scene with `Editor Preview` checked will load into the scene. 
3. To clear holograms, go to Echo3D -> Clear Holograms. 

By default, previewed holograms will not reload when playing the scene. To force a previewed hologram to reload on play, check `Reload on Play`. All holograms are loaded at runtime in application builds. 

### Known Issues
- WebGL builds will fetch and apply hologram metadata on app start but will not respond to live console metadata changes (Scale, offset, rotation, etc) at runtime.


