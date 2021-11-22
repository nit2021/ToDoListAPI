using System.Threading.Tasks;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoAPI.DTO;
using ToDoListAPI.ToDoAPI.Services;

namespace ToDoListAPI.ToDoAPI.GraphQL
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
        public async Task<ToDoItem> createItem(ToDoItemInDTO itemInDTO) => await _toDoService.CreateTodoItem(itemInDTO);
        public async Task<ToDoItem> updateItem(ToDoItemUpDTO itemUpDTO) => await _toDoService.UpdateTodoItem(itemUpDTO);
        public async Task<ToDoItem> deleteitem(int id) => await _toDoService.DeleteTodoItem(id);
        public async Task<Label> createlabel(LabelInDTO labelInDTO) => await _toDoService.CreateLabel(labelInDTO);
        public async Task<Label> deletelabel(int id) => await _toDoService.DeleteLabel(id);

    }
}