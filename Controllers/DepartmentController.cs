using AutoMapper;
using HR.BL.Interfaces;
using HR.BL.ModelDto;
using HR.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class DepartmentController : Controller
    {

        #region Fiels

        // Loosly Coupled
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        // Tightly Coupled
        //DepartmentRep department ;

        #endregion


        #region Ctor

        public DepartmentController(IDepartmentRep department , IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }

        #endregion


        #region Actions


        public IActionResult Index()
        {
            var data = department.Get();
            var model = mapper.Map<IEnumerable<DepartmentDto>>(data); 
            return View(model);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentDto>(data);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentDto model)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(model);
                    department.Create(data);
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentDto>(data);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentDto model)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(model);
                    department.Edit(data);
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }




        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentDto>(data);
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(DepartmentDto model)
        {

            try
            {
                var data = mapper.Map<Department>(model);
                department.Delete(data);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        #endregion


    }
}
