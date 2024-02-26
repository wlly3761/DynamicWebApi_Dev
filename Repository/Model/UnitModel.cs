using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entity
{
    [SugarTable("AM_Unit")]
    public class UnitModel
    {

        [SugarColumn(IsPrimaryKey = true)]
        [Key]
        [Display(Name = "UnitID")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        
        public string UnitID { get; set; }


        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]

        public string BuildingID { get; set; }

        [MaxLength(4)]
        [Column(TypeName = "numeric(4,0)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]

        public string UnitSortNo { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]

        public string UnitName { get; set; }
        [MaxLength(1)]
        [Column(TypeName = "char(1)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]

        public string IsEnable { get; set; }
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]

        public string Note { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]

        public string AddUser { get; set; }
        [Column(TypeName = "datetime")]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]

        public string AddTime { get; set; }
    }
}
