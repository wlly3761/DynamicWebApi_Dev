using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entity
{
    [SugarTable("QM_Certificate")]
    public class CertificateModel
    {

        [SugarColumn(IsPrimaryKey = true)]
        [Key]
        [Display(Name = "CertificateID")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        
        public string CertificateID { get; set; }

         
        [Display(Name = "ArchivingStatus")]
        [Column(TypeName = "numeric")]
        [Editable(true)]
        public decimal? ArchivingStatus { get; set; }

        /// <summary>
        ///证书名称
        /// </summary>
        [Display(Name = "证书名称")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        public string CertificateName { get; set; }

        /// <summary>
        ///委托人
        /// </summary>
        [Display(Name = "委托人")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string ClientUserCompanyID { get; set; }

        /// <summary>
        ///证书编号
        /// </summary>
        [Display(Name = "证书编号")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        public string CertificateNumber { get; set; }

        /// <summary>
        ///有效期
        /// </summary>
        [Display(Name = "有效期")]
        [Column(TypeName = "date")]
        [Editable(true)]
        public DateTime? ValidityDate { get; set; }

        /// <summary>
        ///批准单位
        /// </summary>
        [Display(Name = "批准单位")]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        [Editable(true)]
        public string ApprovedUnit { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        public string Remark { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "IsDeadlineHandle")]
        [MaxLength(1)]
        [Column(TypeName = "char(1)")]
        [Editable(true)]
        public string IsDeadlineHandle { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateID")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string CreateID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateDate")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Creator")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        public string Creator { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ModifyID")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string ModifyID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Modifier")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        public string Modifier { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ModifyDate")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        ///批准日期
        /// </summary>
        [Display(Name = "批准日期")]
        [Column(TypeName = "date")]
        [Editable(true)]
        public DateTime? ApprovalDate { get; set; }
    }
}
