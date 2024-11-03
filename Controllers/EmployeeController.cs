using AutoMapper;
using HR.BL.Interfaces;
using HR.BL.ModelDto;
using HR.DAL.Entity;
using HR.Language;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ICityRep city;
        private readonly IDistrictRep district;


        #region Fiels

        // Loosly Coupled
        private readonly IEmployeeRep employee;
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        // Tightly Coupled
        //DepartmentRep department ;

        #endregion


        #region Ctor

        public EmployeeController(IStringLocalizer<SharedResource> localizer,ICityRep city, IDistrictRep district,IEmployeeRep employee, IDepartmentRep department, IMapper mapper)
        {
            this.localizer=localizer;
            this.city = city;
            this.district = district;
            this.employee = employee;
            this.department = department;
            this.mapper = mapper;
        }

        #endregion


        #region Actions


        public IActionResult Index(string SearchValue = "")
        {
            if( SearchValue == "")
            {
                var data = employee.GetEmployee();
                var model = mapper.Map<IEnumerable<EmployeeDto>>(data);
                return View(model);
            }
            else
            {
                var data = employee.SearchByName(SearchValue);
                var model = mapper.Map<IEnumerable<EmployeeDto>>(data);
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeDto>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name",model.DepartmentId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto model)
        {

            try
            {

                if(model != null)
                {

                    var data = mapper.Map<Employee>(model);
                    employee.Create(data);
                    TempData["x"] = localizer["DASHBOARD"];
                    return RedirectToAction("Index");
                }
                


                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                return View(model);
            }
            catch (Exception ex)
            {

                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeDto>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeDto model)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);
                    employee.Edit(data);
                    return RedirectToAction("Index");
                }


                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return View(model);
            }
            catch (Exception ex)
            {

                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return View(model);
            }
        }




        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeDto>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(EmployeeDto model)
        {

            try
            {
                var data = mapper.Map<Employee>(model);
                employee.Delete(data);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return View(model);
            }
        }

        #endregion


        #region Ajax Requests

        [HttpPost]
        public JsonResult GetCityDataByCountryId(int CtryId)
        {
            var data = city.Get(a => a.CountryId == CtryId);
            var model = mapper.Map<IEnumerable<CityDto>>(data);
            return Json(model);
        }


        [HttpPost]
        public JsonResult GetDistrictDataByCityId(int CtyId)
        {
            var data = district.Get(a => a.CityId == CtyId);
            var model = mapper.Map<IEnumerable<DistrictDto>>(data);
            return Json(model);
        }


        #endregion
    }
}
