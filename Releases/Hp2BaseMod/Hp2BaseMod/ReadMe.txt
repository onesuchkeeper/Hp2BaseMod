Hp2BaseMod v0.3, 4/11/2021

--Contributors--
onesuchkeeper - Benevolent Dictator for Life, design, programming, research, art
Ravenlorde - beta testing

To help with development, please consider supporting me on patreon - https://www.patreon.com/onesuchkeeper

--Description--
Hello! Welcome to the Huniepop 2 base mod. On it's own this mod doesn't affect gameplay, but it does provide handling for other mods to be loaded! Drop dependent mods in the mods folder located in the root directory and they will be loaded on execution of Huniepop 2. This comes with Hp2BaseModTweaks installed by default. The mod is in the mods folder and is safe to remove. This is a beta release. 

This mod requires an installation of Huniepop 2: Double Date. No part of Huniepop 2 is redistributed with this mod, it is entierly additive and can be safely removed with no lasting changes to the installation. Mods loaded by this mod may have lasting changes of their own. Be careful what mods you load, make sure to create backups.

--To Install--
To install, place the contents of the 'Root' folder in your installation of Huniepop 2's root, where 'HuniePop 2 - Double Date.exe' is located. Mods based on the Hp2BaseMod can be placed in the 'mods' folder and will be run on startup.

--Development--
Want to develop a mod using Hp2BaseMod? Great!

THIS IS A BETA RELEASE, several changes are planned for the future, and if you start developing a mod it may break. I'd wait for a more complete version but you do you.

The Hp2BaseMod uses .Net Standard 2.0

All Hp2BaseMods must follow a few rules:
	1) They shall be contained in a subfolder of the mods folder to be seen by the base mod.
	2) They shall contain a C# library dll implementing a IHp2BaseModStarter from Hp2BaseMod.dll
		a) All IHp2BaseModStarter.Start in a library will be run. I advise having only one. If you have multiple make sure they won't be calling one another causing a loop.
		b) The start function accepts a GameDataMod as a parmiter. It can be used for simple additions to the GameData. WIP in this beta.
	3) They shall have a file named Hp2BaseMod.config with 2 elements separated by a comma on it's own line for every dll with a starter you wish to have run. The elements are:
		a) The path to the dll containing your IHp2BaseModStarter
		b) An int representing your starter's loading priority (mods will be loaded lowest value to highest)
		Example: mods/myMod/myMod.dll,0

Loading priority is a tool that gives you control of when your starter runs. If you're changing every girl's name to, "BonJovi", you should probably wait until all mods adding new girls have run so you don't miss any. To help with this, may I present the Keeper Decimal System, or the KDS. The KDS works much like the Dewey Decimal System. Choose a priority in the ranges provided based on what your starter is trying to do. I highly encourage you to use KDS to promote compatibility across all mods. If you make a mod, please contribute it's starting decimals to the KDS documentation - that way other modders can see what decimals are being used and plan theirs accordingly. For more information on the KDS, please see the documentation: https://docs.google.com/document/d/1obP3KF8KTQwmU500Lu8GODqAqSOdgQ8LKIOs6ztlrRk/edit?usp=sharing
