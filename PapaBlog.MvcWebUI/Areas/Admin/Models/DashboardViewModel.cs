using PapaBlog.Dtos.Concrete.ArticleDtos;

namespace PapaBlog.MvcWebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int ArticlesCount { get; set; }
        public int CategoriesCount { get; set; }
        public int CommentsCount { get; set; }
        public int UsersCount { get; set; }
        public ArticleListDto Articles { get; set; }
    }
}
