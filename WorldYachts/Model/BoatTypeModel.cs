using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Services.BoatType;

namespace WorldYachts.Model
{
    class BoatTypeModel:IBoatTypeModel
    {
        public event Func<object, Task> BoatTypeModelChanged;
        private readonly IBoatTypeService _boatTypeService;

        public BoatTypeModel(IBoatTypeService boatTypeService)
        {
            _boatTypeService = boatTypeService;
        }

        public async Task AddAsync(BoatType boatType)
        {
            await _boatTypeService.AddAsync(boatType);
            BoatTypeModelChanged?.Invoke(boatType);
        }

        public async Task<IEnumerable<BoatType>> GetAllAsync()
        {
            return await _boatTypeService.GetAllAsync();
        }

        public async Task DeleteAsync(BoatType boatType)
        {
            await _boatTypeService.DeleteAsync(boatType.Id);
            BoatTypeModelChanged?.Invoke(boatType);
        }

        public async Task UpdateAsync(BoatType boatType)
        {
            await _boatTypeService.UpdateAsync(boatType.Id, boatType);
            BoatTypeModelChanged?.Invoke(boatType);
        }

        public async Task<BoatType> GetByIdAsync(int id)
        {
            return await _boatTypeService.GetByIdAsync(id);
        }
    }
}
