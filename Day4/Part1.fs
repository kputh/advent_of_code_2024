module AdventOfCode2024.Day4.Part1

let countMatchesInArray (pattern: char array) arr =
    arr
    |> Array.windowed (Array.length pattern)
    |> Array.filter (fun window -> pattern = window)
    |> Array.length

let countMatchesInReverseArray pattern arr =
    arr |> Array.rev |> (countMatchesInArray pattern)


let countPatternHorizontally pattern grid =
    let countPatternInRow pattern row =
        countMatchesInArray pattern row + countMatchesInReverseArray pattern row

    Array.sumBy (countPatternInRow pattern) grid

let countPatternVertically pattern grid =
    grid |> Array.transpose |> countPatternHorizontally pattern

let rotateGrid grid =
    let splitAndFill value index arr =
        let front, back = Array.splitAt index arr

        let frontFill = Array.create index value
        let backFill = Array.create (Array.length arr - index) value

        Array.concat [ frontFill; front; back; backFill ]

    grid |> Array.mapi (splitAndFill '.')

let countPatternDiagonally pattern grid =
    let countPatternAlongMainDiagonal pattern grid =
        grid |> rotateGrid |> countPatternVertically pattern

    let mainDiagonalCount = grid |> countPatternAlongMainDiagonal pattern

    let counterdiagonalCount = Array.rev grid |> countPatternAlongMainDiagonal pattern

    mainDiagonalCount + counterdiagonalCount


let countPatternInAllDirections pattern grid =
    countPatternHorizontally pattern grid
    + countPatternVertically pattern grid
    + countPatternDiagonally pattern grid
