﻿using System.ComponentModel.DataAnnotations;

namespace OprawaObrazowWebApi.Models;

public partial class Order
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime DateDue { get; set; }
    public bool IsFinished { get; set; }
    public bool IsClosed { get; set; }
    public DateTime? PlannedDate { get; set; }
    public int ClientId { get; set; }
    public int PictureWidth { get; set; }
    public int PictureHeight { get; set; }
    public virtual Client Client { get; set; } = null!;
    public virtual ICollection<FramePiece> FramePieces { get; set; } = new List<FramePiece>();
}
