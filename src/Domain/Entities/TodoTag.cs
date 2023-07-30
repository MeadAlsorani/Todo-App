using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Entities;
public class TodoTag : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<TodoItemTags> ItemTags { get; set; } = new HashSet<TodoItemTags>();
}
