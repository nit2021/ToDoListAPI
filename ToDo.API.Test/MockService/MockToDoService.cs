using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ToDoAPI.Core.Models;
using ToDoAPI.Core.Utilities;
using ToDoAPI.Test;
using ToDoListAPI.ToDoBLL.DTO;
using ToDoListAPI.ToDoBLL.Services;

namespace ToDoAPI.MockService.Test
{
    public class MockToDoService : IToDoService
    {

        public List<Label> labels { get; set; }
        public List<ToDoItem> toDoItems { get; set; }
        public List<ToDoList> toDoLists { get; set; }
        public bool FailGet { get; set; }
        public MockToDoService()
        {
        }
        public async Task<Label> CreateLabel(LabelInDTO labelInDTO)
        {
            if (FailGet)
                return null;
            Label newlabel;
            if (labelInDTO.ToDoItemID != 0)
                newlabel = new Label() { ToDoItemID = labelInDTO.ToDoItemID, Description = labelInDTO.Description, LabelId = 5 };
            else
                newlabel = new Label() { ToDoListID = labelInDTO.ToDoListID, Description = labelInDTO.Description, LabelId = 5 };
            labels.Add(newlabel);
            return await Task.FromResult(newlabel);
        }

        public async Task<ToDoItem> CreateTodoItem(ToDoItemInDTO itemInDTO)
        {
            if (FailGet)
                return null;
            ToDoItem newtoDoItem = new ToDoItem() { ToDoListID = itemInDTO.ToDoListID, Description = itemInDTO.Description, ItemId = 15 };
            toDoItems.Add(newtoDoItem);
            return await Task.FromResult(newtoDoItem);
        }

        public async Task<ToDoList> CreateToDoList(string listItemDesc)
        {
            if (FailGet)
                return null;
            ToDoList newtoDoList = new ToDoList() { OwnerID = 101, Description = listItemDesc, ListId = 15 };
            toDoLists.Add(newtoDoList);
            return await Task.FromResult(newtoDoList);
        }

        public async Task<Label> DeleteLabel(long id)
        {
            Label label = labels.Where(x => x.LabelId == id).FirstOrDefault();
            if (label == null)
                return null;
            else
            {
                labels.Remove(label);
                return await Task.FromResult(label);
            }
        }

        public async Task<ToDoItem> DeleteTodoItem(long id)
        {
            ToDoItem toDoItem = toDoItems.Where(x => x.ItemId == id).FirstOrDefault();
            if (toDoItem == null)
                return null;
            else
            {
                toDoItems.Remove(toDoItem);
                return await Task.FromResult(toDoItem);
            }
        }

        public async Task<ToDoList> DeleteTodoList(long id)
        {
            ToDoList toDoList = toDoLists.Where(x => x.ListId == id).FirstOrDefault();
            if (toDoList == null)
                return null;
            else
            {
                toDoLists.Remove(toDoList);
                return await Task.FromResult(toDoList);
            }
        }

        public async Task<PagedList<Label>> GetAllLabel(OwnerParameters op)
        {
            if (FailGet)
                return null;
            var labellist = new TestAsyncEnumerable<Label>(labels.AsQueryable());
            return await (PagedList<Label>.ToPagedList(labellist, op.PageNumber, op.PageSize));
        }

        public async Task<PagedList<Label>> GetAllLabelByToDoListID(int toDoListID, OwnerParameters op)
        {
            if (FailGet)
                return null;
            var labellist = new TestAsyncEnumerable<Label>(labels.Where(x => x.ToDoListID == toDoListID).AsQueryable());
            return await (PagedList<Label>.ToPagedList(labellist, op.PageNumber, op.PageSize));
        }

        public async Task<PagedList<Label>> GetAllLabelByToDoItemID(int toDoItemID, OwnerParameters op)
        {
            if (FailGet)
                return null;
            var labellist = new TestAsyncEnumerable<Label>(labels.Where(x => x.ToDoItemID == toDoItemID).AsQueryable());
            return await (PagedList<Label>.ToPagedList(labellist, op.PageNumber, op.PageSize));
        }

