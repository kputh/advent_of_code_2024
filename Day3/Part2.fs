module AdventOfCode2024.Day3.Part2

open System.Text.RegularExpressions
open AdventOfCode2024.Day3.Part1

let extractPairs2 input =
    let strippedInput1 =
        Regex.Replace(input, "don't\(\)\S+?do\(\)", "", RegexOptions.RightToLeft)

    let strippedInput2 =
        Regex.Replace(strippedInput1, "don't\(\)\S+$", "", RegexOptions.RightToLeft)

    extractPairs strippedInput2

let processInput2 (input: string) =
    input.Trim() |> extractPairs2 |> List.sumBy (fun (l, r) -> l * r)
