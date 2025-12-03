using System;

namespace Shared.DTOs;

public class AlgorithmKeyRequiredDTO
{
    public required string key { get; set; }
    public required string iv { get; set; }
    public required string salt { get; set; }
}
