using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace LapShop.Models;

public partial class TbCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;
    [ValidateNever]
    public string CreatedBy { get; set; } = null!;
    [ValidateNever]

    public DateTime CreatedDate { get; set; }
    [ValidateNever]

    public int CurrentState { get; set; }
    [ValidateNever]

    public string ImageName { get; set; } = null!;
    [ValidateNever]

    public bool ShowInHomePage { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
