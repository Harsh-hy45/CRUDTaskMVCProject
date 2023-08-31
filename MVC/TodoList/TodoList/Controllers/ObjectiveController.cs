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
            var taskList = _dbcontext.Objective.ToList();
            var sortedTaskList = taskList
                .OrderBy(task => task.CompletedDate.HasValue)
                .ThenBy(task => task.CompleteByDate)
                .AsEnumerable();
            return View(sortedTaskList);
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
            
            if (obj==null || obj.Title==null || obj.Title.Length < 3 || obj.Title.Length > 50)
            {
                ModelState.AddModelError("Title", "Title should be between 3 and 50 characters.");
            }

            if(obj == null || obj.Description?.Length>200)
            {
                ModelState.AddModelError("Description", "Description should not exceed 200 characters.");
            }

            if (obj == null || obj.CompleteByDate < DateTime.Now )
                ModelState.AddModelError("CompleteByDate", "Complete By Date must be greater than current day.");

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

        [Route("Objective/ToggleCompleted/{taskId}")]
        public IActionResult ToggleCompleted(int taskId)
        {
            var taskFromDb= _dbcontext.Objective.FirstOrDefault(u=>u.TaskId==taskId);
            if(taskFromDb!=null)
            {
                if(taskFromDb.CompletedDate.HasValue)
                {
                    taskFromDb.CompletedDate=null;
                }
                else
                {
                    taskFromDb.CompletedDate =DateTime.Now;
                }

                _dbcontext.SaveChanges();
            }
            var sortedTaskList = _dbcontext.Objective.ToList()
                .OrderBy(task => task.CompletedDate.HasValue)
                .ThenBy(task => task.CompleteByDate)
                .AsEnumerable();
            return PartialView("_FilteredTasksPartial",sortedTaskList);
        }

        [HttpGet]
        public IActionResult SearchTasks(string searchString)
        {
            IEnumerable<Objective> filteredTasks;

            if(!string.IsNullOrWhiteSpace(searchString))
            {
                filteredTasks=_dbcontext.Objective.Where(task=>task.Title.Contains(searchString)).ToList();
            }
            else
            {
                filteredTasks=_dbcontext.Objective.ToList();
            }

            filteredTasks.OrderBy(task => task.CompletedDate.HasValue)
                .ThenBy(task => task.CompleteByDate)
                .AsEnumerable();
            return PartialView("_FilteredTasksPartial", filteredTasks);
        }
    }
}
