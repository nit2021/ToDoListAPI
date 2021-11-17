using System.Collections.Generic;
using AutoMapper;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoAPI.DTO;
namespace ToDoListAPI.ToDoAPI.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Model to DTO
            CreateMap<ToDoItem, ToDoItemDTO>();
            CreateMap<ToDoList, ToDoListDTO>();
            CreateMap<Label, LabelDTO>();
            // DTO to Model
            CreateMap<ToDoItemDTO, ToDoItem>();
            CreateMap<ToDoListDTO, ToDoList>();
            CreateMap<LabelDTO, Label>();
        }
    }
}