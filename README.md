Playable on [itch.io](https://ika-bits.itch.io/exploring-alda-gardens)

---

## Project Structure

Scripts are split into two mirrored halves — `Logic/` (game rules, data, decisions) and `Visual/` (input, UI, presentation) — that never reference each other directly.

- **`LogicManager`** and **`VisualManager`** are the only two classes allowed to cross that boundary; everything else routes through them.
- Each gameplay system (Player, Interaction, Crafting, Quest, Inventory, Popups) has a matching manager pair — e.g. `CraftingSystemManager` (Logic) / `CraftingVisualManager` (Visual) — that its own scripts talk to, instead of reaching into another system or a hub directly.
- `Data/` holds ScriptableObject definitions (`ItemDataSO`, `Recipe`, `Quest`, etc.), read directly by both sides since it's static content, not behavior.
- `Common/` (shared utilities) and `Debug/` (dev-only tools) sit outside the split entirely.

**Rule of thumb**: input handling and UI go in `Visual/`, game logic goes in `Logic/`, and the two only ever talk through their system's manager pair.

### Exceptions to the split

- **`Chest`/`Plot`** — small, single-file systems (`ChestContext`, `PlotStateContext`) that just toggle their own serialized GameObject's visibility (a chest lid, a patch of growing grass). Too small to justify a manager pair — the "visual" effect is a one-line `SetActive` call made directly from Logic.

---

**Copyright © 2026 Ishara MacDonald. All rights reserved.**

This repository is publicly available for viewing and evaluation.

No permission is granted to reproduce, distribute, modify, or commercially use this code or the game's assets without explicit permission.
