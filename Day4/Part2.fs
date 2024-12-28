module AdventOfCode2024.Day4.Part2

open AdventOfCode2024.Day4.Part1

let findMatchesInArray (pattern: char array) arr =
    let patternMidpointOffset = Array.length pattern / 2
    let reversePattern = Array.rev pattern

    arr
    |> Array.windowed (Array.length pattern)
    |> Array.indexed
    |> Array.filter (fun (_index, window) -> pattern = window || reversePattern = window)
    |> Array.map (fun (index, _window) -> index + patternMidpointOffset)

let findMatchesInColumns pattern grid =
    let processRow rowIndex row =
        row
        |> findMatchesInArray pattern
        |> Array.map (fun rowMatches -> (rowIndex, rowMatches))

    grid
    |> Array.transpose
    |> Array.mapi processRow
    |> Array.concat
    |> Array.map (fun (x, y) -> (y, x))

let findMatchesInMainDiagonal pattern grid =
    let columnCount = grid |> Array.head |> Array.length

    grid
    |> rotateGrid
    |> findMatchesInColumns pattern
    |> Array.map (fun (x, y) -> (x, (y - x + columnCount) % columnCount))

let findCounterdiagonalMatches pattern grid =
    let maxIndex = Array.length grid - 1

    grid
    |> Array.rev
    |> findMatchesInMainDiagonal pattern
    |> Array.map (fun (x, y) -> (maxIndex - x, y))

let findXMatches pattern grid =
    let matchesInMainDiagonal = findMatchesInMainDiagonal pattern grid |> Set.ofArray

    let matchesInCounterdiagonal =
        findCounterdiagonalMatches pattern grid |> Set.ofArray

    Set.intersect matchesInMainDiagonal matchesInCounterdiagonal
