using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniManagement.DTOs;
using UniManagement.EF;

namespace UniManagement.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            unimanagementEntities db = new unimanagementEntities();
            var data = db.Students.ToList();
            var converted = Convert(data);
            return View(converted);

            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentDTO s)
        {
            unimanagementEntities db = new unimanagementEntities();
            if (ModelState.IsValid)
            {
                var st = Convert(s);
                db.Students.Add(st);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s);
           
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            unimanagementEntities db = new unimanagementEntities();
            var exobj = db.Students.Find(id);
            return View(exobj);

        }
        [HttpPost]
        public ActionResult Edit(Student s)
        {
            unimanagementEntities db = new unimanagementEntities();
            var exobj = db.Students.Find(s.Id);
            exobj.Address = s.Address;
            exobj.Name = s.Name;
            exobj.Phone = s.Phone;
            exobj.Email = s.Email;

            
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public static StudentDTO Convert(Student s)

        {

            return new StudentDTO()
            {

                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                Email = s.Email,
                Phone = s.Phone



            };
        }
        public static Student Convert(StudentDTO s)
        {
            return new Student()
            {

                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                Email = s.Email,
                Phone = s.Phone
            };
        }
        public static List<StudentDTO> Convert(List<Student> students)
        {
            var list = new List<StudentDTO>();
            foreach (var s in students)
            {
                var st = Convert(s);
                list.Add(st);
            }
            return list;
        }
    }
}