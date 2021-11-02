using System.Threading.Tasks;
using ToDoListAPI.Models;
using ToDoListAPI.Services;

namespace ToDoListAPI.GraphQL
{
    public class Mutation
    {
        private readonly IToDoService _toDoService;
        public Mutation(IToDoService toDoService)
        {
            _toDoService=toDoService;
        }

        public async Task<ToDoItem> Create(string ItemDesc)=>await _toDoService.CreateTodoItem(ItemDesc); 
    }
}