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
            _toDoService = toDoService;
        }

        public async Task<ToDoItem> createitem(string itemDesc) => await _toDoService.CreateTodoItem(itemDesc);
        public async Task<ToDoItem> updateitem(int id, string itemDesc) => await _toDoService.UpdateTodoItem(id, itemDesc);
        public async Task<ToDoItem> deleteitem(int id) => await _toDoService.DeleteTodoItem(id);
        public async Task<Label> createlabel(int id, string labelDesc) => await _toDoService.CreateLabel(id, labelDesc);
        //public async Task updatelabel(int id, string itemDesc) => await _toDoService.Update(id,itemDesc);
        public async Task<Label> deletelabel(int id) => await _toDoService.DeleteLabel(id);

    }
}