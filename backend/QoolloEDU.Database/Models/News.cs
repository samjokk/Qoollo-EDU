﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QoolloEDU.Database.models
{
    public class News
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Project Project { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}