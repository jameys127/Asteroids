# Asteroids

A recreation of the retro arcade classic *Asteroids*, built in Unity with C#. This was my first game dev project, and all of the art was drawn by me on my iPad.

<!-- Add a screenshot or GIF of gameplay here, e.g.:
![Gameplay](screenshots/gameplay.gif)
-->

## Download & Play

Grab the latest build from the [Releases](https://github.com/jameys127/Asteroids/releases) page.

## Controls

| Action | Keys |
| --- | --- |
| Thrust | `W` / `Up Arrow` |
| Rotate | `A` `D` / `Left` `Right` Arrows |
| Shoot | `Space` |
| Pause | `Esc` |

## Features

- **Classic flight controls:** the ship only thrusts forward and has real inertia, building speed while thrusting and drifting to a stop when you let go. Everything wraps around the edges of the screen.
- **Splitting asteroids:** large asteroids break into two smaller, faster pieces when shot.
- **Escalating waves:** each cleared wave brings faster asteroids, with more of them every few waves.
- **Enemy UFOs:** starting on wave 2, UFOs fly across the screen and fire green lasers aimed at your ship, shooting more often as the game goes on.
- **Three lives:** after a crash, the ship respawns with a brief window of invulnerability.
- **Arcade-style leaderboard:** enter your name after a run, and the top 10 scores are saved locally between sessions.

## Scoring

| Target | Points |
| --- | --- |
| Large asteroid | 50 |
| Small asteroid | 100 |
| UFO | 200 |

## Built With

- Unity

## Save Data

High scores are stored as `leaderboard.json` in:

```
%USERPROFILE%\AppData\LocalLow\RustyGames\Asteroids\
```

Delete the file to reset the leaderboard.
