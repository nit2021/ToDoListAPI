using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.Models;

namespace ToDoListAPI.Services
{
    public interface IToDoService
    {
        Task<PagedList<ToDoItem>> GetAllTodoList(OwnerParameters op);
        //Task<ToDoItem> GetTodoListById(long id);
        Task<PagedList<ToDoItem>> SearchTodoList(string filter, OwnerParameters op);
        //Task<ToDoItem> CreateTodoList(ToDoItem newTodoList);
        // Task UpdateTodoList(long todoItemId, ToDoItem todoListToBeUpdated);
        // Task DeleteTodoList(long id);


        //Task<PagedList<ToDoItem>> GetAllTodoItem(OwnerParameters op);
        Task<ToDoItem> GetTodoItemById(long id);
        Task<IEnumerable<ToDoItem>> SearchTodoItem(string filter, OwnerParameters op);
        //Task<IEnumerable<ToDoItem>> GetTodoItemByTodoListId(long todoListId);
        Task<ToDoItem> CreateTodoItem(ToDoItem newTodoItem);
        Task UpdateTodoItem(long todoItemId, ToDoItem todoItemToBeUpdated);
        //Task PatchTodoItem(long id, JsonPatchDocument<ToDoItem> todoItem);
        Task DeleteTodoItem(long id);





        Task<PagedList<Label>> GetAllItemByLabelTag(OwnerParameters op);
        //Task<Label> GetLabelById(long id);
        Task<Label> CreateLabel(Label newLabel);
        Task DeleteLabel(long id);
    }
}