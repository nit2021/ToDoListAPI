using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.DAL;
using ToDoListAPI.Models;
using ToDoListAPI.Services;
using ToDoListAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;

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
            return await Task.FromResult(PagedList<ToDoItem>.ToPagedList(_context.ToDoItem.Where(x => x.User.UserId == _userService.userId), op.PageNumber, op.PageSize));
        }

        public async Task<IEnumerable<ToDoItem>> GetTodoListById(long id)
        {
            return await _context.ToDoItem.Where(x => x.ItemId == id && x.User.UserId == _userService.userId).ToListAsync();
        }

        public async Task<PagedList<Label>> GetAllItemByLabelTag(OwnerParameters op)
        {
            return await Task.FromResult(PagedList<Label>.ToPagedList(_context.Label.Where(x => x.ToDoItem.User.UserId == _userService.userId), op.PageNumber, op.PageSize));
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
            return await Task.FromResult(PagedList<ToDoItem>.ToPagedList(_context.ToDoItem.Where(x => x.Description.Contains(filter) && x.User.UserId == _userService.userId), op.PageNumber, op.PageSize));
        }

        public async Task<ToDoItem> GetTodoItemById(long id)
        {
            return await _context.ToDoItem.Where(x => x.ItemId == id && x.User.UserId == _userService.userId).FirstOrDefaultAsync();
        }

        public async Task<PagedList<ToDoItem>> SearchTodoItem(string filter, OwnerParameters op)
        {
            return await Task.FromResult(PagedList<ToDoItem>.ToPagedList(_context.ToDoItem.Where(x => x.Description.Contains(filter) && x.User.UserId == _userService.userId), op.PageNumber, op.PageSize));
        }

        public async Task<ToDoItem> CreateTodoItem(string ItemDesc)
        {
            ToDoItem newitem = new ToDoItem();
            newitem.Description = ItemDesc;
            User user = _context.User.Where(x => x.UserId == _userService.userId).FirstOrDefault();
            newitem.User = user;
            _context.ToDoItem.Attach(newitem);
            await _context.SaveChangesAsync();
            return newitem;
        }

        public async Task UpdateTodoItem(long todoItemId, string ItemDesc)
        {
            var todoItem = await _context.ToDoItem.Where(x => x.ItemId == todoItemId).FirstOrDefaultAsync();
            if (todoItem != null)
            {
                todoItem.Description = ItemDesc;
                _context.Update(todoItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ToDoItem> PatchTodoItem(long id, JsonPatchDocument<ToDoItem> todoItem)
        {
            var item = await _context.ToDoItem.Where(x => x.ItemId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                todoItem.ApplyTo(item);
                _context.ToDoItem.Update(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<Label> CreateLabel(int ItemId, string LabelDesc)
        {
            Label newLabel = new Label();
            newLabel.Description = LabelDesc;
            newLabel.ToDoItem = new ToDoItem() { ItemId = ItemId };
            _context.Label.Attach(newLabel);
            await _context.SaveChangesAsync();
            return newLabel;
        }

        public async Task DeleteLabel(long id)
        {
            var todoItemToBeDeleted = await _context.Label.Where(x => x.LabelId == id).FirstOrDefaultAsync();
            if (todoItemToBeDeleted != null)
            {
                _context.Remove(todoItemToBeDeleted);
                await _context.SaveChangesAsync();
            }
        }
    }
}