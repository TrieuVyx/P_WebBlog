using System;
using System.Collections.Generic;

namespace blog.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string? Title { get; set; }

    public string? ShortContents { get; set; }

    public string? Contents { get; set; }

    public string? Thumb { get; set; }

    public string? Alias { get; set; }

    public string? Author { get; set; }

    public string? Tags { get; set; }

    public int? CategoryId { get; set; }

    public bool Published { get; set; }

    public bool IsHot { get; set; }

    public int? AccountId { get; set; }

    public bool IsNewFeed { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Category? Category { get; set; }
    public int Views { get; internal set; }
}
