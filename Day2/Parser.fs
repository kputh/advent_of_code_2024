module AdventOfCode2024.Day2.Parser

let parseListOfListsOfIntegers (input: string) =
    let parseLine (line: string) =
        line.Split(" ") |> Array.toList |> List.map int

    input.Trim().Split("\n") |> Array.toList |> List.map parseLine
