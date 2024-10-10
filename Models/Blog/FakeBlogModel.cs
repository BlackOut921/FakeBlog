﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeBlog.Models.Blog
{
    public class FakeBlogModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Author")]
        [DataType(DataType.Text)]
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Author Id")]
        [DataType(DataType.Text)]
        public string AuthorId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Content")]
        [DataType(DataType.Text)]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Display(Name = "Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
