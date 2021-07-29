using System;

namespace QoolloEDU.Database.models
{
    public class Comment
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int DeveloperId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public News News { get; set; }
        public Developer Developer { get; set; }
    }
}