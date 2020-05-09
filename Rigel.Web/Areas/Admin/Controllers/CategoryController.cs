using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rigel.Business.Contracts;
using Rigel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rigel.Web.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.Select();
            var data = _mapper.Map<IEnumerable<Category>>(categories);
            return View(data);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            return View(_mapper.Map<Category>(_categoryService.FindById(id)));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = Guid.NewGuid();
                _categoryService.Insert(_mapper.Map<Data.Entities.Category>(category));
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return View(_mapper.Map<Category>(_categoryService.FindById(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Update(_mapper.Map<Data.Entities.Category>(category));
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(_mapper.Map<Category>(_categoryService.FindById(id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _categoryService.Delete(_mapper.Map<Data.Entities.Category>(_categoryService.FindById(id)));
            return RedirectToAction("Index");
        }
    }
}