<!--
ColorShapeLinks AI 2020 (c) by Nuno Fachada

ColorShapeLinks AI 2020 is licensed under a Creative Commons
Attribution-NonCommercial-ShareAlike 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc-sa/4.0/>.
-->

# ColorShapeLinks AI competition

This competition has been accepted for the [IEEE CoG 2020] conference and is
officially open!

* [Implementation guide][APIDocs] and
  [tutorial video](https://www.youtube.com/watch?v=ELrsLzX3qBY)
* [Daily standings][Standings]

## Description

ColorShapeLinks is an AI competition for the [Simplexity] board game with
arbitrary game dimensions. The first player to place *n* pieces of the same
type in a row wins. In this regard, the base game, with a 6 x 7 board and
_n_ = 4, is similar to [Connect Four]. However, pieces are defined
not only by color, but also by shape: round or square. Round
or white pieces offer the win to player 1, while square or red pieces
do the same for player 2. Contrary to color, players start the game with
pieces of both shapes. This means that a less observant player
can lose in its turn, especially since shape has priority over color as a
winning condition. Given this fact, as well as the arbitrary game dimensions,
the challenges for the AI, namely at the level of the heuristic evaluation
function, are multifold.

The AI must be implemented in C# ([.NET Standard 2.0]) by extending
[one class][`AbstractThinker`] and overriding [one method][`Think()`].
The development framework includes both console and [Unity] frontends and can
be downloaded with the following command (requires [Git] and [Git LFS]):

`git clone --recurse-submodules https://github.com/VideojogosLusofona/color-shape-links-ai-competition.git`

The [implementation guide][APIDocs] contains the required documentation for
developing an AI for ColorShapeLinks.

## The competition

### Tracks

The competition runs on two distinct tracks:

1. The **Base Track** competition will be played using standard [Simplexity]
   rules (6x7 board, 4 pieces in a row for victory) and with a time limit of
   0.2 seconds. Only one processor core will be available for the AIs.
2. The **Unknown Track** competition will be played on a multi-core
   processor under conditions that will only be revealed after the competition
   deadline. These conditions will be derived from the first [EuroMillions]
   draw that takes place after the [final submission
   deadline](#important-dates), most likely at August 18, 2020, as follows:
   * NumberOfRows = Lowest [EuroMillions] ball number higher than 6
     (ascending order).
   * NumberOfCols = Next [EuroMillions] ball (ascending order).
   * WiningSequenceLength = Ceil(NumberOfRows / 2.0).
   * InitialNumberOfRoundPiecesPerPlayer = Floor(NumberOfRows * NumberOfCols / 4.0)
   * InitialNumberOfSquarePiecesPerPlayer = Ceil(NumberOfRows * NumberOfCols / 4.0)
   * TimeLimit (milliseconds) = 25 * Max(NumberOfRows, NumberOfCols)

### Standings

All AIs will play against each other two times, so each AI has the opportunity
to play first. Players will be awarded 3 points per win, 1 point per draw and
0 points per loss. The [standings] for each track will be based
on the total number of points obtained per AI, sorted in descending order.

Tie-breaks are performed only when there are two or more AIs with the same
points in first place. In such cases, ties are solved according to the
following criteria (from most to least important):

1. Greatest number of points obtained in the matches between AIs with the same
   points.
2. Perform a tie-break tournament between the AIs still tied with the time
   limit doubled. Repeat the process at most 10 times until the tie is broken.
3. Perform a tie-break tournament between the AIs still tied with the time
   limit halved. Repeat the process at most 10 times until the tie is broken.
4. If the tie persists, the AIs are considered officially tied and *ex aequo*
   winners of the competition.

Standings for the base track and two other test configurations are updated
daily and available [here][standings]. These offer a good idea of how each AI
is faring, but may not entirely reflect the end results for the base track,
since: i) the actual competition will run on a slightly more powerful
computer, with possibly newer software (e.g., a more recent OS kernel
or .NET Core version); ii) after the [first deadline](#important-dates),
submissions may require changes in order to
[comply with the rules](#rules-for-the-ai-code), which may impact their
performance.

### Submissions

AIs should be submitted via email to
[colorshapelinks@ulusofona.pt](mailto:colorshapelinks@ulusofona.pt). Only one
AI is allowed per team, but multiple submissions are encouraged (within
reasonable frequency) as the AI is being developed and improved. A person may
only be affiliated to one team, except for teachers or supervisors of student
teams. Upon submission, the submitted code is:

1. Superficially checked for [rule](#rules-for-the-ai-code) compliance,
   though not otherwise studied or analysed. Thorough tests will be performed
   only after the [first deadline for AI submissions](#important-dates).
2. Added to the automated competitions, the [standings] of which are
   updated daily.

All submitted codes are considered private and will not be
shared, discussed or analyzed by the organization before the competition
deadline. After this deadline, all the submitted codes will be made public at
this repository. If code authors do not specify a
[valid open source license][ossl] upon submission, the organization will make
the code available under the [Mozilla Public License 2.0][MPLv2].

### Important dates

* **May 22** _(optional)_ - [IEEE CoG competition papers deadline], e.g., for
  participants that wish to submit a paper about their entry for this
  competition.
* **July 31** - First deadline for AI submissions. Submissions will be
  thoroughly checked for [rule compliance](#rules-for-the-ai-code) and
  technical issues.
* **August 7** - Author notification of any issues encountered with the submissions.
* **August 14** - Final deadline for AI submissions + short description of the
  methods used for implementing the AI.
* **August 18** - [EuroMillions] draw which will determine the conditions for
  the [Unknown track](#tracks).
* **August 24-27** - [IEEE CoG 2020] takes place, final competition
  results are announced.

## Rules

### Rules for the AI code

- Can only use cross-platform [.NET Standard 2.0] API calls in C#.
- Can use additional libraries which abide by these same rules.
- Both the AI code and libraries must be made available under a
  [valid open source license][ossl], although AI codes can be open-sourced
  only after the competition deadline.
- Must run in the same process that invokes it.
- Can be multithreaded and use [`unsafe`] contexts.
- Cannot *think* in its opponent time (e.g., by using a background thread).
- Must acknowledge [cancellation requests][`CancellationToken`], in which case
  it should terminate quickly and in an orderly fashion.
- Can only probe the environment for the number of processor cores. It cannot
  search or use any other information, besides what is already available in the
  [`AbstractThinker`] class or passed to the [`Think()`] method, e.g., such as
  using reflection to probe the capabilities of its opponents.
- Cannot use and/or communicate with external data sources.
- Cannot use more than 2GB of memory during the course of a match.
- Must have a reasonable size in disk, including libraries. For example,
  source code, project files and compiled binaries should not exceed 1 Mb.
- The same [setup] parameters should be used for both [tracks](#tracks).

### General competition rules

- Submissions automatically compete in both tracks.
- A person may only be affiliated to one team, except for teachers or
  supervisors of student teams.

## Resources and guides

* [Implementation guide][APIDocs]
* [Classification and Results for the Base Track (updated daily)][standings]

## Licenses

The code is made available under the [Mozilla Public License 2.0][MPLv2].
All the text and documentation (i.e., non-code files) are made available under
the [Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International
License][CC BY-NC-SA 4.0].

## Organization

* [Nuno Fachada], [HEI-Lab], [Lusófona University][ULHT], Portugal
* Contact us by [e-mail](mailto:colorshapelinks@ulusofona.pt) or open
  [an issue](https://github.com/VideojogosLusofona/color-shape-links-ai-competition/issues)

[Git]:https://git-scm.com/downloads
[Git LFS]:https://git-lfs.github.com/
[APIDocs]:https://videojogoslusofona.github.io/color-shape-links-ai-competition/docs/html/index.html
[MPLv2]:https://opensource.org/licenses/MPL-2.0
[CC BY-NC-SA 4.0]:https://creativecommons.org/licenses/by-nc-sa/4.0/
[licvideo]:https://www.ulusofona.pt/en/undergraduate/videogames
[IEEE CoG 2020]:http://ieee-cog.org/2020/
[Nuno Fachada]:https://github.com/fakenmc
[ULHT]:https://www.ulusofona.pt/
[Simplexity]:https://boardgamegeek.com/boardgame/55810/simplexity
[Connect Four]:https://www.boardgamegeek.com/boardgame/2719/connect-four
[EuroMillions]:https://www.euro-millions.com/
[.NET Standard 2.0]:https://docs.microsoft.com/dotnet/standard/net-standard
[Unity]:https://unity.com/
[ossl]:https://opensource.org/licenses
[`unsafe`]:https://docs.microsoft.com/dotnet/csharp/programming-guide/unsafe-code-pointers/
[`CancellationToken`]:https://docs.microsoft.com/dotnet/api/system.threading.cancellationtoken
[standings]:https://videojogoslusofona.github.io/color-shape-links-ai-competition/standings
[HEI-Lab]:http://hei-lab.ulusofona.pt/
[.NET Core]:https://dotnet.microsoft.com/download
[`AbstractThinker`]:https://videojogoslusofona.github.io/color-shape-links-ai-competition/docs/html/class_color_shape_links_1_1_common_1_1_a_i_1_1_abstract_thinker.html
[`Think()`]:https://videojogoslusofona.github.io/color-shape-links-ai-competition/docs/html/class_color_shape_links_1_1_common_1_1_a_i_1_1_abstract_thinker.html#ac8039cba1e4ececb04322fb8e7610f0e
[setup]:https://videojogoslusofona.github.io/color-shape-links-ai-competition/docs/html/thinker-implementation-guide.html#autotoc_md3
[IEEE CoG competition papers deadline]:http://www.ieee-cog.org/2020/cfp
