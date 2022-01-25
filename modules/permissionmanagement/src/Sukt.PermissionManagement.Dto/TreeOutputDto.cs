namespace Sukt.PermissionManagement.Dto
{
    public class TreeOutputDto : OutputDtoBase<string>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Key => Id;
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 所有父级
        /// </summary>
        public string ParentNumbers { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public List<TreeOutputDto> Children = new List<TreeOutputDto>();
    }
}
