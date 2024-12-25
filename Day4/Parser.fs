module AdventOfCode2024.Day4.Parser

open System.Text.RegularExpressions

let parseCharGrid (input: string) =
    let processLine line =
        Regex.Replace(line, "\s+", "") |> Seq.toArray

    input.Trim().Split("\n") |> Seq.toArray |> Array.map processLine
