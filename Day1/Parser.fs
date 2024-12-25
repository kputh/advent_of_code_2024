module AdventOfCode2024.Day1.Parser

let parsePairsOfIntegers (input: string) =
    let lines = input.Trim().Split("\n")
    let words = lines |> Array.toList |> List.map _.Split("   ")

    let getColumn n =
        List.map (fun a -> Array.get a n |> int)

    let firstColumn = getColumn 0 words
    let secondColumn = getColumn 1 words
    (firstColumn, secondColumn)
