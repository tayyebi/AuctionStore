using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Takhfif.Models
{
    
    public partial class Admin
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }

        [Display(Name = "سمت")]
        [Required(ErrorMessage = "*")]
        public string Type { get; set; }

        [Display(Name = "درباره")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string About { get; set; }

        [Display(Name = "توضیح با ساختار اچ تی ام ال")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.MultilineText)]
        public string HTML { get; set; }
    }
    public partial class City
    {
        [Display(Name = "شناسه")]
        public System.Guid Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
    }
    public partial class Finilization
    {
        [Display(Name = "سریال")]
        public int Id { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "نام کاربری ادمین")]
        public string AdminUsername { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "*")]
        public int ProductId { get; set; }

        [Display(Name = "کاربر")]
        public string UserUsername { get; set; }
    }
    public partial class Comment
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "متن")]
        public string Value { get; set; }

        [Display(Name = "شناسه محصول")]
        public int ProductId { get; set; }

        [Display(Name = "نام کاربری فرستنده")]
        public string UserUsername { get; set; }
    }
    public partial class Group
    {
        [Display(Name = "شناسه")]
        public System.Guid Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
    }
    public partial class Image
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "شناسه محصول")]
        [Required(ErrorMessage = "*")]
        public int ProductId { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "*")]
        public string Source { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "*")]
        public string Description { get; set; }
    }
    public partial class InMail
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "گیرنده")]
        [Required(ErrorMessage = "*")]
        public string ToUsername { get; set; }

        [Display(Name = "فرستنده")]
        public string FromUsername { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "متن")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "*")]
        public string Value { get; set; }

        [Display(Name = "خوانده شده است")]
        public Nullable<bool> IsRead { get; set; }

    }
    public partial class Like
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserUsername { get; set; }

        [Display(Name = "شناسه محصول")]
        public int ProductId { get; set; }
    }
    public partial class Order
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "تاریخ خرید")]
        public string Date_Buy { get; set; }

        [Display(Name = "تاریخ اتمام")]
        public string Date_Expire { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "وضعیت")]
        public string Status { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserUsername { get; set; }

        [Display(Name = "شناسه محصول")]
        public int ProductId { get; set; }
    }
    public partial class Post
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "چکیده")]
        public string Abstract { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "نام کاربری ادمین")]
        public string AdminUsername { get; set; }
    }
    public partial class Product
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string Date_Create { get; set; }

        [Display(Name = "تاریخ اتمام")]
        public string Date_Expire { get; set; }

        [Display(Name = "قیمت اصلی")]
        [Required(ErrorMessage = "*")]
        public int Price_Original { get; set; }

        [Display(Name = "قیمت با محاسبه تخفیف")]
        public int Price_Off { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "*")]
        public int Price_Off_Percent { get; set; }

        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }

        [Display(Name = "تعداد علافه مندی")]
        public int Count_Like { get; set; }

        [Display(Name = "تعداد خرید")]
        public int Count_Buy { get; set; }

        [Display(Name = "ارزش")]
        [Required(ErrorMessage = "*")]
        public int Priority { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "*")]
        public string Description { get; set; }

        [Display(Name = "شناسه شهر")]
        [Required(ErrorMessage = "*")]
        public System.Guid CityId { get; set; }

        [Display(Name = "شناسه گروه")]
        [Required(ErrorMessage = "*")]
        public System.Guid GroupId { get; set; }

        [Display(Name = "نقشه")]
        [Required(ErrorMessage="*")]
        public string Map { get; set; }

        [Display(Name = "نام کاربری ادمین")]
        public string AdminUsername { get; set; }
    }
    public partial class Slide
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "*")]
        public string Source { get; set; }

        [Display(Name = "مقصد")]
        [Required(ErrorMessage = "*")]
        public string NavigateUrl { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [Display(Name = "توضیح")]
        [Required(ErrorMessage = "*")]
        public string Description { get; set; }

        [Display(Name = "نام کاربری ادمین")]
        public string AdminUsername { get; set; }
    }
    public partial class Transaction
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "واریز؟")]
        public bool Direction { get; set; }

        [Display(Name = "مقدار")]
        public int Value { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "خلاصه تراکنش")]
        public int Report
        {
            get
            {
                if (Direction == false)
                    return Value * -1;
                else return Value;
            }
        }

        [Display(Name = "نام کاربری")]
        public string UserUsername { get; set; }
    }
    public partial class Transfer
    {
        [Display(Name = "شناسه")]
        public Guid Id { get; set; }

        [Display(Name = "شماره مسلسل")]
        public int OrderId { get; set; }

        [Display(Name = "ارجاع / پیگیری")]
        [Required(ErrorMessage = "*")]
        public string Refer { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "کد شعبه")]
        public string BranchCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "مقدار")]
        public int Value { get; set; }

        [Display(Name = "سال")]
        [Required(ErrorMessage = "*")]
        public int Date_Year { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "ماه")]
        public int Date_Month { get; set; }

        [Display(Name = "روز")]
        [Required(ErrorMessage = "*")]
        public int Date_Day { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserUsername { get; set; }
    }
    public partial class Upload
    {
        [Display(Name = "شناسه")]
        public System.Guid Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Display(Name = "نوع فایل")]
        public string Type { get; set; }

        [Display(Name = "اندازه فایل")]
        public int Lenght { get; set; }

        public byte[] Bytes { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "نام کاربری ادمین")]
        public string AdminUsername { get; set; }

        [Display(Name = "لینک دانلود")]
        public string DownloadLink
        {
            get
            {
                string WebsiteUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host.ToString().ToUpper() + ":" + HttpContext.Current.Request.Url.Port.ToString() + "/";
                return WebsiteUrl + "Download/" + Id;
            }
        }
    }

    public partial class User
    {
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "جنسیت")]
        public Nullable<bool> Gender { get; set; }

        [Display(Name = "سال تولد")]
        public Nullable<int> Birth_Year { get; set; }

        [Display(Name = "ماه تولد")]
        public Nullable<int> Birth_Month { get; set; }

        [Display(Name = "نمره به سایت")]
        public Nullable<int> Rate { get; set; }

        [Display(Name = "موجودی حساب")]
        public Nullable<int> Bank { get; set; }

        [Display(Name = "وضعیت")]
        public Nullable<bool> IsBlocked { get; set; }

        [Display(Name = "پست الکترونیکی")]
        public string Email { get; set; }
    }
    
}