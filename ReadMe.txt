Hp2BaseMod V1.0

--Contributors--
onesuchkeeper - Benevolent Dictator for Life, design, programming, research, testing
Ravenlorde - testing

--Description--
Hello! Welcome to the Huniepop 2 base mod. On it's own this mod doesn't affect gameplay, but it does provide handling for other mods to be loaded! Drop dependent mods in the mods folder located in the root directory and they will be loaded on execution of Huniepop 2. The mods folder will not be there by default, either add it manually or run Hunniepop 2 with Hp2BaseMod installed and it will be added for you.

--To Install--
To install, place the contents of the 'Root' folder in your installation of Huniepop 2's root, where 'HuniePop 2 - Double Date.exe' is located. Mods based on the Hp2BaseMod can be placed in the 'mods' folder and will be run on startup.

--Development--
Want to develop a mod using Hp2BaseMod? Great! Check out Hp2SampleMod which a simple mod example. Take a look at it and make sure it read its readme file.

The Hp2BaseMod uses .Net Standard 2.1

All Hp2BaseMods must follow a few rules:
	1) They shall be contained in a subfolder of the mods folder to be seen by the base mod.
	2) They shall contain a C# library dll implementing a IHp2BaseModStarter from Hp2BaseMod.Interface.dll
		a) All IHp2BaseModStarter.Start in a library and referenced libraries will be run. I advise having only one. If you have multiple make sure they won't be calling one another causing a loop. Make sure your Start isn't referenced in multiple libraries with starters causing it to be run multiple times (unless that's what you intend).
	3) They shall have a file named Hp2BaseMod.config with 2 elements separated by a comma on it's own line for every dll with a starter you wish to have run. The elements are:
		a) The path to the dll containing your IHp2BaseModStarter
		b) An int representing your starter's loading priority (mods will be loaded lowest value to highest)
		Example: mods/myMod/myMod.dll,0

Loading priority is a tool that gives you control of when your starter runs. If you're changing every girl's name to, "BonJovi", you should probably wait until all mods adding new girls have run so you don't miss any. To help with this, may I present the Keeper Decimal System, or the KDS. The KDS works much like the Dewey Decimal System. Choose a priority in the ranges provided based on what your starter is trying to do. I highly encourage you to use KDS to promote compatibility across all mods. If you make a mod, please contribute it's starting decimals to the KDS documentation - that way other modders can see what decimals are being used and plan theirs accordingly. For more information on the KDS, please see the documentation: https://docs.google.com/document/d/1obP3KF8KTQwmU500Lu8GODqAqSOdgQ8LKIOs6ztlrRk/edit?usp=sharing
