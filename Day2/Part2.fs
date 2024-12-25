module AdventOfCode2024.Day2.Part2

open AdventOfCode2024.Day2.Part1

let isSafeEnoughReport report =
    report
    |> List.mapi (fun index _ -> List.removeAt index report)
    |> List.exists isSafeReport
