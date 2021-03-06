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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            this.COMMENTs = new HashSet<COMMENT>();
            this.FRIENDs = new HashSet<FRIEND>();
            this.FRIENDs1 = new HashSet<FRIEND>();
            this.LIKEs = new HashSet<LIKE>();
            this.NOTIFICATIONs = new HashSet<NOTIFICATION>();
            this.NOTIFICATIONs1 = new HashSet<NOTIFICATION>();
            this.POSTs = new HashSet<POST>();
            this.POSTs1 = new HashSet<POST>();
        }
    
        public int ID { get; set; }
        [Required(ErrorMessage = "Username field is required")]
        [Display(Name = "Username")]
        [StringLength(maximumLength: 50,ErrorMessage = "Username field must be at most 50 characters long")]
        [RegularExpression(@"^((\s*([_.]?)\s*[a-zA-Z0-9]+)+([_.]?)\s*)$", ErrorMessage = "Username is invalid. Username only allows Letters, Numbers, Periods(.) and Underscores(_).")]
        public string USER_NAME { get; set; }

        [Required(ErrorMessage ="Password field is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength:50,ErrorMessage = "Password field must be at most 50 characters long")]
        public string PASSWORD { get; set; }

        public string SALT { get; set; }

        [Required(ErrorMessage ="Firstname field is required")]
        [Display(Name ="Firstname")]
        [StringLength(maximumLength:50,ErrorMessage = "Firstname must be at most 50 characters long")]
        [RegularExpression(@"^((\s*[ '.-]?\s*[a-zA-Z0-9]+)+[ '.-]?\s*)$", ErrorMessage = "Firstname is invalid. Firstname only allows Letters, Numbers, Periods(.), Apostrophe('), Parenthesis() and dashes(-)")]
        public string FIRST_NAME { get; set; }

        [Required(ErrorMessage ="Lastname field is required")]
        [Display(Name ="Lastname")]
        [StringLength(maximumLength:50,ErrorMessage = "Lastname must be at most 50 characters long")]
        [RegularExpression(@"^((\s*[ '.-]?\s*[a-zA-Z0-9]+)+[ '.-]?\s*)$", ErrorMessage = "Lastname is invalid. Lastname only allows Letters, Numbers, Periods(.), Apostrophe('), Parenthesis() and dashes(-)")]
        public string LAST_NAME { get; set; }

        [Required(ErrorMessage ="Birthday field is required")]
        [Display(Name ="Birthday")]
        [DataType(DataType.Date,ErrorMessageResourceName = "Birthday is invalid. Birthday must be yyyy/mm/dd format")]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}",ApplyFormatInEditMode = true)]
        public System.DateTime BIRTHDAY { get; set; }

        [Display(Name ="Country")]
        public Nullable<int> COUNTRY_ID { get; set; }

        [Display(Name ="Mobile No.")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "Mobile number is invalid")]
        [StringLength(maximumLength:50,ErrorMessage = "Mobile number must be at most 50 digits long")]
        [Phone(ErrorMessage = "Mobile number is invalid")]
        public string MOBILE_NO { get; set; }

        [Display(Name = "Gender")]
        public string GENDER { get; set; }

        public byte[] PROFILE_PIC { get; set; }

        public System.DateTime DATE_CREATED { get; set; }

        [Display(Name = "About me")]
        public string ABOUT_ME { get; set; }

        [Required(ErrorMessage = "Email address field is required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email address is invalid")]
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        public string EMAIL_ADDRESS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FRIEND> FRIENDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FRIEND> FRIENDs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIKE> LIKEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION> NOTIFICATIONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION> NOTIFICATIONs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POST> POSTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POST> POSTs1 { get; set; }
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
    }
}
