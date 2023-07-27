using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Entities;
public class TodoItemTags : BaseEntity
{
    public int TodoItemId { get; set; }
    [ForeignKey(nameof(TodoItemId))]
    public TodoItem TodoItem { get; set; } = null!;
    public int TodoTagId { get; set; }
    [ForeignKey(nameof(TodoTagId))]
    public TodoTag TodoTag { get; set; } = null!;
}