        public async Task<PagedList<ToDoItem>> GetAllTodoItem(OwnerParameters op)
        {
            if (FailGet)
                return null;
            var toDoItemList = new TestAsyncEnumerable<ToDoItem>(toDoItems.AsQueryable());
            return await (PagedList<ToDoItem>.ToPagedList(toDoItemList, op.PageNumber, op.PageSize));
        }

        public async Task<PagedList<ToDoList>> GetAllTodoList(OwnerParameters op)
        {
            if (FailGet)
                return null;
            var toDoList = new TestAsyncEnumerable<ToDoList>(toDoLists.AsQueryable());
            return await (PagedList<ToDoList>.ToPagedList(toDoList, op.PageNumber, op.PageSize));
        }

        public async Task<ToDoItem> GetTodoItemById(long itemId)
        {
            if (FailGet)
                return null;
            var toDoItem = (toDoItems.Where(x => x.ItemId == itemId).FirstOrDefault());
            return await Task.FromResult(toDoItem);
        }

        public async Task<PagedList<ToDoItem>> GetTodoItemByTodoListId(long listId, OwnerParameters op)
        {
            if (FailGet)
                return null;
            var toDoItemList = new TestAsyncEnumerable<ToDoItem>(toDoItems.Where(x => x.ToDoListID == listId).Select(x => x).AsQueryable());
            return await (PagedList<ToDoItem>.ToPagedList(toDoItemList, op.PageNumber, op.PageSize));
        }

        public async Task<ToDoList> GetTodoListById(long id)
        {
            if (FailGet)
                return null;
            var toDoList = (toDoLists.Where(x => x.ListId == id).FirstOrDefault());
            return await Task.FromResult(toDoList);
        }

        public async Task<ToDoItem> PatchTodoItem(long id, JsonPatchDocument<ToDoItem> todoItem)
        {
            int index = toDoItems.FindIndex(x => x.ToDoListID == id);
            if (index == -1)
                return null;
            ToDoItem item = toDoItems[index];
            item.UpdatedDate = DateTime.UtcNow;
            todoItem.ApplyTo(item);
            return await Task.FromResult(item);
        }

        public async Task<ToDoList> PatchTodoList(long id, JsonPatchDocument<ToDoList> todoListItem)
        {
            int index = toDoLists.FindIndex(x => x.ListId == id);
            if (index == -1)
                return null;
            ToDoList listItem = toDoLists[index];
            listItem.UpdatedDate = DateTime.UtcNow;
            todoListItem.ApplyTo(listItem);
            return await Task.FromResult(listItem);
        }

        public async Task<PagedList<ToDoItem>> SearchTodoItem(string filter, OwnerParameters op)
        {
            if (FailGet)
                return null;
            var toDoItemList = new TestAsyncEnumerable<ToDoItem>(toDoItems.Where(x => x.Description.Contains(filter)).Select(x => x).AsQueryable());
            return await (PagedList<ToDoItem>.ToPagedList(toDoItemList, op.PageNumber, op.PageSize));
        }

        public async Task<PagedList<ToDoList>> SearchTodoList(string filter, OwnerParameters op)
        {
            if (FailGet)
                return null;
            var toDoList = new TestAsyncEnumerable<ToDoList>(toDoLists.Where(x => x.Description.Contains(filter)).Select(x => x).AsQueryable());
            return await (PagedList<ToDoList>.ToPagedList(toDoList, op.PageNumber, op.PageSize));
        }

        public async Task<ToDoItem> UpdateTodoItem(ToDoItemUpDTO itemUpDTO)
        {
            int index = toDoItems.FindIndex(x => x.ItemId == itemUpDTO.ItemId);
            if (index == -1)
                return null;

            toDoItems[index].Description = itemUpDTO.Description;
            toDoItems[index].UpdatedDate = DateTime.UtcNow;
            return await Task.FromResult(toDoItems[index]);
        }

        public async Task<ToDoList> UpdateTodoList(ToDoListUpDTO toDoListUpDTO)
        {
            int index = toDoLists.FindIndex(x => x.ListId == toDoListUpDTO.ListId);
            if (index == -1)
                return null;

            toDoLists[index].Description = toDoListUpDTO.Description;
            toDoLists[index].UpdatedDate = DateTime.UtcNow;
            return await Task.FromResult(toDoLists[index]);
        }
    }
}