using Utils.Interfaces;

namespace Authentication.Models
{
    public class MenuDisplayDo
    {
        public int MenuId { get; set; }
        public string MenuType { get; set; }
        public string MenuName { get; set; }
        public int? ParentMenuId { get; set; }
        public string ScreenId { get; set; }
        public string MenuURL { get; set; }
        public string ImageIcon { get; set; }
        public int SeqNo { get; set; }
        public List<MenuDisplayDo> Childrens { get; set; }

        public MenuDisplayDo()
        {
            this.Childrens = new List<MenuDisplayDo>();
        }
    }

    public class MenuSearchCriteriaDo : ASearchCriteriaDo
    {
        public string AppCode { get; set; }
        public string Language { get; set; }
    }
    public class MenuSearchResultDo : ASearchResultDo<MenuSearchDo>
    {
    }
    public class MenuSearchDo
    {
        public string AppCode { get; set; }
        public int MenuId { get; set; }
        public string MenuType { get; set; }
        public string MenuName { get; set; }
        public int? ParentMenuId { get; set; }
        public string ScreenId { get; set; }
        public string MenuURL { get; set; }
        public string ImageIcon { get; set; }
        public int SeqNo { get; set; }
        public bool ActiveFlag { get; set; }
        public List<MenuSearchDo> Childrens { get; set; }

        public MenuSearchDo()
        {
            this.Childrens = new List<MenuSearchDo>();
        }
    }

    public class MenuSeqUpdateDo
    {
        public string AppCode { get; set; }
        public List<MenuSeqDo> Menus { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateBy { get; set; }

        public MenuSeqUpdateDo()
        {
            this.Menus = new List<MenuSeqDo>();
        }

    }
    public class MenuSeqDo
    {
        public string AppCode { get; set; }
        public int MenuId { get; set; }
        public int SeqNo { get; set; }
    }

    public class MenuCriteriaDo
    {
        public string AppCode { get; set; }
        public int? MenuId { get; set; }
    }
    public class MenuDo
    {
        public string AppCode { get; set; }
        public int MenuId { get; set; }
        public string MenuType { get; set; }
        public int? ParentMenuId { get; set; }
        public string ScreenId { get; set; }
        public string MenuURL { get; set; }
        public string ImageIcon { get; set; }
        public bool ActiveFlag { get; set; }
        public List<MenuNameDo> MenuNames { get; set; }
        public DateTime? UpdateDate { get; set; }

        public MenuDo()
        {
            this.MenuNames = new List<MenuNameDo>();
        }
    }
    public class MenuNameDo
    {
        public string AppCode { get; set; }
        public int MenuId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
    }

    public class MenuUpdateDo
    {
        public string AppCode { get; set; }
        public int? MenuId { get; set; }
        public string MenuType { get; set; }
        public int? ParentMenuId { get; set; }
        public string ImageIcon { get; set; }
        public string ScreenId { get; set; }
        public string MenuURL { get; set; }
        public List<MenuNameDo> MenuNames { get; set; }
        public bool ActiveFlag { get; set; }
        public DateTime? LatestUpdateDate { get; set; }

        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }

        public MenuUpdateDo()
        {
            this.MenuNames = new List<MenuNameDo>();
        }
    }
    public class MenuUpdateResultDo : AUpdateTransactionDo<MenuDo>
    {
    }
}
