using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.DTO;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;

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
        //GET
        
        public IActionResult AddTask()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult AddTask(CreateTaskDTO obj)
        {
            
            if(ModelState.IsValid)
            {
                var databaseobj = _mapper.Map<Objective>(obj);
                _dbcontext.Objective.Add(databaseobj);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult EditTask(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var taskFromDb = _dbcontext.Objective.FirstOrDefault(u => u.TaskId == id);
            if (taskFromDb == null)
            {
                return NotFound();
            }
            var newobj = _mapper.Map<UpdateTaskDTO>(taskFromDb);
            return View(newobj);
        }

        //POST
        [HttpPost]
        public IActionResult EditTask(UpdateTaskDTO obj)
        {
            if (ModelState.IsValid)
            {
                 var updatedDatabaseObj = _mapper.Map<Objective>(obj);
                updatedDatabaseObj.Title = obj.Title;
                updatedDatabaseObj.Description = obj.Description;
                updatedDatabaseObj.CompleteByDate = obj.CompleteByDate;
                updatedDatabaseObj.UpdatedDate = obj.UpdatedDate;

                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
