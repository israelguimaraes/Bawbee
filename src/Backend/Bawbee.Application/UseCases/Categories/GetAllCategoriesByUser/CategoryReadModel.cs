﻿namespace Bawbee.Application.UseCases.Categories.GetAllCategoriesByUser
{
    public class CategoryReadModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryReadModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
