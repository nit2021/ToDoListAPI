using System.Collections.Generic;
using AutoMapper;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoBLL.DTO;
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
            CreateMap<Users, UsersDTO>();

            // DTO to Model
            CreateMap<ToDoItemDTO, ToDoItem>();
            CreateMap<ToDoListDTO, ToDoList>();
            CreateMap<LabelDTO, Label>();
            CreateMap<UsersDTO, Users>();
        }
    }
}