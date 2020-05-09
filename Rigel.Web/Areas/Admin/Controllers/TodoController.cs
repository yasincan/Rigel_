using AutoMapper;
using JqueryDataTables.ServerSide.AspNetCoreWeb.ActionResults;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Binders;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
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
    public class TodoController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;
        public TodoController(ITodoService TodoManager,IMapper mapper)
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
            //return View(_mapper.Map<IEnumerable<Todo>>(_todoService.Select()));
            return View(new ViewModels.Todo());
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]JqueryDataTablesParameters param)
        {
            try
            {
                HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
                var results = await _todoService.GetDataAsync(param);

                return new JsonResult(new JqueryDataTablesResult<ViewModels.Todo>
                {
                    Draw = param.Draw,
                    Data = results.Items,
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

            var results = await _todoService.GetDataAsync(JsonSerializer.Deserialize<JqueryDataTablesParameters>(param));
            return new JqueryDataTablesExcelResult<Todo>(_mapper.Map<List<Todo>>(results.Items), "Todo", "TodoExcel");
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            return View(_mapper.Map<Todo>(_todoService.FindById(id)));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Todo Todo)
        {
            if (ModelState.IsValid)
            {
                Todo.Id = Guid.NewGuid();
                _todoService.Insert(_mapper.Map<Data.Entities.Todo>(Todo));
                return RedirectToAction("Index");
            }

            return View(Todo);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return PartialView("_Edit", _mapper.Map<Todo>(_todoService.FindById(id)));
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Todo Todo)
        {
            if (ModelState.IsValid)
            {
                _todoService.Update(_mapper.Map<Data.Entities.Todo>(Todo));
                return RedirectToAction("Index");
            }
            return NoContent();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(_mapper.Map<Todo>(_todoService.FindById(id)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _todoService.Delete(_mapper.Map<Data.Entities.Todo>(_todoService.FindById(id)));
            return RedirectToAction("Index");
        }
    }
}