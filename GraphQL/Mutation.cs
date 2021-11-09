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

        public async Task<ToDoList> createListItem(string itemDesc) => await _toDoService.CreateToDoList(itemDesc);
        public async Task<ToDoList> updateListItem(int listId, string itemDesc) => await _toDoService.UpdateTodoList(listId, itemDesc);
        public async Task<ToDoList> deleteListItem(int id) => await _toDoService.DeleteTodoList(id);
        public async Task<ToDoItem> createItem(int taskId, string itemDesc) => await _toDoService.CreateTodoItem(taskId, itemDesc);
        public async Task<ToDoItem> updateItem(int id, string itemDesc) => await _toDoService.UpdateTodoItem(id, itemDesc);
        public async Task<ToDoItem> deleteitem(int id) => await _toDoService.DeleteTodoItem(id);
        public async Task<Label> createlabel(int id, string labelDesc) => await _toDoService.CreateLabel(id, labelDesc);
        public async Task<Label> deletelabel(int id) => await _toDoService.DeleteLabel(id);

    }
}