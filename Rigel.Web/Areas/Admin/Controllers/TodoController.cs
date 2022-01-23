using JqueryDataTables.ServerSide.AspNetCoreWeb.ActionResults;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rigel.Business.Contracts;
using Rigel.Business.Models.Dtos;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rigel.Web.Areas.Admin.Controllers
{
    public class TodoController : BaseController
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService TodoManager)
        {
            _todoService = TodoManager;
        }

        public IActionResult Index()
        {
            return View(new TodoDto());
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] JqueryDataTablesParameters param)
        {
            try
            {
                HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
                var results = await _todoService.GetDataTableAsync(param);

                return new JsonResult(new JqueryDataTablesResult<TodoDto>
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
            var results = await _todoService.GetDataTableAsync(JsonSerializer.Deserialize<JqueryDataTablesParameters>(param));
            return new JqueryDataTablesExcelResult<TodoDto>(results.Items, "Todo", "TodoExcel");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _todoService.GetByIdAsync(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoDto todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddAsync(todo);
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            return PartialView("_Edit", await _todoService.GetByIdAsync(id));
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoDto todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.UpdateAsync(todo);
                return RedirectToAction("Index");
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            return View(await _todoService.GetByIdAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _todoService.DeleteAsync(await _todoService.GetByIdAsync(id));
            return RedirectToAction("Index");
        }
    }
}