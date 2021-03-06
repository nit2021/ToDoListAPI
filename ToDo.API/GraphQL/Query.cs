using ToDoListAPI.ToDoBLL.Services;
using ToDoAPI.Core.Models;
using System.Threading.Tasks;
using ToDoAPI.Core.Utilities;

namespace ToDoListAPI.ToDoAPI.GraphQL
{

    public class Query
    {
        private readonly IToDoService _toDoService;
        public Query(IToDoService toDoService)
        {
            _toDoService = toDoService;

        }
        public Task<PagedList<ToDoList>> getToDoListItems(OwnerParameters op) => _toDoService.GetAllTodoList(op);
        public Task<PagedList<ToDoList>> searchToDoListItems(string itemDesc, OwnerParameters op) => _toDoService.SearchTodoList(itemDesc, op);
        public Task<PagedList<ToDoItem>> getToDoItems(OwnerParameters op) => _toDoService.GetAllTodoItem(op);
        public Task<PagedList<ToDoItem>> searchToDoItems(string itemDesc, OwnerParameters op) => _toDoService.SearchTodoItem(itemDesc, op);
        public Task<PagedList<Label>> getToDoLabels(OwnerParameters op) => _toDoService.GetAllLabel(op);
        public Task<PagedList<Label>> getToDoLabelsByToDoListID(int toDoListID, OwnerParameters op) => _toDoService.GetAllLabelByToDoListID(toDoListID, op);
        public Task<PagedList<Label>> getToDoLabelsByToDoItemID(int toDoItemID, OwnerParameters op) => _toDoService.GetAllLabelByToDoItemID(toDoItemID, op);
    }
}