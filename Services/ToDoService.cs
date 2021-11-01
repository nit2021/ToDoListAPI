using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.DAL;
using ToDoListAPI.Models;
using ToDoListAPI.Services;
using ToDoListAPI.Repository;

namespace ToDoListAPI.Services
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoContext _context;
        private readonly IUserService _userService;

        public ToDoService(ToDoContext context, IUserService userService)
        {
            this._context = context;
            this._userService = userService;
        }

        public async Task<PagedList<ToDoItem>> GetAllTodoList(OwnerParameters op)
        {
            return await Task.FromResult(PagedList<ToDoItem>.ToPagedList(_context.ToDoItem, op.PageNumber, op.PageSize));
        }

        public async Task<IEnumerable<ToDoItem>> GetTodoListById(long id)
        {
            return await _context.ToDoItem.Where(x => x.ItemId == id).ToListAsync();
        }

        public async Task<PagedList<Label>> GetAllItemByLabelTag(OwnerParameters op)
        {
            return await Task.FromResult(PagedList<Label>.ToPagedList(_context.Label, op.PageNumber, op.PageSize));
        }
        public async Task DeleteTodoItem(long id)
        {
            var todoItemToBeDeleted = await _context.ToDoItem.Where(x => x.ItemId == id).FirstOrDefaultAsync();
            if (todoItemToBeDeleted != null)
            {
                _context.Remove(todoItemToBeDeleted);
                await _context.SaveChangesAsync();
            }
        }
         async Task<PagedList<ToDoItem>> IToDoService.SearchTodoList(string filter, OwnerParameters op)
        {
            return await Task.FromResult(PagedList<ToDoItem>.ToPagedList(_context.ToDoItem.Where(x => x.Description == filter), op.PageNumber, op.PageSize));
        }

        public async Task<ToDoItem> GetTodoItemById(long id)
        {
            return await _context.ToDoItem.Where(x => x.ItemId == id).FirstOrDefaultAsync();
        }

        Task<IEnumerable<ToDoItem>> IToDoService.SearchTodoItem(string filter, OwnerParameters op)
        {
            throw new NotImplementedException();
        }

        Task<ToDoItem> IToDoService.CreateTodoItem(ToDoItem newTodoItem)
        {
            throw new NotImplementedException();
        }

        Task IToDoService.UpdateTodoItem(long todoItemId, ToDoItem todoItemToBeUpdated)
        {
            throw new NotImplementedException();
        }

        Task IToDoService.DeleteTodoItem(long id)
        {
            throw new NotImplementedException();
        }

        Task<Label> IToDoService.CreateLabel(Label newLabel)
        {
            throw new NotImplementedException();
        }

        Task IToDoService.DeleteLabel(long id)
        {
            throw new NotImplementedException();
        }
    }
}