# Almalingua

<img src="https://github.com/dartmouth-cs98/21f-lingo-defense/blob/main/Images/Team_Photo.png" alt="Team_Photo" width="700"/>

Almalingua is a role-player game focused on practicing Spanish. Users enter the world of Almalingua as a wizard stuck in the wrong dimension. Our wizard must practice an familiar language and help out the village's inhabitants in order to learn the spell to go home.  

Almalingua is a proof-of-concept for a style of language-learning gameplay which emphasizes roleplaying and language expsoure rather than memorization or grammar. We did this based on our research into [language-learning games](https://github.com/dartmouth-cs98/almalingua/wiki/State-Of-The-Art-Research) and [language curriculum design](https://github.com/dartmouth-cs98/almalingua/wiki/Language-Curriculum-Design), which emphasized these characteristics.

## Architecture

Almalingua was built in Unity Engine.

### System Design

* *Scenes and UI*
  * The UI was all controlled under a single "MainUI" prefab added into every scene
  * Each region of the map (farm, forest, etc.) had its own Unity scene.
  * Words and their definitions were stored in the Streaming Assets folder as JSONs. Anytime the dialogue window or quest UI display loaded in new text, each word was checked against our word-bank, and a) linked to the dictionary, and b) linked to an icon, if the image had one.
  * The Dictionary prefab in the UI managed the display of words. 
  * The SceneLoader prefab/script and Portal prefab were used to manage navigations between scenes.
* *Conversations*
  * Conversation objects stored the dialogue of each NPC. See dialogue editor docs [here](https://github.com/dartmouth-cs98/almalingua/wiki/Dialogue-Editor) for more information on set-up.
  * NPCDialogueManager and NPCDialogueUI scripts manage the display of NPC text and the management of NPC dialogue trees / NLP calls, respectively.
* The CombatScripts folder contains scripts used in the combat scene.
* The quests were broken down into "Quest" and "QuestStep", both of which were saved in Unity's PlayerPrefs. 
* Special events, like the melting of the wand, are handled as Unity Events. Read about our events [here](https://github.com/dartmouth-cs98/almalingua/wiki/Unity-Events). 

### LUIS API

We used Microsoft's Luis.API for the natural-language processing condcted when users speak to NPCs:
<img width="700" alt="Screen Shot 2022-03-12 at 4 48 53 PM" src="https://user-images.githubusercontent.com/75859468/158036324-7f475ae1-1832-41f5-8cb2-fb3d921c0315.png">
<img width="700" alt="Screen Shot 2022-03-12 at 4 49 58 PM" src="https://user-images.githubusercontent.com/75859468/158036320-d1f6ad8f-42ca-44fb-bebb-81e85fdcfd5d.png">
(Screenshots of our Luis set-up).

### Pixel Art Resources

Much of our art was made ourselves, using:

* Photoshop 
* Illustrator 
* [Pixel Art Maker](http://pixelartmaker.com/)

We would also like to credit art we used from packs, including:

* FARMLANDIA: BASIC FRUIT PACK (by Helm3t) (from Itch.io) [here](https://helm3t.itch.io/farmlandia-fruit)
* Rocky Roads by essssam [here](https://essssam.itch.io/rocky-roads)
* Crafting Materials by beast-pixels [here](https://beast-pixels.itch.io/crafting-materials)
* Farm Fun by strelllka [here](https://strelllka.itch.io/farm-fun-rpg-asset-pack)
* pixelfood by ghostpixxells [here](https://ghostpixxells.itch.io/pixelfood)
* 4000+ Simple Icon Pixel Pack by sum studio [here](https://assetstore.unity.com/packages/2d/textures-materials/4000-simple-pixel-icon-pack-137666)
* Modern Interiors by limezu [here](https://limezu.itch.io/moderninteriors)
* Pixel Item by henrysoftware [here](https://henrysoftware.itch.io/pixel-item)

## Setup

Our GitHub repository should already be set up to work with Unity, but in general, [here](https://gist.github.com/j-mai/4389f587a079cb9f9f07602e4444a6ed) are helpful Unity/Git tips, which we followed when creating this repository.

To use our Unity project:
* Install Git-LFS if you do not have it already (instructions [here](https://git-lfs.github.com/))
* Download the Unity Hub app, and download Unity Version 2020.3.20f1
* Clone our repository
* In the "CS98" folder (which is actually the Unity project, because the overall repo has some non-Unity parts), add a `.env` file with the following:
  ```
  LUIS_APP=[LUIS_APP_STRING]
  LUIS_SUB_KEY=[LUIS_SUB_KEY]
  ```
  To use our repository's LUIS API key, contact @ray-hc. 
* Open the Unity project!
<img width="700" alt="Screen Shot 2022-03-12 at 5 04 09 PM" src="https://user-images.githubusercontent.com/75859468/158036632-042b5e6b-f7fe-4f9b-9634-1b60cb537a93.png">

## Testing

All gameplay is organized based on the quest & quest-step number! In order to test a scene, you may want to set your quest/quest-step number manually. Each scene has a "QuestSetter" object which can be enabled for this purpose. Note that in order to clear your quest/quest-step # between plays, you must go to "Edit" -> "Clear PlayerPrefs" in Unity. 

Pressing Escape should cause the game to restart and the quest/quest-step to reset (not good UX for a published game, but good for Technigala and testing).

## Deployment

Assuming that you have set-up Unity correctly, you should be able to deploy by selecting "Build and Run..." under "File" after opening the Unity project.

## Bugs

We were never able to 100% figure out why, but sometimes if you stand too close to the wand-frozen-in-ice at the end of step 1, you can't actually pick up your wand. This has to do with the ice-melting Unity event -- if we'd had more time, we could've added a collider to keep users from getting too close.

## Authors

Celina Tala, Vivian Tran, Brandon Guzman, Ray Crist, Tim Yang, Sada Nichols-Worley

## Acknowledgments

Thank you to Tim Tregubov and the CS98 TAs <3
