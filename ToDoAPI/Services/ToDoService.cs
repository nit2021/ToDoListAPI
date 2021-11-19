using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.DAL;
using ToDoAPI.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using ToDoListAPI.ToDoAPI.DTO;

namespace ToDoListAPI.ToDoAPI.Services
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

        public async Task<PagedList<ToDoList>> GetAllTodoList(OwnerParameters op)
        {
            return await (PagedList<ToDoList>.ToPagedList(_context.ToDoLists.Where(x => x.OwnerID == _userService.userId), op.PageNumber, op.PageSize));
        }
        public async Task<PagedList<ToDoItem>> GetAllTodoItem(OwnerParameters op)
        {
            return await (PagedList<ToDoItem>.ToPagedList(_context.ToDoItems.Where(x => x.ToDoList.OwnerID == _userService.userId), op.PageNumber, op.PageSize));
        }
        public async Task<ToDoList> GetTodoListById(long id)
        {
            return await _context.ToDoLists.Where(x => x.ListId == id && x.OwnerID == _userService.userId).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Label>> GetAllLabel(OwnerParameters op)
        {
            var result = from x in _context.Labels
                         join y in _context.ToDoItems on x.ToDoItemID equals y.ItemId
                         join z in _context.ToDoLists on y.ToDoListID equals z.ListId
                         where z.OwnerID == _userService.userId
                         select x;
            return await (PagedList<Label>.ToPagedList(result, op.PageNumber, op.PageSize));
        }
        public async Task<ToDoList> DeleteTodoList(long id)
        {
            var toDoListItem = await _context.ToDoLists.Where(x => x.ListId == id).FirstOrDefaultAsync();
            if (toDoListItem != null)
            {
                _context.Remove(toDoListItem);
                await _context.SaveChangesAsync();
            }
            return toDoListItem;
        }
        public async Task<ToDoItem> DeleteTodoItem(long id)
        {
            var todoItemToBeDeleted = await _context.ToDoItems.Where(x => x.ItemId == id).FirstOrDefaultAsync();
            if (todoItemToBeDeleted != null)
            {
                _context.Remove(todoItemToBeDeleted);
                await _context.SaveChangesAsync();
            }
            return todoItemToBeDeleted;
        }
        async Task<PagedList<ToDoList>> IToDoService.SearchTodoList(string filter, OwnerParameters op)
        {
            return await (PagedList<ToDoList>.ToPagedList(_context.ToDoLists.Where(x => x.Description.Contains(filter) && x.OwnerID == _userService.userId), op.PageNumber, op.PageSize));
        }

        public async Task<ToDoItem> GetTodoItemById(long itemId)
        {
            return await (_context.ToDoItems.Where(x => x.ItemId == itemId && x.ToDoList.OwnerID == _userService.userId).FirstOrDefaultAsync());
        }

        public async Task<PagedList<ToDoItem>> GetTodoItemByTodoListId(long listId, OwnerParameters op)
        {
            return await (PagedList<ToDoItem>.ToPagedList(_context.ToDoItems.Where(x => x.ToDoListID == listId && x.ToDoList.OwnerID == _userService.userId), op.PageNumber, op.PageSize));
        }

        public async Task<PagedList<ToDoItem>> SearchTodoItem(string filter, OwnerParameters op)
        {
            return await (PagedList<ToDoItem>.ToPagedList(_context.ToDoItems.Where(x => x.Description.Contains(filter) && x.ToDoList.OwnerID == _userService.userId), op.PageNumber, op.PageSize));
        }

        public async Task<ToDoList> CreateToDoList(string listItemDesc)
        {
            ToDoList newListItem = new ToDoList();
            newListItem.Description = listItemDesc;
            newListItem.CreatedDate = DateTime.UtcNow;
            newListItem.OwnerID = _userService.userId;
            _context.ToDoLists.Attach(newListItem);
            await _context.SaveChangesAsync();
            return newListItem;
        }
        public async Task<ToDoItem> CreateTodoItem(ToDoItemInDTO itemInDTO)
        {
            ToDoItem newitem = new ToDoItem();
            if (IsToDoList(itemInDTO.ToDoListID))
            {
                newitem.ToDoListID = itemInDTO.ToDoListID;
                newitem.Description = itemInDTO.Description;
                newitem.CreatedDate = DateTime.UtcNow;
                _context.ToDoItems.Attach(newitem);
                await _context.SaveChangesAsync();
                return newitem;
            }
            else
                return null;
        }

        public async Task<ToDoList> UpdateTodoList(long todoListId, string listItemDesc)
        {
            var todoListItem = await _context.ToDoLists.Where(x => x.ListId == todoListId).FirstOrDefaultAsync();
            if (todoListItem != null)
            {
                todoListItem.Description = listItemDesc;
                todoListItem.UpdatedDate = DateTime.UtcNow;
                _context.ToDoLists.Update(todoListItem);
                await _context.SaveChangesAsync();
            }
            return todoListItem;
        }

        public async Task<ToDoItem> UpdateTodoItem(ToDoItemUpDTO itemUpDTO)
        {
            var todoItem = await _context.ToDoItems.Where(x => x.ItemId == itemUpDTO.ItemId).FirstOrDefaultAsync();
            if (todoItem != null)
            {
                todoItem.Description = itemUpDTO.Description;
                todoItem.UpdatedDate = DateTime.UtcNow;
                _context.Update(todoItem);
                await _context.SaveChangesAsync();
            }
            return todoItem;
        }

        public async Task<ToDoItem> PatchTodoItem(long id, JsonPatchDocument<ToDoItem> todoItem)
        {
            var item = await _context.ToDoItems.Where(x => x.ItemId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                todoItem.ApplyTo(item);
                item.UpdatedDate = DateTime.UtcNow;
                _context.ToDoItems.Update(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<Label> CreateLabel(int ToDoItemID, int ToDoListID, string LabelDesc)
        {
            Label newLabel = new Label();
            newLabel.Description = LabelDesc;
            if (IsToDoItem(ToDoItemID))
                newLabel.ToDoItemID = ToDoItemID;
            if (IsToDoList(ToDoListID))
                newLabel.ToDoListID = ToDoListID;
            if (newLabel.ToDoItemID == null && newLabel.ToDoListID == null)
                return null;
            _context.Labels.Attach(newLabel);
            await _context.SaveChangesAsync();
            return newLabel;
        }

        public async Task<Label> DeleteLabel(long id)
        {
            var label = await _context.Labels.Where(x => x.LabelId == id).FirstOrDefaultAsync();
            if (label != null)
            {
                _context.Remove(label);
                await _context.SaveChangesAsync();
            }
            return label;
        }

        public bool IsToDoList(int ListId)
        {
            ToDoList taskitem = _context.ToDoLists.Where(x => x.ListId == ListId).FirstOrDefault();
            if (taskitem == null)
                return false;
            else
                return true;
        }

        public bool IsToDoItem(int itemId)
        {
            ToDoItem item = _context.ToDoItems.Where(x => x.ItemId == itemId).FirstOrDefault();
            if (item == null)
                return false;
            else
                return true;
        }
    }
}