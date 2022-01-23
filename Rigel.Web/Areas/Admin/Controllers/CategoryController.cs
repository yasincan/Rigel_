using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rigel.Business.Contracts;
using Rigel.Business.Models.Dtos;
using Rigel.Data.RigelDB.Concretes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rigel.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var data = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(_mapper.Map<CategoryDto>(await _categoryService.FindAsync(x => x.Id == id)));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(_mapper.Map<Category>(category));
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View(_mapper.Map<CategoryDto>(await _categoryService.FindAsync(x => x.Id == id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(_mapper.Map<Category>(category));
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            return View(_mapper.Map<CategoryDto>(await _categoryService.FindAsync(x => x.Id == id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _categoryService.DeleteAsync(_mapper.Map<Category>(await _categoryService.FindAsync(x => x.Id == id)));
            return RedirectToAction("Index");
        }
    }
}