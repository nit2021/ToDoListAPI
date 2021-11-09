using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ToDoListAPI.Models;

namespace ToDoListAPI.Services
{
    public interface IToDoService
    {
        Task<PagedList<ToDoList>> GetAllTodoList(OwnerParameters op);
        Task<ToDoList> GetTodoListById(long id);
        Task<PagedList<ToDoList>> SearchTodoList(string filter, OwnerParameters op);
        Task<ToDoList> CreateToDoList(string listItemDesc);
        Task<ToDoList> UpdateTodoList(long todoListId, string listItemDesc);
        Task<ToDoList> PatchTodoList(long id, JsonPatchDocument<ToDoList> todoListItem);
        Task<ToDoList> DeleteTodoList(long id);


        Task<PagedList<ToDoItem>> GetAllTodoItem(OwnerParameters op);
        Task<ToDoItem> GetTodoItemById(long itemId);
        Task<PagedList<ToDoItem>> SearchTodoItem(string filter, OwnerParameters op);
        Task<PagedList<ToDoItem>> GetTodoItemByTodoListId(long listId, OwnerParameters op);
        Task<ToDoItem> CreateTodoItem(int taskId, string ItemDesc);
        Task<ToDoItem> UpdateTodoItem(long todoItemId, string ItemDesc);
        Task<ToDoItem> PatchTodoItem(long id, JsonPatchDocument<ToDoItem> todoItem);
        Task<ToDoItem> DeleteTodoItem(long id);

        Task<PagedList<Label>> GetAllLabel(OwnerParameters op);
        Task<Label> CreateLabel(int ItemId, string LabelDesc);
        Task<Label> DeleteLabel(long id);
    }
}