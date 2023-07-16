﻿using Mango.Web.Models;
using Mango.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            var list = new List<CouponDto>();

            var response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {                
                var result = Convert.ToString(response.Result);
                list = JsonConvert.DeserializeObject<List<CouponDto>>(result);
                TempData["success"] = "Coupons found successfully";
            }
            else
                TempData["error"] = response?.Message;

            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {                
                var response = await _couponService.CreateCouponAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }                    
                else
                    TempData["error"] = response?.Message;
            }

            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {           
			var response = await _couponService.GetCouponByIdAsync(couponId);

			if (response != null && response.IsSuccess)
			{
				var result = Convert.ToString(response.Result);
				var model = JsonConvert.DeserializeObject<CouponDto>(result);                
                return View(model);
			}
            else
                TempData["error"] = response?.Message;

			return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            var response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction(nameof(CouponIndex));
            }                
            else
                TempData["error"] = response?.Message;

            return View(couponDto);
        }
    }
}
