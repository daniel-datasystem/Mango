﻿namespace Mango.Web.Utility
{
    /// <summary>
    /// Static Details
    /// </summary>
    public static class SD
    {
        public static string CouponAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}