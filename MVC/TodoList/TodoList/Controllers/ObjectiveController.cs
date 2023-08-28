using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.DTO;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TodoList.Controllers
{
    public class ObjectiveController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;
        public ObjectiveController(ApplicationDbContext db, IMapper mapper)
        {
            _dbcontext = db;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            IEnumerable<Objective> taskList = _dbcontext.Objective;
            return View(taskList);
        }
        public IActionResult TaskForm(int? id)
        {
            if (id.HasValue)
            {
                var taskFromDb = _dbcontext.Objective.FirstOrDefault(u => u.TaskId == id);
                if (taskFromDb == null)
                {
                    return NotFound();
                }
                var editTaskDTO = _mapper.Map<CreateEditTaskDTO>(taskFromDb);
                return View(editTaskDTO);
            }
            return View(new CreateEditTaskDTO());
        }

        [HttpPost]
        public IActionResult TaskForm(CreateEditTaskDTO obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.TaskId > 0)
                {
                    var taskFromDb = _dbcontext.Objective.FirstOrDefault(u => u.TaskId == obj.TaskId);
                    if (taskFromDb == null)
                    {
                        return NotFound();
                    }
                    _mapper.Map(obj, taskFromDb);
                }
                else
                {
                    var databaseobj = _mapper.Map<Objective>(obj);
                    _dbcontext.Objective.Add(databaseobj);
                }
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskToDelete = await _dbcontext.Objective.FirstOrDefaultAsync(u => u.TaskId == id);
            if(taskToDelete==null)
            {
                return NotFound();
            }
            _dbcontext.Objective.Remove(taskToDelete);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id) 
        {
            var taskToView=_dbcontext.Objective.FirstOrDefault(u=>u.TaskId== id);
            if(taskToView==null)
            {
                return NotFound();
            }
            return View(taskToView);
        }
    }
}
