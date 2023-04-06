﻿using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace NortWindAjaxProject.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _service;
        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> GetAll()
        {
           var res=await _service.GetAllSuppliers();
            if (res.Succes)
            {
                return Ok(res.Data);
            }
            else
            {
                return BadRequest(res.Message);
            }

        }

        public async Task<ActionResult> GetById(int supplierId)
        {
            var res = await _service.GetSupplierById(supplierId);
            if (res.Succes)
            {
                return Ok(res.Data);
            }
            else
            {
                return BadRequest(res.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromBody]SupplierResponse supplier)
        {
            var res = await _service.UpdateSupplier(supplier);
            if (res.Succes)
            {
                return Ok(res.Data);
            }
            else
            {
                return BadRequest(res.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateSupplierViewModel supplier)
        {
            var res =await _service.AddSupplier(supplier);
            if (res.Succes)
            {
                return Ok(res.Data);
            }
            else
            {
                return BadRequest(res.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int supplierId)
        {
            var res = await _service.DeleteSupplier(supplierId);
            if (res.Succes)
            {
                return Ok(res.Data);
            }
            else
            {
                return BadRequest(res.Message);
            }
        }




    }
}