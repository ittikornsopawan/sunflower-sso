using System;
using Domain.Common;

namespace Domain.Entities;

public class t_personal_info : AuditableEntity
{
    public byte[] sid { get; set; } = default!;
    public byte[] prefixName { get; set; } = default!;
    public required byte[] firstName { get; set; }
    public byte[] middleName { get; set; } = default!;
    public required byte[] lastName { get; set; }
    public byte[] nickName { get; set; } = default!;
    public byte[] gender { get; set; } = default!;
    public byte[] dateOfBirth { get; set; } = default!;
}
