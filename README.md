# PgBrewer

Helps managing beers and other liquors one can make in the [Project: Gorgon](https://projectgorgon.com/) MMORPG.

## Game mechanics

For a complete up-to-date description of the associated game mechanics, please read this [wiki entry about brewing](http://wiki.projectgorgon.com/wiki/Brewing). The rest of this document assumes you're familiar with the game and what's in that page.

## Application content

This application includes:

* Beers (from Basic Lager to Dwarven Stout), Liquors (from Potato Vodka to Bourbon).
* Each alcohol has a line for each combination of materials used to make it, as follow:
  + The materials ordered from first to last.
  + The associated effect.
    + If the effect is grayed, it means it's calculated automatically using material associations as defined in settings.
	+ If the effect begins with (?) it has not been confirmed with an in-game screenshot.
  + A red question mark if the calculated effect and the manually selected effect don't match. The top of the page counts how many mismatches have been found.
  + A button to clear the manual selection.
* Settings
  + Each material can be sssociated to another used in a higher tier. For example, Cinnamon can be associated to Honey, Mint or Juniper Berries.
  + Some associations work for all alcohols, others only for beers or liquors. Check the group label. Ex: *Flavors #1 for Beer* and *Flavors #1 for liquor*.
* A **Save** button that saves changes in the Windows registry.
* An **Export...** button to export settings in a .cvs file.
* An **Import...** button to import settings in a .cvs file. Use it if you have installed the application on different computers, or to archive your settings (recommended!)
* A back ![Back](/PgBrewer/Resources/Undo.png?raw=true "Back button") and ![Forward](/PgBrewer/Resources/Redo.png?raw=true "Forward button") button.
  + To use these buttons you must be on the page corresponding to an alcohol that has previous or next tier. Ex: Potato Vodka has a next tier: Applejack. Marzen doesn't have a previous nor a next tier.
  + Select the line corresponding to a particular combination of materials.
  + Click *Back* or *Forward* to move the selection to the line that would produce the lower or higher tier (respectively) of the same effect. This can only be done if all associated materials are set in the **Settings** page.

## Invalid effects

If you notice an effect that has the wrong name, the wrong type or wrong values, please [open an issue](https://github.com/dlebansais/PgBrewer/issues) and post a screenshot. **Make sure you didn't craft the beer or liquor under the influence of a tier-modifying effect**.

# Screenshots

Note: icons are copyright © 2022, Elder Game, LLC. They will show up only if you download them with the [PgJsonParse](https://github.com/dlebansais/PgJsonParse) program.

![Main Screen](/Screenshots/MainScreen.png?raw=true "The app main screen")

# Certification

This program is digitally signed with a [CAcert](https://www.cacert.org/) certificate.
