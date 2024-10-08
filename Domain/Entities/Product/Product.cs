﻿using Domain.Abstractions;
using Domain.Entities.Identity;

namespace Domain.Entities.Product;

public class Product : BaseEntity, ISoftDeleteEntity
{
    public string Name { get; set; }
    public DateTime ProduceDate { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedDateTime { get; set; }
}