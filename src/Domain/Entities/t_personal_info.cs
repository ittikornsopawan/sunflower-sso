using System;
using Domain.Common;

namespace Domain.Entities;

public class t_personal_info : AuditableEntity
{
    public byte[] sid { get; set; } = default!;
    public byte[] prefix_name { get; set; } = default!;
    public required byte[] first_name { get; set; }
    public byte[] middle_name { get; set; } = default!;
    public required byte[] last_name { get; set; }
    public byte[] nick_name { get; set; } = default!;
    public byte[] gender { get; set; } = default!;
    public byte[] date_of_birth { get; set; } = default!;
}
