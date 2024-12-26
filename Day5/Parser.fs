module AdventOfCode2024.Day5.Parser

let parseOrderingRulesAndUpdates (input: string) =
    let lines = input.Trim().Split("\n")

    let filterAndParseLines separator (lines: string array) =
        lines
        |> Array.filter (Seq.contains separator)
        |> Array.map _.Split(separator)
        |> Array.map (Array.map int)

    let rules =
        lines
        |> filterAndParseLines '|'
        |> Array.map (fun pageNumbers -> (pageNumbers[0], pageNumbers[1]))

    let updates = lines |> filterAndParseLines ','

    (rules, updates)
