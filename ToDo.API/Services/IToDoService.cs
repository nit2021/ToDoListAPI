using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoAPI.DTO;

namespace ToDoListAPI.ToDoAPI.Services
{
    public interface IToDoService
    {
        Task<PagedList<ToDoList>> GetAllTodoList(OwnerParameters op);
        Task<ToDoList> GetTodoListById(long id);
        Task<PagedList<ToDoList>> SearchTodoList(string filter, OwnerParameters op);
        Task<ToDoList> CreateToDoList(string listItemDesc);
        Task<ToDoList> UpdateTodoList(ToDoListUpDTO toDoListUpDTO);
        Task<ToDoList> PatchTodoList(long id, JsonPatchDocument<ToDoList> todoListItem);
        Task<ToDoList> DeleteTodoList(long id);


        Task<PagedList<ToDoItem>> GetAllTodoItem(OwnerParameters op);
        Task<ToDoItem> GetTodoItemById(long itemId);
        Task<PagedList<ToDoItem>> SearchTodoItem(string filter, OwnerParameters op);
        Task<PagedList<ToDoItem>> GetTodoItemByTodoListId(long listId, OwnerParameters op);
        Task<ToDoItem> CreateTodoItem(ToDoItemInDTO itemInDTO);
        Task<ToDoItem> UpdateTodoItem(ToDoItemUpDTO itemUpDTO);
        Task<ToDoItem> PatchTodoItem(long id, JsonPatchDocument<ToDoItem> todoItem);
        Task<ToDoItem> DeleteTodoItem(long id);

        Task<PagedList<Label>> GetAllLabel(OwnerParameters op);
        Task<PagedList<Label>> GetAllLabelByToDoListID(int toDoListID, OwnerParameters op);
        Task<PagedList<Label>> GetAllLabelByToDoItemID(int toDoItemID, OwnerParameters op);
        Task<Label> CreateLabel(LabelInDTO labelInDTO);
        Task<Label> DeleteLabel(long id);
    }
}