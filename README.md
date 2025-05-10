# CraftSharp
A Unity package for reading and storing basic data of Minecraft.

## > Contribution
Unity packages cannot be directly opened as a project in Unity, instead you need to open it as an 'embedded package' inside a project in order to make changes. You can fork the project, and clone the fork into <code>SomeUnityProject/Packages/com.devbobcorn.craftsharp</code>. Then you can open your <code>SomeUnityProject</code> from Unity Hub and find it in Unity's project explorer.

## > Usage
This package provides helper classes and modules for reading and storing basic data types of Minecraft, such as blocks, blockstates, items, and biomes.

To add this package as a dependency of your project, open Package Manager window in Unity, click the '+' symbol on upper-left corner, select 'Add package from git URL', and then use '[https://github.com/DevBobcorn/CraftSharp.git]()' (with the '.git' suffix) as the target url.

You can refer to [CornCraft](https://github.com/DevBobcorn/CornCraft) or [CornModel](https://github.com/DevBobcorn/CornModel) to see an example of using this package.

## > License
Most code in this repository is open source under CDDL-1.0, and this license applies to all source code except those mention their author and license or with specific license attached.

Some other open-source projects/code examples are used in the project, which use their own licenses. Here's a list of them:
* [Minecraft-Console-Client](https://github.com/MCCTeam/Minecraft-Console-Client) (Json Parser code)

The full CDDL-1.0 license can be reviewed [here](./LICENSE.md).