//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Takhfif.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
            this.Likes = new HashSet<Like>();
            this.Orders = new HashSet<Order>();
            this.Comments = new HashSet<Comment>();
            this.Finilizations = new HashSet<Finilization>();
        }
    
   
    
        public virtual City City { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual ICollection<Finilization> Finilizations { get; set; }
    }
}
