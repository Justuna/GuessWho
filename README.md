# Guess Who

## Register
Don't have an API key? Make sure to register for FREE at [echo3D](https://www.echo3d.co/).

## Versions
- Unity 2020.3.22f1

## How to Play
Behind the red panel is one of your echo3D models! You have 3 guesses (plus one FINAL guess) to figure out which one it is.

For the first 3 guesses, you can guess either names or properties. Guessing a name correctly wins the game! Guessing a property correctly reveals the property in green on the left-hand side. Guessing the wrong name or property reveals it in red on the left-hand side.

If you're not sure what models are available or what properties they have, click on the gallery to browse all of the models that the game picked from, and read what properties they have.

## Setup
1. After downloading the project, open up the GuessWhoProject folder in Unity as a project.
2. If the SampleScene scene is not already open, open it.
3. In the GameManager object, replace any fields named API key with the API key with your own.
4. In the echo3D console, add whatever models you want to play the game with. Make sure they have the following keys:
   - tags: guesswho ***(This tells the game that this model is in play)***
   - name: ***This can be whatever name or names (if it has multiple aliases) you want! Names should be separated by spaces; names with spaces should use underscores.***
   - guesswho: ***This should be the list of properties (separated by spaces) that the model has. Things like color, type of object, etc.***
5. Press Play to try it out in the editor! It may take a little bit of time to load, though.

## Screenshots
![image](https://user-images.githubusercontent.com/79558246/173201642-446feb78-1a99-4302-ba95-9424ae442ac8.png)
![image](https://user-images.githubusercontent.com/79558246/173201650-4a738335-14b4-4906-9370-230fdf0cd90b.png)
![image](https://user-images.githubusercontent.com/79558246/173201661-5811a052-74bc-4ede-88e5-e8cebff85874.png)
![image](https://user-images.githubusercontent.com/79558246/173201696-1ee82f7c-4913-44a9-a80a-ca4fd16e664c.png)
