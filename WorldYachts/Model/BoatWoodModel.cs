using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Model
{
    public class BoatWoodModel:IBoatWoodModel
    {
        public event Func<object, Task> BoatWoodModelChanged;
        private readonly IBoatWoodService _boatWoodService;

        public BoatWoodModel(IBoatWoodService boatWoodService)
        {
            _boatWoodService = boatWoodService;
        }

        public async Task AddAsync(BoatWood boatWood)
        {
            await _boatWoodService.AddAsync(boatWood);
            BoatWoodModelChanged?.Invoke(boatWood);
        }

        public async Task<IEnumerable<BoatWood>> GetAllAsync()
        {
            return await _boatWoodService.GetAllAsync();
        }

        public async Task DeleteAsync(BoatWood boatWood)
        {
            await _boatWoodService.DeleteAsync(boatWood.Id);
            BoatWoodModelChanged?.Invoke(boatWood);
        }

        public async Task UpdateAsync(BoatWood boatWood)
        {
            await _boatWoodService.UpdateAsync(boatWood.Id, boatWood);
            BoatWoodModelChanged?.Invoke(boatWood);
        }

        public async Task<BoatWood> GetByIdAsync(int id)
        {
            return await _boatWoodService.GetByIdAsync(id);
        }
    }
}
