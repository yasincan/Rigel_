using AutoMapper;
using JqueryDataTables.ServerSide.AspNetCoreWeb.ActionResults;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rigel.Business.Contracts;
using Rigel.Business.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rigel.Web.Areas.Admin.Controllers
{
    public class TodoController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;
        public TodoController(ITodoService TodoManager, IMapper mapper)
        {
            _mapper = mapper;
            _todoService = TodoManager;
        }
        //public async Task<IActionResult> LoadTable([ModelBinder(typeof(JqueryDataTablesBinder))] JqueryDataTablesParameters param)
        //{
        //    try
        //    {
        //        var results = await _todoService.GetDataAsync(param);

        //        return new JsonResult(new JqueryDataTablesResult<ViewModels.Todo>
        //        {
        //            Draw = param.Draw,
        //            Data = results.Items,
        //            RecordsFiltered = results.TotalSize,
        //            RecordsTotal = results.TotalSize
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Write(e.Message);
        //        return new JsonResult(new { error = "Internal Server Error" });
        //    }
        //}


        public IActionResult Index()
        {
            //return View(_mapper.Map<IEnumerable<TodoViewModel>>(_todoService.Select()));
            return View(new TodoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] JqueryDataTablesParameters param)
        {
            try
            {
                HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
                var results = await _todoService.GetDataTableAsync(param);

                return new JsonResult(new JqueryDataTablesResult<TodoViewModel>
                {
                    Draw = param.Draw,
                    Data = _mapper.Map<List<TodoViewModel>>(results.Items),
                    RecordsFiltered = results.TotalSize,
                    RecordsTotal = results.TotalSize
                });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }

        public async Task<IActionResult> GetExcel()
        {
            var param = HttpContext.Session.GetString(nameof(JqueryDataTablesParameters));
            var results = await _todoService.GetDataTableAsync(JsonSerializer.Deserialize<JqueryDataTablesParameters>(param));
            return new JqueryDataTablesExcelResult<TodoViewModel>(_mapper.Map<List<TodoViewModel>>(results.Items), "Todo", "TodoExcel");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(_mapper.Map<TodoViewModel>(await _todoService.GetByIdAsync(id)));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoViewModel Todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddAsync(_mapper.Map<Data.RigelDB.Concretes.Entities.Todo>(Todo));
                return RedirectToAction("Index");
            }
            return View(Todo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            return PartialView("_Edit", _mapper.Map<TodoViewModel>(await _todoService.GetByIdAsync(id)));
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoViewModel Todo)
        {
            if (ModelState.IsValid)
            {
               await _todoService.UpdateAsync(_mapper.Map<Data.RigelDB.Concretes.Entities.Todo>(Todo));
                return RedirectToAction("Index");
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            return View(_mapper.Map<TodoViewModel>(await _todoService.GetByIdAsync(id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _todoService.DeleteAsync(_mapper.Map<Data.RigelDB.Concretes.Entities.Todo>(await _todoService.GetByIdAsync(id)));
            return RedirectToAction("Index");
        }
    }
}