﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
namespace YSecOps.Events.EfCore.Models;

public partial class Event
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Major_Version { get; set; }
    public int Minor_Version { get; set; }
    public string Name { get; set; } = null!;
    public string Data { get; set; } = null!;
    public Guid AggregateId { get; set; }
    public string Aggregate { get; set; } = null!;
}