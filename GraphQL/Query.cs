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

        //<QueryCommand>
        // query{
        // getToDoItems(op:{pageNumber:1,pageSize:5})
        // {
        //     description
        // }
        // }
        // </QueryCommand>
        public Task<PagedList<ToDoItem>> getToDoItems(OwnerParameters op) => _toDoService.GetAllTodoList(op);

        // <QueryCommand>
        // query{
        // searchToDoItems(itemDesc:"item",op:{pageNumber:1,pageSize:10})
        // {
        //     description
        // }
        // }
        // </QueryCommand>
        public Task<PagedList<ToDoItem>> searchToDoItems(string itemDesc,OwnerParameters op) => _toDoService.SearchTodoList(itemDesc, op);

        //<QueryCommand>
        // query{
        // getToDoLabels(op:{pageNumber:1,pageSize:5})
        // {
        //     description
        // }
        // }
        // </QueryCommand>
        public Task<PagedList<Label>> getToDoLabels(OwnerParameters op) => _toDoService.GetAllItemByLabelTag(op);
    }
}