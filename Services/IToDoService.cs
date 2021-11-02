using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
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
        Task<PagedList<ToDoItem>> SearchTodoItem(string filter, OwnerParameters op);
        //Task<IEnumerable<ToDoItem>> GetTodoItemByTodoListId(long todoListId);
        Task<ToDoItem> CreateTodoItem(string ItemDesc);
        Task UpdateTodoItem(long todoItemId, string ItemDesc);
        Task<ToDoItem> PatchTodoItem(long id, JsonPatchDocument<ToDoItem> todoItem);
        Task DeleteTodoItem(long id);





        Task<PagedList<Label>> GetAllItemByLabelTag(OwnerParameters op);
        //Task<Label> GetLabelById(long id);
        Task<Label> CreateLabel(int ItemId, string LabelDesc);
        Task DeleteLabel(long id);
    }
}