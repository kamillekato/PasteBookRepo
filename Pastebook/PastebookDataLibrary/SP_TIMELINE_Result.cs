//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PastebookDataLibrary
{
    using System;
    
    public partial class SP_TIMELINE_Result
    {
        public int ID { get; set; }
        public System.DateTime Created_date { get; set; }
        public string Content { get; set; }
        public int profile_Owner_ID { get; set; }
        public int Poster_ID { get; set; }
        public string OwnerName { get; set; }
        public string PosterName { get; set; }
        public Nullable<int> NumberofLikes { get; set; }
        public Nullable<int> NumberOfcomments { get; set; }
    }
}