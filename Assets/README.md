\# Physics Puzzle — Unity Assignment



\## How to Run

1\. Open in Unity 6000.3.11f1 (URP)

2\. Open scene: `Assets/Scenes/PuzzleLevel.unity`

3\. Press Play



\## Controls

| Key | Action |

|-----|--------|

| WASD | Move |

| Space | Jump |

| E | Interact (lever) |

| R | Reset puzzle |



\## Puzzle Solutions

The puzzle is intentionally solvable two ways:

\- \*\*Path A (Lever first):\*\* Activate the lever → ride the moving platform across → win

\- \*\*Path B (Box first):\*\* Push the heavy box onto the pressure plate → gate opens → cross



\## System Architecture



\### Core Interfaces

\- `IInteractable` — any object that changes state and fires `OnStateChanged`

\- `IReceiver` — any object that responds to `OnActivated()` / `OnDeactivated()`

\- `IResettable` — any object the ResetManager can restore to initial state



\### How to add a NEW interactable (e.g. a timed trigger)

1\. Create a class that extends `InteractableBase`

2\. Call `SetState(true/false)` when your trigger fires

3\. That's it — the InteractionConnector handles the rest



\### How to add a NEW receiver (e.g. a spinning blade)

1\. Create a class that extends `ReceiverBase`

2\. Override `OnActivated()` and `OnDeactivated()`

3\. Drag it into any `InteractionConnector`'s receiver list in the Inspector



\### InteractionConnector

The connector is the "wire." It subscribes to any `IInteractable`'s `OnStateChanged` event

and calls `OnActivated()` or `OnDeactivated()` on any list of `IReceiver`s.

No code changes required to connect new types — purely data-driven via the Inspector.



\### Reset System

`ResetManager` calls `FindObjectsByType<MonoBehaviour>()` and invokes `ResetToInitial()`

on every object that implements `IResettable`. No manual registration needed.



\## Extension Examples

| New feature | What to do |

|-------------|------------|

| Timed trigger | Extend `InteractableBase`, call `SetState` on a timer |

| Elevator | Extend `ReceiverBase`, interpolate Y in `OnActivated` |

| Multi-gate puzzle | Add more objects to `InteractionConnector.receiverTargets\[]` |

| AND-logic (both plates needed) | Create a `MultiInputConnector` that checks all sources |

