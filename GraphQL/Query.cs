using System.Linq;
using ToDoListAPI.DAL;
using HotChocolate.Data;
using ToDoListAPI.Services;
using System.Collections.Generic;
using ToDoListAPI.Models;
using System.Threading.Tasks;

namespace ToDoListAPI.GraphQL
{

    public class Query
    {
        private readonly IToDoService _toDoService;
        public Query(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }
        OwnerParameters op=new OwnerParameters();
        public Task<PagedList<ToDoItem>> ToDoItems =>  _toDoService.GetAllTodoList(op);
    }
}