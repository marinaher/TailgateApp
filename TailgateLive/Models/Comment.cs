﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TailgateLive.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Comments { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Events")]
        public int EventId { get; set; }
        public Event Events { get; set; }

    }
}