using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace LapShop.Models;

public partial class TbSlider
{
    public int SliderId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
    [ValidateNever]

    public string ImageName { get; set; } = null!;
    [ValidateNever]
    public string CreatedBy { get; set; } = null!;
    [ValidateNever]

    public DateTime CreatedDate { get; set; }
    [ValidateNever]

    public int CurrentState { get; set; }
    [ValidateNever]

    public string? UpdatedBy { get; set; }
    [ValidateNever]

    public DateTime? UpdatedDate { get; set; }
}
